using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Abstractions.Token;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;

namespace Infrastructure.Services.Token;

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

    public TokenHandler(IConfiguration configuration, DbContextOptions<ApplicationDbContext> dbContextOptions)
    {
        _configuration = configuration;
        _dbContextOptions = dbContextOptions;
    }


    public Application.DTOs.Token.Token CreateAccessToken(int second, AppUser user)
    {
        Application.DTOs.Token.Token token = new();
        token.Expiration = DateTime.UtcNow.AddSeconds(second);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]!));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        var tokensJwtSecurityToken = new JwtSecurityToken(
            issuer: _configuration["Token:Issuer"],
            audience: _configuration["Token:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: signingCredentials
        );
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(tokensJwtSecurityToken);

        token.RefreshToken = CreateRefreshToken();
        return token;
    }

    public string CreateRefreshToken()
    {
        var number = new byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);
    }

    public void RevokeRefreshToken(string token, AppUser user)
    {
        user.RefreshToken = null;
        user.RefreshTokenTime = null;

        using (var dbContext = new ApplicationDbContext(_dbContextOptions))
        {
            dbContext.Entry(user).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
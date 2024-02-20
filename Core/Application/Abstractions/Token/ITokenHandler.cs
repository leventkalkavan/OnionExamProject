using Domain.Entities.Identity;

namespace Application.Abstractions.Token;

public interface ITokenHandler
{
    DTOs.Token.Token CreateAccessToken(int second, AppUser user);
    string CreateRefreshToken();
    void RevokeRefreshToken(string token, AppUser user);
}
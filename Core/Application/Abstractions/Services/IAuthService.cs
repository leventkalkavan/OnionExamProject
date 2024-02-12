namespace Application.Abstractions.Services;

public interface IAuthService
{
    Task<DTOs.Token.Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
    Task<DTOs.Token.Token> RefreshTokenLoginAsync(string refreshToken);
}
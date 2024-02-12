using Domain.Entities.Identity;

namespace Application.Abstractions.Services;

public interface IUserService
{
    Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
}
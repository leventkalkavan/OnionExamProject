using Application.Abstractions.Services;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Services;

public class UserService: IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate,
        int refreshTokenDate)
    {
        var appUser = await _userManager.FindByIdAsync(user.Id);
        if (appUser != null)
        {
            appUser.RefreshToken = refreshToken;
            appUser.RefreshTokenTime = accessTokenDate.AddSeconds(refreshTokenDate);
            await _userManager.UpdateAsync(appUser);
        }
        else
        {
            throw new Exception();
        }
    }
}
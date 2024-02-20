using System.Security.Claims;
using System.Threading.Tasks;
using Application.Abstractions.Services;
using Application.DTOs.Login;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, UserManager<AppUser> userManager, IUserService userService)
        {
            _authService = authService;
            _userManager = userManager;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var token = await _authService.LoginAsync(model.UsernameOrEmail, model.Password, 3600);
            return Ok(token);
        }

        [HttpPost("refresh-token-login")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] string refreshToken)
        {
            var token = await _authService.RefreshTokenLoginAsync(refreshToken);
            return Ok(token);
        }
    }
}
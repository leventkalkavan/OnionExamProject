using System.Threading.Tasks;
using Application.Abstractions.Services;
using Application.DTOs.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
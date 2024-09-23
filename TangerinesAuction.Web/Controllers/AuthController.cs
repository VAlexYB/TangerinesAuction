using Microsoft.AspNetCore.Mvc;
using TangerinesAuction.Core.Abstractions;
using TangerinesAuction.Core.Abstractions.Services;
using TangerinesAuction.Core.Exceptions;
using TangerinesAuction.Web.Contracts.Requests;

namespace TangerinesAuction.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        private readonly IPasswordHasher _passwordHasher;

        public AuthController(IAuthService authService, IPasswordHasher passwordHasher)
        {
            _authService = authService;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRequest request)
        {
            try
            {
                var passwordHash = _passwordHasher.Generate(request.Password);
                await _authService.RegisterAsync(request.Email, passwordHash);
                return Ok();
            }
            catch (UserException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
           
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            try
            {
                var token = await _authService.AuthenticateAsync(request.Email, request.Password);
                HttpContext.Response.Cookies.Append("app-special-key", token);
                return Ok();
            }
            catch (UserException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [Route("logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("app-special-key");
            return Ok();
        }
    }
}

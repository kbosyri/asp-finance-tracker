using FinanceTracker.API.App.Dtos.Requests;
using FinanceTracker.API.DataBase.Contexts;
using FinanceTracker.API.DataBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FinanceTracker.API.App.Services;

namespace FinanceTracker.API.App.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtTokenService _jwtTokenService;


        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, JwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> registerUser([FromBody] RegisterUserRequest request)
        {
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                DisplayName = request.DisplayName,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(
                user,
                request.Password
            );

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    Errors = result.Errors.Select(e => e.Description)
                });
            }

            return Ok(new
            {
                Message = "User registered successfully",
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginWithEmail([FromBody] UserLoginRequest request)
        {
            // Find the user by email
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                return StatusCode(StatusCodes.Status403Forbidden, new { Message = "Invalid email or password." });

            // Verify the password and check lockout status
            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                lockoutOnFailure: true
            );

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status403Forbidden, new { Message = "Invalid email or password." });

            var tokenResult = _jwtTokenService.GenerateToken(user);

            return Ok(new
            {
                Message = "Login successful",
                AccessToken = tokenResult.Token,
                TokenType = "Bearer",
                ExpiresAt = tokenResult.ExpiresAtUtc,
                User = new
                {
                    user.Id,
                    user.UserName,
                    user.Email
                }
            });
        }
    }
}

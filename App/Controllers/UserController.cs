using FinanceTracker.API.App.Dtos.Requests;
using FinanceTracker.API.DataBase.Contexts;
using FinanceTracker.API.DataBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.API.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> registerUser([FromBody] RegisterUserRequest request)
        {
            var user = new User
            {
                Username = request.Username,
                Email = request.Email
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
            return Ok("ok");
        }
    }
}

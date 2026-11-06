using Attempt1.Models;
using Attempt1.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Attempt1.Controllers
{
    public class UserController : Controller
    {
        private static List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "tiber_septim",
                Email = "tiberseptim@dmc.ed.ph",
                PasswordHash="password123",
                Role= "Admin",
            },
            new User
            {
                Id = 2,
                Username = "maiq",
                Email = "maiq@dmc.edu.ph",
                PasswordHash="password456",
                Role= "User",
            }
        };
        //[HttpGet("{id}")]
        //public IActionResult GetUserById(int id)
        //{
        //    var user = _users.FirstOrDefault(u => u.Id == id);

        //    if (user == null)
        //    {
        //        return NotFound($"User with ID {id} not found.");
        //    }

        //    var dto = new UserDTO
        //    {
        //        Id = user.Id,
        //        Username = user.Username,
        //        Email = user.Email,
        //    };

        //    return Ok(dto);
        //}
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserRequest request)
        {

            return Ok(new { Message = "User registered successfully.", Username = request.UserName, Email = request.Email, Age = request.Age });
        }
    }
}

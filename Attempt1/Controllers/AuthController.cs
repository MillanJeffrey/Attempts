using Attempt1.Data;
using Attempt1.DTOs;
using Attempt1.Models;
using Attempt1.Services;
using Microsoft.AspNetCore.Mvc;
using Dapper;

namespace Attempt1.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly DapperContext _context;
        private readonly TokenService _tokenService;

        public AuthController(DapperContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // POST /api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            var sql = @"SELECT Id, Username, Email, PasswordHash, Role
                    FROM Users
                    WHERE Username = @Username";

            using var connection = _context.CreateConnection();
            var user = connection.QueryFirstOrDefault<User>(sql, new { dto.Username });

            if (user == null)
                return Unauthorized(new { message = "Invalid username or password." });

            if (dto.Password != user.PasswordHash)
                return Unauthorized(new { message = "Invalid username or password." });

            // Generate the JWT
            var token = _tokenService.GenerateToken(user);

            return Ok(new AuthResponseDTO
            {
                Token = token,
                Username = user.Username,
                Role = user.Role
            });
        }
    }
}

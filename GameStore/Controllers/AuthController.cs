using Dapper;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

    
        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var parameters = new
            {
                dto.Username,
                dto.Email,
                PasswordHash = HashPassword(dto.Password)
            };

            try
            {
                conn.Execute("RegisterUser", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { message = "Регистрация прошла успешно" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            Console.WriteLine("HASH = " + HashPassword(dto.Password));
            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var result = conn.QueryFirstOrDefault<User>(
                "LoginUser",
                new
                {
                    dto.Username,
                    PasswordHash = HashPassword(dto.Password)
                },
                commandType: CommandType.StoredProcedure
            );

            if (result == null)
                return Unauthorized(new { error = "Неверные имя пользователя или пароль" });
          
            return Ok(new
            {
                result.Id,
                result.Username,
                result.Email

            });
        }
    }
}

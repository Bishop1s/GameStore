using Dapper;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GamesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetGames()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var games = connection.Query<Game>("SELECT * FROM Games").ToList();
            return Ok(games);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGame(int id, [FromBody] Game game)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                var parameters = new
                {
                    Id = id,
                    Title = game.Title,
                    Genre = game.Genre,
                    Price = game.Price,
                    ImageUrl = game.ImageUrl,
                    TotalSold = game.TotalSold
                };

                connection.Execute("UpdateGame", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { message = "Игра успешно обновлена" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при обновлении игры: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddGame([FromBody] Game game)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                var parameters = new
                {
                    Title = game.Title,
                    Genre = game.Genre,
                    Price = game.Price,
                    ImageUrl = game.ImageUrl,
                    TotalSold = game.TotalSold
                };

                connection.Execute("AddGame", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { message = "Игра успешно добавлена" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при добавлении игры: {ex.Message}");
            }
        }


    }
}

using Dapper;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CartController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("add")]
        public IActionResult AddToCart([FromBody] CartItem item)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Execute("AddToCart", new
            {
                item.GameId,
                item.Quantity,
                item.UserId
            }, commandType: CommandType.StoredProcedure);
            return Ok();
        }

        [HttpGet("{userId}")]
        public IActionResult GetCart(int userId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var cart = connection.Query("GetCartItems", new { UserId = userId }, commandType: CommandType.StoredProcedure);
            return Ok(cart);
        }

        [HttpPost("remove")]
        public IActionResult RemoveFromCart([FromBody] RemoveCartItemDto dto)
        {
            Console.WriteLine($"Удаление элемента корзины с ID = {dto.CartItemId}");
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Execute("RemoveCartItem", new { CartItemId = dto.CartItemId }, commandType: CommandType.StoredProcedure);
            return Ok();
        }

        [HttpPost("confirm")]
        public IActionResult ConfirmCart([FromBody] ConfirmCartDto dto)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Execute("ConfirmCart", new { UserId = dto.UserId }, commandType: CommandType.StoredProcedure);
            return Ok();
        }
    }

    public class RemoveCartItemDto
    {
        public int CartItemId { get; set; }
    }
}

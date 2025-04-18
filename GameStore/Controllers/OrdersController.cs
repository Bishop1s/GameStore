using GameStore.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;


namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public OrdersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var orders = connection.Query<Order>("SELECT * FROM Orders").ToList();
            return Ok(orders);
        }
        [HttpGet("{userId}")]
        public IActionResult GetOrdersByUser(int userId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var orders = connection.Query<OrderDto>(
                "GetUserOrders",                    
                new { userId },
                commandType: CommandType.StoredProcedure
            ).ToList();

            return Ok(orders);
        }
    }
}

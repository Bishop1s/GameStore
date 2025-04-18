namespace GameStore.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }

     
        public decimal GamePrice { get; set; }
        public string GameTitle { get; set; }
    }
}

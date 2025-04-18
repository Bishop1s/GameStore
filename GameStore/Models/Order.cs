namespace GameStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int? GameId { get; set; }

        public int? Quantity { get; set; }

        public int? UserId { get; set; }

        public DateTime? OrderDate { get; set; }
    }
}

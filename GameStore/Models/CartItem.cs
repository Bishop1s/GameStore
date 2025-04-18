namespace GameStore.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int? GameId { get; set; }

        public int? Quantity { get; set; }

        public int? UserId { get; set; }

        public DateTime? AddedAt { get; set; }
    }
}

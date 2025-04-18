namespace GameStore.Models
{
    public class RemoveCartItemDto
    {
        public int CartItemId { get; set; }
    }

    public class ConfirmCartDto
    {
        public int UserId { get; set; }
    }
}
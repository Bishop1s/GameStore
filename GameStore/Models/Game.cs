namespace GameStore.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }  
        public decimal? Price { get; set; }
        public decimal? TotalSales { get; set; }
        public string? ImageUrl { get; set; }

        public int? TotalSold { get; set; }
    }
}

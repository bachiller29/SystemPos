using SQLite;

namespace PosSystem.Models
{
    [Table("Products")]
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ApiId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public double Rating { get; set; }
        public int RatingCount { get; set; }

        [Ignore]
        public bool IsRecommended { get; set; }

        [Ignore]
        public ImageSource ImageSource { get; set; }
    }
}

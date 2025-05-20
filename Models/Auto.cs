using System.ComponentModel.DataAnnotations.Schema;

namespace AutoCatalog.Models
{
    public class Auto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public int ViewCount { get; set; } = 0;
        public string BodyType { get; set; }
        public string Color { get; set; }
        
        [NotMapped]
        public string ImageData { get; set; }
    }
}

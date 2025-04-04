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
        
        [NotMapped]
        public string ImageData { get; set; }
    }
}

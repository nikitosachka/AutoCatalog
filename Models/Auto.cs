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

        public int Mileage { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public double EngineVolume { get; set; }
        public string Description { get; set; }
        
        [NotMapped]
        public string ImageData { get; set; }
    }
}

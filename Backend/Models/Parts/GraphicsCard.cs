using System.ComponentModel.DataAnnotations;

namespace Models.Parts {
    public class GraphicsCard {

        [Key]
        public int ID { get; set; }

        [MaxLength(16), Required]
        public string SerialNumber { get; set; }

        [MaxLength(16), Required]
        public string Manufacturer { get; set; }

        [MaxLength(32), Required]
        public string Model { get; set; }

        [Range(1, float.MaxValue), Required]
        public double Price { get; set; }

        //nullable ✔
        [Range(1, 32)]
        public int? MemoryGB { get; set; }

    }
}
using System.ComponentModel.DataAnnotations;

namespace Models.Parts {
    public class Storage {

        [Key]
        public int ID { get; set; }

        [MaxLength(16), Required]
        public string SerialNumber { get; set; }

        [MaxLength(32), Required]
        public string Manufacturer { get; set; }

        [MaxLength(16), Required]
        public string Model { get; set; }

        [Range(1, double.MaxValue), Required]
        public double Price { get; set; }

        [Range(1, int.MaxValue), Required]
        public int MemoryGB { get; set; }

    }
}
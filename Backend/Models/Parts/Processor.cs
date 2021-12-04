using System.ComponentModel.DataAnnotations;

namespace Models.Parts {
    public class Processor {

        [Key]
        public int ID { get; set; }

        [MaxLength(16), Required]
        public string SerialNumber { get; set; }

        [MaxLength(16), Required]
        public string Manufacturer { get; set; }

        [MaxLength(32), Required]
        public string Model { get; set; }

        [Range(1, double.MaxValue), Required]
        public double Price { get; set; }

        //nullable ✔
        [Range(0.1, double.MaxValue)]
        public double? FrequencyGHz { get; set; }
        [Range(1, 64)]
        public int? Cores { get; set; }

    }
}
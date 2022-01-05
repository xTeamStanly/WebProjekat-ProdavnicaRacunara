using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Models.Helpers {
    public class CustomerContactHelper {
        [NotNull, Range(1, int.MaxValue), Required]
        public int CustomerID { get; set; }

        [Required, MaxLength(64)]
        public string CustomerContact { get; set; }
    }
}
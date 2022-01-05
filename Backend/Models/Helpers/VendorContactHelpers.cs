using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Models.Helpers {
    public class VendorContactHelper {
        [NotNull, Range(1, int.MaxValue), Required]
        public int VendorID { get; set; }

        [Required, MaxLength(64)]
        public string VendorContact { get; set; }
    }
}
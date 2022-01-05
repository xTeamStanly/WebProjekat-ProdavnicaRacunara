using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Models.Helpers {
    public class PurchaseAdd {
        [NotNull, Range(1, int.MaxValue), Required]
        public int CustomerID { get; set; }

        [NotNull, Range(1, int.MaxValue), Required]
        public int VendorID { get; set; }

        [NotNull, Range(1, int.MaxValue), Required]
        public int ConfigID { get; set; }
    }
}
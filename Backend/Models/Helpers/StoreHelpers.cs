using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Models.Helpers {
    public class StoreAdd {
        [MaxLength(64), Required]
        public string StoreName { get; set; }

        [MaxLength(64), Required]
        public string StoreAddress { get; set; }
    }

    public class StoreHireVendor {
        [NotNull, Range(1, int.MaxValue), Required]
        public int StoreID { get; set; }

        [NotNull, Range(1, int.MaxValue), Required]
        public int VendorID { get; set; }
    }

    public class StoreAddPurchase {
        [NotNull, Range(1, int.MaxValue), Required]
        public int StoreID { get; set; }

        [NotNull, Range(1, int.MaxValue), Required]
        public int PurchaseID { get; set; }

        [NotNull, Range(1, int.MaxValue), Required]
        public int ConfigID { get; set; }

        //nullable ✔
        [DisplayFormat(DataFormatString = "dd-mmm-yyyy")]
        public DateTime? Date { get; set; }

        [MaxLength(32)]
        public string PaymentType { get; set; }
    }
}
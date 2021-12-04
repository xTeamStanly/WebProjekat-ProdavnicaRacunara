using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models {
    public class Purchase {

        [Key]
        public int ID { get; set; }

        [Required, JsonIgnore] //ne serijalizuj
        public Customer.Customer Customer { get; set; }

        [Required, JsonIgnore] //ne serijalizuj
        public Vendor.Vendor Vendor { get; set; }

        [Required, JsonIgnore] //ne serijalizuj
        public Configuration Configuration { get; set; }

        //nullable ✔
        [DisplayFormat(DataFormatString = "dd-mmm-yyyy")]
        public DateTime? Date { get; set; }

        [MaxLength(32)]
        public string PaymentType { get; set; }

        //helperi za output json
        [NotMapped]
        public int customerID { get; set; }
        [NotMapped]
        public int vendorID { get; set; }
        [NotMapped]
        public int configurationID { get; set; }


    }
}
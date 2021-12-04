using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.Vendor {
    public class Vendor {

        [Key]
        public int ID { get; set; }

        //! bolje da maticni bude string
        //[Range(1000000000000, 9999999999999), Required]
        //public Int64 JMBG { get; set; }
        [RegularExpression("^[1-9][0-9]{12}$"), Required]
        public string JMBG { get; set; }

        [MaxLength(32), Required]
        public string Name { get; set; }

        [MaxLength(2)]
        public string MiddleName { get; set; }

        [MaxLength(32), Required]
        public string Surname { get; set; }

        [MaxLength(1), RegularExpression("Ž|M"), Required] //pol ili M ili Ž
        public string Gender { get; set; }

        [Range(50000, float.MaxValue), Required]
        public double Salary { get; set; }

        //nullable ✔

        public DateTime? BirthDate { get; set; }

        [MaxLength(64)]
        public string Address { get; set; }

        [NotMapped]
        public List<string> Contacts { get; set; }

        [JsonIgnore]
        public List<Purchase> Purchases { get; set; }

        //helper property
        [NotMapped]
        public List<Object> VendorPurchases { get; set; }

    }
}
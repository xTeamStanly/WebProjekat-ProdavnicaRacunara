using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.Customer {
    public class Customer {

        [Key]
        public int ID { get; set; }

        //! bolje da maticni bude string
        //[Range(1000000000000, 9999999999999), Required]
        //public Int64 JMBG { get; set; }
        [RegularExpression("^[1-9][0-9]{12}$"), Required]
        public string JMBG { get; set; }

        [MaxLength(32), Required]
        public string Name { get; set; }

        //nullable ✔
        [MaxLength(2)]
        public string MiddleName { get; set; }

        [MaxLength(32), Required]
        public string Surname { get; set; }

        [MaxLength(1), RegularExpression("Ž|M"), Required] //pol ili M ili Ž
        public string Gender { get; set; }

        [NotMapped]
        public List<Configuration> Configurations { get; set; }
        [NotMapped]
        public List<string> Contacts { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models {
    public class Store {

        [Key]
        public int ID { get; set; }

        [MaxLength(64), Required]
        public string Name { get; set; }

        [MaxLength(64), Required]
        public string Address { get; set; }

        //nullable ✔
        [JsonIgnore]
        public List<Vendor.Vendor> Employees { get; set; }

        [JsonIgnore]
        public List<Purchase> Purchases { get; set; }

    }
}
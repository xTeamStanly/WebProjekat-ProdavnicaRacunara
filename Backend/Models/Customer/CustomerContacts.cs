using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Customer {
    public class CustomerContacts {

        [Key]
        public int ID { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required, MaxLength(64)]
        public string Contact { get; set; }

    }
}
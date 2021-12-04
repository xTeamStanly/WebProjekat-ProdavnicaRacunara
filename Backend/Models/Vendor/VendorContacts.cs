using System.ComponentModel.DataAnnotations;

namespace Models.Vendor {
    public class VendorContacts {

        [Key]
        public int ID { get; set; }

        [Required]
        public Vendor Vendor { get; set; }

        [Required]
        public string Contact { get; set; }

    }
}
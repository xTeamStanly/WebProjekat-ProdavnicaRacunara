using Microsoft.EntityFrameworkCore;

namespace Models {
    public class ProdavnicaRacunaraContext : DbContext {
        //customer
        public DbSet<Customer.Customer> Customers { get; set; }
        public DbSet<Customer.CustomerContacts> CustomerContacts { get; set; }

        //parts
        public DbSet<Parts.Processor> Processors { get; set; }
        public DbSet<Parts.GraphicsCard> GraphicsCards { get; set; }
        public DbSet<Parts.Motherboard> Motherboards { get; set; }
        public DbSet<Parts.RAM> RAMs { get; set; }
        public DbSet<Parts.Storage> Storages { get; set; }

        //vendor
        public DbSet<Vendor.Vendor> Vendors { get; set; }
        public DbSet<Vendor.VendorContacts> VendorContacts { get; set; }

        //store
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Store> Stores { get; set; }

        public ProdavnicaRacunaraContext(DbContextOptions options) : base(options) {}
    }
}
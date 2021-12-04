using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Parts;

namespace WebProjekat.Controller {

    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase {

        public ProdavnicaRacunaraContext Context { get; set; }
        public PurchaseController(ProdavnicaRacunaraContext context) { Context = context; }

        /* ------------------------------------ CREATE ------------------------------------ */
        //! ?paymentType=<TIP>
        //! ?date=<DATUM>, format po mogucstvu dd-mmm-yyyy
        [Route("AddPurchase/{CustomerID}/{VendorID}/{ConfigurationID}")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddPurchase(int CustomerID, int VendorID, int ConfigurationID, string paymentType = null, DateTime? date = null) {

            if(CustomerID <= 0) { return BadRequest("Invalid customer ID!"); }
            if(VendorID <= 0) { return BadRequest("Invalid vendor ID!"); }
            if(ConfigurationID <= 0) { return BadRequest("Invalid configuration ID!"); }
            if(paymentType != null && paymentType.Length > 33) {
                return BadRequest("Invalid payment type!");
            }

            try {

                var kupac = await Context.Customers.FindAsync(CustomerID);
                if(kupac == null) { return BadRequest("Customer not found!"); }

                var prodavac = await Context.Vendors.FindAsync(VendorID);
                if(prodavac == null) { return BadRequest("Vendor not found!"); }

                var konfiguracija = await Context.Configurations.FindAsync(ConfigurationID);
                if(konfiguracija == null) { return BadRequest("Configuration not found!"); }

                var kupovina = new Purchase{
                    Customer = kupac,
                    Vendor = prodavac,
                    Configuration = konfiguracija,
                    Date = date,
                    PaymentType = paymentType
                };

                await Context.Purchases.AddAsync(kupovina);
                await Context.SaveChangesAsync();
                return Ok("Added to the database successfully!");
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ READ ------------------------------------ */
        [Route("GetPurchase/ID/{ID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetPurchaseID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {

                var kupovina = await Context.Purchases.FindAsync(ID);
                if(kupovina == null) { return StatusCode(StatusCodes.Status404NotFound, "Purchase not found!"); }

                kupovina = await Context.Purchases
                                .Where(p => p.ID == ID)
                                .Include(p => p.Customer)
                                .Include(p => p.Vendor)
                                .Include(p => p.Configuration)
                                .FirstAsync();

                kupovina.customerID = kupovina.Customer.ID;
                kupovina.vendorID = kupovina.Vendor.ID;
                kupovina.configurationID = kupovina.Configuration.ID;

                return Ok(kupovina);

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }


        public static string[] purchaseTypes = {
            "customer",
            "vendor",
            "configuration",
            "date", //! format po mogucstvu dd-mmm-yyyy
            "payment"
        };

        [Route("GetPurchases/{Type}/{Value}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetPurchase(string Type, string Value) {

            var kriterijum = Type.ToLower();

            if(purchaseTypes.Any(kriterijum.Contains) != true) { return BadRequest("Invalid type!"); }

            try {

                List<Purchase> rezultat = null;

                //filter by customer
                if(kriterijum == "customer") {

                    int vrednost = 0;
                    if(Int32.TryParse(Value, out vrednost) == false) { return BadRequest("Customer ID not a number!"); } //failed parse
                    if(vrednost <= 0) { return BadRequest("Invalid customer ID!"); } //invalid id

                    var kupac = await Context.Customers.FindAsync(vrednost);
                    if(kupac == null) { return StatusCode(StatusCodes.Status404NotFound, "Customer not found!"); }

                    rezultat = await Context.Purchases
                                        .Where(p => p.Customer == kupac)
                                        .Include(p => p.Customer)
                                        .Include(p => p.Vendor)
                                        .Include(p => p.Configuration)
                                        .ToListAsync();

                } else if(kriterijum == "vendor") { //filter by vendor

                    int vrednost = 0;
                    if(Int32.TryParse(Value, out vrednost) == false) { return BadRequest("Vendor ID not a number!"); }
                    if(vrednost <= 0) { return BadRequest("Invalid vendor ID!"); }

                    var prodavac = await Context.Vendors.FindAsync(vrednost);
                    if(prodavac == null) { return StatusCode(StatusCodes.Status404NotFound, "Vendor not found!"); }

                    rezultat = await Context.Purchases
                                        .Where(p => p.Vendor == prodavac)
                                        .Include(p => p.Customer)
                                        .Include(p => p.Vendor)
                                        .Include(p => p.Configuration)
                                        .ToListAsync();

                } else if(kriterijum == "configuration") { //filter by configuration

                    int vrednost = 0;
                    if(Int32.TryParse(Value, out vrednost) == false) { return BadRequest("Configuration ID not a number!"); }
                    if(vrednost <= 0) { return BadRequest("Invalid configuration ID!"); }

                    var konfiguracija = await Context.Configurations.FindAsync(vrednost);
                    if(konfiguracija == null) { return StatusCode(StatusCodes.Status404NotFound, "Configuration not found!"); }

                    rezultat = await Context.Purchases
                                        .Where(p => p.Configuration == konfiguracija)
                                        .Include(p => p.Customer)
                                        .Include(p => p.Vendor)
                                        .Include(p => p.Configuration)
                                        .ToListAsync();

                } else if(kriterijum == "date") { //filter by date

                    DateTime vrednost = new DateTime(0001, 1, 1);
                    if(DateTime.TryParse(Value, out vrednost) == false) { return BadRequest("Date not a date!"); }

                    rezultat = await Context.Purchases
                                        .Where(p => p.Date == vrednost)
                                        .Include(p => p.Customer)
                                        .Include(p => p.Vendor)
                                        .Include(p => p.Configuration)
                                        .ToListAsync();

                } else if(kriterijum == "payment") {

                    if(string.IsNullOrWhiteSpace(Value) || Value.Length > 33) { return BadRequest("Payment type invalid!"); }
                    Value = Value.ToLower();

                    rezultat = await Context.Purchases
                                        .Where(p => p.PaymentType.ToLower() == Value)
                                        .Include(p => p.Customer)
                                        .Include(p => p.Vendor)
                                        .Include(p => p.Configuration)
                                        .ToListAsync();
                } else {
                    return BadRequest("Unknown!");
                }

                if(rezultat.Count > 0) {
                    foreach(var rez in rezultat) {
                        rez.customerID = rez.Customer.ID;
                        rez.vendorID = rez.Vendor.ID;
                        rez.configurationID = rez.Configuration.ID;
                    }

                    return Ok(rezultat);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Not found!");
                }


            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ UPDATE ------------------------------------ */
        [Route("UpdatePurchase/{ID}/Customer/{CustomerID}/Vendor/{VendorID}/Configuration/{ConfigurationID}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdatePurchase(int ID, int CustomerID, int VendorID, int ConfigurationID, string paymentType = null, DateTime? date = null) {

            if(ID <= 0) { return BadRequest("Invalid purchase ID!"); }
            if(CustomerID <= 0) { return BadRequest("Invalid customer ID!"); }
            if(VendorID <= 0) { return BadRequest("Invalid vendor ID!"); }
            if(ConfigurationID <= 0) { return BadRequest("Invalid configuration ID!"); }
            if(paymentType != null && paymentType.Length > 33) {
                return BadRequest("Invalid payment type!");
            }

            try {

                var kupovinaZaPromenu = await Context.Purchases.FindAsync(ID);
                if(kupovinaZaPromenu == null) { return BadRequest("Purchase not found!"); }

                var kupac = await Context.Customers.FindAsync(CustomerID);
                if(kupac == null) { return BadRequest("Customer not found!"); }

                var prodavac = await Context.Vendors.FindAsync(VendorID);
                if(prodavac == null) { return BadRequest("Vendor not found!"); }

                var konfiguracija = await Context.Configurations.FindAsync(ConfigurationID);
                if(konfiguracija == null) { return BadRequest("Configuration not found!"); }

                kupovinaZaPromenu.Customer = kupac;
                kupovinaZaPromenu.Vendor = prodavac;
                kupovinaZaPromenu.Configuration = konfiguracija;
                kupovinaZaPromenu.Date = date;
                kupovinaZaPromenu.PaymentType = paymentType;

                await Context.SaveChangesAsync();
                return Ok("Purchase updated successfully!");

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ DELETE ------------------------------------ */
        [Route("DeletePurchase/ID/{ID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePurchaseID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {
                var kupovinaZaBrisanje = await Context.Purchases.FindAsync(ID);
                if(kupovinaZaBrisanje != null) {
                    Context.Purchases.Remove(kupovinaZaBrisanje);
                    await Context.SaveChangesAsync();
                    return Ok("Purchase successfully deleted!");

                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Purchase not found!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}
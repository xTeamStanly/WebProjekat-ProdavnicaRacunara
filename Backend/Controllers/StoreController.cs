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
    public class StoreController : ControllerBase {

        public ProdavnicaRacunaraContext Context { get; set; }
        public StoreController(ProdavnicaRacunaraContext context) { Context = context; }

        /* ------------------------------------ CREATE ------------------------------------ */
        [Route("AddStore")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddStore([FromBody] Models.Helpers.StoreAdd bodyData) {

            string StoreName = bodyData.StoreName;
            string StoreAddress = bodyData.StoreAddress;

            if(string.IsNullOrWhiteSpace(StoreName) || StoreName.Length > 64) { return BadRequest("Invalid name!"); }
            if(string.IsNullOrEmpty(StoreAddress) || StoreAddress.Length > 64) { return BadRequest("Invalid address!"); }

            var prodavnica = await Context.Stores
                                    .Where(s => ((s.Name == StoreName) || (s.Address == StoreAddress)))
                                    .FirstOrDefaultAsync();

            if(prodavnica != null) { return BadRequest("Store already exists!"); }

            try {
                prodavnica = new Store{
                    Name = StoreName,
                    Address = StoreAddress
                };
                await Context.Stores.AddAsync(prodavnica);
                await Context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, "Added to the database successfully!");
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("HireVendor")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> HireVendor([FromBody] Models.Helpers.StoreHireVendor bodyData) {

            int StoreID = bodyData.StoreID;
            int VendorID = bodyData.VendorID;

            try {

                if(StoreID <= 0) { return BadRequest("Invalid store ID!"); }
                if(VendorID <= 0) { return BadRequest("Invalid vendor ID!"); }

                if(await Context.Stores.FindAsync(StoreID) == null) { return BadRequest("Store not found!"); }
                var prodavnica = await Context.Stores.Where(p => p.ID == StoreID).Include(p => p.Employees).FirstAsync();

                var zaposleni = await Context.Vendors.FindAsync(VendorID);
                if(zaposleni == null) { return BadRequest("Vendor not found!"); }

                if(prodavnica.Employees.Contains(zaposleni) == true) {
                    return BadRequest("Vendor already hired!");
                } else {
                    prodavnica.Employees.Add(zaposleni);
                    await Context.SaveChangesAsync();
                    return Ok("Vendor hired successfully!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("AddPurchase")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddPurchase([FromBody] Models.Helpers.StoreAddPurchase bodyData) {

            int StoreID = bodyData.StoreID;
            int PurchaseID = bodyData.PurchaseID;

            if(StoreID <= 0) { return BadRequest("Invalid store ID!"); }
            if(PurchaseID <= 0) { return BadRequest("Invalid purchase ID!"); }

            try {
                if(await Context.Stores.FindAsync(StoreID) == null) { return BadRequest("Store not found!"); }
                var prodavnica = await Context.Stores.Where(p => p.ID == StoreID).Include(p => p.Purchases).FirstAsync();

                var kupovina = await Context.Purchases.FindAsync(PurchaseID);
                if(kupovina == null) { return BadRequest("Purchase not found!"); }

                if(prodavnica.Purchases.Contains(kupovina) == true) {
                    return BadRequest("Purchase already exits!");
                } else {
                    prodavnica.Purchases.Add(kupovina);
                    await Context.SaveChangesAsync();
                    return Ok("Purchase added successfully!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ READ ------------------------------------ */
        [Route("GetStores")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetStores() {
            try {
                var prodavnice = await Context.Stores.ToListAsync();
                return Ok(prodavnice);
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetStore/ID/{ID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetStoreID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {

                var prodavnica = await Context.Stores.FindAsync(ID);

                if(prodavnica == null) { return StatusCode(StatusCodes.Status404NotFound, "Store not found!"); }

                Context.Stores.Where(p => p.ID == ID).Include(p => p.Employees);

                var listaZaposlenih = (await Context.Stores
                                            .Where(p => p.ID == ID)
                                            .Select(p => p.Employees
                                            ).FirstAsync())
                                                .Select(p => new {p.ID, p.Name, p.Surname, p.Salary})
                                                .ToList();

                return Ok(new {
                    ID = prodavnica.ID,
                    Name = prodavnica.Name,
                    Address = prodavnica.Address,
                    Employees = listaZaposlenih
                });

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetStore/Name/{StoreName}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetStoreName(string StoreName) {

            if(string.IsNullOrWhiteSpace(StoreName) || StoreName.Length > 64) { return BadRequest("Invalid store name!"); }

            try {

                var prodavnica = await Context.Stores.Where(p => p.Name == StoreName).FirstOrDefaultAsync();

                if(prodavnica == null) { return StatusCode(StatusCodes.Status404NotFound, "Store not found!"); }


                Context.Stores.Where(p => p.Name == StoreName).Include(p => p.Employees);

                var listaZaposlenih = (await Context.Stores
                                            .Where(p => p.Name == StoreName)
                                            .Select(p => p.Employees
                                            ).FirstAsync())
                                                .Select(p => new {p.ID, p.Name, p.Surname, p.Salary})
                                                .ToList();

                return Ok(new {
                    ID = prodavnica.ID,
                    Name = prodavnica.Name,
                    Address = prodavnica.Address,
                    Employees = listaZaposlenih
                });

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        //!! SPOR QUERY - PUNO PODATAKA !!
        [Route("GetStorePurchasesID/ID/{ID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetStorePurchasesID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {

                var prodavnica = await Context.Stores.FindAsync(ID);

                if(prodavnica == null) { return StatusCode(StatusCodes.Status404NotFound, "Store not found!"); }

                Context.Stores.Where(p => p.ID == ID).Include(p => p.Purchases);

                var listaKupovina = (await Context.Stores
                                            .Where(p => p.ID == ID)
                                            .Select(p => p.Purchases
                                            ).FirstAsync())
                                                .Select(p => p.ID)
                                                .ToList();

                return Ok(new {
                    ID = prodavnica.ID,
                    Name = prodavnica.Name,
                    Address = prodavnica.Address,
                    Purchases = listaKupovina
                });

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ UPDATE ------------------------------------ */
        [Route("UpdateStore/{ID}/{StoreName}/{StoreAddress}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateStore(int ID, string StoreName, string StoreAddress) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }
            if(string.IsNullOrWhiteSpace(StoreName) || StoreName.Length > 64) { return BadRequest("Invalid name!"); }
            if(string.IsNullOrEmpty(StoreAddress) || StoreAddress.Length > 64) { return BadRequest("Invalid address!"); }

            try {

                var prodavnicaZaPromenu = await Context.Stores.Where(p => p.ID == ID).FirstOrDefaultAsync();
                if(prodavnicaZaPromenu == null) { return StatusCode(StatusCodes.Status404NotFound, "Store not found!"); }

                prodavnicaZaPromenu.Name = StoreName;
                prodavnicaZaPromenu.Address = StoreAddress;

                await Context.SaveChangesAsync();
                return Ok("Store updated successfully!");

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ DELETE ------------------------------------ */
        [Route("FireVendor/{StoreID}/{VendorID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> FireVendor(int StoreID, int VendorID) {

            if(StoreID <= 0) { return BadRequest("Invalid store ID!"); }
            if(VendorID <= 0) { return BadRequest("Invalid vendor ID!"); }

            try {
                if(await Context.Stores.FindAsync(StoreID) == null) { return BadRequest("Store not found!"); }
                var prodavnica = await Context.Stores.Where(p => p.ID == StoreID).Include(p => p.Employees).FirstAsync();

                var zaposleni = await Context.Vendors.FindAsync(VendorID);
                if(zaposleni == null) { return BadRequest("Vendor not found!"); }

                if(prodavnica.Employees.Contains(zaposleni) == false) {
                    return BadRequest("Vendor already fired!");
                } else {
                    prodavnica.Employees.Remove(zaposleni);
                    await Context.SaveChangesAsync();
                    return Ok("Vendor fired successfully!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("DeletePurchase/{StoreID}/{PurchaseID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeletePurchase(int StoreID, int PurchaseID) {

            if(StoreID <= 0) { return BadRequest("Invalid store ID!"); }
            if(PurchaseID <= 0) { return BadRequest("Invalid purchase ID!"); }

            try {
                if(await Context.Stores.FindAsync(StoreID) == null) { return BadRequest("Store not found!"); }
                var prodavnica = await Context.Stores.Where(p => p.ID == StoreID).Include(p => p.Purchases).FirstAsync();

                var kupovina = await Context.Purchases.FindAsync(PurchaseID);
                if(kupovina == null) { return BadRequest("Purchase not found!"); }

                if(prodavnica.Purchases.Contains(kupovina) == false) {
                    return BadRequest("Purchase doesn't exist!");
                } else {
                    prodavnica.Purchases.Remove(kupovina);
                    await Context.SaveChangesAsync();
                    return Ok("Purchase removed successfully!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("DeleteStore/{StoreID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteStore(int StoreID) {

            if(StoreID <= 0) { return BadRequest("Invalid ID!"); }

            try {
                var prodavnicaZaBrisanje = await Context.Stores.FindAsync(StoreID);

                if(prodavnicaZaBrisanje == null) { return StatusCode(StatusCodes.Status404NotFound, "Configuration not found!"); }

                prodavnicaZaBrisanje = await Context.Stores.Where(p => p.ID == StoreID).Include(p => p.Purchases).FirstAsync();
                var listaKupovinaID = prodavnicaZaBrisanje.Purchases.Select(p => p.ID).ToList();
                //izbrisi sve kupovine iz te prodavnice
                foreach(var kupovinaID in listaKupovinaID) {
                    try { Context.Purchases.Remove(await Context.Purchases.FindAsync(kupovinaID)); }
                    catch(Exception e) { return BadRequest(e.Message); }
                }

                prodavnicaZaBrisanje = await Context.Stores.Where(p => p.ID == StoreID).Include(p => p.Employees).FirstAsync();
                var listaZaposlenihID = prodavnicaZaBrisanje.Employees.Select(p => p.ID).ToList();
                //izbrisi sve prodavce iz te prodavnice
                foreach(var zaposleniID in listaZaposlenihID) {
                    try { Context.Vendors.Remove(await Context.Vendors.FindAsync(zaposleniID)); }
                    catch(Exception e) { return BadRequest(e.Message); }
                }

                prodavnicaZaBrisanje.Employees.Clear();
                prodavnicaZaBrisanje.Purchases.Clear();
                Context.Stores.Remove(prodavnicaZaBrisanje);

                await Context.SaveChangesAsync();
                return Ok("Store successfully deleted!");

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}
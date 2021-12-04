using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace WebProjekat.Controller.Vendor {

    [ApiController]
    [Route("[controller]")]
    public class VendorController : ControllerBase {

        public ProdavnicaRacunaraContext Context { get; set; }
        public VendorController(ProdavnicaRacunaraContext context) { Context = context; }

        /* ------------------------------------ CREATE ------------------------------------ */
        [Route("AddVendor")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddVendor([FromBody] Models.Vendor.Vendor prodavac) {


            if(Regex.IsMatch(prodavac.JMBG, "^[1-9][0-9]{12}$") == false) { return BadRequest("Invalid JMBG!"); }
            if(Regex.IsMatch(prodavac.Gender, "Ž|M") == false) { return BadRequest("Invalid gender!"); }

            if(string.IsNullOrWhiteSpace(prodavac.Name) || prodavac.Name.Length > 32) { return BadRequest("Invalid name!"); }
            if(string.IsNullOrWhiteSpace(prodavac.Surname) || prodavac.Surname.Length > 32) { return BadRequest("Invalid surname!"); }
            if(prodavac.MiddleName != null && prodavac.MiddleName.Length > 2) { return BadRequest("Invalid middle name!"); }
            if(prodavac.Address != null && prodavac.Address.Length > 65) { return BadRequest("Invalid address!"); }

            try {

                var privremeno = await Context.Vendors.Where(v => v.JMBG == prodavac.JMBG).FirstOrDefaultAsync();

                if(privremeno == null) {
                    await Context.Vendors.AddAsync(prodavac);
                    await Context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status201Created, "Added to the database successfully!");
                } else {
                    return BadRequest("JMBG duplicate!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ READ ------------------------------------ */
        [Route("GetVendor/ID/{ID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetVendorID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {

                var prodavac = await Context.Vendors.Where(p => p.ID == ID).FirstOrDefaultAsync();

                if(prodavac != null) {

                    prodavac.Contacts = new List<string>();

                    await Context.VendorContacts
                        .Where(p => p.Vendor.ID == prodavac.ID)
                        .Select(p => p.Contact)
                        .ForEachAsync(p => prodavac.Contacts.Add(p));

                    return Ok(prodavac);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Vendor not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetVendor/JMBG/{JMBG}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetVendorJMBG(string JMBG) {

            if(Regex.IsMatch(JMBG, "^[1-9][0-9]{12}$") == false) { return BadRequest("Invalid JMBG!"); }

            try {

                var prodavac = await Context.Vendors.Where(p => p.JMBG == JMBG).FirstOrDefaultAsync();

                if(prodavac != null) {
                    prodavac.Contacts = new List<string>();

                    await Context.VendorContacts
                        .Where(p => p.Vendor.ID == prodavac.ID)
                        .Select(p => p.Contact)
                        .ForEachAsync(p => prodavac.Contacts.Add(p));

                    return Ok(prodavac);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Vendor not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetVendorPurchases/ID/{ID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetVendorPurchases(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {

                var prodavac = await Context.Vendors.Where(p => p.ID == ID).FirstOrDefaultAsync();

                if(prodavac == null) {
                    return StatusCode(StatusCodes.Status404NotFound, "Vendor not found!");
                }

                prodavac.VendorPurchases = new List<Object>();
                await Context.Purchases
                    .Where(p => p.Vendor.ID == prodavac.ID)
                    .Select(p => new {
                        customerID = p.Customer.ID,
                        confID = p.Configuration.ID,
                        date = p.Date,
                        paymentType = p.PaymentType
                    })
                    .ForEachAsync(p => prodavac.VendorPurchases.Add(p));

                prodavac.Contacts = new List<string>();
                await Context.VendorContacts
                    .Where(p => p.Vendor.ID == prodavac.ID)
                    .Select(p => p.Contact)
                    .ForEachAsync(p => prodavac.Contacts.Add(p));

                return Ok(prodavac);

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ UPDATE ------------------------------------ */
        [Route("UpdateVendor")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateVendor([FromBody] Models.Vendor.Vendor prodavac) {

            if(prodavac.ID <= 0) { return BadRequest("Invalid ID!"); }
            if(Regex.IsMatch(prodavac.JMBG, "^[1-9][0-9]{12}$") == false) { return BadRequest("Invalid JMBG!"); }
            if(Regex.IsMatch(prodavac.Gender, "Ž|M") == false) { return BadRequest("Invalid gender!"); }

            if(string.IsNullOrWhiteSpace(prodavac.Name) || prodavac.Name.Length > 32) { return BadRequest("Invalid name!"); }
            if(string.IsNullOrWhiteSpace(prodavac.Surname) || prodavac.Surname.Length > 32) { return BadRequest("Invalid surname!"); }
            if(prodavac.MiddleName != null && prodavac.MiddleName.Length > 2) { return BadRequest("Invalid middle name!"); }
            if(prodavac.Address != null && prodavac.Address.Length > 65) { return BadRequest("Invalid address!"); }

            try {

                var prodavacZaPromenu = await Context.Vendors.FindAsync(prodavac.ID);

                if(prodavacZaPromenu == null) {
                    return StatusCode(StatusCodes.Status404NotFound, "Vendor not found!");
                }

                prodavacZaPromenu.ID = prodavac.ID;
                prodavacZaPromenu.JMBG = prodavac.JMBG;
                prodavacZaPromenu.Name = prodavac.Name;
                prodavacZaPromenu.MiddleName = prodavac.MiddleName;
                prodavacZaPromenu.Surname = prodavac.Surname;
                prodavacZaPromenu.Gender = prodavac.Gender;
                prodavacZaPromenu.Salary = prodavac.Salary;
                prodavacZaPromenu.BirthDate = prodavac.BirthDate;
                prodavacZaPromenu.Address = prodavac.Address;

                await Context.SaveChangesAsync();
                return Ok("Vendor updated successfully!");

            } catch(Exception e) {
                return BadRequest(e.Message);
            }

        }

        /* ------------------------------------ DELETE ------------------------------------ */
        [Route("DeleteVendor/ID/{ID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteVendorID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {
                var prodavacZaBrisanje = await Context.Vendors.FindAsync(ID);
                if(prodavacZaBrisanje != null) {

                    Context.Vendors.Remove(prodavacZaBrisanje);
                    await Context.SaveChangesAsync();
                    return Ok("Vendor successfully deleted!");

                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Vendor not found!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}
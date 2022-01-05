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
    public class VendorContactsController : ControllerBase {

        public ProdavnicaRacunaraContext Context { get; set; }
        public VendorContactsController(ProdavnicaRacunaraContext context) { Context = context; }

        /* ------------------------------------ CREATE ------------------------------------ */
        [Route("AddVendorContact")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddVendorContact([FromBody] Models.Helpers.VendorContactHelper bodyData) {

            int VendorID = bodyData.VendorID;
            string VendorContact = bodyData.VendorContact;

            if(VendorID <= 0) { return BadRequest("Invalid ID!"); }

            if(string.IsNullOrWhiteSpace(VendorContact) && VendorContact.Length > 64) {
                return BadRequest("Invalid vendor contact!");
            }

            try {
                var privremeno = await Context.Vendors.Where(p => p.ID == VendorID).FirstOrDefaultAsync();
                if(privremeno != null) {

                    await Context.VendorContacts.AddAsync(new Models.Vendor.VendorContacts {
                        Vendor = privremeno,
                        Contact = VendorContact
                    });
                    await Context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status201Created, "Added to the database successfully!");

                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Vendor not found!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }

        }

        /* ------------------------------------ READ ------------------------------------ */
        [Route("GetVendorContacts/ID/{ID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetVendorContactID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {

                var kontakt = await Context.VendorContacts
                    .Where(p => p.Vendor.ID == ID)
                    .Select(p => p.Contact)
                    .ToListAsync();

                if(kontakt.Count != 0) {
                    return Ok(kontakt);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Vendor not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetVendorContacts/JMBG/{JMBG}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetVendorContactsJMBG(string JMBG) {

            if(Regex.IsMatch(JMBG, "^[1-9][0-9]{12}$") == false) { return BadRequest("Invalid JMBG!"); }

            try {

                var kontakt = await Context.VendorContacts.
                    Where(p => p.Vendor.JMBG == JMBG)
                    .Select(p => p.Contact)
                    .ToListAsync();

                if(kontakt.Count != 0) {
                    return Ok(kontakt);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Vendor not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetVendorFromContact/{Contact}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetVendorFromContact(string Contact) {

            if(string.IsNullOrWhiteSpace(Contact) || Contact.Length > 64) {
                return BadRequest("Invalid contact!");
            }

            try {

                var kontakt = await Context.VendorContacts
                    .Where(p => p.Contact == Contact)
                    .Include(p => p.Vendor)
                    .FirstOrDefaultAsync();

                if(kontakt == null) {
                    return StatusCode(StatusCodes.Status404NotFound, "Vendor not found!");
                }

                kontakt.Vendor.Contacts = await Context.VendorContacts
                    .Where(p => p.Vendor.ID == kontakt.Vendor.ID)
                    .Select(p => p.Contact)
                    .ToListAsync();

                return Ok(kontakt.Vendor);

            } catch(Exception e) {
                return BadRequest(e.Message);
            }

        }


        /* ------------------------------------ UPDATE ------------------------------------ */
        [Route("UpdateVendorContact/{VendorID}/{OldContact}/{NewContact}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateVendorContact(int VendorID, string OldContact, string NewContact) {

            if(VendorID <= 0) { return BadRequest("Invalid ID!"); }

            if(string.IsNullOrWhiteSpace(OldContact) || OldContact.Length > 64) { return BadRequest("Invalid old contact!"); }
            if(string.IsNullOrWhiteSpace(NewContact) || NewContact.Length > 64) { return BadRequest("Invalid new contact!"); }

            try {

                var kontaktZaPromenu = await Context.VendorContacts
                    .Where(p => p.Vendor.ID == VendorID && p.Contact == OldContact)
                    .FirstOrDefaultAsync();

                if(kontaktZaPromenu == null) {
                    return StatusCode(StatusCodes.Status404NotFound, "Contact not found");
                }

                kontaktZaPromenu.Contact = NewContact;
                await Context.SaveChangesAsync();
                return Ok("Vendor contact updated successfully!");

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ DELETE ------------------------------------ */
        [Route("DeleteVendorContact/{VendorID}/{VendorContact}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteVendorContactID(int VendorID, string VendorContact) {

            if(VendorID <= 0) { return BadRequest("Invalid ID!"); }
            if(string.IsNullOrWhiteSpace(VendorContact) || VendorContact.Length > 64) { return BadRequest("Invalid contact!"); }

            try {

                var kontaktZaBrisanje = await Context.VendorContacts
                    .Where(p => p.Vendor.ID == VendorID && p.Contact == VendorContact)
                    .FirstOrDefaultAsync();

                if(kontaktZaBrisanje == null) {
                    return StatusCode(StatusCodes.Status404NotFound, "Vendor not found!");
                }

                Context.VendorContacts.Remove(kontaktZaBrisanje);
                await Context.SaveChangesAsync();
                return Ok("Vendor contact deleted!");

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}
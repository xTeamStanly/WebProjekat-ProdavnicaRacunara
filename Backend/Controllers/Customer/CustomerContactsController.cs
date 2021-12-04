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

namespace WebProjekat.Controller.Customer {

    [ApiController]
    [Route("[controller]")]
    public class CustomerContactsController : ControllerBase {

        public ProdavnicaRacunaraContext Context { get; set; }
        public CustomerContactsController(ProdavnicaRacunaraContext context) { Context = context; }

        /* ------------------------------------ CREATE ------------------------------------ */
        [Route("AddCustomerContact/{CustomerID}/{CustomerContact}")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddCustomerContact(int CustomerID, string CustomerContact) {

            if(CustomerID <= 0) { return BadRequest("Invalid ID!"); }

            if(string.IsNullOrWhiteSpace(CustomerContact) && CustomerContact.Length > 64) {
                return BadRequest("Invalid customer contact!");
            }

            try {
                var privremeno = await Context.Customers.Where(p => p.ID == CustomerID).FirstOrDefaultAsync();
                if(privremeno != null) {

                    await Context.CustomerContacts.AddAsync(new Models.Customer.CustomerContacts {
                        Customer = privremeno,
                        Contact = CustomerContact
                    });
                    await Context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status201Created, "Added to the database successfully!");

                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Customer not found!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }

        }

        /* ------------------------------------ READ ------------------------------------ */
        [Route("GetCustomerContacts/ID/{ID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCustomerContactID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {

                var kontakt = await Context.CustomerContacts.
                    Where(p => p.Customer.ID == ID)
                    .Select(p => p.Contact)
                    .ToListAsync();

                if(kontakt.Count != 0) {
                    return Ok(kontakt);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Customer not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetCustomerContacts/JMBG/{JMBG}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCustomerContactsJMBG(string JMBG) {

            if(Regex.IsMatch(JMBG, "^[1-9][0-9]{12}$") == false) { return BadRequest("Invalid JMBG!"); }

            try {

                var kontakt = await Context.CustomerContacts.
                    Where(p => p.Customer.JMBG == JMBG)
                    .Select(p => p.Contact)
                    .ToListAsync();

                if(kontakt.Count != 0) {
                    return Ok(kontakt);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Customer not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetCustomerFromContact/{Contact}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCustomerFromContact(string Contact) {

            if(string.IsNullOrWhiteSpace(Contact) || Contact.Length > 64) {
                return BadRequest("Invalid contact!");
            }

            try {

                var kontakt = await Context.CustomerContacts
                    .Where(p => p.Contact == Contact)
                    .Include(p => p.Customer)
                    .FirstOrDefaultAsync();

                if(kontakt == null) {
                    return StatusCode(StatusCodes.Status404NotFound, "Contact not found!");
                }

                kontakt.Customer.Contacts = await Context.CustomerContacts
                    .Where(p => p.Customer.ID == kontakt.Customer.ID)
                    .Select(p => p.Contact)
                    .ToListAsync();

                return Ok(kontakt.Customer);

            } catch(Exception e) {
                return BadRequest(e.Message);
            }

        }


        /* ------------------------------------ UPDATE ------------------------------------ */
        [Route("UpdateCustomerContact/{CustomerID}/{OldContact}/{NewContact}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCustomerContact(int CustomerID, string OldContact, string NewContact) {

            if(CustomerID <= 0) { return BadRequest("Invalid ID!"); }

            if(string.IsNullOrWhiteSpace(OldContact) || OldContact.Length > 64) { return BadRequest("Invalid old contact!"); }
            if(string.IsNullOrWhiteSpace(NewContact) || NewContact.Length > 64) { return BadRequest("Invalid new contact!"); }

            try {

                var kontaktZaPromenu = await Context.CustomerContacts
                    .Where(p => p.Customer.ID == CustomerID && p.Contact == OldContact)
                    .FirstOrDefaultAsync();

                if(kontaktZaPromenu == null) {
                    return StatusCode(StatusCodes.Status404NotFound, "Contact not found");
                }

                kontaktZaPromenu.Contact = NewContact;
                await Context.SaveChangesAsync();
                return Ok("Customer contact updated successfully!");

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ DELETE ------------------------------------ */
        [Route("DeleteCustomerContact/{CustomerID}/{CustomerContact}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCustomerContactID(int CustomerID, string CustomerContact) {

            if(CustomerID <= 0) { return BadRequest("Invalid ID!"); }
            if(string.IsNullOrWhiteSpace(CustomerContact) || CustomerContact.Length > 64) { return BadRequest("Invalid contact!"); }

            try {

                var kontaktZaBrisanje = await Context.CustomerContacts
                    .Where(p => p.Customer.ID == CustomerID && p.Contact == CustomerContact)
                    .FirstOrDefaultAsync();

                if(kontaktZaBrisanje == null) {
                    return StatusCode(StatusCodes.Status404NotFound, "Customer not found!");
                }

                Context.CustomerContacts.Remove(kontaktZaBrisanje);
                await Context.SaveChangesAsync();
                return Ok("Customer contact deleted!");

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}
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
    public class CustomerController : ControllerBase {

        public ProdavnicaRacunaraContext Context { get; set; }
        public CustomerController(ProdavnicaRacunaraContext context) { Context = context; }

        /* ------------------------------------ CREATE ------------------------------------ */
        [Route("AddCustomer")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddCustomer([FromBody] Models.Customer.Customer musterija) {

            if(Regex.IsMatch(musterija.JMBG, "^[1-9][0-9]{12}$") == false) { return BadRequest("Invalid JMBG!"); }

            if(Regex.IsMatch(musterija.Gender, "Ž|M") == false) { return BadRequest("Invalid gender!"); }

            if(string.IsNullOrWhiteSpace(musterija.Name) || musterija.Name.Length > 32) { return BadRequest("Invalid name!"); }

            if(string.IsNullOrWhiteSpace(musterija.Surname) || musterija.Surname.Length > 32) { return BadRequest("Invalid surname!"); }

            if(musterija.MiddleName != null && musterija.MiddleName.Length > 2) { return BadRequest("Invalid middle name!"); }

            try {

                var privremeno = await Context.Customers.Where(c => c.JMBG == musterija.JMBG).FirstOrDefaultAsync();

                if(privremeno == null) {
                    await Context.Customers.AddAsync(musterija);
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
        [Route("GetCustomer/ID/{ID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCustomerID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {

                var musterija = await Context.Customers.Where(p => p.ID == ID).FirstOrDefaultAsync();

                if(musterija != null) {

                    musterija.Contacts = new List<string>();
                    await Context.CustomerContacts
                        .Where(p => p.Customer.ID == musterija.ID)
                        .Select(p => p.Contact)
                        .ForEachAsync(p => musterija.Contacts.Add(p));

                    return Ok(musterija);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Customer not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetCustomer/JMBG/{JMBG}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCustomerJMBG(string JMBG) {

            if(Regex.IsMatch(JMBG, "^[1-9][0-9]{12}$") == false) { return BadRequest("Invalid JMBG!"); }

            try {

                var musterija = await Context.Customers.Where(p => p.JMBG == JMBG).FirstOrDefaultAsync();

                if(musterija != null) {

                    musterija.Contacts = new List<string>();
                    await Context.CustomerContacts
                        .Where(p => p.Customer.ID == musterija.ID)
                        .Select(p => p.Contact)
                        .ForEachAsync(p => musterija.Contacts.Add(p));

                    return Ok(musterija);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Customer not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetCustomerConfigurations/ID/{ID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCustomerConfigurations(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {

                var musterija = await Context.Customers.Where(p => p.ID == ID).FirstOrDefaultAsync();

                if(musterija == null) {
                    return StatusCode(StatusCodes.Status404NotFound, "Customer not found!");
                }

                musterija.Configurations = new List<Configuration>();
                await Context.Purchases
                    .Where(p => p.Customer.ID == musterija.ID)
                    .Include(p => p.Configuration).ThenInclude(p => p.CPU)
                    .Include(p => p.Configuration).ThenInclude(p => p.GPU)
                    .Include(p => p.Configuration).ThenInclude(p => p.MB)
                    .Include(p => p.Configuration).ThenInclude(p => p.RAM)
                    .Include(p => p.Configuration).ThenInclude(p => p.STORAGE)
                    .ForEachAsync(conf => musterija.Configurations.Add(conf.Configuration));

                musterija.Contacts = new List<string>();
                await Context.CustomerContacts
                    .Where(p => p.Customer.ID == musterija.ID)
                    .ForEachAsync(p => musterija.Contacts.Add(p.Contact));

                return Ok(musterija);

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ UPDATE ------------------------------------ */
        [Route("UpdateCustomer")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCustomer([FromBody] Models.Customer.Customer musterija) {

            if(musterija.ID <= 0) { return BadRequest("Invalid ID!"); }

            if(Regex.IsMatch(musterija.JMBG, "^[1-9][0-9]{12}$") == false) { return BadRequest("Invalid JMBG!"); }

            if(Regex.IsMatch(musterija.Gender, "Ž|M") == false) { return BadRequest("Invalid gender!"); }

            if(string.IsNullOrWhiteSpace(musterija.Name) || musterija.Name.Length > 32) { return BadRequest("Invalid name!"); }

            if(string.IsNullOrWhiteSpace(musterija.Surname) || musterija.Surname.Length > 32) { return BadRequest("Invalid surname!"); }

            if(musterija.MiddleName != null && musterija.MiddleName.Length > 2) { return BadRequest("Invalid middle name!"); }

            try {

                var musterijaZaPromenu = await Context.Customers.FindAsync(musterija.ID);

                if(musterijaZaPromenu != null) {
                    musterijaZaPromenu.ID = musterija.ID;
                    musterijaZaPromenu.JMBG = musterija.JMBG;
                    musterijaZaPromenu.Name = musterija.Name;
                    musterijaZaPromenu.MiddleName = musterija.MiddleName;
                    musterijaZaPromenu.Surname = musterija.Surname;
                    musterijaZaPromenu.Gender = musterija.Gender;

                    await Context.SaveChangesAsync();
                    return Ok("Customer updated successfully!");

                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Customer not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }

        }

        /* ------------------------------------ DELETE ------------------------------------ */
        [Route("DeleteCustomer/ID/{ID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCustomerID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {
                var musterijaZaBrisanje = await Context.Customers.FindAsync(ID);
                if(musterijaZaBrisanje != null) {

                    Context.Customers.Remove(musterijaZaBrisanje);
                    await Context.SaveChangesAsync();
                    return Ok("Customer successfully deleted!");

                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Customer not found!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}
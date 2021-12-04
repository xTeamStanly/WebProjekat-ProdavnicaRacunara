using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Parts;

namespace WebProjekat.Controller.Parts {

    [ApiController]
    [Route("[controller]")]
    public class MotherboardController : ControllerBase {

        public ProdavnicaRacunaraContext Context { get; set; }
        public MotherboardController(ProdavnicaRacunaraContext context) { Context = context; }

        /* ------------------------------------ CREATE ------------------------------------ */
        [Route("AddMotherBoard")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddMotherboard([FromBody] Motherboard maticna) {

            if(string.IsNullOrWhiteSpace(maticna.SerialNumber) || maticna.SerialNumber.Length > 16) {
                return BadRequest("Invalid serial number!");
            }

            if(string.IsNullOrWhiteSpace(maticna.Manufacturer) || maticna.Manufacturer.Length > 16) {
                return BadRequest("Invalid manufacturer name!");
            }

            if(string.IsNullOrWhiteSpace(maticna.Model) || maticna.Model.Length > 32) {
                return BadRequest("Invalid model name!");
            }

            if(maticna.Price < 1) { return BadRequest("Invalid price!"); }

            try {

                var privremeno = await Context.Motherboards.Where(p => p.SerialNumber == maticna.SerialNumber).FirstOrDefaultAsync();

                if(privremeno == null) {
                    await Context.Motherboards.AddAsync(maticna);
                    await Context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status201Created, "Added to the database successfully!");
                } else {
                    return BadRequest("Serial number duplicate!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ READ ------------------------------------ */
        [Route("GetMotherboard/ID/{ID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetMotherboardID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {

                var maticna = await Context.Motherboards.Where(p => p.ID == ID).FirstOrDefaultAsync();

                if(maticna != null) {
                    return Ok(maticna);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Motherboard not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetMotherboard/SerialNumber/{SerialNumber}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetMotherboardSerialNumber(string SerialNumber) {

            if(string.IsNullOrWhiteSpace(SerialNumber) || SerialNumber.Length > 16) {
                return BadRequest("Invalid Serial Number!");
            }

            try {

                var maticna = await Context.Motherboards.Where(p => p.SerialNumber == SerialNumber).FirstOrDefaultAsync();

                if(maticna != null) {
                    return Ok(maticna);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Motherboard not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ UPDATE ------------------------------------ */
        [Route("UpdateMotherboard")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateMotherboard([FromBody] Motherboard maticna) {

            if(string.IsNullOrWhiteSpace(maticna.SerialNumber) || maticna.SerialNumber.Length > 16) {
                return BadRequest("Invalid serial number!");
            }

            if(string.IsNullOrWhiteSpace(maticna.Manufacturer) || maticna.Manufacturer.Length > 16) {
                return BadRequest("Invalid manufacturer name!");
            }

            if(string.IsNullOrWhiteSpace(maticna.Model) || maticna.Model.Length > 32) {
                return BadRequest("Invalid model name!");
            }

            if(maticna.Price < 1) { return BadRequest("Invalid price!"); }

            try {

                var maticnaZaPromenu = await Context.Motherboards.FindAsync(maticna.ID);

                if(maticnaZaPromenu != null) {
                    maticnaZaPromenu.SerialNumber = maticna.SerialNumber;
                    maticnaZaPromenu.Manufacturer = maticna.Manufacturer;
                    maticnaZaPromenu.Model = maticna.Model;
                    maticnaZaPromenu.Price = maticna.Price;

                    await Context.SaveChangesAsync();
                    return Ok("Motherboard updated successfully!");

                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Motherboard not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }

        }

        /* ------------------------------------ DELETE ------------------------------------ */
        [Route("DeleteMotherboard/ID/{ID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteMotherboardID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {
                var maticnaZaBrisanje = await Context.Motherboards.FindAsync(ID);
                if(maticnaZaBrisanje != null) {

                    Context.Motherboards.Remove(maticnaZaBrisanje);
                    await Context.SaveChangesAsync();
                    return Ok("Motherboard successfully deleted!");

                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Motherboard not found!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}
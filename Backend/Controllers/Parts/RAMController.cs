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
    public class RAMController : ControllerBase {

        public ProdavnicaRacunaraContext Context { get; set; }
        public RAMController(ProdavnicaRacunaraContext context) { Context = context; }

        /* ------------------------------------ CREATE ------------------------------------ */
        [Route("AddRAM")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddRAM([FromBody] RAM ram) {

            if(string.IsNullOrWhiteSpace(ram.SerialNumber) || ram.SerialNumber.Length > 16) {
                return BadRequest("Invalid serial number!");
            }

            if(string.IsNullOrWhiteSpace(ram.Manufacturer) || ram.Manufacturer.Length > 16) {
                return BadRequest("Invalid manufacturer name!");
            }

            if(string.IsNullOrWhiteSpace(ram.Model) || ram.Model.Length > 32) {
                return BadRequest("Invalid model name!");
            }

            if(ram.Price < 1) { return BadRequest("Invalid price!"); }

            if(ram.MemoryGB > 128 || ram.MemoryGB < 1) { return BadRequest("Invalid memory amount!"); }

            if(ram.FrequencyMHz != null && (ram.FrequencyMHz < 1 || ram.FrequencyMHz > 5333)) {
                return BadRequest("Invalid memory frequency!");
            }

            try {

                var privremeno = await Context.RAMs.Where(p => p.SerialNumber == ram.SerialNumber).FirstOrDefaultAsync();

                if(privremeno == null) {
                    await Context.RAMs.AddAsync(ram);
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
        [Route("GetRAM/ID/{ID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetRAMID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {

                var ram = await Context.RAMs.Where(p => p.ID == ID).FirstOrDefaultAsync();

                if(ram != null) {
                    return Ok(ram);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "RAM not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetRAM/SerialNumber/{SerialNumber}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetRAMSerialNumber(string SerialNumber) {

            if(string.IsNullOrWhiteSpace(SerialNumber) || SerialNumber.Length > 16) {
                return BadRequest("Invalid Serial Number!");
            }

            try {

                var ram = await Context.RAMs.Where(p => p.SerialNumber == SerialNumber).FirstOrDefaultAsync();

                if(ram != null) {
                    return Ok(ram);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "RAM not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ UPDATE ------------------------------------ */
        [Route("UpdateRAM")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateRAM([FromBody] RAM ram) {

            if(string.IsNullOrWhiteSpace(ram.SerialNumber) || ram.SerialNumber.Length > 16) {
                return BadRequest("Invalid serial number!");
            }

            if(string.IsNullOrWhiteSpace(ram.Manufacturer) || ram.Manufacturer.Length > 16) {
                return BadRequest("Invalid manufacturer name!");
            }

            if(string.IsNullOrWhiteSpace(ram.Model) || ram.Model.Length > 32) {
                return BadRequest("Invalid model name!");
            }

            if(ram.Price < 1) { return BadRequest("Invalid price!"); }

            if(ram.MemoryGB > 128 || ram.MemoryGB < 1) { return BadRequest("Invalid memory amount!"); }

            if(ram.FrequencyMHz != null && (ram.FrequencyMHz < 1 || ram.FrequencyMHz > 5333)) {
                return BadRequest("Invalid memory frequency!");
            }

            try {

                var ramZaPromenu = await Context.RAMs.FindAsync(ram.ID);

                if(ramZaPromenu != null) {
                    ramZaPromenu.SerialNumber = ram.SerialNumber;
                    ramZaPromenu.Manufacturer = ram.Manufacturer;
                    ramZaPromenu.Model = ram.Model;
                    ramZaPromenu.Price = ram.Price;
                    ramZaPromenu.MemoryGB = ram.MemoryGB;
                    ramZaPromenu.FrequencyMHz = ram.FrequencyMHz;

                    await Context.SaveChangesAsync();
                    return Ok("RAM updated successfully!");

                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "RAM not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }

        }

        /* ------------------------------------ DELETE ------------------------------------ */
        [Route("DeleteRAM/ID/{ID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteRAMID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {
                var ramZaBrisanje = await Context.RAMs.FindAsync(ID);
                if(ramZaBrisanje != null) {

                    Context.RAMs.Remove(ramZaBrisanje);
                    await Context.SaveChangesAsync();
                    return Ok("RAM successfully deleted!");

                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "RAM not found!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}
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
    public class GraphicsCardController : ControllerBase {

        public ProdavnicaRacunaraContext Context { get; set; }
        public GraphicsCardController(ProdavnicaRacunaraContext context) { Context = context; }

        /* ------------------------------------ CREATE ------------------------------------ */
        [Route("AddGraphicsCard")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddGraphicsCard([FromBody] GraphicsCard kartica) {

            if(string.IsNullOrWhiteSpace(kartica.SerialNumber) || kartica.SerialNumber.Length > 16) {
                return BadRequest("Invalid serial number!");
            }

            if(string.IsNullOrWhiteSpace(kartica.Manufacturer) || kartica.Manufacturer.Length > 16) {
                return BadRequest("Invalid manufacturer name!");
            }

            if(string.IsNullOrWhiteSpace(kartica.Model) || kartica.Model.Length > 32) {
                return BadRequest("Invalid model name!");
            }

            if(kartica.Price < 1) { return BadRequest("Invalid price!"); }

            if(kartica.MemoryGB != null && (kartica.MemoryGB < 1 || kartica.MemoryGB > 32)) {
                return BadRequest("Invlid memory amount!");
            }

            try {

                var privremeno = await Context.GraphicsCards.Where(p => p.SerialNumber == kartica.SerialNumber).FirstOrDefaultAsync();

                if(privremeno == null) {
                    await Context.GraphicsCards.AddAsync(kartica);
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
        [Route("GetGraphicsCard/ID/{ID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetGraphicsCardID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {
                var kartica = await Context.GraphicsCards.Where(k => k.ID == ID).FirstOrDefaultAsync();

                if(kartica != null) {
                    return Ok(kartica);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Graphics card not found!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetGraphicsCard/SerialNumber/{SerialNumber}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetGraphicsCardSerialNumber(string SerialNumber) {

            if(string.IsNullOrWhiteSpace(SerialNumber) || SerialNumber.Length > 16) {
                return BadRequest("Invalid Serial Number!");
            }

            try {

                var kartica = await Context.GraphicsCards.Where(p => p.SerialNumber == SerialNumber).FirstOrDefaultAsync();

                if(kartica != null) {
                    return Ok(kartica);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Graphics card not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ UPDATE ------------------------------------ */
        [Route("UpdateGraphicsCard")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateGraphicsCard([FromBody] GraphicsCard kartica) {

            if(kartica.ID <= 0) { return BadRequest("Invalid ID!"); }

            if(string.IsNullOrWhiteSpace(kartica.SerialNumber) || kartica.SerialNumber.Length > 16) {
                return BadRequest("Invalid serial number!");
            }

            if(string.IsNullOrWhiteSpace(kartica.Manufacturer) || kartica.Manufacturer.Length > 16) {
                return BadRequest("Invalid manufacturer name!");
            }

            if(string.IsNullOrWhiteSpace(kartica.Model) || kartica.Model.Length > 32) {
                return BadRequest("Invalid model name!");
            }

            if(kartica.Price < 1) { return BadRequest("Invalid price!"); }

            if(kartica.MemoryGB != null && (kartica.MemoryGB < 1 || kartica.MemoryGB > 32)) {
                return BadRequest("Invlid memory amount!");
            }

            try {

                var karticaZaPromenu = await Context.GraphicsCards.FindAsync(kartica.ID);

                if(karticaZaPromenu != null) {
                    karticaZaPromenu.SerialNumber = kartica.SerialNumber;
                    karticaZaPromenu.Manufacturer = kartica.Manufacturer;
                    karticaZaPromenu.Model = kartica.Model;
                    karticaZaPromenu.Price = kartica.Price;
                    karticaZaPromenu.MemoryGB = kartica.MemoryGB;

                    await Context.SaveChangesAsync();
                    return Ok("Graphics card updated successfully!");

                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Graphics card not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }

        }

        /* ------------------------------------ DELETE ------------------------------------ */
        [Route("DeleteGraphicsCard/ID/{ID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteGraphicsCardID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {
                var karticaZaBrisanje = await Context.GraphicsCards.FindAsync(ID);
                if(karticaZaBrisanje != null) {

                    Context.GraphicsCards.Remove(karticaZaBrisanje);
                    await Context.SaveChangesAsync();
                    return Ok("Graphics card successfully deleted!");

                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Graphics card not found!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }


    }
}
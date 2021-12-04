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
    public class ProcessorController : ControllerBase {

        public ProdavnicaRacunaraContext Context { get; set; }
        public ProcessorController(ProdavnicaRacunaraContext context) { Context = context; }

        /* ------------------------------------ CREATE ------------------------------------ */
        [Route("AddProcessor")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddProcessor([FromBody] Processor procesor) {

            if(string.IsNullOrWhiteSpace(procesor.SerialNumber) || procesor.SerialNumber.Length > 16) {
                return BadRequest("Invalid serial number!");
            }

            if(string.IsNullOrWhiteSpace(procesor.Manufacturer) || procesor.Manufacturer.Length > 16) {
                return BadRequest("Invalid manufacturer name!");
            }

            if(string.IsNullOrWhiteSpace(procesor.Model) || procesor.Model.Length > 32) {
                return BadRequest("Invalid model name!");
            }

            if(procesor.Price < 1) { return BadRequest("Invalid price!"); }

            if(procesor.FrequencyGHz != null && procesor.FrequencyGHz < 0.1) { return BadRequest("Invalid frequency!"); }

            if(procesor.Cores != null && (procesor.Cores < 1 || procesor.Cores > 64)) {
                return BadRequest("Invalid core count!");
            }

            try {

                var privremeno = await Context.Processors.Where(p => p.SerialNumber == procesor.SerialNumber).FirstOrDefaultAsync();

                if(privremeno == null) {
                    await Context.Processors.AddAsync(procesor);
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
        [Route("GetProcessor/ID/{ID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetProcessorID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {

                //First() moze da baci exception, FirstOrDefault() daje null ako nije nadjen
                var procesor = await Context.Processors.Where(p => p.ID == ID).FirstOrDefaultAsync();

                if(procesor != null) { //nadjen je u bazi
                    return Ok(procesor);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Processor not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetProcessor/SerialNumber/{SerialNumber}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetProcessorSerialNumber(string SerialNumber) {

            if(string.IsNullOrWhiteSpace(SerialNumber) || SerialNumber.Length > 16) {
                return BadRequest("Invalid Serial Number!");
            }

            try {

                //First() moze da baci exception, FirstOrDefault() daje null ako nije nadjen
                var procesor = await Context.Processors.Where(p => p.SerialNumber == SerialNumber).FirstOrDefaultAsync();

                if(procesor != null) { //nadjen je u bazi
                    return Ok(procesor);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Processor not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ UPDATE ------------------------------------ */
        [Route("UpdateProcessor")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateProcessor([FromBody] Processor procesor) {

            if(procesor.ID <= 0) { return BadRequest("Invalid ID!"); }

            if(string.IsNullOrWhiteSpace(procesor.SerialNumber) || procesor.SerialNumber.Length > 16) {
                return BadRequest("Invalid serial number lenght!");
            }

            if(string.IsNullOrWhiteSpace(procesor.Manufacturer) || procesor.Manufacturer.Length > 16) {
                return BadRequest("Invalid manufacturer length!");
            }

            if(string.IsNullOrWhiteSpace(procesor.Model) || procesor.Model.Length > 32) {
                return BadRequest("Invalid model length!");
            }

            if(procesor.Price < 1) { return BadRequest("Invalid price!"); }

            if(procesor.FrequencyGHz != null && procesor.FrequencyGHz < 0.1) { return BadRequest("Invalid frequency!"); }

            if(procesor.Cores != null && (procesor.Cores < 1 || procesor.Cores > 64)) {
                return BadRequest("Invalid core count!");
            }

            try {

                var procesorZaPromenu = await Context.Processors.FindAsync(procesor.ID);

                if(procesorZaPromenu != null) { //nadjen
                    procesorZaPromenu.SerialNumber = procesor.SerialNumber;
                    procesorZaPromenu.Manufacturer = procesor.Manufacturer;
                    procesorZaPromenu.Model = procesor.Model;
                    procesorZaPromenu.Price = procesor.Price;
                    procesorZaPromenu.FrequencyGHz = procesor.FrequencyGHz;
                    procesorZaPromenu.Cores = procesor.Cores;

                    await Context.SaveChangesAsync();
                    return Ok("Processor updated successfully!");

                } else { //nije nadjen
                    return StatusCode(StatusCodes.Status404NotFound, "Processor not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }

        }

        /* ------------------------------------ DELETE ------------------------------------ */
        [Route("DeleteProcessor/ID/{ID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteProcessorID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {
                var procesorZaBrisanje = await Context.Processors.FindAsync(ID);
                if(procesorZaBrisanje != null) { //postoji

                    Context.Processors.Remove(procesorZaBrisanje);
                    await Context.SaveChangesAsync();
                    return Ok("Processor successfully deleted!");

                } else { //ne postoji
                    return StatusCode(StatusCodes.Status404NotFound, "Processor not found!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}
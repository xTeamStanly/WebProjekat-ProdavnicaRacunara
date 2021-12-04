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
    public class StorageController : ControllerBase {

        public ProdavnicaRacunaraContext Context { get; set; }
        public StorageController(ProdavnicaRacunaraContext context) { Context = context; }

        /* ------------------------------------ CREATE ------------------------------------ */
        [Route("AddStorage")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddStorage([FromBody] Storage disk) {

            if(string.IsNullOrWhiteSpace(disk.SerialNumber) || disk.SerialNumber.Length > 16) {
                return BadRequest("Invalid serial number!");
            }

            if(string.IsNullOrWhiteSpace(disk.Manufacturer) || disk.Manufacturer.Length > 16) {
                return BadRequest("Invalid manufacturer name!");
            }

            if(string.IsNullOrWhiteSpace(disk.Model) || disk.Model.Length > 16) {
                return BadRequest("Invalid model name!");
            }

            if(disk.Price < 1) { return BadRequest("Invalid price!"); }

            if(disk.MemoryGB < 0.1) { return BadRequest("Invalid memory amount!"); }

            try {
                var privremeno = await Context.Storages.Where(p => p.SerialNumber == disk.SerialNumber).FirstOrDefaultAsync();

                if(privremeno == null) {
                    await Context.Storages.AddAsync(disk);
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
        [Route("GetStorage/ID/{ID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetStorageID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {

                var disk = await Context.Storages.Where(p => p.ID == ID).FirstOrDefaultAsync();

                if(disk != null) {
                    return Ok(disk);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Storage not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetStorage/SerialNumber/{SerialNumber}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetStorageSerialNumber(string SerialNumber) {

            if(string.IsNullOrWhiteSpace(SerialNumber) || SerialNumber.Length > 16) {
                return BadRequest("Invalid Serial Number!");
            }

            try {

                var disk = await Context.Storages.Where(p => p.SerialNumber == SerialNumber).FirstOrDefaultAsync();

                if(disk != null) {
                    return Ok(disk);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Storage not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ UPDATE ------------------------------------ */
        [Route("UpdateStorage")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateStorage([FromBody] Storage disk) {

            if(string.IsNullOrWhiteSpace(disk.SerialNumber) || disk.SerialNumber.Length > 16) {
                return BadRequest("Invalid serial number!");
            }

            if(string.IsNullOrWhiteSpace(disk.Manufacturer) || disk.Manufacturer.Length > 16) {
                return BadRequest("Invalid manufacturer name!");
            }

            if(string.IsNullOrWhiteSpace(disk.Model) || disk.Model.Length > 16) {
                return BadRequest("Invalid model name!");
            }

            if(disk.Price < 1) { return BadRequest("Invalid price!"); }

            if(disk.MemoryGB < 0.1) { return BadRequest("Invalid memory amount!"); }

            try {

                var diskZaPromenu = await Context.Storages.FindAsync(disk.ID);

                if(diskZaPromenu != null) {
                    diskZaPromenu.SerialNumber = disk.SerialNumber;
                    diskZaPromenu.Manufacturer = disk.Manufacturer;
                    diskZaPromenu.Model = disk.Model;
                    diskZaPromenu.Price = disk.Price;
                    diskZaPromenu.MemoryGB = disk.MemoryGB;

                    await Context.SaveChangesAsync();
                    return Ok("Storage updated successfully!");

                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Storage not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }

        }

        /* ------------------------------------ DELETE ------------------------------------ */
        [Route("DeleteStorage/ID/{ID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteStorageID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {
                var diskZaBrisanje = await Context.Storages.FindAsync(ID);
                if(diskZaBrisanje != null) {

                    Context.Storages.Remove(diskZaBrisanje);
                    await Context.SaveChangesAsync();
                    return Ok("Storage successfully deleted!");

                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Storage not found!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}
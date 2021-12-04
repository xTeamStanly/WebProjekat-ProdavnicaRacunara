using System;
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
    public class ConfigurationController : ControllerBase {

        public ProdavnicaRacunaraContext Context { get; set; }
        public ConfigurationController(ProdavnicaRacunaraContext context) { Context = context; }

        /* ------------------------------------ CREATE ------------------------------------ */
        [Route("AddConfiguration/{Name}/CPU/{CPUID}/GPU/{GPUID}/RAM/{RAMID}/MB/{MBID}/STORAGE/{STORAGEID}")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddConfiguration(string Name, int CPUID, int GPUID, int RAMID, int MBID, int STORAGEID) {

            if(string.IsNullOrWhiteSpace(Name) || Name.Length > 64) { return BadRequest("Invalid name!"); }

            if(CPUID <= 0) { return BadRequest("Invalid CPU ID!"); }
            if(GPUID <= 0) { return BadRequest("Invalid GPU ID!"); }
            if(RAMID <= 0) { return BadRequest("Invalid RAM ID!"); }
            if(MBID <= 0) { return BadRequest("Invalid MB ID!"); }
            if(STORAGEID <= 0) { return BadRequest("Invalid STORAGE ID!"); }

            var procesor = await Context.Processors.FindAsync(CPUID);
            if(procesor == null) { return BadRequest("Processor not found!"); }

            var graficka = await Context.GraphicsCards.FindAsync(GPUID);
            if(graficka == null) { return BadRequest("Graphics card not found!"); }

            var ram = await Context.RAMs.FindAsync(RAMID);
            if(ram == null) { return BadRequest("RAM not found!"); }

            var maticna = await Context.Motherboards.FindAsync(MBID);
            if(maticna == null) { return BadRequest("Motherboard not found!"); }

            var skladiste = await Context.Storages.FindAsync(STORAGEID);
            if(skladiste == null) { return BadRequest("Storage not found!"); }

            try {

                var privremeno = await Context.Configurations
                    .Where(p => p.Name == Name)
                    .Where(p => p.CPU.ID == CPUID && p.GPU.ID == GPUID && p.RAM.ID == RAMID && p.MB.ID == MBID && p.STORAGE.ID == STORAGEID)
                    .FirstOrDefaultAsync();

                if(privremeno != null) { return BadRequest("Configuration already exists!"); }

                var konfiguracija = new Configuration{
                    Name = Name,
                    CPU = procesor,
                    GPU = graficka,
                    RAM = ram,
                    MB = maticna,
                    STORAGE = skladiste
                };

                await Context.Configurations.AddAsync(konfiguracija);
                await Context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, "Added to the database successfully!");

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ READ ------------------------------------ */
        [Route("GetConfiguration/ID/{ID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ConfigurationID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {

                var konfiguracija = await Context.Configurations.FindAsync(ID);

                if(konfiguracija != null) {
                    konfiguracija = await Context.Configurations
                                        .Where(p => p.ID == ID)
                                        .Include(p => p.CPU)
                                        .Include(p => p.GPU)
                                        .Include(p => p.RAM)
                                        .Include(p => p.MB)
                                        .Include(p => p.STORAGE)
                                        .FirstAsync();

                    return Ok(konfiguracija);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Configuration not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("GetConfiguration/Name/{Name}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetConfigurationName(string Name) {

            if(string.IsNullOrWhiteSpace(Name) || Name.Length > 64) {
                return BadRequest("Invalid configuration name!");
            }

            try {

                var konfiguracija = await Context.Configurations.Where(p => p.Name == Name).FirstOrDefaultAsync();

                if(konfiguracija != null) {
                    konfiguracija = await Context.Configurations
                                        .Where(p => p.Name == Name)
                                        .Include(p => p.CPU)
                                        .Include(p => p.GPU)
                                        .Include(p => p.RAM)
                                        .Include(p => p.MB)
                                        .Include(p => p.STORAGE)
                                        .FirstAsync();

                    return Ok(konfiguracija);
                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Configuration not found!");
                }

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ UPDATE ------------------------------------ */
        [Route("UpdateConfiguration/{Name}/CPU/{CPUID}/GPU/{GPUID}/RAM/{RAMID}/MB/{MBID}/STORAGE/{STORAGEID}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateConfiguration(string Name, int CPUID, int GPUID, int RAMID, int MBID, int STORAGEID) {

            if(string.IsNullOrWhiteSpace(Name) || Name.Length > 64) { return BadRequest("Invalid name!"); }
            if(CPUID <= 0) { return BadRequest("Invalid CPU ID!"); }
            if(GPUID <= 0) { return BadRequest("Invalid GPU ID!"); }
            if(RAMID <= 0) { return BadRequest("Invalid RAM ID!"); }
            if(MBID <= 0) { return BadRequest("Invalid MB ID!"); }
            if(STORAGEID <= 0) { return BadRequest("Invalid STORAGE ID!"); }

            try {

                var konfiguracijaZaPromenu = await Context.Configurations.Where(p => p.Name == Name).FirstOrDefaultAsync();
                if(konfiguracijaZaPromenu == null) { return StatusCode(StatusCodes.Status404NotFound, "Configuration not found!"); }

                var procesor = await Context.Processors.FindAsync(CPUID);
                if(procesor == null) { return BadRequest("Processor not found!"); }

                var graficka = await Context.GraphicsCards.FindAsync(GPUID);
                if(graficka == null) { return BadRequest("Graphics card not found!"); }

                var ram = await Context.RAMs.FindAsync(RAMID);
                if(ram == null) { return BadRequest("RAM not found!"); }

                var maticna = await Context.Motherboards.FindAsync(MBID);
                if(maticna == null) { return BadRequest("Motherboard not found!"); }

                var skladiste = await Context.Storages.FindAsync(STORAGEID);
                if(skladiste == null) { return BadRequest("Storage not found!"); }

                konfiguracijaZaPromenu.Name = Name;
                konfiguracijaZaPromenu.CPU = procesor;
                konfiguracijaZaPromenu.GPU = graficka;
                konfiguracijaZaPromenu.RAM = ram;
                konfiguracijaZaPromenu.MB = maticna;
                konfiguracijaZaPromenu.STORAGE = skladiste;

                await Context.SaveChangesAsync();
                return Ok("Configuration updated successfully!");

            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /* ------------------------------------ DELETE ------------------------------------ */
        [Route("DeleteConfiguration/ID/{ID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteConfigurationID(int ID) {

            if(ID <= 0) { return BadRequest("Invalid ID!"); }

            try {
                var konfiguracijaZaBrisanje = await Context.Configurations.FindAsync(ID);
                if(konfiguracijaZaBrisanje != null) {
                    Context.Configurations.Remove(konfiguracijaZaBrisanje);
                    await Context.SaveChangesAsync();
                    return Ok("Configuration successfully deleted!");

                } else {
                    return StatusCode(StatusCodes.Status404NotFound, "Configuration not found!");
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}
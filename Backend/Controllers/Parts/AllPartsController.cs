using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Parts;

namespace WebProjekat.Controller.Parts {

    [ApiController]
    [Route("[controller]")]
    public class AllPartsController : ControllerBase {

        public ProdavnicaRacunaraContext Context { get; set; }
        public AllPartsController(ProdavnicaRacunaraContext context) { Context = context; }

        /* ------------------------------------ READ ------------------------------------ */
        [Route("GetAllParts")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllParts() {

            List<Object> finalProcesori = new List<object>();
            var procesori = await Context.Processors.ToListAsync();
            procesori.ForEach(i => {
                finalProcesori.Add(new { id = i.ID, name = $"{i.Manufacturer} {i.Model}" });
            });

            List<Object> finalGraficke = new List<object>();
            var graficke = await Context.GraphicsCards.ToListAsync();
            graficke.ForEach(i => {
                finalGraficke.Add(new { id = i.ID, name = $"{i.Manufacturer} {i.Model}" });
            });

            List<Object> finalSkladiste = new List<object>();
            var skladiste = await Context.Storages.ToListAsync();
            skladiste.ForEach(i => {
                finalSkladiste.Add(new { id = i.ID, name = $"{i.Manufacturer} {i.Model}" });
            });

            List<Object> finalRam = new List<object>();
            var ram = await Context.GraphicsCards.ToListAsync();
            ram.ForEach(i => {
                finalRam.Add(new { id = i.ID, name = $"{i.Manufacturer} {i.Model}" });
            });

            List<Object> finalMaticne = new List<object>();
            var maticne = await Context.GraphicsCards.ToListAsync();
            maticne.ForEach(i => {
                finalMaticne.Add(new { id = i.ID, name = $"{i.Manufacturer} {i.Model}" });
            });

            return Ok(new {
                cpu = finalProcesori,
                gpu = finalGraficke,
                ram = finalRam,
                mb = finalMaticne,
                storage = finalSkladiste
            });
        }
    }
}
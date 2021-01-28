using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplyChain;
using SupplyChain.Server.DataAccess;
using SupplyChain.Shared.Models;

namespace SupplyChain.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CeldaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CeldaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Celdas>>> GetUnidades()
        {
            return await _context.Celda.ToListAsync();
        }

        // GET: api/Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Celdas>> GetUnidad(string id)
        {
            var celda = await _context.Celda.FindAsync(id);

            if (celda == null)
            {
                return NotFound();
            }

            return celda;
        }

        // PUT: api/Unidades/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnidad(string id, Celdas celda)
        {
            if (id != celda.CG_CELDA)
            {
                return BadRequest();
            }

            _context.Entry(celda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnidadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Unidades
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Celdas>> PostUnidad(Celdas celda)
        {
            _context.Celda.Add(celda);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UnidadExists(celda.CG_CELDA))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUnidad", new { id = celda.CG_CELDA }, celda);
        }

        // DELETE: api/Unidades/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Celdas>> DeleteUnidad(string id)
        {
            var celda = await _context.Celda.FindAsync(id);
            if (celda == null)
            {
                return NotFound();
            }

            _context.Celda.Remove(celda);
            await _context.SaveChangesAsync();

            return celda;
        }

        private bool UnidadExists(string id)
        {
            return _context.Celda.Any(e => e.CG_CELDA == id);
        }
    }
}

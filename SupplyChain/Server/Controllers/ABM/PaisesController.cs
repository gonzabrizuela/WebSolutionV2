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
    public class PaisController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaisController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pais>>> GetUnidades()
        {
            try
            {
                return await _context.Pais.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pais>> GetUnidad(int id)
        {
            var pais = await _context.Pais.FindAsync(id);

            if (pais == null)
            {
                return NotFound();
            }

            return pais;
        }

        // PUT: api/Unidades/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnidad(int id, Pais pais)
        {
            if (id != pais.Cg_Pais)
            {
                return BadRequest();
            }

            _context.Entry(pais).State = EntityState.Modified;

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
        public async Task<ActionResult<Pais>> PostUnidad(Pais pais)
        {
            _context.Pais.Add(pais);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UnidadExists(pais.Cg_Pais))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUnidad", new { id = pais.Cg_Pais }, pais);

        }

        // DELETE: api/Unidades/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pais>> DeleteUnidad(int id)
        {
            var pais = await _context.Pais.FindAsync(id);
            if (pais == null)
            {
                return NotFound();
            }

            _context.Pais.Remove(pais);
            await _context.SaveChangesAsync();

            return pais;
        }

        private bool UnidadExists(int id)
        {
            return _context.Pais.Any(e => e.Cg_Pais == id);
        }
    }
}

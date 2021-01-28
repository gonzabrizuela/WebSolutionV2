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
    public class ParadasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ParadasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Paradas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paradas>>> GetParadas()
        {
            return await _context.Paradas.ToListAsync();
        }

        // GET: api/Paradas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paradas>> GetParadas(int id)
        {
            var Paradas = await _context.Paradas.FindAsync(id);

            if (Paradas == null)
            {
                return NotFound();
            }

            return Paradas;
        }

        // PUT: api/Paradas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParadas(int id, Paradas Paradas)
        {
            if (id != Paradas.CP)
            {
                return BadRequest();
            }

            _context.Entry(Paradas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParadasExists(id))
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

        // POST: api/Paradas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Paradas>> PostParadas(Paradas Paradas)
        {
            _context.Paradas.Add(Paradas);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParadasExists(Paradas.CP))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetParadas", new { id = Paradas.CP }, Paradas);
        }

        // DELETE: api/Paradas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Paradas>> DeleteParadas(int id)
        {
            var Paradas = await _context.Paradas.FindAsync(id);
            if (Paradas == null)
            {
                return NotFound();
            }

            _context.Paradas.Remove(Paradas);
            await _context.SaveChangesAsync();

            return Paradas;
        }

        private bool ParadasExists(int id)
        {
            return _context.Paradas.Any(e => e.CP == id);
        }
    }
}
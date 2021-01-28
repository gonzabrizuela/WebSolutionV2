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
    public class ProtabController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProtabController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Protab
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Protab>>> GetProd()
        {
            return await _context.Protab.ToListAsync();
        }

        // GET: api/Prod/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Protab>> GetProd(string id)
        {
            var Protab = await _context.Protab.FindAsync(id);

            if (Protab == null)
            {
                return NotFound();
            }

            return Protab;
        }

        // PUT: api/Prod/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProd(string id, Protab Protab)
        {
            if (id != Protab.PROCESO)
            {
                return BadRequest();
            }

            _context.Entry(Protab).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdExists(id))
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

        // POST: api/Prod
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Protab>> PostProd(Protab Protab)
        {
            _context.Protab.Add(Protab);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProdExists(Protab.PROCESO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProd", new { id = Protab.PROCESO }, Protab);
        }

        // DELETE: api/Protab/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Protab>> DeleteProd(string id)
        {
            var Protab = await _context.Protab.FindAsync(id);
            if (Protab == null)
            {
                return NotFound();
            }

            _context.Protab.Remove(Protab);
            await _context.SaveChangesAsync();

            return Protab;
        }

        private bool ProdExists(string id)
        {
            return _context.Protab.Any(e => e.PROCESO == id);
        }
    }
}
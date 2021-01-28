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
    public class ProtareaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProtareaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Prod
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Protarea>>> GetProd()
        {
            return await _context.Protarea.ToListAsync();
        }

        // GET: api/Prod/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Protarea>> GetProd(string id)
        {
            var Protarea = await _context.Protarea.FindAsync(id);

            if (Protarea == null)
            {
                return NotFound();
            }

            return Protarea;
        }

        // PUT: api/Prod/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProd(string id, Protarea Protarea)
        {
            if (id != Protarea.TAREAPROC)
            {
                return BadRequest();
            }

            _context.Entry(Protarea).State = EntityState.Modified;

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
        public async Task<ActionResult<Protarea>> PostProd(Protarea Protarea)
        {
            _context.Protarea.Add(Protarea);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProdExists(Protarea.TAREAPROC))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProd", new { id = Protarea.TAREAPROC }, Protarea);
        }

        // DELETE: api/Prod/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Protarea>> DeleteProd(string id)
        {
            var Protarea = await _context.Protarea.FindAsync(id);
            if (Protarea == null)
            {
                return NotFound();
            }

            _context.Protarea.Remove(Protarea);
            await _context.SaveChangesAsync();

            return Protarea;
        }

        private bool ProdExists(string id)
        {
            return _context.Protarea.Any(e => e.TAREAPROC == id);
        }
    }
}
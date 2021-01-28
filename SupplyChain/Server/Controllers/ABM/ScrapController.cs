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
    public class ScrapController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ScrapController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Scrap
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scrap>>> GetScrap()
        {
            return await _context.Scrap.ToListAsync();
        }

        // GET: api/Scrap/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Scrap>> GetScrap(int id)
        {
            var Scrap = await _context.Scrap.FindAsync(id);

            if (Scrap == null)
            {
                return NotFound();
            }

            return Scrap;
        }

        // PUT: api/Scrap/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScrap(int id, Scrap Scrap)
        {
            if (id != Scrap.CG_SCRAP)
            {
                return BadRequest();
            }

            _context.Entry(Scrap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScrapExists(id))
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

        // POST: api/Scrap
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Scrap>> PostScrap(Scrap Scrap)
        {
            _context.Scrap.Add(Scrap);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ScrapExists(Scrap.CG_SCRAP))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetScrap", new { id = Scrap.CG_SCRAP }, Scrap);
        }

        // DELETE: api/Scrap/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Scrap>> DeleteScrap(int id)
        {
            var Scrap = await _context.Scrap.FindAsync(id);
            if (Scrap == null)
            {
                return NotFound();
            }

            _context.Scrap.Remove(Scrap);
            await _context.SaveChangesAsync();

            return Scrap;
        }

        private bool ScrapExists(int id)
        {
            return _context.Scrap.Any(e => e.CG_SCRAP == id);
        }
    }
}
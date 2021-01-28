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
    public class AreaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AreaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Area
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetUnidades()
        {
            return await _context.Area.ToListAsync();
        }

        // GET: api/Area/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetUnidad(int id)
        {
            var area = await _context.Area.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            return area;
        }

        // PUT: api/Unidades/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnidad(int id, Area area)
        {
            if (id != area.CG_AREA)
            {
                return BadRequest();
            }

            _context.Entry(area).State = EntityState.Modified;

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
        public async Task<ActionResult<Area>> PostUnidad(Area area)
        {
            _context.Area.Add(area);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UnidadExists(area.CG_AREA))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUnidad", new { id = area.CG_AREA }, area);
        }

        // DELETE: api/Unidades/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Area>> DeleteUnidad(int id)
        {
            var area = await _context.Area.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }

            _context.Area.Remove(area);
            await _context.SaveChangesAsync();

            return area;
        }

        private bool UnidadExists(int id)
        {
            return _context.Area.Any(e => e.CG_AREA == id);
        }
    }
}

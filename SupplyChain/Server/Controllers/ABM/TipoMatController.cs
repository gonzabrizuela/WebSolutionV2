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
    public class TipoMatController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TipoMatController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoMat
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoMat>>> GetTipoMat()
        {
            return await _context.TipoMat.ToListAsync();
        }

        // GET: api/TipoMat/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoMat>> GetTipoMat(string id)
        {
            var TipoMat = await _context.TipoMat.FindAsync(id);

            if (TipoMat == null)
            {
                return NotFound();
            }

            return TipoMat;
        }

        // PUT: api/TipoMat/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoMat(string id, TipoMat TipoMat)
        {
            if (id != TipoMat.TIPO)
            {
                return BadRequest();
            }

            _context.Entry(TipoMat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoMatExists(id))
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

        // POST: api/TipoMat
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TipoMat>> PostTipoMat(TipoMat TipoMat)
        {
            _context.TipoMat.Add(TipoMat);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TipoMatExists(TipoMat.TIPO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTipoMat", new { id = TipoMat.TIPO }, TipoMat);
        }

        // DELETE: api/TipoMat/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoMat>> DeleteTipoMat(int id)
        {
            var TipoMat = await _context.TipoMat.FindAsync(id);
            if (TipoMat == null)
            {
                return NotFound();
            }

            _context.TipoMat.Remove(TipoMat);
            await _context.SaveChangesAsync();

            return TipoMat;
        }

        private bool TipoMatExists(string id)
        {
            return _context.TipoMat.Any(e => e.TIPO == id);
        }
    }
}
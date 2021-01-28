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
    public class MonedasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MonedasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Monedas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Monedas>>> GetMonedas()
        {
            return await _context.Monedas.ToListAsync();
        }

        // GET: api/Monedas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Monedas>> GetMonedas(string id)
        {
            var Monedas = await _context.Monedas.FindAsync(id);

            if (Monedas == null)
            {
                return NotFound();
            }

            return Monedas;
        }

        // PUT: api/Monedas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonedas(string id, Monedas Monedas)
        {
            if (id != Monedas.MONEDA)
            {
                return BadRequest();
            }

            _context.Entry(Monedas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonedasExists(id))
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

        // POST: api/Monedas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Monedas>> PostMonedas(Monedas Monedas)
        {
            _context.Monedas.Add(Monedas);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MonedasExists(Monedas.MONEDA))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMonedas", new { id = Monedas.MONEDA }, Monedas);
        }

        // DELETE: api/Monedas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Monedas>> DeleteMonedas(string id)
        {
            var Monedas = await _context.Monedas.FindAsync(id);
            if (Monedas == null)
            {
                return NotFound();
            }

            _context.Monedas.Remove(Monedas);
            await _context.SaveChangesAsync();

            return Monedas;
        }

        private bool MonedasExists(string id)
        {
            return _context.Monedas.Any(e => e.MONEDA == id);
        }
    }
}
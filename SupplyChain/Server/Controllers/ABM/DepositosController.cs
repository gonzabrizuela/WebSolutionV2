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
    public class DepositoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepositoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Deposito
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deposito>>> GetUnidades()
        {
            return await _context.Deposito.ToListAsync();
        }

        // GET: api/Deposito/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Deposito>> GetUnidad(int id)
        {
            var Deposito = await _context.Deposito.FindAsync(id);

            if (Deposito == null)
            {
                return NotFound();
            }

            return Deposito;
        }

        // PUT: api/Unidades/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnidad(int id, Deposito Deposito)
        {
            if (id != Deposito.CG_DEP)
            {
                return BadRequest();
            }

            _context.Entry(Deposito).State = EntityState.Modified;

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
        public async Task<ActionResult<Deposito>> PostUnidad(Deposito Deposito)
        {
            _context.Deposito.Add(Deposito);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UnidadExists(Deposito.CG_DEP))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUnidad", new { id = Deposito.CG_DEP }, Deposito);
        }

        // DELETE: api/Unidades/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Deposito>> DeleteUnidad(int id)
        {
            var Deposito = await _context.Deposito.FindAsync(id);
            if (Deposito == null)
            {
                return NotFound();
            }

            _context.Deposito.Remove(Deposito);
            await _context.SaveChangesAsync();

            return Deposito;
        }

        private bool UnidadExists(int id)
        {
            return _context.Deposito.Any(e => e.CG_DEP == id);
        }
    }
}

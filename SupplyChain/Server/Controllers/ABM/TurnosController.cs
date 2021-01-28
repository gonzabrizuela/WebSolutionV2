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
    public class TurnosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TurnosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Turnos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turnos>>> GetTurnos()
        {
            return await _context.Turnos.ToListAsync();
        }

        // GET: api/Turnos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Turnos>> GetTurnos(int id)
        {
            var Turnos = await _context.Turnos.FindAsync(id);

            if (Turnos == null)
            {
                return NotFound();
            }

            return Turnos;
        }

        // PUT: api/Turnos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurnos(int id, Turnos Turnos)
        {
            if (id != Turnos.CG_TURNO)
            {
                return BadRequest();
            }

            _context.Entry(Turnos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurnosExists(id))
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

        // POST: api/Turnos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Turnos>> PostTurnos(Turnos Turnos)
        {
            _context.Turnos.Add(Turnos);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TurnosExists(Turnos.CG_TURNO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTurnos", new { id = Turnos.CG_TURNO }, Turnos);
        }

        // DELETE: api/Turnos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Turnos>> DeleteTurnos(int id)
        {
            var Turnos = await _context.Turnos.FindAsync(id);
            if (Turnos == null)
            {
                return NotFound();
            }

            _context.Turnos.Remove(Turnos);
            await _context.SaveChangesAsync();

            return Turnos;
        }

        private bool TurnosExists(int id)
        {
            return _context.Turnos.Any(e => e.CG_TURNO == id);
        }
    }
}
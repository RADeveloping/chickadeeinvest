using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using chickadee.Data;
using chickadee.Models;

namespace chickadee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UnitController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Unit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnit()
        {
          if (_context.Unit == null)
          {
              return NotFound();
          }
            return await _context.Unit.ToListAsync();
        }

        // GET: api/Unit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(string id)
        {
          if (_context.Unit == null)
          {
              return NotFound();
          }
            var unit = await _context.Unit.FindAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return unit;
        }

        // PUT: api/Unit/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnit(string id, Unit unit)
        {
            if (id != unit.UnitId)
            {
                return BadRequest();
            }

            _context.Entry(unit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitExists(id))
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

        // POST: api/Unit
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(Unit unit)
        {
          if (_context.Unit == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Unit'  is null.");
          }
            _context.Unit.Add(unit);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UnitExists(unit.UnitId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUnit", new { id = unit.UnitId }, unit);
        }

        // DELETE: api/Unit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(string id)
        {
            if (_context.Unit == null)
            {
                return NotFound();
            }
            var unit = await _context.Unit.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            _context.Unit.Remove(unit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnitExists(string id)
        {
            return (_context.Unit?.Any(e => e.UnitId == id)).GetValueOrDefault();
        }
    }
}

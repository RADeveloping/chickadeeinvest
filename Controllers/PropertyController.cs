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
    public class PropertyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PropertyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Property
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Property>>> GetProperty()
        {
          if (_context.Property == null)
          {
              return NotFound();
          }
            return await _context.Property.ToListAsync();
        }

        // GET: api/Property/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetProperty(string id)
        {
          if (_context.Property == null)
          {
              return NotFound();
          }
            var @property = await _context.Property.FindAsync(id);

            if (@property == null)
            {
                return NotFound();
            }

            return @property;
        }

        // PUT: api/Property/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProperty(string id, Property @property)
        {
            if (id != @property.PropertyId)
            {
                return BadRequest();
            }

            _context.Entry(@property).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
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

        // POST: api/Property
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Property>> PostProperty(Property @property)
        {
          if (_context.Property == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Property'  is null.");
          }
            _context.Property.Add(@property);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PropertyExists(@property.PropertyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProperty", new { id = @property.PropertyId }, @property);
        }

        // DELETE: api/Property/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(string id)
        {
            if (_context.Property == null)
            {
                return NotFound();
            }
            var @property = await _context.Property.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }

            _context.Property.Remove(@property);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PropertyExists(string id)
        {
            return (_context.Property?.Any(e => e.PropertyId == id)).GetValueOrDefault();
        }
    }
}

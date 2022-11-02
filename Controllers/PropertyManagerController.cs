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
    public class PropertyManagerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PropertyManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PropertyManager
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyManager>>> GetPropertyManagers()
        {
          if (_context.PropertyManagers == null)
          {
              return NotFound();
          }
            return await _context.PropertyManagers.ToListAsync();
        }

        // GET: api/PropertyManager/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyManager>> GetPropertyManager(string id)
        {
          if (_context.PropertyManagers == null)
          {
              return NotFound();
          }
            var propertyManager = await _context.PropertyManagers.FindAsync(id);

            if (propertyManager == null)
            {
                return NotFound();
            }

            return propertyManager;
        }

        // PUT: api/PropertyManager/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPropertyManager(string id, PropertyManager propertyManager)
        {
            if (id != propertyManager.Id)
            {
                return BadRequest();
            }

            _context.Entry(propertyManager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyManagerExists(id))
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

        // POST: api/PropertyManager
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PropertyManager>> PostPropertyManager(PropertyManager propertyManager)
        {
          if (_context.PropertyManagers == null)
          {
              return Problem("Entity set 'ApplicationDbContext.PropertyManagers'  is null.");
          }
            _context.PropertyManagers.Add(propertyManager);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PropertyManagerExists(propertyManager.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPropertyManager", new { id = propertyManager.Id }, propertyManager);
        }

        // DELETE: api/PropertyManager/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyManager(string id)
        {
            if (_context.PropertyManagers == null)
            {
                return NotFound();
            }
            var propertyManager = await _context.PropertyManagers.FindAsync(id);
            if (propertyManager == null)
            {
                return NotFound();
            }

            _context.PropertyManagers.Remove(propertyManager);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PropertyManagerExists(string id)
        {
            return (_context.PropertyManagers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

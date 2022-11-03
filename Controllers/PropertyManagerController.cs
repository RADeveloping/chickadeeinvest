using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using chickadee.Data;
using chickadee.Models;
using Microsoft.AspNetCore.Identity;

namespace chickadee.Controllers
{
    [Route("api/properties/{propertyId}/units/{propertyId}")]
    [ApiController]
    public class PropertyManagerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PropertyManagerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: api/PropertyManager
        [HttpGet("PropertyManager")]
        public async Task<ActionResult<IEnumerable<PropertyManager>>> GetPropertyManager(string propertyId, string unitId)
        {
            var requestingUser = await _userManager.GetUserAsync(User);

            if (_context.Unit == null || requestingUser == null || _context.Property == null || _context.PropertyManagers == null)
            {
                return NotFound();
            }

            var propertyManager = _context.PropertyManagers
                // .Include(p => p.PropertyManager)
                // .Where(u => u.UnitId == unitId)
                // .Where(u => u.Property.PropertyId == propertyId)
                //
                // .Select(p => new PropertyManager()
                // {
                //     
                //     FirstName = p.PropertyManager.FirstName,
                //     LastName = tenant.LastName,
                //     Id = tenant.Id,
                //     UserName = tenant.UserName,
                //     ProfilePicture = tenant.ProfilePicture,
                //     LeaseNumber = tenant.LeaseNumber,
                //     Unit = tenant.Unit,
                // })
                .ToList();
          
            
            var propertySa = _context.Unit
                .Include(x => x.Tenants)
                .Where(p=>p.UnitId == unitId && p.PropertyId == propertyId)
                .ToList();


            return Ok(User.IsInRole("SuperAdmin") ? propertySa : propertyManager);
        }

        // // GET: api/PropertyManager/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<PropertyManager>> GetPropertyManager(string id)
        // {
        //   if (_context.PropertyManagers == null)
        //   {
        //       return NotFound();
        //   }
        //     var propertyManager = await _context.PropertyManagers.FindAsync(id);
        //
        //     if (propertyManager == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return propertyManager;
        // }

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

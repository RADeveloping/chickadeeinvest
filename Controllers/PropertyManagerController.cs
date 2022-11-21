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
using Microsoft.AspNetCore.Authorization;

namespace chickadee.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyManager>>> GetPropertyManagers()
        {
            var requestingUser = await _userManager.GetUserAsync(User);

            if (_context.Unit == null || requestingUser == null || _context.Property == null || _context.PropertyManagers == null)
            {
                return NotFound();
            }

            var property = _context.PropertyManagers
                .Select(p => new
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Id = p.Id,
                    Email = p.Email,
                    ProfilePicture = p.ProfilePicture,
                    Company = p.Company
                })
                .ToList();

            return Ok(property);
        }

        // GET: api/PropertyManager/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyManager>> GetPropertyManager(string id)
        {
            
            var requestingUser = await _userManager.GetUserAsync(User);

            if (_context.Unit == null || requestingUser == null || _context.Property == null || _context.PropertyManagers == null)
            {
                return NotFound();
            }

            var propertyManager = _context.PropertyManagers
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Id = p.Id,
                    Email = p.Email,
                    ProfilePicture = p.ProfilePicture,
                    Company = p.Company
                }).FirstOrDefault();

            if (propertyManager == null)
            {
                return NotFound();
            }
        
            return Ok(propertyManager);
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
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<PropertyManager>> PostPropertyManager(PropertyManager propertyManager)
        {
          if (_context.PropertyManagers == null)
          {
              return Problem("Entity set 'ApplicationDbContext.PropertyManagers'  is null.");
          }

          if (_context.Company == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Companies' is null.");
          }

          if (_context.Unit == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Unit' is null.");
          }

          if (propertyManager.CompanyId != null && _context.Company.FindAsync(propertyManager.CompanyId) != null)
          {
              propertyManager.Company = _context.Company.FindAsync(propertyManager.CompanyId).Result;
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

            return Ok(new {
                PropertyManagerId = propertyManager.Id,
                Email = propertyManager.Email,
                FirstName = propertyManager.FirstName,
                LastName = propertyManager.LastName,
                PhoneNumber = propertyManager.PhoneNumber
            });
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

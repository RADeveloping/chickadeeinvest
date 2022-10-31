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
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PropertiesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        
       
        // GET: api/Properties/count
        [HttpGet]
        [Route("count")]
        public async Task<ActionResult> GetPropertiesCount()
        {
            if (_context.Properties == null)
            {
                return NotFound();
            }

            var user = _userManager.GetUserAsync(User).Result;

            var isTenant = await _userManager.IsInRoleAsync(user, "Tenant");

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            var isPropertyManager = await _userManager.IsInRoleAsync(user, "PropertyManager");

            var isSuperAdmin = await _userManager.IsInRoleAsync(user, "SuperAdmin");
          
            Console.WriteLine( await _context.Properties.Where(p => p.PropertyManagerId == user.Id).CountAsync());

            
            if ((isTenant || isAdmin) && !isSuperAdmin)
            {
                return NoContent();
            }

            
            if (isPropertyManager && !isSuperAdmin)
            {
                var pmPropertiesCount = await _context.Properties.Where(p => p.PropertyManagerId == user.Id).CountAsync();
                return Ok(
                new {
                    count = pmPropertiesCount
                });
            }
            
            var allPropertiesCount = await _context.Properties.CountAsync();
            return Ok(
            new {
                count = allPropertiesCount
            });
        }
        
        
        // GET: api/Properties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Property>>> GetProperties()
        {
          if (_context.Properties == null)
          {
              return NotFound();
          }

          var user = _userManager.GetUserAsync(User).Result;

          var isTenant = await _userManager.IsInRoleAsync(user, "Tenant");

          var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

          var isPropertyManager = await _userManager.IsInRoleAsync(user, "PropertyManager");

          var isSuperAdmin = await _userManager.IsInRoleAsync(user, "SuperAdmin");
          
          if ((isTenant || isAdmin) && !isSuperAdmin)
          {
            return NoContent();
          }

          if (isPropertyManager && !isSuperAdmin)
          {
            return await _context.Properties
              .Include(p => p.PropertyManager)
              .Where(d => d.PropertyManager == user)
              .Include(u => u.Units)
              .ToListAsync();
          }
            return await _context.Properties
              .Include(u => u.Units)
                .ThenInclude(t => t.Tickets)
              .Include(p => p.PropertyManager)
              .ToListAsync();
        }

        
         // GET: api/Properties/current
        [HttpGet]
        [Route("current")]
        public async Task<ActionResult> GetSpecificPropertiesForUser()
        {
          if (_context.Properties == null)
          {
              return NotFound();
          }
            var user = _userManager.GetUserAsync(User).Result;

            return Ok(
              _context.Properties
                .Include(j => j.PropertyManager)
                .Where(d => d.PropertyManager == user)
                .Select(property => new {
                  propertyId = property.PropertyId,
                  address = property.Address,
                  propertyManagerId = property.PropertyManagerId,
                  propertyManagerFirstName = property.PropertyManager.FirstName,
                  propertyManagerLastName = property.PropertyManager.LastName,
                  propertyManagerPhoneNumber = property.PropertyManager.PhoneNumber,
                  units = property.Units
                    .Select(unit => new {
                        unitId = unit.UnitId,
                        unitNo = unit.UnitNo,
                        tenants = unit.Tenants
                            .Select(tenant => new {
                                FirstName = tenant.FirstName,
                                LastName = tenant.LastName,
                                Id = tenant.Id,
                                Email = tenant.Email
                            }),
                        tickets = unit.Tickets
                    })
                }));
        }

        // GET: api/Properties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetProperty(int id)
        {
          var user = _userManager.GetUserAsync(User).Result;
          
          var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

          var isPropertyManager = await _userManager.IsInRoleAsync(user, "PropertyManager");

          var isTenant = await _userManager.IsInRoleAsync(user, "Tenant");

          var isSuperAdmin = await _userManager.IsInRoleAsync(user, "SuperAdmin");

          if (_context.Properties == null || user == null)
          {
              return NotFound();
          }

          if (isSuperAdmin)
          {
            var superAdminProperty = await _context.Properties
                .Where(p => p.PropertyId == id)
                .Select(property => new
                {
                    PropertyId = property.PropertyId,
                    Address = property.Address,
                    PropertyManagerId = property.PropertyManagerId,
                    PropertyManager = new 
                    {
                        FirstName = property.PropertyManager.FirstName,
                        LastName = property.PropertyManager.LastName,
                        Id = property.PropertyManager.Id,
                        UserName = property.PropertyManager.UserName,
                        ProfilePicture = property.PropertyManager.ProfilePicture
                    }
                }).FirstOrDefaultAsync(i => i.PropertyId == id);

            if (superAdminProperty == null)
            {
                return NotFound();
            }
            return Ok(superAdminProperty);
          }

          if(isPropertyManager)
          {
            var propManProperties = await _context.Properties
              .Where(p => p.PropertyManager == user)
              .Select(property => new
                {
                    PropertyId = property.PropertyId,
                    Address = property.Address,
                    PropertyManagerId = property.PropertyManagerId,
                    PropertyManager = new 
                    {
                        FirstName = property.PropertyManager.FirstName,
                        LastName = property.PropertyManager.LastName,
                        Id = property.PropertyManager.Id,
                        UserName = property.PropertyManager.UserName,
                        ProfilePicture = property.PropertyManager.ProfilePicture
                    }
                }).FirstOrDefaultAsync(i => i.PropertyId == id);
            if (propManProperties == null)
            {
                return NotFound();
            }

            return Ok(propManProperties);
          }

          if (isTenant)
          {
            var tenantProperty = await _context.Properties
                .SelectMany(p => p.Units.Where(unit => unit.Tenants.Contains(user)))
                .Select(unit => new {
                    PropertyId = unit.Property.PropertyId,
                    Address = unit.Property.Address,
                    PropertyManagerId = unit.Property.PropertyManagerId,
                    PropertyManager = new 
                    {
                        FirstName = unit.Property.PropertyManager.FirstName,
                        LastName = unit.Property.PropertyManager.LastName,
                        Id = unit.Property.PropertyManager.Id,
                        UserName = unit.Property.PropertyManager.UserName,
                        ProfilePicture = unit.Property.PropertyManager.ProfilePicture
                    }
                }).FirstOrDefaultAsync(i => i.PropertyId == id);
            if (tenantProperty == null)
            {
                return NotFound();
            }
            return Ok(tenantProperty);
          }
          return NotFound();
        }
        

        // PUT: api/Properties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProperty(int id, Property @property)
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

        // POST: api/Properties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Property>> PostProperty(Property @property)
        {
          if (_context.Properties == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Properties'  is null.");
          }
            _context.Properties.Add(@property);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProperty", new { id = @property.PropertyId }, @property);
        }

        // DELETE: api/Properties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            if (_context.Properties == null)
            {
                return NotFound();
            }
            var @property = await _context.Properties.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }

            _context.Properties.Remove(@property);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PropertyExists(int id)
        {
            return (_context.Properties?.Any(e => e.PropertyId == id)).GetValueOrDefault();
        }
    }
}

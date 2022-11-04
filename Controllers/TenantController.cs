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
    using System.Linq.Expressions;
    using Microsoft.AspNetCore.Identity;

    [Route("api/properties/{propertyId}/units/{unitId}")]
    // [Route("api/[controller]")]

    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TenantController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        
        // GET: api/properties/{propertyId}/units/{unitId}/tenants
        [HttpGet]
        [Route("tenants")]
        public async Task<ActionResult<IEnumerable<Tenant>>> GetTenants(string propertyId, string unitId)
        {
            var requestingUser = await _userManager.GetUserAsync(User);

            if (_context.Unit == null || requestingUser == null || _context.Tenant == null)
            {
                return NotFound();
            }

            var property = _context.Unit
                .Include(x => x.Tenants)
                .Where(p => p.Tenants != null && p.Tenants.Any(u =>
                    (u.Id == requestingUser.Id) ||
                    p.PropertyManagerId == requestingUser.UnitId
                ))
                .Where(p => p.UnitId == unitId && p.PropertyId == propertyId)
                .Select(p => new
                {
                    Tenants = p.Tenants.Select(t => new Tenant()
                    {
                        FirstName = t.FirstName,
                        LastName = t.LastName,
                        ProfilePicture = t.ProfilePicture,
                        Id = t.Id,
                        Email = t.Email,
                        PhoneNumber = t.PhoneNumber
                    })
                }).FirstOrDefault();



            var propertySa = _context.Unit
                .Include(x => x.Tenants)
                .Where(p => p.Tenants != null && p.UnitId == unitId && p.PropertyId == propertyId)
                .Select(p => new
                {
                    Tenants = p.Tenants == null ? null : p.Tenants.Select(t => new
                    {
                        FirstName = t.FirstName,
                        LastName = t.LastName,
                        ProfilePicture = t.ProfilePicture,
                        Id = t.Id,
                        Email = t.Email,
                        PhoneNumber = t.PhoneNumber
                    })
                }).FirstOrDefault()
                ?.Tenants;
            return Ok(User.IsInRole("SuperAdmin") ? propertySa : property);

        }

        // GET: api/properties/{propertyId}/units/{unitId}/tenants
        [HttpGet]
        [Route("tenants/{tenantId}")]
        public async Task<ActionResult<Tenant>> GetTenant(string unitId, string tenantId,string propertyId)
        {
            var requestingUser = await _userManager.GetUserAsync(User);

            if (_context.Unit == null || requestingUser == null || _context.Tenant == null)
            {
                return NotFound();
            }

            var property = _context.Unit
                .Include(x => x.Tenants)
                .Where(p => p.Tenants != null && p.Tenants.Any(u =>
                    (u.Id == requestingUser.Id) ||
                    p.PropertyManagerId == requestingUser.UnitId
                ))
                .Where(p => p.UnitId == unitId && p.PropertyId == propertyId)
                .Where(p => p.Tenants.Any(p => p.Id == tenantId))
                .Select(p => new
                {
                    Tenants = p.Tenants.Select(t => new
                        {
                            FirstName = t.FirstName,
                            LastName = t.LastName,
                            ProfilePicture = t.ProfilePicture,
                            Id = t.Id,
                            Email = t.Email,
                            PhoneNumber = t.PhoneNumber
                        })
                        .FirstOrDefault()
                })
                .FirstOrDefault().Tenants;



            var propertySa = _context.Unit
                .Include(x => x.Tenants)
                .Where(p => p.Tenants != null && p.UnitId == unitId && p.PropertyId == propertyId
                            && p.Tenants.Any(t => t.Id == tenantId))
                .Select(u => new
                {
                    Tenants = u.Tenants.Select(t => new
                        {
                            FirstName = t.FirstName,
                            LastName = t.LastName,
                            ProfilePicture = t.ProfilePicture,
                            Id = t.Id,
                            Email = t.Email,
                            PhoneNumber = t.PhoneNumber
                        })
                        .FirstOrDefault()
                }).FirstOrDefault().Tenants;



            return Ok(User.IsInRole("SuperAdmin") ? propertySa : property);
        }

        // PUT: api/Tenants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTenant(string id, Tenant tenant)
        {
            if (id != tenant.Id)
            {
                return BadRequest();
            }

            _context.Entry(tenant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TenantExists(id))
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

        // POST: api/Tenants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tenant>> PostTenant(Tenant tenant)
        {
          if (_context.Tenant == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Tenant'  is null.");
          }
            _context.Tenant.Add(tenant);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TenantExists(tenant.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTenant", new { id = tenant.Id }, tenant);
        }

        // DELETE: api/Tenants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTenant(string id)
        {
            if (_context.Tenant == null)
            {
                return NotFound();
            }
            var tenant = await _context.Tenant.FindAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }

            _context.Tenant.Remove(tenant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TenantExists(string id)
        {
            return (_context.Tenant?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

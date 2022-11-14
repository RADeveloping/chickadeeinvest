using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using chickadee.Data;
using chickadee.Enums;
using chickadee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace chickadee.Controllers
{
    [Route("api/properties")]
    [ApiController]
    [Authorize]
    public class PropertyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PropertyController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: api/Properties
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Property>>> GetProperties(string? sort, string? param, string? query)
        {
            var requestingUser = await _userManager.GetUserAsync(User);

            if (_context.Property == null)
            {
                return NotFound();
            }

            var anonymous = _context.Property
                .Select(x => new
                {
                    PropertyId = x.PropertyId,
                    Name = x.Name,
                })
                .ToList();
            if (requestingUser == null)
            {
                return Ok(anonymous);
            }
                
            
            
            var properties = _context.Property
                .Include(x => x.Units)
                .Where(p => p.Units != null && p.Units.Any(u =>
                        (u.PropertyManagerId == requestingUser.Id)
                    || u.UnitId == requestingUser.UnitId
                ))
                .Select(x => new
                {
                    PropertyId = x.PropertyId,
                    Name = x.Name,
                    Address = x.Address,
                    TenantsCount = x.Units == null ? 0 : x.Units.Select(u => u.Tenants).Count(),
                    UnitsCount = x.Units == null ? 0 : x.Units.Count,
                    OutstandingTickets = x.Units == null
                        ? 0
                        : x.Units.Select(u => u.Tickets != null && u.Tickets.Any(t => t.Status == TicketStatus.Open))
                            .Count(),
                })
                .ToList();

            var propertiesSa = _context.Property
                .Include(x => x.Units)
                .Select(x => new
                {
                    PropertyId = x.PropertyId,
                    Name = x.Name,
                    Address = x.Address,
                    TenantsCount = x.Units == null ? 0 : x.Units.Select(u => u.Tenants).Count(),
                    UnitsCount = x.Units == null ? 0 : x.Units.Count,
                    OutstandingTickets = x.Units == null
                        ? 0
                        : x.Units.Select(u => u.Tickets != null && u.Tickets.Any(t => t.Status == TicketStatus.Open)).Count(),
                })
                .ToList();


            switch (sort)
            {
                case "asc" when param == "address":
                    propertiesSa = propertiesSa.OrderBy(s => s.Address).ToList();
                    properties = properties.OrderBy(s => s.Address).ToList();
                    break;
                case "asc" when param == "id":
                    propertiesSa = propertiesSa.OrderBy(s => s.PropertyId).ToList();
                    properties = properties.OrderBy(s => s.PropertyId).ToList();
                    break;
                case "asc" when param == "open_count":
                    propertiesSa = propertiesSa.OrderBy(s => s.OutstandingTickets).ToList();
                    properties = properties.OrderBy(s => s.OutstandingTickets).ToList();
                    break;
                case "asc" when param == "unit_count":
                    propertiesSa = propertiesSa.OrderBy(s => s.UnitsCount).ToList();
                    properties = properties.OrderBy(s => s.UnitsCount).ToList();
                    break;
                case "asc" when param == "tenants_count":
                    propertiesSa = propertiesSa.OrderBy(s => s.TenantsCount).ToList();
                    properties = properties.OrderBy(s => s.TenantsCount).ToList();
                    break;
                case "asc" when param == "name":
                    propertiesSa = propertiesSa.OrderBy(s => s.Name).ToList();
                    properties = properties.OrderBy(s => s.Name).ToList();
                    break;
                case "asc":
                    propertiesSa = propertiesSa.OrderBy(s => s.Name).ToList();
                    properties = properties.OrderBy(s => s.Name).ToList();
                    break;
                case "desc" when param == "address":
                    propertiesSa = propertiesSa.OrderByDescending(s => s.Address).ToList();
                    properties = properties.OrderByDescending(s => s.Address).ToList();
                    break;
                case "desc" when param == "id":
                    propertiesSa = propertiesSa.OrderByDescending(s => s.PropertyId).ToList();
                    properties = properties.OrderByDescending(s => s.PropertyId).ToList();
                    break;
                case "desc" when param == "open_count":
                    propertiesSa = propertiesSa.OrderByDescending(s => s.OutstandingTickets).ToList();
                    properties = properties.OrderByDescending(s => s.OutstandingTickets).ToList();
                    break;
                case "desc" when param == "unit_count":
                    propertiesSa = propertiesSa.OrderByDescending(s => s.UnitsCount).ToList();
                    properties = properties.OrderByDescending(s => s.UnitsCount).ToList();
                    break;
                case "desc" when param == "tenants_count":
                    propertiesSa = propertiesSa.OrderByDescending(s => s.TenantsCount).ToList();
                    properties = properties.OrderByDescending(s => s.TenantsCount).ToList();
                    break;
                case "desc" when param == "name":
                    propertiesSa = propertiesSa.OrderByDescending(s => s.Name).ToList();
                    properties = properties.OrderByDescending(s => s.Name).ToList();
                    break;
                case "desc":
                    propertiesSa = propertiesSa.OrderByDescending(s => s.Name).ToList();
                    properties = properties.OrderByDescending(s => s.Name).ToList();
                    break;
            }

            if (query != null)
            {
                return Ok(User.IsInRole("SuperAdmin")
                    ? propertiesSa.Where(s => 
                        s.Address.Contains(query, StringComparison.InvariantCultureIgnoreCase) ||
                        s.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                    : properties.Where(s => 
                        s.Address.Contains(query, StringComparison.InvariantCultureIgnoreCase) ||
                        s.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase)));
            }

            return Ok(User.IsInRole("SuperAdmin") ? propertiesSa : properties);
        }


        // GET: api/Property/{unit
        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetProperty(string id)
        {
            var requestingUser = await _userManager.GetUserAsync(User);

            if (_context.Property == null || requestingUser == null)
            {
                return NotFound();
            }

            var property = _context.Property
                .Include(x => x.Units)
                .Where(p => p.Units != null && p.Units.Any(u =>
                    (u.PropertyManagerId == requestingUser.Id) ||
                    u.UnitId == requestingUser.UnitId
                ))
                .Where(p => p.PropertyId == id)
                .Select(x => new
                {
                    PropertyId = x.PropertyId,
                    Name = x.Name,
                    Address = x.Address,
                })
                .ToList();

            var propertySa = _context.Property
                .Include(x => x.Units)
                .Where(p => p.PropertyId == id)
                .Select(x => new
                {
                    PropertyId = x.PropertyId,
                    Name = x.Name,
                    Address = x.Address,
                })
                .ToList();

            return Ok(User.IsInRole("SuperAdmin") ? propertySa : property);
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
        [HttpPost("/api/properties")]
        [Authorize(Roles = "PropertyManager")]
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

            return Ok(@property);
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
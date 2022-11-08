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
using System.Net;

namespace chickadee.Controllers
{
    [Route("api/properties/{propertyId}/units")]
    [ApiController]
    
    public class UnitsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UnitsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/properties/{propertyId}/units
        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Admin, PropertyManager, Tenant")]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits(string propertyId)
        {
            var requestingUser = await _userManager.GetUserAsync(User);
            if (_context.Unit == null || requestingUser == null || _context.Property == null)
          {
              return NotFound();
          }
          
            var property = await _context.Property.FindAsync(propertyId);
            if (property == null)
            {
                return NotFound();
            }
            
            var unitsSimple = _context.Unit
                .Where(u => u.PropertyId == propertyId)
                .Select(unit => new
                {
                    unitId = unit.UnitId,
                    unitNo = unit.UnitNo,
                    unitType = unit.UnitType,
                }).ToList();


            var units = _context.Unit
                .Where(u => u.PropertyId == propertyId)
                .Select(unit => new
                {
                    unitId = unit.UnitId,
                    unitNo = unit.UnitNo,
                    unitType = unit.UnitType,
                    propertyId = unit.PropertyId,
                    propertyManagerId = unit.PropertyManagerId,
                }).ToList();
               

            if (User.IsInRole("SuperAdmin") || units.Any(p => p.propertyManagerId == requestingUser.Id || p.unitId == requestingUser.UnitId))
            {
                return Ok(units);
            }

            return Ok(unitsSimple);
        }

        
        // GET: api/properties/{propertyId}/units/{unitId
        [HttpGet]
        [Route("{unitId}")]
        public async Task<ActionResult<Unit>> GetUnit(string? unitId, string? propertyId)
        {
            var requestingUser = await _userManager.GetUserAsync(User);

            if (propertyId == null || unitId == null || _context.Property == null || _context.Unit == null || requestingUser == null)
            {
                return NotFound();
            }

            var unit = _context.Unit
                .Where(u => u.UnitId == unitId)
                .Where(u => u.Property.PropertyId == propertyId)
                .Select(u => new Unit()
                {
                    UnitId = u.UnitId,
                    UnitNo = u.UnitNo,
                    UnitType = u.UnitType,
                    PropertyId = u.PropertyId,
                    PropertyManagerId = u.PropertyManagerId,
                
                })
                .FirstOrDefault();

            if (unit == null)
            {
                return NotFound();
            }
            
            if (User.IsInRole("SuperAdmin") || unit.PropertyManagerId == requestingUser.Id || unit.UnitId == requestingUser.UnitId)
            {
                return Ok(unit);
            }
            
            return Ok(unit);
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

        // POST: api/properties/{propertyId}/units
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<Unit>> PostUnit(string? propertyId, Unit unit)
        {
          if (_context.Unit == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Unit'  is null.");
          }
          if (_context.Property == null)
          {
             return Problem("Entity set 'ApplicationDbContext.Property'  is null.");
          }
          if (_context.PropertyManagers == null)
          {
            return Problem("Entity set 'ApplicationDbContext.PropertyManagers' is null.");
          }
          var property = await _context.Property.FindAsync(propertyId);
          
          var propertyManager = await _context.PropertyManagers.FindAsync(unit.PropertyManagerId);

          if (property == null)
          {
              HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
              return BadRequest("Property does not exist");
          }

          if (propertyManager == null)
          {
            unit.PropertyManagerId = null;
          }

          unit.Property = property;

          unit.PropertyManager = propertyManager != null ? propertyManager : null;

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

            return Ok(unit);
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

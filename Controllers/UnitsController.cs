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

        // GET: api/units
        [HttpGet]
        [Route("api/units")]
        public async Task<ActionResult<IEnumerable<Unit>>> GetAllUnits(string? sort, string? param, string? query)
        {
            var requestingUser = await _userManager.GetUserAsync(User);
            if (_context.Unit == null || requestingUser == null || _context.Property == null)
            {
                return NotFound();
            }

            var units = _context.Unit
                .Where(u => u.PropertyManager != null &&
                            (requestingUser.UnitId == u.UnitId || u.PropertyManager.Id == requestingUser.Id))
                .Select(unit => new
                {
                    unitId = unit.UnitId,
                    unitNo = unit.UnitNo,
                    unitType = unit.UnitType,
                    propertyId = unit.PropertyId,
                    propertyName = unit.Property.Name,
                    propertyManagerId = unit.PropertyManagerId,
                });

            var unitsSa = _context.Unit
                .Select(unit => new
                {
                    unitId = unit.UnitId,
                    unitNo = unit.UnitNo,
                    unitType = unit.UnitType,
                    propertyId = unit.PropertyId,
                    propertyName = unit.Property.Name,
                    propertyManagerId = unit.PropertyManagerId,
                });

            switch (sort)
            {
                case "asc" when param == "id":
                    units = units.OrderBy(s => s.unitId);
                    unitsSa = unitsSa.OrderBy(s => s.unitId);

                    break;
                case "desc" when param == "id":
                    units = units.OrderByDescending(s => s.unitId);
                    unitsSa = unitsSa.OrderByDescending(s => s.unitId);

                    break;
                
                case "asc" when param == "number":
                    units = units.OrderBy(s => s.unitNo);
                    unitsSa = unitsSa.OrderBy(s => s.unitNo);

                    break;
                case "desc" when param == "number":
                    units = units.OrderByDescending(s => s.unitNo);
                    unitsSa = unitsSa.OrderByDescending(s => s.unitNo);

                    break;
               
                case "asc" when param == "type":
                    units = units.OrderBy(s => s.unitType);
                    unitsSa = unitsSa.OrderBy(s => s.unitType);

                    break;
                case "desc" when param == "type":
                    units = units.OrderByDescending(s => s.unitType);
                    unitsSa = unitsSa.OrderByDescending(s => s.unitType);

                    break;
                
                default:
                    units = units.OrderBy(s => s.unitNo);
                    unitsSa = unitsSa.OrderBy(s => s.unitNo);
                    break;
            }

             if (query != null)
            {
                try
                {
                    var parsedInt = int.Parse(query);
                    if (User.IsInRole("SuperAdmin"))
                    {
                        return Ok(unitsSa.Where(s =>
                            s.unitNo == parsedInt).ToList());
                    }
                    else
                    {
                        return Ok(units.Where(s =>
                            s.unitNo == parsedInt).ToList());
                    }
                }catch
                {
                    if (User.IsInRole("SuperAdmin"))
                    {
                        return Ok(unitsSa.Where(s =>
                            s.propertyName.Contains(query, StringComparison.InvariantCultureIgnoreCase)).AsEnumerable());
                    }
                    else
                    {
                        return Ok(units.Where(s =>
                            s.propertyName.Contains(query, StringComparison.InvariantCultureIgnoreCase)).AsEnumerable());

                    }
                }
            }
            

            return Ok(User.IsInRole("SuperAdmin") ? unitsSa : units);
        }
        
        
       
        
        // GET: api/properties/{propertyId}/units
        [HttpGet]
        [Route("api/properties/{propertyId}/units")]
        public async Task<ActionResult> GetUnits(string propertyId, string? sort, string? param)
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

            var units = _context.Unit
                .Where(u => u.PropertyId == propertyId)
                .Select(unit => new
                {
                    unitId = unit.UnitId,
                    unitNo = unit.UnitNo,
                    unitType = unit.UnitType,
                    propertyId = unit.PropertyId,
                    propertyManagerId = unit.PropertyManagerId,
                });
              
            
            switch (sort)
            {
                case "asc" when param == "id":
                    units = units.OrderBy(s => s.unitId);
                    break;
                case "desc" when param == "id":
                    units = units.OrderByDescending(s => s.unitId);
                    break;
                
                case "asc" when param == "number":
                    units = units.OrderBy(s => s.unitNo);
                    break;
                case "desc" when param == "number":
                    units = units.OrderByDescending(s => s.unitNo);
                    break;
               
                case "asc" when param == "type":
                    units = units.OrderBy(s => s.unitType);
                    break;
                case "desc" when param == "type":
                    units = units.OrderByDescending(s => s.unitType);
                    break;
                
                default:
                    units = units.OrderBy(s => s.unitNo);
                    break;
            }
            
            
            if (User.IsInRole("SuperAdmin") || units.Any(p => p.propertyManagerId == requestingUser.Id || p.unitId == requestingUser.UnitId))
            {
                return Ok(units.ToList());
            }
            
            return NotFound();
        }

        
        // GET: api/properties/{propertyId}/units/{unitId
        [HttpGet]
        [Route("api/properties/{propertyId}/units/{unitId}")]
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
        [HttpPut]
        [Route("/api/units/{id}")]
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
        [Route("api/units")]
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
        [HttpDelete]
        [Route("api/units/{id}")]

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

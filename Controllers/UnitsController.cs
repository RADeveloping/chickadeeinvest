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
    public class UnitsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UnitsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Units
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        {
          if (_context.Units == null)
          {
              return NotFound();
          }

          var user = _userManager.GetUserAsync(User).Result;

          var isTenant = await _userManager.IsInRoleAsync(user, "Tenant");

          var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

          var isPropertyManager = await _userManager.IsInRoleAsync(user, "PropertyManager");

          var isSuperAdmin = await _userManager.IsInRoleAsync(user, "Admin") && 
                             await _userManager.IsInRoleAsync(user, "PropertyManager") &&
                             await _userManager.IsInRoleAsync(user, "Tenant");

          if ((isTenant || isAdmin) && !isSuperAdmin)
          {
            return NoContent();
          }

          if (isPropertyManager && _context.Properties != null && !isSuperAdmin)
          {
            var unitList = new List<Unit>();
            var specificProperties = await _context.Properties
              .Include(m => m.PropertyManager)
              .Where(p => p.PropertyManager == user)
              .Include(u => u.Units).ThenInclude(t => t.Tickets)
              .ToListAsync();
            specificProperties.ForEach((property) => {
              property.Units.ForEach((unit) => {
                unitList.Add(unit);
              });
            });
            return unitList;
          }
            // Ideally for super admins to get ALL units
            return await _context.Units
              .Include(t => t.Tenants)
              .Include(i => i.Tickets)
              .Include(j => j.Property)
              .ToListAsync();
        }

         // GET: api/Units/current
        [HttpGet]
        [Route("current")]
        public async Task<ActionResult> GetSpecificUnitForUser()
        {
          if (_context.Units == null)
          {
              return NotFound();
          }
            var user = _userManager.GetUserAsync(User).Result;


            return Ok(_context.Units
                .Include(t => t.Tenants)
                .Where(unit => unit.Tenants.Contains(user))
                .Select(p => new
                {
                    unitId = p.UnitId,
                    unitNo = p.UnitNo,
                    tenants = p.Tenants
                        .Select(t => new
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Id = user.Id,
                            Email = user.Email,
                        }),
                    Tickets = p.Tickets,
                    Property = p.Property
                }));
        }

        // GET: api/Units/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(int id)
        {
          if (_context.Units == null)
          {
              return NotFound();
          }
            var unit = await _context.Units.FindAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return unit;
        }

        // PUT: api/Units/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnit(int id, Unit unit)
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

        // POST: api/Units
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(Unit unit)
        {
          if (_context.Units == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Units'  is null.");
          }
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnit", new { id = unit.UnitId }, unit);
        }

        // DELETE: api/Units/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            if (_context.Units == null)
            {
                return NotFound();
            }
            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnitExists(int id)
        {
            return (_context.Units?.Any(e => e.UnitId == id)).GetValueOrDefault();
        }
    }
}

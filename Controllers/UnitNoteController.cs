using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using chickadee.Data;
using chickadee.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace chickadee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitNoteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UnitNoteController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/UnitNote
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitNote>>> GetUnitNote()
        {
          if (_context.UnitNote == null)
          {
              return NotFound();
          }
            return await _context.UnitNote.ToListAsync();
        }

        // GET: api/UnitNote/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitNote>> GetUnitNote(string id)
        {
          if (_context.UnitNote == null)
          {
              return NotFound();
          }
            var unitNote = await _context.UnitNote.FindAsync(id);

            if (unitNote == null)
            {
                return NotFound();
            }

            return unitNote;
        }

        // PUT: api/UnitNote/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnitNote(string id, UnitNote unitNote)
        {
            if (id != unitNote.UnitNoteId)
            {
                return BadRequest();
            }

            _context.Entry(unitNote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitNoteExists(id))
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

        // POST: api/UnitNote
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "PropertyManager")]
        public async Task<ActionResult<UnitNote>> PostUnitNote(UnitNote unitNote)
        {
          if (_context.UnitNote == null)
          {
              return Problem("Entity set 'ApplicationDbContext.UnitNote'  is null.");
          }
          if (_context.Unit == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Unit'  is null.");
          }
          var unit = await _context.Unit.FindAsync(unitNote.UnitId);

          if (unit == null)
          {
               // Only the PMs who have the unit can upload the image for that unit
               HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
               return BadRequest("Unit with that Id not found");
          }

          var requestingUser = await _userManager.GetUserAsync(User);

          if (unit.PropertyManagerId != requestingUser.Id)
          {
               // PM id of the specific unit does not match current PM id
               HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
               return BadRequest("Property Manager Id from Unit does not match current user Id");
          }

          unitNote.Unit = unit;
          
            _context.UnitNote.Add(unitNote);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UnitNoteExists(unitNote.UnitNoteId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUnitNote", new { id = unitNote.UnitNoteId }, unitNote);
        }

        // DELETE: api/UnitNote/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitNote(string id)
        {
            if (_context.UnitNote == null)
            {
                return NotFound();
            }
            var unitNote = await _context.UnitNote.FindAsync(id);
            if (unitNote == null)
            {
                return NotFound();
            }

            _context.UnitNote.Remove(unitNote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnitNoteExists(string id)
        {
            return (_context.UnitNote?.Any(e => e.UnitNoteId == id)).GetValueOrDefault();
        }
    }
}

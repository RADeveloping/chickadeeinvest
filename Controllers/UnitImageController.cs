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
    [Route("api/properties/{propertyId}/units/{unitId}/images")]
    [ApiController]
    public class UnitImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UnitImageController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: api/UnitImage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitImage>>> GetUnitImages(string propertyId, string unitId)
        {
            var requestingUser = await _userManager.GetUserAsync(User);

          if (_context.UnitImage == null || _context.Property == null || _context.Unit == null || requestingUser == null)
          {
              return NotFound();
          }

          // var unit = _context.Unit
          //     .FirstOrDefault(u => ((u.UnitId == requestingUser.UnitId || u.PropertyManagerId == requestingUser.Id) &&
          //                           u.UnitId == unitId));

          
            return await _context.UnitImage.ToListAsync();
        }

        // GET: api/UnitImage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitImage>> GetUnitImage(string id)
        {
          if (_context.UnitImage == null)
          {
              return NotFound();
          }
            var unitImage = await _context.UnitImage.FindAsync(id);

            if (unitImage == null)
            {
                return NotFound();
            }

            return unitImage;
        }

        // PUT: api/UnitImage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnitImage(string id, UnitImage unitImage)
        {
            if (id != unitImage.UnitImageId)
            {
                return BadRequest();
            }

            _context.Entry(unitImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitImageExists(id))
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

        // POST: api/UnitImage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "PropertyManager")]
        public async Task<ActionResult<UnitImage>> PostUnitImage(UnitImage unitImage)
        {
          if (_context.UnitImage == null)
          {
              return Problem("Entity set 'ApplicationDbContext.UnitImage'  is null.");
          }

          if (_context.Unit == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Unit'  is null.");
          }
          var unit = await _context.Unit.FindAsync(unitImage.UnitId);

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

          unitImage.Unit = unit;

            _context.UnitImage.Add(unitImage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UnitImageExists(unitImage.UnitImageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new {
                UnitImageId = unitImage.UnitImageId,
                Data = unitImage.data,
                UploadDate = unitImage.UploadDate,
                UnitId = unit.UnitId
            });
        }

        // DELETE: api/UnitImage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitImage(string id)
        {
            if (_context.UnitImage == null)
            {
                return NotFound();
            }
            var unitImage = await _context.UnitImage.FindAsync(id);
            if (unitImage == null)
            {
                return NotFound();
            }

            _context.UnitImage.Remove(unitImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnitImageExists(string id)
        {
            return (_context.UnitImage?.Any(e => e.UnitImageId == id)).GetValueOrDefault();
        }
    }
}

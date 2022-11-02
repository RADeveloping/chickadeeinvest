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
    [Route("api/[controller]")]
    [ApiController]
    public class UnitImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UnitImageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UnitImage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitImage>>> GetUnitImage()
        {
          if (_context.UnitImage == null)
          {
              return NotFound();
          }
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
        public async Task<ActionResult<UnitImage>> PostUnitImage(UnitImage unitImage)
        {
          if (_context.UnitImage == null)
          {
              return Problem("Entity set 'ApplicationDbContext.UnitImage'  is null.");
          }
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

            return CreatedAtAction("GetUnitImage", new { id = unitImage.UnitImageId }, unitImage);
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

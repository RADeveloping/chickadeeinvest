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
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace chickadee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketImageController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/TicketImage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketImage>>> GetTicketImage()
        {
          if (_context.TicketImage == null)
          {
              return NotFound();
          }
            return await _context.TicketImage.ToListAsync();
        }

        // GET: api/TicketImage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketImage>> GetTicketImage(string id)
        {
          if (_context.TicketImage == null)
          {
              return NotFound();
          }
            var ticketImage = await _context.TicketImage.FindAsync(id);

            if (ticketImage == null)
            {
                return NotFound();
            }

            return ticketImage;
        }

        // PUT: api/TicketImage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketImage(string id, TicketImage ticketImage)
        {
            if (id != ticketImage.TicketImageId)
            {
                return BadRequest();
            }

            _context.Entry(ticketImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketImageExists(id))
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

        // POST: api/TicketImage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Tenant")]
        public async Task<ActionResult<TicketImage>> PostTicketImage(TicketImage ticketImage)
        {
          if (_context.TicketImage == null)
          {
              return Problem("Entity set 'ApplicationDbContext.TicketImage'  is null.");
          }
          if (_context.Tickets == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Tickets' is null.");
          }
            var currentTicket = await _context.Tickets.FindAsync(ticketImage.TicketId);

            var requestingUser = await _userManager.GetUserAsync(User);

            if (currentTicket == null)
            {
                return NotFound();
            }

            if (currentTicket.CreatedById != requestingUser.Id)
            {
                // Only the user who created the ticket can upload the image
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return BadRequest("Creator Id of the ticket does not match current user Id");
            }

            ticketImage.Ticket = currentTicket;

            ticketImage.CreatedBy = requestingUser;

            _context.TicketImage.Add(ticketImage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TicketImageExists(ticketImage.TicketImageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(ticketImage);
        }

        // DELETE: api/TicketImage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketImage(string id)
        {
            if (_context.TicketImage == null)
            {
                return NotFound();
            }
            var ticketImage = await _context.TicketImage.FindAsync(id);
            if (ticketImage == null)
            {
                return NotFound();
            }

            _context.TicketImage.Remove(ticketImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketImageExists(string id)
        {
            return (_context.TicketImage?.Any(e => e.TicketImageId == id)).GetValueOrDefault();
        }
    }
}

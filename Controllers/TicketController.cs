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
    using Microsoft.AspNetCore.Identity;

    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Ticket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket()
        {
            
            var requestingUser = await _userManager.GetUserAsync(User);
            if (requestingUser == null)
            {
                return Unauthorized();
            }
            var isSuperAdmin = await _userManager.IsInRoleAsync(requestingUser, "SuperAdmin");
            
            var result = _context.Ticket
                .Select(t=> new
                {
                   ticketId = t.TicketId,
                   problem = t.Problem,
                   description =  t.Description,
                   createdOn = t.CreatedOn,
                   estimatedDate = t.EstimatedDate,
                   status = t.Status,
                   severity = t.Severity,
                   closedDate = t.ClosedDate,
                   unitId = t.UnitId,
                   unit = new
                   {
                       unitId = t.Unit.UnitId,
                       unitNo = t.Unit.UnitNo,
                       unitType = t.Unit.UnitType,
                       propertyId = t.Unit.PropertyId,
                       property = t.Unit.Property,
                       propertyManagerId = t.Unit.PropertyManagerId,
                       propertyManager = t.Unit.PropertyManager,
                       tenants = new
                       {
                           t.CreatedBy.FirstName,
                           t.CreatedBy.LastName,
                           t.CreatedBy.Id,
                           t.CreatedBy.UserName,
                           t.CreatedBy.ProfilePicture
                       },
                   },
                   createdBy = new
                   {
                       t.CreatedBy.FirstName,
                       t.CreatedBy.LastName,
                       t.CreatedBy.Id,
                       t.CreatedBy.UserName,
                       t.CreatedBy.ProfilePicture
                   },
                   messages = t.Messages,
                   images = t.Images,
                })
                .ToList();
            
          var userHasAccess = result.Any(t => t.unit.propertyManagerId == requestingUser.Id || t.unit.tenants.Id == requestingUser.Id || isSuperAdmin);

          if (userHasAccess)
          {
              return Ok(result);

          }
          else
          {
              return Forbid();
          }
          

            
        }

        // GET: api/Ticket/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
          if (_context.Ticket == null)
          {
              return NotFound();
          }
            var ticket = await _context.Ticket.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Ticket/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: api/Ticket
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
          if (_context.Ticket == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Ticket'  is null.");
          }
            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.TicketId }, ticket);
        }

        // DELETE: api/Ticket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return (_context.Ticket?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }
    }
}

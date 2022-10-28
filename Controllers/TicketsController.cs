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
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
          if (_context.Tickets == null)
          {
              return NotFound();
          }

          var user = _userManager.GetUserAsync(User).Result;

          var isTenant = await _userManager.IsInRoleAsync(user, "Tenant");

          var isPropertyManager = await _userManager.IsInRoleAsync(user, "PropertyManager");

          var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

          var isSuperAdmin = await _userManager.IsInRoleAsync(user, "Admin") && 
                             await _userManager.IsInRoleAsync(user, "PropertyManager") &&
                             await _userManager.IsInRoleAsync(user, "Tenant");

          if (isAdmin && !isSuperAdmin)
          {
            return NoContent();
          }

          if (isTenant && _context.Units != null && !isSuperAdmin)
          {
            var ticketList = new List<Ticket>();
            var tenantUnits = await _context.Units
              .Include(t => t.Tenants)
              .Where(unit => unit.Tenants.Contains(user))
              .Include(i => i.Tickets)
              .Include(j => j.Property)
              .ToListAsync();
            tenantUnits.ForEach((unit) => {
              unit.Tickets.ForEach((ticket) => {
                ticketList.Add(ticket);
              });
            });
            return ticketList;
          }

          if (isPropertyManager && _context.Properties != null && !isSuperAdmin)
          {
            var ticketList = new List<Ticket>();
            var properties = await _context.Properties
              .Include(j => j.PropertyManager)
              .Where(t => t.PropertyManager == user)
              .Include(t => t.Units)
                .ThenInclude(s => s.Tickets)
              .ToListAsync();
            properties.ForEach((property) => {
              property.Units.ForEach((unit) => {
                unit.Tickets.ForEach((ticket) => {
                  ticketList.Add(ticket);
                });
              });
            });
            return ticketList;
          }
            return await _context.Tickets.ToListAsync();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
          if (_context.Tickets == null)
          {
              return NotFound();
          }
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Tickets/5
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

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
          if (_context.Tickets == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Tickets'  is null.");
          }
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.TicketId }, ticket);
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return (_context.Tickets?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }
    }
}

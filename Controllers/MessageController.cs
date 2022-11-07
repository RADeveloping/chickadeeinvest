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
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace chickadee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class MessageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Message
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
          if (_context.Messages == null)
          {
              return NotFound();
          }
            return await _context.Messages.ToListAsync();
        }

        // GET: api/Message/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(string id)
        {
          if (_context.Messages == null)
          {
              return NotFound();
          }
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // PUT: api/Message/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(string id, Message message)
        {
            if (id != message.MessageId)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // POST: api/Message
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
          if (_context.Messages == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Messages'  is null.");
          }

          if (_context.Tickets == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
          }

          if (_context.Property == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Property'  is null.");
          }

          var requestingUser = await _userManager.GetUserAsync(User);

          if (message.SenderId != requestingUser.Id)
          {
              HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
              return BadRequest("Message sender Id does not match current user Id");
          }

          var currentTicket = await _context.Tickets.FindAsync(message.TicketId);
          
          if (currentTicket == null)
          {
              HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
              return BadRequest("Ticket not found");
          }
          if (requestingUser.Tickets == null)
          {
              HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
              return BadRequest("Cannot find any tickets from current user");
          }

          var isTenant = await _userManager.IsInRoleAsync(requestingUser, "Tenant");
          var isPropertyManager = await _userManager.IsInRoleAsync(requestingUser, "PropertyManager");

          var propertyManagerTickets = await _context.Property
            .SelectMany(p => p.Units)
            .Where(p => p.PropertyManagerId == requestingUser.Id)
            .SelectMany(p => p.Tickets)
            .ToListAsync();

          if (isTenant && !requestingUser.Tickets.Contains(currentTicket) || isPropertyManager && !propertyManagerTickets.Contains(currentTicket))
          {
              HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
              return BadRequest("Current user does not have the current ticket");
          }

          message.Sender = requestingUser;
          message.Ticket = currentTicket;

            _context.Messages.Add(message);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MessageExists(message.MessageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(message);
        }

        // DELETE: api/Message/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(string id)
        {
            if (_context.Messages == null)
            {
                return NotFound();
            }
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageExists(string id)
        {
            return (_context.Messages?.Any(e => e.MessageId == id)).GetValueOrDefault();
        }
    }
}

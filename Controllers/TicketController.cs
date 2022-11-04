using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using chickadee.Data;
using chickadee.Enums;
using chickadee.Models;

namespace chickadee.Controllers
{
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Identity;

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

        // GET:   api/properties/{propertyId}/units/{unitId}/tickets
        [HttpGet]
        [Route("api/properties/{propertyId}/units/{unitId}/tickets")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets(string propertyId, string unitId)
        {
            
              var requestingUser = await _userManager.GetUserAsync(User);
            if (requestingUser == null || _context.Property == null | _context.Unit == null || _context.Tickets == null)
            {
                return NotFound();
            }
            
            var isSuperAdmin = await _userManager.IsInRoleAsync(requestingUser, "SuperAdmin");

            var result = _context.Tickets
                .Include(t => t.Unit)
                .ThenInclude(u => u.Property)
                .Where(t => (t.Unit.PropertyId == propertyId) && t.Unit.UnitId == unitId);
            
            var userHasAccess = result.Any(t => t.Unit.PropertyManagerId == requestingUser.Id || (t.Unit.Tenants != null && t.Unit.Tenants.Any(x=>x.Id == requestingUser.Id)) || isSuperAdmin);

            var simpleResult =
                result
                    .Select(t => new
                    {
                        ticketId = t.TicketId,
                        problem = t.Problem,
                        description = t.Description,
                        createdOn = t.CreatedOn,
                        estimatedDate = t.EstimatedDate,
                        status = t.Status,
                        severity = t.Severity,
                        closedDate = t.ClosedDate,
                        unitId = t.UnitId,
                        // unit = new
                        // {
                        //     unitId = t.Unit.UnitId,
                        //     unitNo = t.Unit.UnitNo,
                        //     unitType = t.Unit.UnitType,
                        //     propertyId = t.Unit.PropertyId,
                        //     property = t.Unit.Property,
                        //     propertyManagerId = t.Unit.PropertyManagerId,
                        //     propertyManager = t.Unit.PropertyManager,
                        //     tenants = new
                        //     {
                        //         t.CreatedBy.FirstName,
                        //         t.CreatedBy.LastName,
                        //         t.CreatedBy.Id,
                        //         t.CreatedBy.UserName,
                        //         t.CreatedBy.ProfilePicture
                        //     },
                        // },
                        createdBy = new
                        {
                           FirstName = t.CreatedBy.FirstName,
                           LastName = t.CreatedBy.LastName,
                           Id = t.CreatedBy.Id,
                           Email = t.CreatedBy.UserName,
                           PhoneNumber = t.CreatedBy.PhoneNumber,
                           ProfilePicture = t.CreatedBy.ProfilePicture
                        },
                        // messages = t.Messages,
                        // images = t.Images,
                    }).ToList();
            
            
          if (userHasAccess)
          {
              return Ok(simpleResult);

          }
        
          return NotFound();
            
        }

        // GET: api/Ticket/5
        
        [HttpGet]
        [Route("api/tickets/{sortOrder?}")]
        public async Task<ActionResult> GetTickets(string? sortOrder)
        {
            var requestingUser = await _userManager.GetUserAsync(User);
            if (requestingUser == null || _context.Property == null | _context.Unit == null || _context.Tickets == null)
            {
                return NotFound();
            }
            
            var isSuperAdmin = await _userManager.IsInRoleAsync(requestingUser, "SuperAdmin");

            var tickets = _context.Tickets
                .Include(t => t.Unit)
                .ThenInclude(u => u.Property)
                .Where(t => (t.Unit.PropertyManagerId == requestingUser.Id) || t.Unit.UnitId == requestingUser.UnitId)
                .Select(t => new
                {
                    ticketId = t.TicketId,
                    problem = t.Problem,
                    description = t.Description,
                    createdOn = t.CreatedOn,
                    estimatedDate = t.EstimatedDate,
                    status = t.Status,
                    severity = t.Severity,
                    closedDate = t.ClosedDate,
                    unitId = t.UnitId,
                    createdBy = new
                    {
                        FirstName = t.CreatedBy.FirstName,
                        LastName = t.CreatedBy.LastName,
                        Id = t.CreatedBy.Id,
                        Email = t.CreatedBy.UserName,
                        PhoneNumber = t.CreatedBy.PhoneNumber,
                        ProfilePicture = t.CreatedBy.ProfilePicture
                    }
                }).ToList();
                
            var allTickets = _context.Tickets
                .Select(t => new
                {
                ticketId = t.TicketId,
                problem = t.Problem,
                description = t.Description,
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
                    propertyManager =  t.Unit.PropertyManager == null ? null : new
                    {
                        t.Unit.PropertyManager.FirstName,
                        t.Unit.PropertyManager.LastName,
                        t.Unit.PropertyManager.Id,
                        t.Unit.PropertyManager.UserName,
                        t.Unit.PropertyManager.Email,
                        t.Unit.PropertyManager.ProfilePicture
                    } ,
                    tenants = new
                    {
                        FirstName = t.CreatedBy.FirstName,
                        LastName = t.CreatedBy.LastName,
                        Id = t.CreatedBy.Id,
                        Email = t.CreatedBy.UserName,
                        PhoneNumber = t.CreatedBy.PhoneNumber,
                        ProfilePicture = t.CreatedBy.ProfilePicture
                    },
                },
                createdBy = new
                {
                    FirstName = t.CreatedBy.FirstName,
                    LastName = t.CreatedBy.LastName,
                    Id = t.CreatedBy.Id,
                    Email = t.CreatedBy.UserName,
                    PhoneNumber = t.CreatedBy.PhoneNumber,
                    ProfilePicture = t.CreatedBy.ProfilePicture
                },
                // messages = t.Messages,
                // images = t.Images,
            }).ToList();

           
            
            return isSuperAdmin ? Ok(allTickets) : Ok(tickets);
        }
        
        
        

        // // PUT: api/Ticket/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        // {
        //     if (id != ticket.TicketId)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(ticket).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!TicketExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        //
        //     return NoContent();
        // }
        //
        // POST: api/Ticket
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("api/tickets")]
        [AllowAnonymous]
        // [Authorize(Roles = "Tenant")]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
          if (_context.Tickets == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Ticket'  is null.");
          }
            ticket.CreatedBy = _userManager.FindByIdAsync(ticket.CreatedById).Result;
            ticket.Unit = _context.Unit.Find(ticket.UnitId);
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        
            return CreatedAtAction("GetTicket", new { id = ticket.TicketId }, ticket);
        }
        //
        // // DELETE: api/Ticket/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteTicket(int id)
        // {
        //     if (_context.Tickets == null)
        //     {
        //         return NotFound();
        //     }
        //     var ticket = await _context.Tickets.FindAsync(id);
        //     if (ticket == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.Tickets.Remove(ticket);
        //     await _context.SaveChangesAsync();
        //
        //     return NoContent();
        // }

        private bool TicketExists(int id)
        {
            return (_context.Tickets?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }
    }
}

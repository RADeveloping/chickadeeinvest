using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using chickadee.Data;
using chickadee.Enums;
using chickadee.Models;
using Microsoft.AspNetCore.Authorization;

namespace chickadee.Controllers
{
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
        public async Task<ActionResult> GetTickets(string propertyId, string unitId, string? sortOrder, string? param)
        {
            
              var requestingUser = await _userManager.GetUserAsync(User);
            if (requestingUser == null || _context.Property == null | _context.Unit == null || _context.Tickets == null)
            {
                return NotFound();
            }
            
            var isSuperAdmin = await _userManager.IsInRoleAsync(requestingUser, "SuperAdmin");

            var allTickets = _context.Tickets
                .Where(t => t.Unit != null && (t.Unit.PropertyId == propertyId) && t.Unit.UnitId == unitId)
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
                        propertyId = t.Unit != null ? t.Unit.PropertyId : null,
                        propertyName= t.Unit != null ? t.Unit.Property.Name : null,
                        unit = new
                        {
                            unitId = t.Unit.UnitId,
                            unitNo = t.Unit.UnitNo,
                            unitType = t.Unit.UnitType,
                            propertyId = t.Unit.PropertyId,
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
                            property = new
                            {
                                t.Unit.Property.Address,
                                t.Unit.Property.Name,
                                t.Unit.Property.PropertyId,
                            },
                            tenants = t.Unit.Tenants == null ? null : t.Unit.Tenants.Select(tenant => new
                            {
                                FirstName = tenant.FirstName,
                                LastName = tenant.LastName,
                                Id = tenant.Id,
                                Email = tenant.UserName,
                                PhoneNumber = tenant.PhoneNumber,
                                ProfilePicture = tenant.ProfilePicture
                            })
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
                    });
            
            var tickets = _context.Tickets
                .Where(t => t.Unit != null && (t.Unit.PropertyId == propertyId) && t.Unit.UnitId == unitId)
                .Where(t => t.Unit != null && (t.UnitId == requestingUser.UnitId || t.Unit.PropertyManagerId == requestingUser.Id))
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
                        propertyId = t.Unit != null ? t.Unit.PropertyId : null,
                        propertyName= t.Unit != null ? t.Unit.Property.Name : null,
                        unit = new
                        {
                            unitId = t.Unit.UnitId,
                            unitNo = t.Unit.UnitNo,
                            unitType = t.Unit.UnitType,
                            propertyId = t.Unit.PropertyId,
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
                            property = new
                            {
                                t.Unit.Property.Address,
                                t.Unit.Property.Name,
                                t.Unit.Property.PropertyId,
                            },
                            tenants = t.Unit.Tenants == null ? null : t.Unit.Tenants.Select(tenant => new 
                            {
                                FirstName = tenant.FirstName,
                                LastName = tenant.LastName,
                                Id = tenant.Id,
                                Email = tenant.UserName,
                                PhoneNumber = tenant.PhoneNumber,
                                ProfilePicture = tenant.ProfilePicture
                            })
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
                    });



            switch (sortOrder)
            {
                case "asc" when param == "address":
                    allTickets = allTickets.OrderBy(s => s.unit.property.Address);
                    tickets = tickets.OrderBy(s => s.unit.property.Address);

                    break;
                case "desc" when param == "address":
                    allTickets = allTickets.OrderByDescending(s => s.unit.property.Address);
                    tickets = tickets.OrderByDescending(s => s.unit.property.Address);

                    break;
                case "asc" when param == "id":
                    allTickets = allTickets.OrderBy(s => s.ticketId);
                    tickets = tickets.OrderBy(s => s.ticketId);
                    break;
                case "desc" when param == "id":
                    allTickets = allTickets.OrderByDescending(s => s.ticketId);
                    tickets = tickets.OrderByDescending(s => s.ticketId);
                    break;
                case "asc" when param == "property_manager_name":
                    allTickets = allTickets.OrderBy(s => s.unit.property.Name);
                    tickets = tickets.OrderBy(s => s.unit.property.Name);

                    break;
                case "desc" when param == "property_manager_name":
                    allTickets = allTickets.OrderByDescending(s => s.unit.property.Name);
                    tickets = tickets.OrderByDescending(s => s.unit.property.Name);
                    break;
                default:
                    allTickets = allTickets.OrderByDescending(s => s.createdOn);
                    tickets = tickets.OrderByDescending(s => s.createdOn);

                    break;
            }

            
            
            return isSuperAdmin ? Ok(allTickets.ToList()) : Ok(tickets.ToList());
            
            
        }

        // GET:   api/properties/{propertyId}/units/{unitId}/tickets/{ticketId}
        [HttpGet]
        [Route("api/properties/{propertyId}/units/{unitId}/tickets/{ticketId:int}")]
        public async Task<ActionResult> GetTicket(string propertyId, string unitId, int ticketId)
        {
            
            var requestingUser = await _userManager.GetUserAsync(User);
            if (requestingUser == null || _context.Property == null | _context.Unit == null || _context.Tickets == null)
            {
                return NotFound();
            }
            
            var isSuperAdmin = await _userManager.IsInRoleAsync(requestingUser, "SuperAdmin");

            var ticketSA = _context.Tickets
                .Where(t => t.Unit != null && (t.Unit.PropertyId == propertyId) && t.Unit.UnitId == unitId)
                .Where(t=>t.TicketId == ticketId)
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
                        propertyId = t.Unit != null ? t.Unit.PropertyId : null,
                        propertyName= t.Unit != null ? t.Unit.Property.Name : null,
                        unit = new
                        {
                            unitId = t.Unit.UnitId,
                            unitNo = t.Unit.UnitNo,
                            unitType = t.Unit.UnitType,
                            propertyId = t.Unit.PropertyId,
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
                            property = new
                            {
                                t.Unit.Property.Address,
                                t.Unit.Property.Name,
                                t.Unit.Property.PropertyId,
                            },
                            tenants = t.Unit.Tenants == null ? null : t.Unit.Tenants.Select(tenant => new
                            {
                                FirstName = tenant.FirstName,
                                LastName = tenant.LastName,
                                Id = tenant.Id,
                                Email = tenant.UserName,
                                PhoneNumber = tenant.PhoneNumber,
                                ProfilePicture = tenant.ProfilePicture
                            })
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
                    });
            
            var ticket = _context.Tickets
                .Where(t => t.Unit != null && (t.Unit.PropertyId == propertyId) && t.Unit.UnitId == unitId)
                .Where(t => t.Unit != null && (t.UnitId == requestingUser.UnitId || t.Unit.PropertyManagerId == requestingUser.Id))
                .Where(t=>t.TicketId == ticketId)
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
                        propertyId = t.Unit != null ? t.Unit.PropertyId : null,
                        propertyName= t.Unit != null ? t.Unit.Property.Name : null,
                        unit = new
                        {
                            unitId = t.Unit.UnitId,
                            unitNo = t.Unit.UnitNo,
                            unitType = t.Unit.UnitType,
                            propertyId = t.Unit.PropertyId,
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
                            property = new
                            {
                                t.Unit.Property.Address,
                                t.Unit.Property.Name,
                                t.Unit.Property.PropertyId,
                            },
                            tenants = t.Unit.Tenants == null ? null : t.Unit.Tenants.Select(tenant => new 
                            {
                                FirstName = tenant.FirstName,
                                LastName = tenant.LastName,
                                Id = tenant.Id,
                                Email = tenant.UserName,
                                PhoneNumber = tenant.PhoneNumber,
                                ProfilePicture = tenant.ProfilePicture
                            })
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
                    });
                    
            return isSuperAdmin ? Ok(ticketSA.FirstOrDefault()) : Ok(ticket.FirstOrDefault());
            
            
        }
        
        // GET: api/Ticket/5
        
        [HttpGet]
        [Route("api/tickets")]
        public async Task<ActionResult> GetTickets(string? sortOrder, string? query, string? param)
        {
            var requestingUser = await _userManager.GetUserAsync(User);
            if (requestingUser == null || _context.Property == null | _context.Unit == null || _context.Tickets == null)
            {
                return NotFound();
            }
            
            var isSuperAdmin = await _userManager.IsInRoleAsync(requestingUser, "SuperAdmin");

            var tickets = _context.Tickets
                .Where(t => t.Unit != null && ((t.Unit.PropertyManagerId == requestingUser.Id) ||
                                               t.Unit.UnitId == requestingUser.UnitId))
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
                    propertyId = t.Unit != null ? t.Unit.PropertyId : null,
                    propertyName = t.Unit != null ? t.Unit.Property.Name : null,
                    unit = new
                    {
                        unitId = t.Unit.UnitId,
                        unitNo = t.Unit.UnitNo,
                        unitType = t.Unit.UnitType,
                        propertyId = t.Unit.PropertyId,
                        propertyManagerId = t.Unit.PropertyManagerId,
                        propertyManager = t.Unit.PropertyManager == null
                            ? null
                            : new
                            {
                                t.Unit.PropertyManager.FirstName,
                                t.Unit.PropertyManager.LastName,
                                t.Unit.PropertyManager.Id,
                                t.Unit.PropertyManager.UserName,
                                t.Unit.PropertyManager.Email,
                                t.Unit.PropertyManager.ProfilePicture
                            },
                        property = new
                        {
                            t.Unit.Property.Address,
                            t.Unit.Property.Name,
                            t.Unit.Property.PropertyId,
                        },
                        tenants = t.Unit.Tenants == null
                            ? null
                            : t.Unit.Tenants.Select(tenant => new
                            {
                                FirstName = tenant.FirstName,
                                LastName = tenant.LastName,
                                Id = tenant.Id,
                                Email = tenant.UserName,
                                PhoneNumber = tenant.PhoneNumber,
                                ProfilePicture = tenant.ProfilePicture
                            })
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
                });

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
                    propertyId = t.Unit != null ? t.Unit.PropertyId : null,
                    propertyName = t.Unit != null ? t.Unit.Property.Name : null,
                    unit = new
                    {
                        unitId = t.Unit.UnitId,
                        unitNo = t.Unit.UnitNo,
                        unitType = t.Unit.UnitType,
                        propertyId = t.Unit.PropertyId,
                        property = t.Unit.Property,
                        propertyManagerId = t.Unit.PropertyManagerId,
                        propertyManager = t.Unit.PropertyManager == null
                            ? null
                            : new
                            {
                                t.Unit.PropertyManager.FirstName,
                                t.Unit.PropertyManager.LastName,
                                t.Unit.PropertyManager.Id,
                                t.Unit.PropertyManager.UserName,
                                t.Unit.PropertyManager.Email,
                                t.Unit.PropertyManager.ProfilePicture
                            },
                        tenants = t.Unit.Tenants == null
                            ? null
                            : t.Unit.Tenants.Select(tenant => new
                            {
                                FirstName = tenant.FirstName,
                                LastName = tenant.LastName,
                                Id = tenant.Id,
                                Email = tenant.UserName,
                                PhoneNumber = tenant.PhoneNumber,
                                ProfilePicture = tenant.ProfilePicture
                            })
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
                });

            
            switch (sortOrder)
            {
                case "asc" when param == "address":
                    allTickets = allTickets.OrderBy(s => s.unit.property.Address);
                    tickets = tickets.OrderBy(s => s.unit.property.Address);

                    break;
                case "desc" when param == "address":
                    allTickets = allTickets.OrderByDescending(s => s.unit.property.Address);
                    tickets = tickets.OrderByDescending(s => s.unit.property.Address);

                    break;
                case "asc" when param == "id":
                    allTickets = allTickets.OrderBy(s => s.ticketId);
                    tickets = tickets.OrderBy(s => s.ticketId);
                    break;
                case "desc" when param == "id":
                    allTickets = allTickets.OrderByDescending(s => s.ticketId);
                    tickets = tickets.OrderByDescending(s => s.ticketId);
                    break;
                case "asc" when param == "property_manager_name":
                    allTickets = allTickets.OrderBy(s => s.unit.property.Name);
                    tickets = tickets.OrderBy(s => s.unit.property.Name);

                    break;
                case "desc" when param == "property_manager_name":
                    allTickets = allTickets.OrderByDescending(s => s.unit.property.Name);
                    tickets = tickets.OrderByDescending(s => s.unit.property.Name);
                    break;
                default:
                    allTickets = allTickets.OrderByDescending(s => s.createdOn);
                    tickets = tickets.OrderByDescending(s => s.createdOn);

                    break;
            }

            
            return isSuperAdmin ? Ok(allTickets.ToList()) : Ok(tickets.ToList());
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
        [HttpPost("/api/tickets")]
        [AllowAnonymous]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
          if (_context.Tickets == null || _context.Unit == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Ticket'  is null.");
          }
          var requestingUser = await _userManager.GetUserAsync(User);


          // if (ticket.CreatedById != requestingUser.Id)
          // {
          //     HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
          // }
          //
          var unit = await _context.Unit.FindAsync(ticket.UnitId);

          if (unit == null)
          {
              // ERROR NO UNIT FOUND 
              HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

          }

          ticket.Unit = unit;
          ticket.CreatedBy = requestingUser;
          
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return Ok(ticket);
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

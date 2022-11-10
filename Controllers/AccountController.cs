using Microsoft.AspNetCore.Mvc;
using chickadee.Data;
using chickadee.Enums;
using chickadee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace chickadee.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        
        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
            var requestingUser = await _userManager.GetUserAsync(User);
            if (requestingUser == null || _context.Property == null | _context.Unit == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var unit = (Unit?)null;
            
            if (_context.Unit != null)
            {
                unit = _context.Unit
                    .Include(u=>u.Property)
                    .FirstOrDefault(u => requestingUser.UnitId != null && u.UnitId == requestingUser.UnitId);
            }
            
            var roles = _userManager.GetRolesAsync(requestingUser).Result;

            return Ok(new
            {
                FirstName = requestingUser.FirstName,
                LastName = requestingUser.LastName,
                Id = requestingUser.Id,
                Email = requestingUser.Email,
                Roles = roles,
                PhoneNumber = requestingUser.PhoneNumber,
                ProfilePicture = requestingUser.ProfilePicture,
                PropertyName = unit?.Property?.Name ?? (string?)null,
                UnitNo = unit?.UnitNo ?? (int?)null,
                UnitType = unit?.UnitType ?? (UnitType?)null
                
            });
            
        }

    }

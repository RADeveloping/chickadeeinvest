using Microsoft.AspNetCore.Mvc;
using chickadee.Data;
using chickadee.Models;
using Microsoft.AspNetCore.Identity;

namespace chickadee.Controllers;

    [Route("api/[controller]")]
    [ApiController]
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
        public  IActionResult GetUser()
        {
            var user = _userManager.GetUserAsync(User).Result;
            
            if (user != null)
            {
                var roles = _userManager.GetRolesAsync(user).Result;

                var simpleUser = new
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Email = user.Email,
                    Roles = roles,
                    ProfilePicture = user.ProfilePicture,
                };
                
                
                return Ok(simpleUser);

            };
            
            return NoContent();

        }

    }

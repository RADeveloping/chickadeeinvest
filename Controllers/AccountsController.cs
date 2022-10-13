using Microsoft.AspNetCore.Mvc;
using chickadee.Data;
using chickadee.Models;

namespace chickadee.Controllers
{
 
    using Microsoft.AspNetCore.Identity;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public  IActionResult GetUser()
        {
            var user = _userManager.GetUserAsync(User).Result;
            
            if (_signInManager.IsSignedIn(User) && user != null)
            {
                var roles = _userManager.GetRolesAsync(user).Result;

                var simpleUser = new
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ProfilePicture = user.ProfilePicture,
                    Id = user.Id,
                    Email = user.Email,
                    roles = roles
                };
                
                
                return Ok(simpleUser);

            };
            
            return NoContent();

        }

    }
}

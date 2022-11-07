using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using chickadee.Data;
using chickadee.Models;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace chickadee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationDocumentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public VerificationDocumentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/VerificationDocument
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VerificationDocument>>> GetVerificationDocuments()
        {
          if (_context.VerificationDocuments == null)
          {
              return NotFound();
          }
            return await _context.VerificationDocuments.ToListAsync();
        }

        // GET: api/VerificationDocument/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VerificationDocument>> GetVerificationDocument(string id)
        {
          if (_context.VerificationDocuments == null)
          {
              return NotFound();
          }
            var verificationDocument = await _context.VerificationDocuments.FindAsync(id);

            if (verificationDocument == null)
            {
                return NotFound();
            }

            return verificationDocument;
        }

        // PUT: api/VerificationDocument/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVerificationDocument(string id, VerificationDocument verificationDocument)
        {
            if (id != verificationDocument.VerificationDocumentId)
            {
                return BadRequest();
            }

            _context.Entry(verificationDocument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificationDocumentExists(id))
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

        // POST: api/VerificationDocument
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Tenant")]
        public async Task<ActionResult<VerificationDocument>> PostVerificationDocument(VerificationDocument verificationDocument)
        {
          if (_context.VerificationDocuments == null)
          {
              return Problem("Entity set 'ApplicationDbContext.VerificationDocuments'  is null.");
          }

          if (_context.Tenant == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Tenant'  is null.");
          }

          var requestingUser = await _userManager.GetUserAsync(User);
          var tenant = await _context.Tenant.FindAsync(verificationDocument.TenantId);

          if (tenant == null)
          {
               // If tenant does not exist, they cannot 
               HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
               return BadRequest("Tenant not found");
          }

          if (verificationDocument.TenantId != requestingUser.Id)
          {
               // If tenant does not exist, they cannot 
               HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
               return BadRequest("Your Id does not match Tenant Id");
          }

          verificationDocument.Tenant = tenant;

            _context.VerificationDocuments.Add(verificationDocument);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VerificationDocumentExists(verificationDocument.VerificationDocumentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(verificationDocument);
        }

        // DELETE: api/VerificationDocument/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVerificationDocument(string id)
        {
            if (_context.VerificationDocuments == null)
            {
                return NotFound();
            }
            var verificationDocument = await _context.VerificationDocuments.FindAsync(id);
            if (verificationDocument == null)
            {
                return NotFound();
            }

            _context.VerificationDocuments.Remove(verificationDocument);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VerificationDocumentExists(string id)
        {
            return (_context.VerificationDocuments?.Any(e => e.VerificationDocumentId == id)).GetValueOrDefault();
        }
    }
}

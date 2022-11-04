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
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationDocumentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VerificationDocumentController(ApplicationDbContext context)
        {
            _context = context;
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
        public async Task<ActionResult<VerificationDocument>> PostVerificationDocument(VerificationDocument verificationDocument)
        {
          if (_context.VerificationDocuments == null)
          {
              return Problem("Entity set 'ApplicationDbContext.VerificationDocuments'  is null.");
          }
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

            return CreatedAtAction("GetVerificationDocument", new { id = verificationDocument.VerificationDocumentId }, verificationDocument);
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

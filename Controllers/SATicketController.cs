using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using chickadee.Data;
using chickadee.Models;
using Microsoft.AspNetCore.Authorization;

namespace chickadee.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class SATicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SATicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SATicketManager
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tickets.Include(t => t.CreatedBy).Include(t => t.Unit);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SATicketManager/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.CreatedBy)
                .Include(t => t.Unit)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: SATicketManager/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.User, "Id", "FirstName");
            ViewData["UnitId"] = new SelectList(_context.Unit, "UnitId", "UnitNo");
            return View();
        }

        // POST: SATicketManager/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId,Problem,Description,CreatedOn,EstimatedDate,Status,Severity,ClosedDate,UnitId,CreatedById")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.User, "Id", "FirstName", ticket.CreatedById);
            ViewData["UnitId"] = new SelectList(_context.Unit, "UnitId", "UnitId", ticket.UnitId);
            return View(ticket);
        }

        // GET: SATicketManager/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.User, "Id", "FirstName", ticket.CreatedById);
            ViewData["UnitId"] = new SelectList(_context.Unit, "UnitId", "UnitNo", ticket.UnitId);
            return View(ticket);
        }

        // POST: SATicketManager/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,Problem,Description,CreatedOn,EstimatedDate,Status,Severity,ClosedDate,UnitId,CreatedById")] Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.User, "Id", "Id", ticket.CreatedById);
            ViewData["UnitId"] = new SelectList(_context.Unit, "UnitId", "UnitId", ticket.UnitId);
            return View(ticket);
        }

        // GET: SATicketManager/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.CreatedBy)
                .Include(t => t.Unit)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: SATicketManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tickets'  is null.");
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
          return (_context.Tickets?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }
    }
}

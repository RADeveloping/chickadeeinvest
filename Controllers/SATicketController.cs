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
            var applicationDbContext = _context.Tickets.Include(t => t.CreatedBy)
                .Include(t => t.Unit)
                .ThenInclude(u => u.Property);
            
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
                .ThenInclude(t=>t.Property)
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
            List<SelectListItem> li = new List<SelectListItem> { new() { Text = "", Value = "" } };
            if (_context.Property != null)
                foreach (var property in _context.Property.Where(p => p.Units != null && p.Units.Any()))
                {
                    li.Add(new SelectListItem { Text = property.Address, Value = property.PropertyId });
                }

            ViewData["properties"] = li;
            ViewData["CreatedById"] = new SelectList(_context.User, "Id", "FullName");
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
            
            ViewData["CreatedById"] = new SelectList(_context.User, "Id", "FullName", ticket.CreatedById);
            ViewData["UnitId"] = new SelectList(_context.Unit, "UnitId", "UnitId", ticket.UnitId);
            
            if (_context.Property == null) return View(ticket);
            List<SelectListItem> li = new List<SelectListItem> { new() { Text = "", Value = "" } };
            foreach (var property in _context.Property.Where(p=>p.Units.Any()))
            {
                li.Add(new SelectListItem { Text = property.Address, Value = property.PropertyId });
            }
            ViewData["properties"] = li;

            
            return View(ticket);
        }

        // GET: SATicketManager/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = _context.Tickets.Include(u => u.Unit)
                .ThenInclude(p => p.Property).FirstOrDefault(t => t.TicketId == id);
            
            if (ticket == null)
            {
                return NotFound();
            }
            
            
            List<SelectListItem> li = new List<SelectListItem> { new() { Text = ticket.Unit?.Property?.Name, Value = ticket.Unit?.PropertyId } };
            if (_context.Property != null)
                foreach (var property in _context.Property.Where(p => p.Units != null && p.Units.Any()))
                {
                    li.Add(new SelectListItem { Text = property.Address, Value = property.PropertyId });
                }
            
            ViewData["properties"] = li;
            ViewData["CreatedById"] = new SelectList(_context.User, "Id", "FullName", ticket.CreatedById);
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
            List<SelectListItem> li = new List<SelectListItem> { new() { Text = ticket.Unit?.Property?.Name, Value = ticket.Unit?.PropertyId } };
            if (_context.Property != null)
                foreach (var property in _context.Property.Where(p => p.Units != null && p.Units.Any()))
                {
                    li.Add(new SelectListItem { Text = property.Address, Value = property.PropertyId });
                }

            ViewData["properties"] = li;
            ViewData["CreatedById"] = new SelectList(_context.User, "Id", "FullName", ticket.CreatedById);
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

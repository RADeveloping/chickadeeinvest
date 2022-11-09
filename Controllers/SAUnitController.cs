using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using chickadee.Data;
using chickadee.Models;

namespace chickadee.Controllers
{
    public class SAUnitController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SAUnitController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SAUnitManager
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Unit.Include(u => u.Property).Include(u => u.PropertyManager);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SAUnitManager/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Unit == null)
            {
                return NotFound();
            }

            var unit = await _context.Unit
                .Include(u => u.Property)
                .Include(u => u.PropertyManager)
                .FirstOrDefaultAsync(m => m.UnitId == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // GET: SAUnitManager/Create
        public IActionResult Create()
        {
            ViewData["PropertyId"] = new SelectList(_context.Property, "PropertyId", "PropertyId");
            ViewData["PropertyManagerId"] = new SelectList(_context.PropertyManagers, "Id", "Id");
            return View();
        }

        // POST: SAUnitManager/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnitId,UnitNo,UnitType,PropertyId,PropertyManagerId")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PropertyId"] = new SelectList(_context.Property, "PropertyId", "PropertyId", unit.PropertyId);
            ViewData["PropertyManagerId"] = new SelectList(_context.PropertyManagers, "Id", "Id", unit.PropertyManagerId);
            return View(unit);
        }

        // GET: SAUnitManager/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Unit == null)
            {
                return NotFound();
            }

            var unit = await _context.Unit.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }
            ViewData["PropertyId"] = new SelectList(_context.Property, "PropertyId", "PropertyId", unit.PropertyId);
            ViewData["PropertyManagerId"] = new SelectList(_context.PropertyManagers, "Id", "Id", unit.PropertyManagerId);
            return View(unit);
        }

        // POST: SAUnitManager/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UnitId,UnitNo,UnitType,PropertyId,PropertyManagerId")] Unit unit)
        {
            if (id != unit.UnitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitExists(unit.UnitId))
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
            ViewData["PropertyId"] = new SelectList(_context.Property, "PropertyId", "PropertyId", unit.PropertyId);
            ViewData["PropertyManagerId"] = new SelectList(_context.PropertyManagers, "Id", "Id", unit.PropertyManagerId);
            return View(unit);
        }

        // GET: SAUnitManager/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Unit == null)
            {
                return NotFound();
            }

            var unit = await _context.Unit
                .Include(u => u.Property)
                .Include(u => u.PropertyManager)
                .FirstOrDefaultAsync(m => m.UnitId == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // POST: SAUnitManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Unit == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Unit'  is null.");
            }
            var unit = await _context.Unit.FindAsync(id);
            if (unit != null)
            {
                _context.Unit.Remove(unit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitExists(string id)
        {
          return (_context.Unit?.Any(e => e.UnitId == id)).GetValueOrDefault();
        }
    }
}

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

    public class SAPropertyManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SAPropertyManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SAPropertyManager
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PropertyManagers.Include(p => p.Company);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SAPropertyManager/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PropertyManagers == null)
            {
                return NotFound();
            }

            var propertyManager = await _context.PropertyManagers
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyManager == null)
            {
                return NotFound();
            }

            return View(propertyManager);
        }

        // GET: SAPropertyManager/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "CompanyId");
            return View();
        }

        // POST: SAPropertyManager/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,FirstName,LastName,UsernameChangeLimit,DateOfBirth,ProfilePicture,UnitId,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] PropertyManager propertyManager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propertyManager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "CompanyId", propertyManager.CompanyId);
            return View(propertyManager);
        }

        // GET: SAPropertyManager/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PropertyManagers == null)
            {
                return NotFound();
            }

            var propertyManager = await _context.PropertyManagers.FindAsync(id);
            if (propertyManager == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "CompanyId", propertyManager.CompanyId);
            return View(propertyManager);
        }

        // POST: SAPropertyManager/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CompanyId,FirstName,LastName,UsernameChangeLimit,DateOfBirth,ProfilePicture,UnitId,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] PropertyManager propertyManager)
        {
            if (id != propertyManager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyManager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyManagerExists(propertyManager.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "CompanyId", propertyManager.CompanyId);
            return View(propertyManager);
        }

        // GET: SAPropertyManager/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PropertyManagers == null)
            {
                return NotFound();
            }

            var propertyManager = await _context.PropertyManagers
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyManager == null)
            {
                return NotFound();
            }

            return View(propertyManager);
        }

        // POST: SAPropertyManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PropertyManagers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PropertyManagers'  is null.");
            }
            var propertyManager = await _context.PropertyManagers.FindAsync(id);
            if (propertyManager != null)
            {
                _context.PropertyManagers.Remove(propertyManager);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyManagerExists(string id)
        {
          return (_context.PropertyManagers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

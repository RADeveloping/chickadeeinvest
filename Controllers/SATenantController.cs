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
    public class SATenantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SATenantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SATenant
        public async Task<IActionResult> Index()
        {
              return _context.Tenant != null ? 
                          View(await _context.Tenant.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Tenant'  is null.");
        }

        // GET: SATenant/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Tenant == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // GET: SATenant/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SATenant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,UsernameChangeLimit,DateOfBirth,ProfilePicture,UnitId,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tenant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tenant);
        }

        // GET: SATenant/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Tenant == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenant.FindAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }
            return View(tenant);
        }

        // POST: SATenant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName,UsernameChangeLimit,DateOfBirth,ProfilePicture,UnitId,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Tenant tenant)
        {
            if (id != tenant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantExists(tenant.Id))
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
            return View(tenant);
        }

        // GET: SATenant/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Tenant == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // POST: SATenant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Tenant == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tenant'  is null.");
            }
            var tenant = await _context.Tenant.FindAsync(id);
            if (tenant != null)
            {
                _context.Tenant.Remove(tenant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenantExists(string id)
        {
          return (_context.Tenant?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

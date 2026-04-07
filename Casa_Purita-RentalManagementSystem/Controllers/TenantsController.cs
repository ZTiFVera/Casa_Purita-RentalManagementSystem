using Casa_Purita_RentalManagementSystem.Data;
using Casa_Purita_RentalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Casa_Purita_RentalManagementSystem.Controllers
{
    [Authorize]
    public class TenantsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TenantsController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index(string? search)
        {
            var tenants = _context.Tenants.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                tenants = tenants.Where(t =>
                    t.FirstName.Contains(search) ||
                    t.LastName.Contains(search) ||
                    t.Email.Contains(search));

            ViewBag.Search = search;
            return View(await tenants.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var tenant = await _context.Tenants
                .Include(t => t.Contracts)
                    .ThenInclude(c => c.Room)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tenant == null) return NotFound();
            return View(tenant);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tenant tenant)
        {
            if (!ModelState.IsValid) return View(tenant);
            tenant.DateRegistered = DateTime.Now;
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Tenant added successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null) return NotFound();
            return View(tenant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tenant tenant)
        {
            if (id != tenant.Id) return NotFound();
            if (!ModelState.IsValid) return View(tenant);

            try
            {
                _context.Update(tenant);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Tenant updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tenants.Any(t => t.Id == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null) return NotFound();
            return View(tenant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant != null)
            {
                _context.Tenants.Remove(tenant);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Tenant deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

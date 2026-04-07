using Casa_Purita_RentalManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Casa_Purita_RentalManagementSystem.Controllers
{
    [Authorize]
    public class ContractsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ContractsController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index(string? search, string? status)
        {
            var contracts = _context.Contracts
                .Include(c => c.Tenant)
                .Include(c => c.Room)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
                contracts = contracts.Where(c =>
                    c.Tenant!.FirstName.Contains(search) ||
                    c.Tenant!.LastName.Contains(search) ||
                    c.Room!.RoomNumber.Contains(search));

            if (!string.IsNullOrEmpty(status))
                contracts = contracts.Where(c => c.Status == status);

            ViewBag.Search = search;
            ViewBag.Status = status;
            return View(await contracts.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var contract = await _context.Contracts
                .Include(c => c.Tenant)
                .Include(c => c.Room)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contract == null) return NotFound();
            return View(contract);
        }

        public IActionResult Create()
        {
            ViewBag.Tenants = new SelectList(
                _context.Tenants.ToList(), "Id", "FullName");
            ViewBag.Rooms = new SelectList(
                _context.Rooms.Where(r => r.IsAvailable).ToList(),
                "Id", "RoomNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract contract)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Tenants = new SelectList(
                    _context.Tenants.ToList(), "Id", "FullName");
                ViewBag.Rooms = new SelectList(
                    _context.Rooms.Where(r => r.IsAvailable).ToList(),
                    "Id", "RoomNumber");
                return View(contract);
            }

            contract.Status = "Active";
            _context.Contracts.Add(contract);

            var room = await _context.Rooms.FindAsync(contract.RoomId);
            if (room != null) room.IsAvailable = false;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Contract created successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null) return NotFound();

            ViewBag.Tenants = new SelectList(
                _context.Tenants.ToList(), "Id", "FullName", contract.TenantId);
            ViewBag.Rooms = new SelectList(
                _context.Rooms.ToList(), "Id", "RoomNumber", contract.RoomId);
            return View(contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contract contract)
        {
            if (id != contract.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Tenants = new SelectList(
                    _context.Tenants.ToList(), "Id", "FullName", contract.TenantId);
                ViewBag.Rooms = new SelectList(
                    _context.Rooms.ToList(), "Id", "RoomNumber", contract.RoomId);
                return View(contract);
            }

            try
            {
                _context.Update(contract);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Contract updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Contracts.Any(c => c.Id == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var contract = await _context.Contracts
                .Include(c => c.Tenant)
                .Include(c => c.Room)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contract == null) return NotFound();
            return View(contract);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract != null)
            {
                var room = await _context.Rooms.FindAsync(contract.RoomId);
                if (room != null) room.IsAvailable = true;

                _context.Contracts.Remove(contract);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Contract deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Terminate(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract != null)
            {
                contract.Status = "Terminated";
                var room = await _context.Rooms.FindAsync(contract.RoomId);
                if (room != null) room.IsAvailable = true;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Contract terminated successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}


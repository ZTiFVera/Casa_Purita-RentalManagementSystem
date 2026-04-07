using Casa_Purita_RentalManagementSystem.Data;
using Casa_Purita_RentalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Casa_Purita_RentalManagementSystem.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RoomsController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index(string? search, string? filter)
        {
            var rooms = _context.Rooms.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                rooms = rooms.Where(r =>
                    r.RoomNumber.Contains(search) ||
                    r.Type.Contains(search));

            if (filter == "available")
                rooms = rooms.Where(r => r.IsAvailable);
            else if (filter == "occupied")
                rooms = rooms.Where(r => !r.IsAvailable);

            ViewBag.Search = search;
            ViewBag.Filter = filter;
            return View(await rooms.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var room = await _context.Rooms
                .Include(r => r.Contracts)
                    .ThenInclude(c => c.Tenant)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null) return NotFound();
            return View(room);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            if (!ModelState.IsValid) return View(room);
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Room added successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound();
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Room room)
        {
            if (id != room.Id) return NotFound();
            if (!ModelState.IsValid) return View(room);

            try
            {
                _context.Update(room);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Room updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Rooms.Any(r => r.Id == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound();
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Room deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

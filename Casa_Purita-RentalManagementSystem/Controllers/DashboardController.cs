using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Casa_Purita_RentalManagementSystem.Data;

namespace Casa_Purita_RentalManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            var today = DateTime.Today;

            // Room Stats
            ViewBag.TotalRooms = _context.Rooms.Count();
            ViewBag.AvailableRooms = _context.Rooms.Count(r => r.Status == "Available");
            ViewBag.OccupiedRooms = _context.Rooms.Count(r => r.Status == "Occupied");
            ViewBag.MaintenanceRooms = _context.Rooms.Count(r => r.Status == "Maintenance");

            // Tenant & Contract Stats
            ViewBag.TotalTenants = _context.Tenants.Count();
            ViewBag.ActiveContracts = _context.Contracts.Count(c => c.Status == "Active");
            ViewBag.ExpiredContracts = _context.Contracts.Count(c => c.Status == "Expired");

            // Contracts expiring within 30 days
            ViewBag.ExpiringContracts = _context.Contracts
                .Include(c => c.Tenant)
                .Include(c => c.Room)
                .Where(c => c.Status == "Active" &&
                            c.EndDate >= today &&
                            c.EndDate <= today.AddDays(30))
                .ToList();

            // 5 most recently added tenants
            ViewBag.RecentTenants = _context.Tenants
                .OrderByDescending(t => t.CreatedAt)
                .Take(5)
                .ToList();

            return View();
        }
    }
}
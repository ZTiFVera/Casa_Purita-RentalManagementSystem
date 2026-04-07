using Casa_Purita_RentalManagementSystem.Data;
using Casa_Purita_RentalManagementSystem.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Casa_Purita_RentalManagementSystem.Controllers
{

    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _context.Rooms.ToListAsync();
            var tenants = await _context.Tenants.ToListAsync();
            var contracts = await _context.Contracts
                                .Include(c => c.Tenant)
                                .Include(c => c.Room)
                                .ToListAsync();

            var expiredList = contracts.Where(c => c.IsExpired).ToList();
            foreach (var c in expiredList)
            {
                c.Status = "Expired";
                var room = rooms.FirstOrDefault(r => r.Id == c.RoomId);
                if (room != null) room.IsAvailable = true;
            }
            if (expiredList.Any())
                await _context.SaveChangesAsync();

            var activeContracts = contracts.Where(c => c.Status == "Active").ToList();
            var expiringContracts = contracts.Where(c => c.IsExpiringSoon).ToList();

            var vm = new DashboardViewModel
            {
                TotalRooms = rooms.Count,
                OccupiedRooms = rooms.Count(r => !r.IsAvailable),
                AvailableRooms = rooms.Count(r => r.IsAvailable),
                TotalTenants = tenants.Count,
                ActiveContracts = activeContracts.Count,
                ExpiringContracts = expiringContracts.Count,
                MonthlyRevenue = activeContracts.Sum(c => c.MonthlyRent),
                ExpiringContractsList = expiringContracts
            };

            return View(vm);
        }
    }
}
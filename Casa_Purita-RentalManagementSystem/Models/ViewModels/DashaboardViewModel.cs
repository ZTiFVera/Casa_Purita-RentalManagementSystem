namespace Casa_Purita_RentalManagementSystem.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalRooms { get; set; }
        public int AvailableRooms { get; set; }
        public int OccupiedRooms { get; set; }
        public int TotalTenants { get; set; }
        public int ActiveContracts { get; set; }
        public int ExpiringContracts { get; set; } // within 30 days
        public decimal MonthlyRevenue { get; set; }
        public List<Contract> RecentContracts { get; set; } = new();
    }
 
}

namespace Casa_Purita_RentalManagementSystem.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalRooms { get; set; }
        public int OccupiedRooms { get; set; }
        public int AvailableRooms { get; set; }
        public int TotalTenants { get; set; }
        public int ActiveContracts { get; set; }
        public int ExpiringContracts { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public double OccupancyRate =>
            TotalRooms == 0 ? 0 : Math.Round((double)OccupiedRooms / TotalRooms * 100, 1);
        public List<Contract> ExpiringContractsList { get; set; } = new();
    }
}
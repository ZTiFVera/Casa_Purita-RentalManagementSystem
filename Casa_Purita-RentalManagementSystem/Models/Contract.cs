using System.ComponentModel.DataAnnotations;

namespace Casa_Purita_RentalManagementSystem.Models
{
    public enum ContractStatus { Active, Expired, Terminated }

    public class Contract
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }
        public Room? Room { get; set; }

        [Required]
        public int TenantId { get; set; }
        public Tenant? Tenant { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Monthly Rent")]
        public decimal MonthlyRent { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Security Deposit")]
        public decimal SecurityDeposit { get; set; }

        public ContractStatus Status { get; set; } = ContractStatus.Active;

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Casa_Purita_RentalManagementSystem.Models
{
    public class Contract
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tenant")]
        public int TenantId { get; set; }

        [Required]
        [Display(Name = "Room")]
        public int RoomId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Monthly Rent (₱)")]
        public decimal MonthlyRent { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; } = "Active";

        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        [ForeignKey("TenantId")]
        public Tenant? Tenant { get; set; }

        [ForeignKey("RoomId")]
        public Room? Room { get; set; }

        public bool IsExpiringSoon =>
            EndDate <= DateTime.Now.AddDays(30) &&
            EndDate >= DateTime.Now &&
            Status == "Active";

        public bool IsExpired =>
            EndDate < DateTime.Now && Status == "Active";
    }

}
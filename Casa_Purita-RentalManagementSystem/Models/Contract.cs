using System.ComponentModel.DataAnnotations;

namespace Casa_Purita_RentalManagementSystem.Models
{
    public class Contract
    {
        public int Id { get; set; }

        [Required]
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        [Required]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Monthly Rent")]
        public decimal MonthlyRent { get; set; }

        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
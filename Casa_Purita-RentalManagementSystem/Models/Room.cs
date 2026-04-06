using System.ComponentModel.DataAnnotations;

namespace Casa_Purita_RentalManagementSystem.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Unit Number")]
        public string UnitNumber { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        [Display(Name = "Monthly Rate")]
        public decimal MonthlyRate { get; set; }

        public string Status { get; set; } = "Available";

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
using System.ComponentModel.DataAnnotations;

namespace Casa_Purita_RentalManagementSystem.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Monthly Rent (₱)")]
        public decimal MonthlyRent { get; set; }

        [Display(Name = "Floor")]
        public int Floor { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; } = true;

        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
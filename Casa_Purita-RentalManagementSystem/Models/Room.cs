using System.ComponentModel.DataAnnotations;

namespace Casa_Purita_RentalManagementSystem.Models
{
    public enum RoomStatus { Available, Occupied, Maintenance }

    public class Room
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Unit Number")]
        public string UnitNumber { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty; // Studio, 1BR, 2BR

        [Required]
        [DataType(DataType.Currency)]
        public decimal MonthlyRent { get; set; }

        public RoomStatus Status { get; set; } = RoomStatus.Available;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
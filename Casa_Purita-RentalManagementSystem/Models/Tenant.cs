using System.ComponentModel.DataAnnotations;

namespace Casa_Purita_RentalManagementSystem.Models
{
    public class Tenant
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "ID Number")]
        public string IdNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
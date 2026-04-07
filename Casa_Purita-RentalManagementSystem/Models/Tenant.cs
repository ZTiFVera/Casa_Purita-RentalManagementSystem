using System.ComponentModel.DataAnnotations;

namespace Casa_Purita_RentalManagementSystem.Models
{
    public class Tenant
    {
         
            public int Id { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; } = string.Empty;

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; } = string.Empty;

            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Phone]
            [Display(Name = "Phone Number")]
            public string? Phone { get; set; }

            [Display(Name = "Emergency Contact")]
            public string? EmergencyContact { get; set; }

            [Display(Name = "Address")]
            public string? Address { get; set; }

            [Display(Name = "Date Registered")]
            public DateTime DateRegistered { get; set; } = DateTime.Now;

            public ICollection<Contract> Contracts { get; set; } = new List<Contract>();

            public string FullName => $"{FirstName} {LastName}";
        }
   
}   
using Microsoft.AspNetCore.Identity;

namespace Casa_Purita_RentalManagementSystem.Models
{
   
        public class ApplicationUser : IdentityUser
        {
            public string FullName { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; } = DateTime.Now;
        }
   
}

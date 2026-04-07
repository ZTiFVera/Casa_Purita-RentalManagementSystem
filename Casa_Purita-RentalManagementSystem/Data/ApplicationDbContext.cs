using Casa_Purita_RentalManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Casa_Purita_RentalManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Room>().HasData(
                new Room { Id = 1, RoomNumber = "101", Type = "Studio", MonthlyRent = 8000, Floor = 1, IsAvailable = true, Description = "Cozy studio unit" },
                new Room { Id = 2, RoomNumber = "102", Type = "1BR", MonthlyRent = 12000, Floor = 1, IsAvailable = true, Description = "1 Bedroom unit" },
                new Room { Id = 3, RoomNumber = "201", Type = "2BR", MonthlyRent = 18000, Floor = 2, IsAvailable = true, Description = "2 Bedroom unit" },
                new Room { Id = 4, RoomNumber = "202", Type = "Studio", MonthlyRent = 8500, Floor = 2, IsAvailable = true, Description = "Studio with balcony" }
            );
        }
    }
}
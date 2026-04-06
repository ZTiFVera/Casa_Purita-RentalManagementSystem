using Casa_Purita_RentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Casa_Purita_RentalManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Contract> Contracts { get; set; }
    }
}
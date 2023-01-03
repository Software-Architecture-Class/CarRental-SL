using CarRentalServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalServiceAPI.Data
{
    public class DbConnectionContext : DbContext
    {
        public DbConnectionContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().ToTable("Users").Property(c => c.userType).HasConversion<string>();

            modelBuilder.Entity<Car>().ToTable("Cars");
            modelBuilder.Entity<Car>().ToTable("Cars").Property(c => c.gearboxType).HasConversion<string>();
            modelBuilder.Entity<Car>().ToTable("Cars").Property(c => c.carCategory).HasConversion<string>();

            modelBuilder.Entity<CarEvent>().ToTable("CarEvents");
            modelBuilder.Entity<CarEvent>().ToTable("CarEvents").Property(c => c.rentType).HasConversion<string>();
            modelBuilder.Entity<CarEvent>().ToTable("CarEvents").Property(c => c.Discount).HasConversion<string>();

            modelBuilder.Entity<AuthenticationCredentials>().ToTable("AuthenticationCredentials");           
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarEvent> CarEvents { get; set; }
        public DbSet<AuthenticationCredentials> AuthenticationCredentials { get; set; }
    }
}

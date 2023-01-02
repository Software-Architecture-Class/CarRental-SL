﻿using CarRentalServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalServiceAPI.Data
{
    public class DbConnectionContext : DbContext
    {
        public DbConnectionContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Car>? Cars { get; set; }
        public DbSet<CarEvent>? CarEvents { get; set; }
    }
}

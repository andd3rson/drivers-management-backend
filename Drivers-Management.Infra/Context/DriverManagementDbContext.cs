using Microsoft.EntityFrameworkCore;
using Drivers_Management.Domain.Models;

namespace Drivers_Management.Infra.Context
{
    public class DriverManagementDbContext : DbContext
    {
        public DriverManagementDbContext(DbContextOptions<DriverManagementDbContext> opt)
         : base(opt)
        {


        }


        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DriverManagementDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
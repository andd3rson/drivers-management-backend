using Drivers_Management.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drivers_Management.Infra.Mappings
{
    public class DriverVehicleMapping : IEntityTypeConfiguration<DriverVehicle>
    {
        public void Configure(EntityTypeBuilder<DriverVehicle> modelBuilder)
        {
            modelBuilder
                   .HasKey(bc => new { bc.DriversId, bc.VehiclesId });

            
            modelBuilder
                .HasOne(bc => bc.Drivers)
                .WithMany(b => b.Vehicles)
                .HasForeignKey(bc => bc.DriversId);


            modelBuilder
                .HasOne(bc => bc.Vehicles)
                .WithMany(b => b.Drivers)
                .HasForeignKey(bc => bc.VehiclesId);

        }
    }
}
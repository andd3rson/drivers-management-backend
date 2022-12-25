using Drivers_Management.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drivers_Management.Infra.Mappings
{
    public class DriverMappings : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("tb_drivers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                    .ValueGeneratedOnAdd();
            builder.HasIndex(x => x.Cpf);

            builder.Property(x => x.Cpf)
                   .HasColumnType("varchar(11)");


            builder.Property(x => x.Name)
                 .HasColumnType("varchar(50)")
                .IsRequired();

            builder.HasMany(x => x.Vehicles)
                .WithMany(x => x.Drivers)
                .UsingEntity<DriverVehicle>(x => x.ToTable("tb_driver_vehicle"));

        }
    }
}
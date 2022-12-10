using Drivers_Management.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drivers_Management.Infra.Mappings
{
    public class VehicleMappings : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Plate);

            builder.Property(x => x.Plate)
                   .HasColumnType("varchar(6)");

            builder.Property(x => x.Brand)
               .HasColumnType("varchar(20)")
              .IsRequired();

            // builder.Property(x => x.CreatedAt)
            //     .ValueGeneratedOnAdd()
            //     .HasValueGenerator(typeof(DateTime));

            // builder.Property(x => x.UpdateAt)
            //         .ValueGeneratedOnUpdate();

        }
    }
}
using Drivers_Management.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drivers_Management.Infra.Mappings
{
    public class DriverMappings : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Cpf);

            builder.Property(x => x.Cpf)
                   .HasColumnType("varchar(11)");


            builder.Property(x => x.Name)
                 .HasColumnType("varchar(50)")
                .IsRequired();

            // builder.Property(x => x.CreatedAt)
            //     .ValueGeneratedOnAdd()
            //     .HasValueGenerator(typeof(DateTime));

            // builder.Property(x => x.UpdateAt)
            //         .ValueGeneratedOnUpdate();

            builder.Ignore(x => x.Vehicles);

            builder.HasMany(x => x.Vehicles)
                    .WithMany(x => x.Drivers);

        }
    }
}
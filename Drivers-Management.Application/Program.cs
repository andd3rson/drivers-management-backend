using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Infra.Context;
using Drivers_Management.Infra.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
       .AddDbContext<DriverManagementDbContext>(
           optionsAction =>
           optionsAction.UseSqlServer("Server=localhost,1433;Database=drivers;user=sa;password=Pass123!;",
            b => b.MigrationsAssembly("Drivers-Management.Application")
           ));

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Services;
using Drivers_Management.Domain.Validators;
using Drivers_Management.Infra.Context;
using Drivers_Management.Infra.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services
       .AddDbContext<DriverManagementDbContext>(
           optionsAction =>
           optionsAction.UseSqlServer("Server=localhost;Database=Drivers;user=sa;password=Pass123!;",
            b => b.MigrationsAssembly("Drivers-Management.Application")
           ));

builder.Services.AddScoped<IValidator<Driver>, DriverValidator>();
builder.Services.AddScoped<IValidator<Vehicle>, VehicleValidator>();
builder.Services.AddScoped<IDriverServices, DriverServices>();
builder.Services.AddScoped<IVehicleServices, VehicleServices>();
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

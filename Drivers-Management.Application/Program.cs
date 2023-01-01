using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Services;
using Drivers_Management.Domain.Validators;
using Drivers_Management.Infra.Context;
using Drivers_Management.Infra.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

#region Data context
builder.Services
       .AddDbContext<DriverManagementDbContext>(
           optionsAction =>
           optionsAction.UseSqlServer("Data Source=db_drivers;Initial Catalog=Drivers;user=sa;password=Pass123!;Integrated Security=False;",
            b => b.MigrationsAssembly("Drivers-Management.Application")
           ));

builder.Services.AddDefaultIdentity<User>(
    opt =>
    {
        opt.User.RequireUniqueEmail = true;
    }
).AddEntityFrameworkStores<DriverManagementDbContext>();

#endregion

builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<IValidator<Driver>, DriverValidator>();
builder.Services.AddScoped<IValidator<Vehicle>, VehicleValidator>();
builder.Services.AddScoped<IDriverServices, DriverServices>();
builder.Services.AddScoped<IVehicleServices, VehicleServices>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DriverManagementDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

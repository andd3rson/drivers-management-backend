using System.Text;
using Drivers_Management.Application;
using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Services;
using Drivers_Management.Domain.Validators;
using Drivers_Management.Infra.Context;
using Drivers_Management.Infra.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

#region Data context
builder.Services
       .AddDbContext<DriverManagementDbContext>(
           optionsAction =>
           optionsAction.UseSqlServer("Data Source=localhost;Initial Catalog=Drivers;user=sa;password=Pass123!;Integrated Security=False;",
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
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Description = "It's Driver Management documentation. All endpoints is here.",
        Contact = new OpenApiContact
        {
            Name = "Anderson Conceição",
            Email = "andersonconceiicao@gmail.com"
        }
    });
});
var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.SaveToken = true;
    jwt.RequireHttpsMetadata = false;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateAudience = false,
        ValidateIssuer = false

    };
});


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

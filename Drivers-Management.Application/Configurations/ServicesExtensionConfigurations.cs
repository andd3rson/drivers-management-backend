using System.Text;
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

namespace Drivers_Management.Application.Configurations
{
    public class ServicesExtensionConfigurations
    {
        public static void AddInversionDependecy(IServiceCollection services)
        {

            services.AddScoped<IValidator<Driver>, DriverValidator>();
            services.AddScoped<IValidator<Vehicle>, VehicleValidator>();
            services.AddScoped<IDriverServices, DriverServices>();
            services.AddScoped<IUserServices, UserServices>();

            services.AddScoped<IVehicleServices, VehicleServices>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Description = "It's Driver Management Documentation. All endpoints is here.",
                    Contact = new OpenApiContact
                    {
                        Name = "Anderson Conceição",
                        Email = "andersonconceiicao@gmail.com"
                    }
                });
            });
        }

        public static void AddConfigurationDbContext(IServiceCollection services, string conn)
        {

            services
                    .AddDbContext<DriverManagementDbContext>(
                        optionsAction =>
                        optionsAction.UseSqlServer(conn,
                         b => b.MigrationsAssembly("Drivers-Management.Application")
                        ));

            services.AddDefaultIdentity<User>(
                 opt =>
                 {
                     opt.User.RequireUniqueEmail = true;
                 }
             ).AddEntityFrameworkStores<DriverManagementDbContext>();
        }

        public static void AddConfigurationAuth(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(options =>
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

        }
    }
}
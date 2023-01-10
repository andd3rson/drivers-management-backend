using System.Text;
using Drivers_Management.Application.Middleware;
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
            services.AddScoped<UserServices>();

            services.AddScoped<IVehicleServices, VehicleServices>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            
            services.AddSingleton<IUriServices>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor?.HttpContext?.Request;
                var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
                return new UriServices(uri);
            });
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
            
            services.AddTransient<GlobalErrorExceptions>();
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

        public static void AddConfigurationAuth(IServiceCollection services, string secretKey)
        {

            var key = Encoding.ASCII.GetBytes(secretKey);
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
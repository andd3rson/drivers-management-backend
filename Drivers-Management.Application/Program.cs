using Drivers_Management.Application;
using Drivers_Management.Application.Configurations;
using Drivers_Management.Application.Middleware;
using Drivers_Management.Infra.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var conn = builder.Configuration["ConnectionStrings:Conn"].ToString();
ServicesExtensionConfigurations.AddConfigurationDbContext(builder.Services, conn);


ServicesExtensionConfigurations.AddInversionDependecy(builder.Services);

builder.Services.AddEndpointsApiExplorer();


var key = builder.Configuration["SecretKey"];
builder.Services.Configure<Settings>(opt => opt.SecretKey = key);
ServicesExtensionConfigurations.AddConfigurationAuth(builder.Services, key);


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
app.UseMiddleware<GlobalErrorExceptions>();
app.MapControllers();
app.Run();

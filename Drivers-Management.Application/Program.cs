using Drivers_Management.Application.Configurations;
using Drivers_Management.Infra.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var conn = builder.Configuration["ConnectionStrings:Conn"].ToString();
ServicesExtensionConfigurations.AddConfigurationDbContext(builder.Services, conn);


ServicesExtensionConfigurations.AddInversionDependecy(builder.Services);

builder.Services.AddEndpointsApiExplorer();


ServicesExtensionConfigurations.AddConfigurationAuth(builder.Services);


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

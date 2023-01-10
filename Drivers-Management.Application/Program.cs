using Drivers_Management.Application;
using Drivers_Management.Application.Configurations;
using Drivers_Management.Application.Middleware;
using Drivers_Management.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);


ConfigureLogging();
builder.Host.UseSerilog();

var conn = builder.Configuration["ConnectionStrings:Conn"].ToString();
ServicesExtensionConfigurations.AddConfigurationDbContext(builder.Services, conn);


ServicesExtensionConfigurations.AddInversionDependecy(builder.Services);




builder.Services.AddControllers()
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
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


void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly()?.GetName().Name?.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}
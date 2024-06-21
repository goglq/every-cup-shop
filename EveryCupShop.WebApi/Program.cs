using EveryCupShop.Extensions;
using EveryCupShop.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Server is building...");
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Services.AddSerilog((services, lc) => lc
        .ReadFrom.Configuration(builder.Configuration));

    builder.Services
        .AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

    builder.Services
        .AddControllers();
    
    builder.SetupCors();
    builder.SetupJwt();

    builder.Services.AddAppRepositories();
    builder.Services.AddAppServices();
    builder.Services.AddAppValidation();
    builder.Services.AddAppMapperProfiles();

    var app = builder.Build();
    Log.Information("Server finished building");

    app.UseCors("Default");
    
    app.UseSerilogRequestLogging();
    
    app.MapControllers();

    Log.Information("Server is running");
    app.Run();
}
catch (Exception ex) when (ex.GetType().Name != "HostAbortedException")
{
    Log.Fatal(ex, @"Unhandled exception {ExceptionType}", ex.GetType().Name);
}
finally
{
    Log.Information("Server is shutting down");
    Log.CloseAndFlush();
}
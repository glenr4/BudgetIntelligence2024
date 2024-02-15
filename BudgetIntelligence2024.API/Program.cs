using BudgetIntelligence2024.API;
using BudgetIntelligence2024.API.Auth;
using BudgetIntelligence2024.Application;
using BudgetIntelligence2024.Persistence.DependencyInjection;
using FastEndpoints;
using Mapster;
using Serilog;
using Serilog.Filters;


AddSerilog();

try
{
    var builder = WebApplication.CreateSlimBuilder(args);

    builder.Host.UseSerilog();
    
    AddServices(builder);

    var app = builder.Build();

    AddEndpoints(app);

    app.UseCustomExceptionHandler(env: builder.Environment);

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

void AddSerilog()
{
    var configuration = 
        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
            .Build();

    var logger = 
        new LoggerConfiguration()
            // UseCustomExceptionHandler logs unhandled exceptions, so don't need the default middleware
            // Could not get this filter to work in appsettings    
            .Filter.ByExcluding(Matching.FromSource<Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware>())
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

    Log.Logger = logger;
}

void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddFastEndpoints();

    builder.Services.AddScoped<IUserContext, UserContext>();
    builder.Services.AddPersistence();
    builder.Services.AddApplication();
    builder.Services.AddMapster();
}

void AddEndpoints(WebApplication app)
{
    app.UseFastEndpoints();

    app.MapGet("/ping", () => "pong");
}
using BudgetIntelligence2024.API.Auth;
using BudgetIntelligence2024.Application;
using BudgetIntelligence2024.Persistence.DependencyInjection;
using FastEndpoints;
using Mapster;
using Serilog;
using Serilog.Events;


AddSerilog();

try
{
    var builder = WebApplication.CreateSlimBuilder(args);

    builder.Host.UseSerilog();

    AddServices(builder);

    var app = builder.Build();

    AddEndpoints(app);

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
    var outputTemplate = "[{Timestamp:HH:mm:ss.fff} {Level:u3} {TraceId}] {Message:lj}{NewLine}{Exception}";

    using var log = 
        new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information, outputTemplate: outputTemplate)
            .WriteTo.File("logs/BudgetIntelligence2024.log", rollingInterval: RollingInterval.Day, outputTemplate: outputTemplate)
            .CreateLogger();
    
    Log.Logger = log;

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
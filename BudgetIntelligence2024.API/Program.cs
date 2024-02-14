using BudgetIntelligence2024.API.Auth;
using BudgetIntelligence2024.Application;
using BudgetIntelligence2024.Persistence.DependencyInjection;
using FastEndpoints;
using Mapster;
using Serilog;

var builder = WebApplication.CreateSlimBuilder(args);

AddSerilog(builder);

AddServices(builder);

var app = builder.Build();

AddEndpoints(app);

app.Run();

void AddSerilog(WebApplicationBuilder builder)
{
    using var log = 
        new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/BudgetIntelligence2024.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    
    Log.Logger = log;

    builder.Host.UseSerilog();
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
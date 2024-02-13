using BudgetIntelligence2024.API.Auth;
using BudgetIntelligence2024.Application;
using BudgetIntelligence2024.Persistence.DependencyInjection;
using FastEndpoints;
using Mapster;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddFastEndpoints();

builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddPersistence();
builder.Services.AddApplication();
builder.Services.AddMapster();

var app = builder.Build();

app.UseFastEndpoints();

app.MapGet("/ping", () => "pong");

app.Run();


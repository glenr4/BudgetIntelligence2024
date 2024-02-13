using BudgetIntelligence2024.API.Auth;
using BudgetIntelligence2024.Application;
using BudgetIntelligence2024.Persistence.DependencyInjection;
using FastEndpoints;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddScoped<ITransactionImporter, TransactionImporterNAB>();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddPersistence();

var app = builder.Build();

app.UseFastEndpoints();

app.MapGet("/ping", () => "pong");

app.Run();


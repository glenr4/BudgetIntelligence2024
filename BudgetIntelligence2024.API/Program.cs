using BudgetIntelligence2024.API.Auth;
using BudgetIntelligence2024.API.TestEndpoint;
using BudgetIntelligence2024.Application;
using BudgetIntelligence2024.Persistence.DependencyInjection;
using FastEndpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddScoped<ITransactionImporter, TransactionImporterNAB>();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddPersistence();

// https://learn.microsoft.com/en-us/dotnet/core/compatibility/serialization/7.0/reflection-fallback
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

app.UseFastEndpoints();

app.MapGet("/ping", () => "pong");

app.Run();

[JsonSerializable(typeof(object))]
[JsonSerializable(typeof(FastEndpoints.ErrorResponse))]
[JsonSerializable(typeof(TestRequest))]

internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
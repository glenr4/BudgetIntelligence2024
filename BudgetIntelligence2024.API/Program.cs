using BudgetIntelligence2024.Application;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddScoped<ITransactionImporter, TransactionImporterNAB>();

var app = builder.Build();

app.UseFastEndpoints();

app.Run();


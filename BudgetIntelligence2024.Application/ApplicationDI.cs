using Microsoft.Extensions.DependencyInjection;

namespace BudgetIntelligence2024.Application;

public static class ApplicationDI
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITransactionParser, TransactionParserNAB>();
    }
}

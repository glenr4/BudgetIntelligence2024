using BudgetIntelligence2024.Persistence.StoredProcedures;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetIntelligence2024.Persistence.DependencyInjection;

public static class PersistenceDI
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IAddDistinctToTransactions, SpAddDistinctToTransactions>();
        services.AddScoped<ITransactionStore, SqlTransactionStore>();
    }
}

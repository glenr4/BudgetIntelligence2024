using BudgetIntelligence2024.Persistence.DBContext;
using BudgetIntelligence2024.Persistence.StoredProcedures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetIntelligence2024.Persistence.DependencyInjection;

public static class PersistenceDI
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<BudgetIntelligenceDbContext>(
            options => options.UseSqlServer("name=ConnectionStrings:BudgetIntelligence"));
        services.AddScoped<IAddDistinctToTransactions, SpAddDistinctToTransactions>();
        services.AddScoped<ITransactionStore, SqlTransactionStore>();
    }
}

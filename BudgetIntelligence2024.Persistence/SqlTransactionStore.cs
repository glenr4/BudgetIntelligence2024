using BudgetIntelligence2024.Persistence.DBContext;
using BudgetIntelligence2024.Persistence.StoredProcedures;

namespace BudgetIntelligence2024.Persistence;

internal class SqlTransactionStore : ITransactionStore
{
    private readonly BudgetIntelligenceDbContext _ctx;
    private readonly IAddDistinctToTransactions _addDistinctToTransactions;

    public SqlTransactionStore(BudgetIntelligenceDbContext ctx, IAddDistinctToTransactions addDistinctToTransactions)
    {
        _ctx = ctx;
        _addDistinctToTransactions = addDistinctToTransactions;
    }

    public async Task<int> AddAsync(IEnumerable<TransactionStaging> transactions, int userId)
    {
        //_logger.Information($"{nameof(SqlTransactionStagingStore)} adding transactions");

        _ctx.TransactionStagings.AddRange(transactions);

        await _ctx.SaveChangesAsync();

        return await _addDistinctToTransactions.Execute(userId);
    }
}

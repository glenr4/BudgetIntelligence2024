using BudgetIntelligence2024.Persistence.DBContext;

namespace BudgetIntelligence2024.Persistence;

public interface ITransactionStore
{
    Task<int> AddAsync(IEnumerable<TransactionStaging> transactions, int userId);
}
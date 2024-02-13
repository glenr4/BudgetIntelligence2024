namespace BudgetIntelligence2024.Persistence.StoredProcedures;

public interface IAddDistinctToTransactions
{
    Task<int> Execute(int userId);
}
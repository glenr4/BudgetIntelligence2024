namespace BudgetIntelligence2024.Persistence.StoredProcedures;

internal interface IAddDistinctToTransactions
{
    Task<int> Execute(int userId);
}
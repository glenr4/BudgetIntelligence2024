
namespace BudgetIntelligence2024.Application
{
    public interface ITransactionImporter
    {
        int ImportCSV(Stream file, int userId, int accountId);
    }
}
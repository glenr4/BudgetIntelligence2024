
namespace BudgetIntelligence2024.Application
{
    public interface ITransactionParser
    {
        IEnumerable<ImportedTransactionDto> ParseCSV(string fileName, Stream file, int userId, int accountId);
    }
}
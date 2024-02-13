
namespace BudgetIntelligence2024.Application
{
    public interface ITransactionParser
    {
        IEnumerable<ImportedTransactionDto> ParseCSV(Stream file, int userId, int accountId);
    }
}
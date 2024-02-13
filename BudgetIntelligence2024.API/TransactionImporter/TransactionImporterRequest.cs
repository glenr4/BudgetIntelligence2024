namespace BudgetIntelligence2024.API.TransactionImporter
{
    public class TransactionImporterRequest
    {
        public int AccountId { get; set; }
        public IFormFile File { get; set; }
    }
}
using BudgetIntelligence2024.Application;
using FastEndpoints;

namespace BudgetIntelligence2024.API.TransactionImporter
{
    public class TransactionImporterEndpoint : Endpoint<TransactionImporterRequest>
    {
        private readonly ITransactionImporter _transactionImporter;

        public TransactionImporterEndpoint(ITransactionImporter transactionImporter)
        {
            _transactionImporter = transactionImporter;
        }

        public override void Configure()
        {
            Post("import/{accountId}");
            AllowFileUploads();
        }

        public override Task HandleAsync(TransactionImporterRequest req, CancellationToken ct)
        {
            return base.HandleAsync(req, ct);
        }
    }
}

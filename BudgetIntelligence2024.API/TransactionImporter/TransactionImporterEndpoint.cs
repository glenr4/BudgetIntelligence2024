using BudgetIntelligence2024.API.Auth;
using BudgetIntelligence2024.Application;
using FastEndpoints;

namespace BudgetIntelligence2024.API.TransactionImporter
{
    public class TransactionImporterEndpoint : Endpoint<TransactionImporterRequest>
    {
        private readonly ITransactionImporter _transactionImporter;
        private readonly IUserContext _userContext;

        public TransactionImporterEndpoint(ITransactionImporter transactionImporter, IUserContext userContext)
        {
            _transactionImporter = transactionImporter;
            _userContext = userContext;
        }

        public override void Configure()
        {
            Post("import-transactions/{accountId}");
            AllowFileUploads();
            
            AllowAnonymous();   //TODO: implement auth
        }

        public override Task HandleAsync(TransactionImporterRequest req, CancellationToken ct)
        {
            using (var file = req.File.OpenReadStream())
            {
                _transactionImporter.ImportCSV(file, _userContext.UserId, req.AccountId);
            }

            return Task.CompletedTask;
        }
    }
}

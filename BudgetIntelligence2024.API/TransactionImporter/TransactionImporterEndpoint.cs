using BudgetIntelligence2024.API.Auth;
using BudgetIntelligence2024.Application;
using BudgetIntelligence2024.Persistence;
using BudgetIntelligence2024.Persistence.DBContext;
using FastEndpoints;
using Mapster;

namespace BudgetIntelligence2024.API.TransactionImporter
{
    public class TransactionImporterEndpoint : Endpoint<TransactionImporterRequest>
    {
        private readonly ITransactionParser _transactionParser;
        private readonly ITransactionStore _transactionStore;
        private readonly IUserContext _userContext;

        public TransactionImporterEndpoint(ITransactionParser transactionParser, ITransactionStore transactionStore, IUserContext userContext)
        {
            _transactionParser = transactionParser;
            _transactionStore = transactionStore;
            _userContext = userContext;
        }

        public override void Configure()
        {
            Post("import-transactions/{accountId}");
            AllowFileUploads();
            
            AllowAnonymous();   //TODO: implement auth
        }

        public async override Task HandleAsync(TransactionImporterRequest req, CancellationToken ct)
        {
            using (var file = req.File.OpenReadStream())
            {
                var transactions = _transactionParser.ParseCSV(file, _userContext.UserId, req.AccountId);
            
                await _transactionStore.AddAsync(transactions.Adapt<IEnumerable<TransactionStaging>>(), _userContext.UserId);
            }
        }
    }
}

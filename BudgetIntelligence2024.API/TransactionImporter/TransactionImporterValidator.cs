using FastEndpoints;
using FluentValidation;

namespace BudgetIntelligence2024.API.TransactionImporter
{
    public class TransactionImporterValidator : Validator<TransactionImporterRequest>
    {
        public TransactionImporterValidator()
        {
            RuleFor(x => x.File)
                .NotNull()
                .WithMessage("Where is the file?");
        }
    }
}

namespace BudgetIntelligence2024.Application;

public partial class ImportedTransactionDto
{
    public int UserId { get; set; }

    public DateOnly Date { get; set; }

    public decimal Amount { get; set; }

    public string? Type { get; set; }

    public string Description { get; set; } = null!;

    public decimal? Balance { get; set; }

    public int AccountId { get; set; }
}

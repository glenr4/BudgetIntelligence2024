using System;
using System.Collections.Generic;

namespace BudgetIntelligence2024.Persistence;

public partial class TransactionList
{
    public int TransactionId { get; set; }

    public int UserId { get; set; }

    public DateOnly Date { get; set; }

    public decimal Amount { get; set; }

    public string? Type { get; set; }

    public string Description { get; set; } = null!;

    public decimal? Balance { get; set; }

    public int AccountId { get; set; }

    public int ImportOrder { get; set; }

    public int? CategoryId { get; set; }

    public bool IsDuplicate { get; set; }

    public int? SplitNo { get; set; }

    public bool? IsSplitOrigin { get; set; }
}

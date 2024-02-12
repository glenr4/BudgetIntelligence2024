using System;
using System.Collections.Generic;

namespace BudgetIntelligence2024.Persistence.DBContext;

public partial class FutureTransaction
{
    public int FutureTransactionId { get; set; }

    public int UserId { get; set; }

    public DateOnly Date { get; set; }

    public decimal Amount { get; set; }

    public string? Type { get; set; }

    public string Description { get; set; } = null!;

    public decimal? Balance { get; set; }

    public int AccountId { get; set; }

    public int? CategoryId { get; set; }

    public bool IsDuplicate { get; set; }

    public int? SplitNo { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Category? Category { get; set; }

    public virtual User User { get; set; } = null!;
}

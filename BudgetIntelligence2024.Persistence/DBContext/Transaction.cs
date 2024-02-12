using System;
using System.Collections.Generic;

namespace BudgetIntelligence2024.Persistence.DBContext;

public partial class Transaction
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

    public virtual Account Account { get; set; } = null!;

    public virtual Category? Category { get; set; }

    public virtual ICollection<EditDistance> EditDistanceRelateds { get; set; } = new List<EditDistance>();

    public virtual ICollection<EditDistance> EditDistanceTransactions { get; set; } = new List<EditDistance>();

    public virtual ICollection<Phrase> Phrases { get; set; } = new List<Phrase>();

    public virtual User User { get; set; } = null!;
}

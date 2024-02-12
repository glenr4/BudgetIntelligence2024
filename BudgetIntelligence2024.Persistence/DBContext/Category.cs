using System;
using System.Collections.Generic;

namespace BudgetIntelligence2024.Persistence.DBContext;

public partial class Category
{
    public int CategoryId { get; set; }

    public int? ParentId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsTransfer { get; set; }

    public string Type { get; set; } = null!;

    public string? Typo { get; set; }

    public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

    public virtual ICollection<FutureTransaction> FutureTransactions { get; set; } = new List<FutureTransaction>();

    public virtual ICollection<Category> InverseParent { get; set; } = new List<Category>();

    public virtual Category? Parent { get; set; }

    public virtual ICollection<Phrase> Phrases { get; set; } = new List<Phrase>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}

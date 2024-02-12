using System;
using System.Collections.Generic;

namespace BudgetIntelligence2024.Persistence.DBContext;

public partial class Account
{
    public int AccountId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsScenario { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }

    public virtual ICollection<AccountBalance> AccountBalances { get; set; } = new List<AccountBalance>();

    public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

    public virtual ICollection<FutureTransaction> FutureTransactions { get; set; } = new List<FutureTransaction>();

    public virtual ICollection<TransactionStaging> TransactionStagings { get; set; } = new List<TransactionStaging>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User User { get; set; } = null!;
}

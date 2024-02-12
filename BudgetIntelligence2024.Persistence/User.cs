using System;
using System.Collections.Generic;

namespace BudgetIntelligence2024.Persistence;

public partial class User
{
    public int UserId { get; set; }

    public string LoginName { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public Guid? Salt { get; set; }

    public virtual ICollection<AccountBalance> AccountBalances { get; set; } = new List<AccountBalance>();

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

    public virtual ICollection<EditDistance> EditDistances { get; set; } = new List<EditDistance>();

    public virtual ICollection<FutureTransaction> FutureTransactions { get; set; } = new List<FutureTransaction>();

    public virtual ICollection<Phrase> Phrases { get; set; } = new List<Phrase>();

    public virtual ICollection<TransactionStaging> TransactionStagings { get; set; } = new List<TransactionStaging>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}

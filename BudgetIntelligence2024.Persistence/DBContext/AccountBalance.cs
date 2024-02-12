using System;
using System.Collections.Generic;

namespace BudgetIntelligence2024.Persistence.DBContext;

public partial class AccountBalance
{
    public int AccountBalanceId { get; set; }

    public DateOnly Date { get; set; }

    public int UserId { get; set; }

    public int AccountId { get; set; }

    public decimal? Balance { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

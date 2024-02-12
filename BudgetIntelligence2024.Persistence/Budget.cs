using System;
using System.Collections.Generic;

namespace BudgetIntelligence2024.Persistence;

public partial class Budget
{
    public int BudgetId { get; set; }

    public int UserId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int PeriodTypeId { get; set; }

    public decimal Amount { get; set; }

    public string Description { get; set; } = null!;

    public int CategoryId { get; set; }

    public int AccountId { get; set; }

    public string? Comment { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }

    public decimal? PeriodTypeQty { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual PeriodType PeriodType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

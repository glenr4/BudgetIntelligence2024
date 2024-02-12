using System;
using System.Collections.Generic;

namespace BudgetIntelligence2024.Persistence.DBContext;

public partial class PeriodType
{
    public int PeriodTypeId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int Qty { get; set; }

    public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();
}

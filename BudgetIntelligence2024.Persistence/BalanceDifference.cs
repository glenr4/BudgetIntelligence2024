using System;
using System.Collections.Generic;

namespace BudgetIntelligence2024.Persistence;

public partial class BalanceDifference
{
    public int BalanceDifferenceId { get; set; }

    public int? BalanceDifferenceType { get; set; }

    public DateOnly Date { get; set; }

    public decimal Difference { get; set; }

    public int UserId { get; set; }
}

using System;
using System.Collections.Generic;

namespace BudgetIntelligence2024.Persistence.DBContext;

public partial class EditDistance
{
    public int EditDistanceId { get; set; }

    public int TransactionId { get; set; }

    public int RelatedId { get; set; }

    public int EditDistance1 { get; set; }

    public int UserId { get; set; }

    public virtual Transaction Related { get; set; } = null!;

    public virtual Transaction Transaction { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

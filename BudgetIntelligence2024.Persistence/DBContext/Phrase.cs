using System;
using System.Collections.Generic;

namespace BudgetIntelligence2024.Persistence.DBContext;

public partial class Phrase
{
    public int PhraseId { get; set; }

    public string PhraseText { get; set; } = null!;

    public int? TransactionId { get; set; }

    public int? UserId { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Transaction? Transaction { get; set; }

    public virtual User? User { get; set; }
}

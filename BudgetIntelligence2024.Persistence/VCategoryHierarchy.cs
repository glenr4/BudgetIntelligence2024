using System;
using System.Collections.Generic;

namespace BudgetIntelligence2024.Persistence;

public partial class VCategoryHierarchy
{
    public int CategoryId { get; set; }

    public int? ParentId { get; set; }

    public string Name { get; set; } = null!;

    public int? ChildId { get; set; }

    public string? ChildName { get; set; }
}

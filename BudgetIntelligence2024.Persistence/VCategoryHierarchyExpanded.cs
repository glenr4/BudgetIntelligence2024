using System;
using System.Collections.Generic;

namespace BudgetIntelligence2024.Persistence;

public partial class VCategoryHierarchyExpanded
{
    public int RootId { get; set; }

    public string RootName { get; set; } = null!;

    public int? Level1Id { get; set; }

    public string? Level1Name { get; set; }

    public int? Level2Id { get; set; }

    public string? Level2Name { get; set; }

    public int? Level3Id { get; set; }

    public string? Level3Name { get; set; }
}

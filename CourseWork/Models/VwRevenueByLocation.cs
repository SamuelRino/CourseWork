using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class VwRevenueByLocation
{
    public string Address { get; set; } = null!;

    public string? BuildingName { get; set; }

    public int? SalesCount { get; set; }

    public decimal? TotalRevenue { get; set; }
}

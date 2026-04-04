using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class VwSalesByMachine
{
    public string SerialNumber { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int? TotalSales { get; set; }

    public decimal? TotalRevenue { get; set; }

    public decimal? AvgPrice { get; set; }
}

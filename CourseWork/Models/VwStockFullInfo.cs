using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class VwStockFullInfo
{
    public string SerialNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string Category { get; set; } = null!;

    public int Quantity { get; set; }

    public int? MinLevel { get; set; }

    public string StockStatus { get; set; } = null!;
}

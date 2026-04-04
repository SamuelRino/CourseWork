using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class VwLowStockAlert
{
    public string SerialNumber { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public int Quantity { get; set; }

    public int? MinLevel { get; set; }

    public int? NeedToLoad { get; set; }
}

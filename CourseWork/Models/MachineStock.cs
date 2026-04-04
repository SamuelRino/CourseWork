using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class MachineStock
{
    public int StockId { get; set; }

    public int? MachineId { get; set; }

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public int? MinLevel { get; set; }

    public virtual VendingMachine? Machine { get; set; }

    public virtual Product? Product { get; set; }
}

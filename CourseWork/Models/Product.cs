using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public decimal BasePrice { get; set; }

    public int? Barcode { get; set; }

    public int CategoryId { get; set; }

    public virtual DictProductCategory Category { get; set; } = null!;

    public virtual ICollection<MachineStock> MachineStocks { get; set; } = new List<MachineStock>();

    public virtual ICollection<RestockLog> RestockLogs { get; set; } = new List<RestockLog>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}

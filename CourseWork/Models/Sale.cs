using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public int? MachineId { get; set; }

    public int? ProductId { get; set; }

    public DateTime? SaleDate { get; set; }

    public decimal SalePrice { get; set; }

    public int MethodId { get; set; }

    public virtual VendingMachine? Machine { get; set; }

    public virtual DictPaymentMethod Method { get; set; } = null!;

    public virtual Product? Product { get; set; }
}

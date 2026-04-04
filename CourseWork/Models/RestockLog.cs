using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class RestockLog
{
    public int RestockId { get; set; }

    public int? EmployeeId { get; set; }

    public int? MachineId { get; set; }

    public int? ProductId { get; set; }

    public int QuantityAdded { get; set; }

    public DateTime? RestockDate { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual VendingMachine? Machine { get; set; }

    public virtual Product? Product { get; set; }
}

using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class DictMachineStatus
{
    public int StatusId { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}

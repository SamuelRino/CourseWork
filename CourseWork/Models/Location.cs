using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string Address { get; set; } = null!;

    public string? BuildingName { get; set; }

    public int? Floor { get; set; }

    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}

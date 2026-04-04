using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class VwEmployeeActivity
{
    public int EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public string? RoleName { get; set; }

    public int? RestocksCount { get; set; }

    public int? MaintenancesCount { get; set; }

    public int TotalItemsLoaded { get; set; }
}

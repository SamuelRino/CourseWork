using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Login { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<MaintenanceLog> MaintenanceLogs { get; set; } = new List<MaintenanceLog>();

    public virtual ICollection<RestockLog> RestockLogs { get; set; } = new List<RestockLog>();

    public virtual DictEmployeeRole Role { get; set; } = null!;
}

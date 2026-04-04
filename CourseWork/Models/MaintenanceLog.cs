using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class MaintenanceLog
{
    public int MaintenanceId { get; set; }

    public int? MachineId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? MaintenanceDate { get; set; }

    public string? Description { get; set; }

    public decimal? Cost { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual VendingMachine? Machine { get; set; }
}

using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class VendingMachine
{
    public int MachineId { get; set; }

    public string SerialNumber { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int? LocationId { get; set; }

    public DateOnly? InstallDate { get; set; }

    public int StatusId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Location? Location { get; set; }

    public virtual ICollection<MachineStock> MachineStocks { get; set; } = new List<MachineStock>();

    public virtual ICollection<MaintenanceLog> MaintenanceLogs { get; set; } = new List<MaintenanceLog>();

    public virtual ICollection<RestockLog> RestockLogs { get; set; } = new List<RestockLog>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual DictMachineStatus Status { get; set; } = null!;
}

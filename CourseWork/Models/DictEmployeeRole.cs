using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class DictEmployeeRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class DictPaymentMethod
{
    public int MethodId { get; set; }

    public string MethodName { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}

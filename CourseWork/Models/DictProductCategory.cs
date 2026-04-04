using System;
using System.Collections.Generic;

namespace CourseWork.Models;

public partial class DictProductCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

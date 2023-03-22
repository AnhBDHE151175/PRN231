using System;
using System.Collections.Generic;

namespace PRN231.Models;

public partial class Department : BaseEntity
{

    public string DepartmentName { get; set; } = null!;

    public int? LocationId { get; set; }

    //public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    //public virtual Location? Location { get; set; }
}

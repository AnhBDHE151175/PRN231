using System;
using System.Collections.Generic;

namespace PRN231.Models;

public partial class Job: BaseEntity
{

    public string JobTitle { get; set; } = null!;

    public decimal? MinSalary { get; set; }

    public decimal? MaxSalary { get; set; }

    //public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}

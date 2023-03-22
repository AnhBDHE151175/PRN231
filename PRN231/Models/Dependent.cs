using System;
using System.Collections.Generic;

namespace PRN231.Models;

public partial class Dependent : BaseEntity
{

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Relationship { get; set; } = null!;

    public int EmployeeId { get; set; }

    //public virtual Employee Employee { get; set; } = null!;
}

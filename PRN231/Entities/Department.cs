using System;
using System.Collections.Generic;

namespace PRN231.Entities;

public partial class Department : BaseEntity
{

    public string DepartmentName { get; set; } = null!;

    public int? LocationId { get; set; }

    public virtual ICollection<Candidate> Candidates { get; } = new List<Candidate>();

    public virtual Location? Location { get; set; }
}

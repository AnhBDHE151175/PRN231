using System;
using System.Collections.Generic;

namespace PRN231.Entities;

public partial class Job : BaseEntity
{

    public DateTime? CreatedDate { get; set; }

    public DateTime? ExpiredDate { get; set; }

    public string JobTitle { get; set; } = null!;

    public decimal? MinSalary { get; set; }

    public decimal? MaxSalary { get; set; }


    public virtual ICollection<Candidate> Candidates { get; } = new List<Candidate>();

}

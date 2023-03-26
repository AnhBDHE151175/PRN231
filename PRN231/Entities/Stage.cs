using System;
using System.Collections.Generic;

namespace PRN231.Entities;

public partial class Stage : BaseEntity
{

    public string Name { get; set; } = null!;

    public int StageIndex { get; set; }

    //public virtual ICollection<Candidate> Candidates { get; } = new List<Candidate>();
}

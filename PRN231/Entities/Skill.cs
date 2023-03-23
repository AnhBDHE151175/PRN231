using System;
using System.Collections.Generic;

namespace PRN231.Entities;

public partial class Skill : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<JobsSkill> JobsSkills { get; } = new List<JobsSkill>();
}

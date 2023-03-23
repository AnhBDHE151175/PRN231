using System;
using System.Collections.Generic;

namespace PRN231.Entities;

public partial class JobsSkill
{
    public int JobId { get; set; }

    public int? ExpYear { get; set; }

    public int SkillId { get; set; }

    public virtual Job Job { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}

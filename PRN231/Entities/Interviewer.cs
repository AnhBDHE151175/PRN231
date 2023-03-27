using System;
using System.Collections.Generic;

namespace PRN231.Entities;

public partial class Interviewer : BaseEntity
{

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string? Password { get; set; } = null!;

    public virtual ICollection<InterviewerCandidate> InterviewerCandidates { get; } = new List<InterviewerCandidate>();
}

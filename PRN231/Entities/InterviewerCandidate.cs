using System;
using System.Collections.Generic;

namespace PRN231.Entities;

public partial class InterviewerCandidate
{
    public int InterviewerId { get; set; }

    public int CandidateId { get; set; }

    public int Score { get; set; }

    public virtual Candidate Candidate { get; set; } = null!;

    public virtual Interviewer Interviewer { get; set; } = null!;
}

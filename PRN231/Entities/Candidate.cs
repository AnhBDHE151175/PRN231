using System;
using System.Collections.Generic;

namespace PRN231.Entities;

public partial class Candidate : BaseEntity
{

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public DateTime? HireDate { get; set; }

    public int JobId { get; set; }

    public string? InterviewerId { get; set; }

    public decimal Salary { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    //public virtual ICollection<InterviewerCandidate> InterviewerCandidates { get; } = new List<InterviewerCandidate>();

    //public virtual Job Job { get; set; } = null!;

    //public virtual ICollection<Stage> Stages { get; } = new List<Stage>();
}

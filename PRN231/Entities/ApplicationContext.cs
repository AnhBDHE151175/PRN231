using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PRN231.Entities;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Interviewer> Interviewers { get; set; }

    public virtual DbSet<InterviewerCandidate> InterviewerCandidates { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobsSkill> JobsSkills { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Stage> Stages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("PRN231DB"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__employee__3213E83F50F84446");

            entity.ToTable("candidates");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate)
                .HasColumnType("date")
                .HasColumnName("hire_date");
            entity.Property(e => e.InterviewerId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("interviewer_id");
            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("salary");

            

            
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__countrie__7E8CD055782E7D58");

            entity.ToTable("countries");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("country_name");
            entity.Property(e => e.RegionId).HasColumnName("region_id");

            entity.HasOne(d => d.Region).WithMany(p => p.Countries)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("FK__countries__regio__286302EC");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__departme__3213E83F88E2573C");

            entity.ToTable("departments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("department_name");
            entity.Property(e => e.LocationId).HasColumnName("location_id");

            entity.HasOne(d => d.Location).WithMany(p => p.Departments)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__departmen__locat__35BCFE0A");
        });

        modelBuilder.Entity<Interviewer>(entity =>
        {
            entity.ToTable("interviewer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Position)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("position");
        });

        modelBuilder.Entity<InterviewerCandidate>(entity =>
        {
            entity.HasKey(e => new { e.InterviewerId, e.CandidateId });

            entity.ToTable("interviewer_candidate");

            entity.Property(e => e.InterviewerId).HasColumnName("interviewer_id");
            entity.Property(e => e.CandidateId).HasColumnName("candidate_id");
            entity.Property(e => e.Score).HasColumnName("score");

            

            entity.HasOne(d => d.Interviewer).WithMany(p => p.InterviewerCandidates)
                .HasForeignKey(d => d.InterviewerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_interviewer_candidate_interviewer");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jobs__3213E83F0D65D3F9");

            entity.ToTable("jobs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.ExpiredDate)
                .HasColumnType("datetime")
                .HasColumnName("expired_date");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(50)
                .HasColumnName("job_title");
            entity.Property(e => e.MaxSalary)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("max_salary");
            entity.Property(e => e.MinSalary)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("min_salary");
        });

        modelBuilder.Entity<JobsSkill>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.SkillId });

            entity.ToTable("jobs_skill");

            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.SkillId).HasColumnName("skill_id");
            entity.Property(e => e.ExpYear).HasColumnName("exp_year");

            //entity.HasOne(d => d.Job).WithMany(p => p.JobsSkills)
            //    .HasForeignKey(d => d.JobId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_jobs_skill_jobs");

            entity.HasOne(d => d.Skill).WithMany(p => p.JobsSkills)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_jobs_skill_skills");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__location__771831EA07343670");

            entity.ToTable("locations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("postal_code");
            entity.Property(e => e.StateProvince)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("state_province");
            entity.Property(e => e.StreetAddress)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("street_address");

            entity.HasOne(d => d.Country).WithMany(p => p.Locations)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__locations__count__2E1BDC42");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__regions__3213E83FF388C1A2");

            entity.ToTable("regions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RegionName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("region_name");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.ToTable("skills");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Stage>(entity =>
        {
            entity.ToTable("stages");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.StageIndex).HasColumnName("stage_index");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

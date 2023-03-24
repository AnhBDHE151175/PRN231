using Microsoft.EntityFrameworkCore;
using PRN231.DTOs.RequestModels;
using PRN231.DTOs.ResponseModels;
using PRN231.Entities;

namespace PRN231.Services
{
    public class JobService : BaseService<Job>
    {
        private ApplicationContext _context;

        public JobService(ApplicationContext context) : base(context)
        {
            _context = context;
        }


        public async Task<ListDataOutput<JobResponse>> GetFilter(Pager pager)
        {
            var response = new ListDataOutput<JobResponse>();
            try
            {
                var dataJobs = _context.Jobs.ToList();
                if (!string.IsNullOrEmpty(pager.Keyword))
                {
                    dataJobs = dataJobs.Where(o => o.JobTitle.ToLower().Contains(pager.Keyword.ToLower())).ToList();
                }
                var dataJobSkills = _context.JobsSkills.ToList();
                var dataSkills = _context.Skills.ToList();

                var data = dataJobs
                    .Select(o => new JobResponse()
                    {
                        ID = o.Id,
                        CreatedDate = o.CreatedDate,
                        ExpiredDate = o.ExpiredDate,
                        JobTitle = o.JobTitle,
                        MaxSalary = o.MaxSalary,
                        MinSalary = o.MinSalary,

                    }).ToList();

                data.ForEach(o =>
                {
                    var jobSkill = dataJobSkills.Where(x => x.JobId == o.ID).Select(x => x.SkillId).ToList();
                    var skills = dataSkills.Where(x => jobSkill.Contains(x.Id)).Select(x => x.Name).ToList();
                    if (skills.Count > 0)
                        o.Skills = skills.Aggregate((i, j) => i + " , " + j);

                });
                response.Data = data;
                response.IsError = false;
            }
            catch (Exception ex)
            {
                response.IsError = true;
            }
            return response;
        }
    }
}

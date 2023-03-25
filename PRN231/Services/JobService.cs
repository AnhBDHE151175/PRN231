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
                var dataJobs = _context.Jobs.OrderByDescending(o=>o.Id).ToList();
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
                response.Data = data.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();
                response.IsError = false;
            }
            catch (Exception ex)
            {
                response.IsError = true;
            }
            return response;
        }
        public async Task<Response> Insert(JobRequest entity)
        {
            var response = new Response();
            try
            {
                if (entity.job != null)
                {
                    var save = await Insert(entity.job);
                    if (!save.IsError)
                    {
                        var maxJobID = _context.Jobs.Max(o => o.Id);

                        var listSkills = entity.listSkills.Split(",");
                        if (listSkills.Length > 0)
                        {
                            foreach (var i in listSkills)
                            {
                                await _context.JobsSkills.AddAsync(new JobsSkill() { JobId = maxJobID, SkillId = Int32.Parse(i) });
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                    response.IsError = false;
                }
            }
            catch (Exception ex)
            {
                response.IsError = true;
            }
            return response;
        }
    }
}

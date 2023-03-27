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
                var dataJobs = _context.Jobs.OrderByDescending(o => o.Id).ToList();
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
        public async Task<Response> Update(JobRequest entity)
        {
            var response = new Response();
            try
            {
                var dataJob = _context.Jobs.FirstOrDefault(o => o.Id == entity.job.Id);
                var dataJobSkill = _context.JobsSkills.Where(o => o.JobId == entity.job.Id);

                dataJob.MaxSalary = entity.job.MaxSalary;
                dataJob.MinSalary = entity.job.MinSalary;
                dataJob.JobTitle = entity.job.JobTitle;
                dataJob.ExpiredDate = entity.job.ExpiredDate;

                _context.JobsSkills.RemoveRange(dataJobSkill);
                await _context.SaveChangesAsync();

                var listSkills = entity.listSkills.Split(",");

                if (listSkills.Length > 0)
                {
                    foreach (var i in listSkills)
                    {
                        await _context.JobsSkills.AddAsync(new JobsSkill() { JobId = entity.job.Id, SkillId = Int32.Parse(i) });
                        await _context.SaveChangesAsync();
                    }
                }



                response.IsError = false;

            }
            catch (Exception ex)
            {
                response.IsError = true;
            }
            return response;
        }
        public async Task<DataOutput<JobByIdResponse>> GetByID(int id)
        {
            var response = new DataOutput<JobByIdResponse>();
            try
            {
                var data = await _context.Jobs.FirstOrDefaultAsync(o => o.Id == id);

                var listSkills = await _context.JobsSkills.Where(o => o.JobId == id).Select(o => o.SkillId.ToString()).ToListAsync();

                response.IsError = false;
                response.Data = new JobByIdResponse()
                {
                    Id = id,
                    CreatedDate = data.CreatedDate,
                    ExpiredDate = data.ExpiredDate,
                    JobTitle = data.JobTitle,
                    MaxSalary = data.MaxSalary,
                    MinSalary = data.MinSalary,
                    Skills = listSkills.Count > 0 ? listSkills.Aggregate((i, j) => i + "," + j) : "",
                };
            }
            catch (Exception ex)
            {
                response.IsError = true;
            }
            return response;
        }
        public string generatePassword()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;
        }
    }
}

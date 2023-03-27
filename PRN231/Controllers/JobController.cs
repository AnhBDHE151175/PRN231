using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MimeKit;
using PRN231.DTOs.RequestModels;
using PRN231.DTOs.ResponseModels;
using PRN231.Entities;
using PRN231.Services;

namespace PRN231.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private JobService service;
        private ApplicationContext applicationContext;

        public JobController(JobService service, ApplicationContext applicationContext)
        {
            this.service = service;
            this.applicationContext = applicationContext;
        }
        [HttpPost]
        public async Task<ListDataOutput<JobResponse>> GetFilter(Pager pager)
        {
            return await service.GetFilter(pager);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<DataOutput<JobByIdResponse>> GetByID(int id)
        {
            return await service.GetByID(id);
        }
        [HttpPost]
        public async Task<Response> Insert(JobRequest entity)
        {
            entity.job.CreatedDate = DateTime.Now;
            return await service.Insert(entity);
        }
        [HttpPost]
        public async Task<Response> Update(JobRequest entity)
        {
            return await service.Update(entity);
        }
        [HttpGet]
        [Route("{id}")]

        public async Task<Response> Delete(int id)
        {
            var dataSkills = applicationContext.JobsSkills.Where(o => o.JobId == id).ToList();
            applicationContext.JobsSkills.RemoveRange(dataSkills);
            await applicationContext.SaveChangesAsync();

            return await service.Delete(id);
        }
        [HttpGet]

        public async Task<AnalysisResponse> Analysis()
        {
            var n1 = applicationContext.Departments.Count();
            var n2 = applicationContext.Jobs.Count();
            var n3 = applicationContext.Candidates.Count();
            var n4 = applicationContext.Interviewers.Count();

            var data = new AnalysisResponse() { NumCan = n3, NumDe = n1, NumInter = n4, NumJob = n2 };
            return data;
        }
        [HttpPost]
        public async Task<LoginResponse> Login(LoginRequest entity)
        {
            var data = await applicationContext.Interviewers.FirstOrDefaultAsync(o => o.Email == entity.email && o.Password == entity.password);
            var fullName = data != null ? $"{data.FirstName}{data.LastName}" : "";

            return new LoginResponse() { FullName = fullName };
        }
        [HttpPost]
        public async Task<Response> SendMail(MailRequest mail)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(MailboxAddress.Parse("ducanhbui09@gmail.com"));
                emailMessage.To.Add(MailboxAddress.Parse(mail.ToEmail));

                emailMessage.Subject = mail.Subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mail.Message };


                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("ducanhbui09@gmail.com", "rcryybiawyxsoibq");
                await smtp.SendAsync(emailMessage);
                smtp.Disconnect(true);
            }
            catch (Exception e)
            {
                return new Response() { IsError = true };

            }
            return new Response() { IsError = false };


        }
    }
}

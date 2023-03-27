using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Rules;
using PRN231.DTOs.RequestModels;
using PRN231.DTOs.ResponseModels;
using PRN231.Entities;
using PRN231.Services;
using System.Net.Mail;
using System.Text;

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
        [HttpGet]
        public async Task<ForgotResponse> Forgot(string email)
        {
            //MailMessage mail = new MailMessage();

            //SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            //smtpServer.Credentials = new System.Net.NetworkCredential("ducanhbui09@gmail.com", "tflunuwljsdztmpi");
            //smtpServer.Port = 587;
            //smtpServer.EnableSsl = true;

            //var newpass = service.generatePassword().Trim();

            //mail.From = new MailAddress("ducanhbui09@gmail.com");
            //mail.To.Add(email);
            //mail.Subject = "CONFIRM YOUR ACCOUNT";
            //mail.Body = "Email: " + email + "\nPassword: " + newpass;

            //var user = await applicationContext.Interviewers.FirstOrDefaultAsync(u => u.Email == email);

            //user.Password = newpass;

            //applicationContext.Interviewers.Update(user);
            //var save = applicationContext.SaveChanges();

            //smtpServer.Send(mail);
            


            return 1 > 0 ? new ForgotResponse() { Message = "SentFail" } : new ForgotResponse() { Message = "SentFail" };

        }
    }
}

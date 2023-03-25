using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public JobController(JobService service)
        {
            this.service = service;
        }
        [HttpPost]
        public async Task<ListDataOutput<JobResponse>> GetFilter(Pager pager)
        {
            return await service.GetFilter(pager);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Job> GetByID(int id)
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
        public async Task<Response> Update(Job entity)
        {
            return await service.Update(entity);
        }
        [HttpGet]
        [Route("{id}")]

        public async Task<Response> Delete(int id)
        {
            return await service.Delete(id);
        }
    }
}

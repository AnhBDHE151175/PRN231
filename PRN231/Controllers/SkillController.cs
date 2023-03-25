using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231.DTOs.RequestModels;
using PRN231.DTOs.ResponseModels;
using PRN231.Entities;
using PRN231.Services;

namespace PRN231.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private SkillService service;

        public SkillController(SkillService service)
        {
            this.service = service;
        }
        [HttpPost]
        public async Task<ListDataOutput<Skill>> GetFilter(Pager pager)
        {

            return await service.GetFilter(pager);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Skill> GetByID(int id)
        {
            return await service.GetByID(id);
        }
        [HttpPost]
        public async Task<Response> Insert(Skill entity)
        {
            return await service.Insert(entity);
        }
        [HttpPost]
        public async Task<Response> Update(Skill entity)
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

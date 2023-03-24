using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.DTOs.ResponseModels;
using PRN231.Entities;
using PRN231.Services;

namespace PRN231.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StageController : ControllerBase
    {
        private StageService service;

        public StageController(StageService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Stage> GetByID(int id)
        {
            return await service.GetByID(id);
        }
        [HttpPost]
        public async Task<Response> Insert(Stage entity)
        {
            return await service.Insert(entity);
        }
        [HttpPost]
        public async Task<Response> Update(Stage entity)
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

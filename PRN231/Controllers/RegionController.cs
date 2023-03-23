using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.Entities;
using PRN231.Services;

namespace PRN231.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private RegionService service;

        public RegionController(RegionService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Region> GetByID(int id)
        {
            return await service.GetByID(id);
        }
    }
}

﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.DTOs.ResponseModels;
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
        [HttpPost]
        public async Task<Response> Insert(Region entity)
        {
            return await service.Insert(entity);
        }
        [HttpPost]
        public async Task<Response> Update(Region entity)
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

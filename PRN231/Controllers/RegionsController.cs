using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PRN231.Entities;

namespace PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public RegionsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Regions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegions(string? name)
        {
            if (_context.Regions == null)
            {
                return NotFound();
            }
            return await _context.Regions.AsQueryable().Where(x => name.IsNullOrEmpty() || x.RegionName.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        // GET: api/Regions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Region>> GetRegion(int id)
        {
            if (_context.Regions == null)
            {
                return NotFound();
            }
            var region = await _context.Regions.FindAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return region;
        }

        // PUT: api/Regions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutRegion(Region region)
        {

            _context.Entry(region).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionExists(region.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Regions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Region>> PostRegion(Region region)
        {
            if (_context.Regions == null)
            {
                return Problem("Entity set 'ApplicationContext.Regions'  is null.");
            }

            if(_context.Regions.Any(x => x.RegionName.ToLower().Equals(region.RegionName.ToLower()))){
                return Problem("CountryName have already existed!");
            }
            
            _context.Regions.Add(region);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegion", new { id = region.Id }, region);
        }

        // DELETE: api/Regions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            if (_context.Regions == null)
            {
                return NotFound();
            }
            var region = await _context.Regions.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegionExists(int id)
        {
            return (_context.Regions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

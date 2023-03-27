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
    public class CandidatesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CandidatesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Candidates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidates(string? name, int? departmentId, string? email, DateTime? hireDate, string? phone)
        {
            if (_context.Candidates == null)
            {
                return NotFound();
            }
            return await _context.Candidates.Include(x => x.Department)
                .AsQueryable()
                .Where(x => name.IsNullOrEmpty() || x.FirstName.Contains(name))
                .Where(x => departmentId == 0 || x.DepartmentId == departmentId)
                .Where(x => phone.IsNullOrEmpty() || x.PhoneNumber.Contains(phone))
                .Where(x => email.IsNullOrEmpty() || x.Email.Contains(email))
                .Where(x => hireDate == null || x.HireDate.Equals(hireDate))
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        // GET: api/Candidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidate(int id)
        {
            if (_context.Candidates == null)
            {
                return NotFound();
            }
            var candidate = await _context.Candidates.FindAsync(id);

            if (candidate == null)
            {
                return NotFound();
            }

            return candidate;
        }

        // PUT: api/Candidates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidate(int id, Candidate candidate)
        {
            if (id != candidate.Id)
            {
                return BadRequest();
            }

            _context.Entry(candidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(id))
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

        // POST: api/Candidates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<bool> PostCandidate(Candidate candidate)
        {
            if (_context.Candidates == null)
            {
                return false;
            }
            candidate.HireDate = DateTime.Now;
            _context.Candidates.Add(candidate);
            var save = await _context.SaveChangesAsync();
            return save > 0;
        }

        // DELETE: api/Candidates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            if (_context.Candidates == null)
            {
                return NotFound();
            }
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CandidateExists(int id)
        {
            return (_context.Candidates?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

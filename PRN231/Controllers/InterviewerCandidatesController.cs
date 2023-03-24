using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231.Entities;

namespace PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewerCandidatesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public InterviewerCandidatesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/InterviewerCandidates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterviewerCandidate>>> GetInterviewerCandidates()
        {
          if (_context.InterviewerCandidates == null)
          {
              return NotFound();
          }
            return await _context.InterviewerCandidates.ToListAsync();
        }

        // GET: api/InterviewerCandidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InterviewerCandidate>> GetInterviewerCandidate(int id)
        {
          if (_context.InterviewerCandidates == null)
          {
              return NotFound();
          }
            var interviewerCandidate = await _context.InterviewerCandidates.FindAsync(id);

            if (interviewerCandidate == null)
            {
                return NotFound();
            }

            return interviewerCandidate;
        }

        // PUT: api/InterviewerCandidates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterviewerCandidate(int id, InterviewerCandidate interviewerCandidate)
        {
            if (id != interviewerCandidate.InterviewerId)
            {
                return BadRequest();
            }

            _context.Entry(interviewerCandidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterviewerCandidateExists(id))
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

        // POST: api/InterviewerCandidates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InterviewerCandidate>> PostInterviewerCandidate(InterviewerCandidate interviewerCandidate)
        {
          if (_context.InterviewerCandidates == null)
          {
              return Problem("Entity set 'ApplicationContext.InterviewerCandidates'  is null.");
          }
            _context.InterviewerCandidates.Add(interviewerCandidate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InterviewerCandidateExists(interviewerCandidate.InterviewerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInterviewerCandidate", new { id = interviewerCandidate.InterviewerId }, interviewerCandidate);
        }

        // DELETE: api/InterviewerCandidates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterviewerCandidate(int id)
        {
            if (_context.InterviewerCandidates == null)
            {
                return NotFound();
            }
            var interviewerCandidate = await _context.InterviewerCandidates.FindAsync(id);
            if (interviewerCandidate == null)
            {
                return NotFound();
            }

            _context.InterviewerCandidates.Remove(interviewerCandidate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterviewerCandidateExists(int id)
        {
            return (_context.InterviewerCandidates?.Any(e => e.InterviewerId == id)).GetValueOrDefault();
        }
    }
}

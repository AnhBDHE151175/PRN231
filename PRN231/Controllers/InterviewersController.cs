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
    public class InterviewersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public InterviewersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Interviewers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interviewer>>> GetInterviewers(string? name)
        {
            if (_context.Interviewers == null)
            {
                return NotFound();
            }
            var data = await _context.Interviewers.ToListAsync();
            if (!string.IsNullOrEmpty(name))
            {
                data = data.Where(o => o.FirstName.Contains(name) || o.LastName.Contains(name)).ToList();
            }
            return data;
        }


        // GET: api/Interviewers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Interviewer>> GetInterviewer(int id)
        {
            if (_context.Interviewers == null)
            {
                return NotFound();
            }
            var interviewer = await _context.Interviewers.FindAsync(id);

            if (interviewer == null)
            {
                return NotFound();
            }

            return interviewer;
        }

        // PUT: api/Interviewers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterviewer(int id, Interviewer interviewer)
        {
            if (id != interviewer.Id)
            {
                return BadRequest();
            }

            _context.Entry(interviewer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterviewerExists(id))
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

        // POST: api/Interviewers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Interviewer>> PostInterviewer(Interviewer interviewer)
        {
            if (_context.Interviewers == null)
            {
                return Problem("Entity set 'ApplicationContext.Interviewers'  is null.");
            }
            _context.Interviewers.Add(interviewer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInterviewer", new { id = interviewer.Id }, interviewer);
        }

        // DELETE: api/Interviewers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterviewer(int id)
        {
            if (_context.Interviewers == null)
            {
                return NotFound();
            }
            var interviewer = await _context.Interviewers.FindAsync(id);
            if (interviewer == null)
            {
                return NotFound();
            }

            _context.Interviewers.Remove(interviewer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterviewerExists(int id)
        {
            return (_context.Interviewers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

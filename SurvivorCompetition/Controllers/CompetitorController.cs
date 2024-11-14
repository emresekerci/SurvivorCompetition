using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurvivorCompetition.Models;

namespace SurvivorCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitorsController : ControllerBase
    {
        private readonly SurvivorContext _context;

        public CompetitorsController(SurvivorContext context)
        {
            _context = context;
        }

        // GET: api/competitors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Competitor>>> GetCompetitors()
        {
            return await _context.Competitors.ToListAsync();
        }

        // GET: api/competitors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Competitor>> GetCompetitor(int id)
        {
            var competitor = await _context.Competitors.FindAsync(id);

            if (competitor == null)
            {
                return NotFound();
            }

            return competitor;
        }

        // GET: api/competitors/categories/{CategoryId}
        [HttpGet("categories/{CategoryId}")]
        public async Task<ActionResult<IEnumerable<Competitor>>> GetCompetitorsByCategoryId(int categoryId)
        {
            return await _context.Competitors.Where(c => c.CategoryId == categoryId).ToListAsync();
        }

        // POST: api/competitors
        [HttpPost]
        public async Task<ActionResult<Competitor>> PostCompetitor(Competitor competitor)
        {
            _context.Competitors.Add(competitor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompetitor", new { id = competitor.Id }, competitor);
        }

        // PUT: api/competitors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompetitor(int id, Competitor competitor)
        {
            if (id != competitor.Id)
            {
                return BadRequest();
            }

            competitor.ModifiedDate = DateTime.Now;
            _context.Entry(competitor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitorExists(id))
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

        // DELETE: api/competitors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompetitor(int id)
        {
            var competitor = await _context.Competitors.FindAsync(id);
            if (competitor == null)
            {
                return NotFound();
            }

            _context.Competitors.Remove(competitor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompetitorExists(int id)
        {
            return _context.Competitors.Any(e => e.Id == id);
        }
    }

}

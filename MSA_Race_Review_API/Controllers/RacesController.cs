using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSA_Race_Review_API;

namespace MSA_Race_Review_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacesController : ControllerBase
    {
        private readonly ReviewContext _context;

        public RacesController(ReviewContext context)
        {
            _context = context;
        }

        // GET: api/Races
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Race>>> GetRace()
        {
            return await _context.Race.ToListAsync();
        }

        // GET: api/Races/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Race>> GetRace(int id)
        {
            var race = await _context.Race.FindAsync(id);

            if (race == null)
            {
                return NotFound();
            }

            return race;
        }

        // GET: api/RaceReviews/5
        [HttpGet("year/{year}/raceName/{raceName}/championship/{championship}/track/{track}/location/{location}")]
        public async Task<ActionResult<IEnumerable<Race>>> GetSearchRace(int year, string raceName, string championship, string track, string location)
        {
            using (var context = new ReviewContext())
            {
                List<Race> raceList;
                if (raceName == "*")
                {
                    raceName = "";
                }
                if (championship == "*")
                {
                    championship = "";
                }
                if (track == "*")
                {
                    track = "";
                }
                if (location == "*")
                {
                    location = "";
                }
                if (year > 1893) // first ever motorrace was in 1894 or 1895, so can't have any earlier
                {        
                    raceList = await context.Race.Where(x => x.year == year
                                                                && x.raceName.Contains(raceName)
                                                                && x.championship.Contains(championship)
                                                                && x.track.Contains(track)
                                                                && x.location.Contains(location)
                                                                )
                                                                .ToListAsync();
                }
                else
                {
                    raceList = await context.Race.Where(x => x.raceName.Contains(raceName)
                                                                && x.raceName.Contains(raceName)
                                                                && x.championship.Contains(championship)
                                                                && x.track.Contains(track)
                                                                && x.location.Contains(location)
                                                                )
                                                                .ToListAsync();
                }
                if (raceList == null)
                {
                    return NotFound();
                }
                else
                {
                    var raceToReturn = raceList; 
                    return raceToReturn;
                }
            }
        }

        // PUT: api/Races/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        // public async Task<IActionResult> PutRace(int id, Race race)
        public async Task<ActionResult<Race>> PutRace(int id, Race race)
        {
            if (id != race.raceId)
            {
                return BadRequest();
            }

            _context.Entry(race).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            var updatedRace = await _context.Race.FindAsync(id);
            return updatedRace;
            // return NoContent();
        }

        // POST: api/Races
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Race>> PostRace(Race race)
        {
            _context.Race.Add(race);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRace", new { id = race.raceId }, race);
        }

        // DELETE: api/Races/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Race>> DeleteRace(int id)
        {
            var race = await _context.Race.FindAsync(id);
            if (race == null)
            {
                return NotFound();
            }

            _context.Race.Remove(race);
            await _context.SaveChangesAsync();

            return race;
        }

        private bool RaceExists(int id)
        {
            return _context.Race.Any(e => e.raceId == id);
        }
    }
}

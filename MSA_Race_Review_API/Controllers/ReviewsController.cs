using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSA_Race_Review_API;

namespace MSA_Race_Review_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewContext _context;

        public ReviewsController(ReviewContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReview()
        {
            return await _context.Review.ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Review.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // GET: api/RaceReviews/5
        [HttpGet("raceId/{raceId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetRaceReview(int raceId)
        {
            using (var context = new ReviewContext())
            {
                List<Review> reviewList = await context.Review.Where(x => x.raceId == raceId).ToListAsync();
                // Console.WriteLine("Movie Name: " + movieList[0].Name);

                if (reviewList == null)
                {
                    return NotFound();
                }
                else
                {
                    var reviewToReturn = reviewList; //.filter(b => b.Url.Contains("dotnet"));
                    return reviewToReturn;
                }
            }

            // var review = await _context.Review.ToListAsync();

            // if (review == null)
            // {
            // return NotFound();
            // }

            // var reviewToReturn = review.filter(b => b.Url.Contains("dotnet"));

            // return reviewToReturn;
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.reviewId)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // PUT: api/Reviews/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("/upvote/{id}")]
        public async Task<ActionResult<Review>> PutUpvoteReview(int id) // public async Task<IActionResult> PutUpvoteReview(int id)
        {
            // update race info
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            // int score_to_add = (int)review.upvotes;
            review.upvotes = review.upvotes + 1;

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(review.reviewId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var updatedReview = await _context.Review.FindAsync(id);
            return updatedReview;
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("/downvote/{id}")]
        public async Task<ActionResult<Review>> PutDownvoteReview(int id) // public async Task<IActionResult> PutDownvoteReview(int id)
        {
            // update race info
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            // int score_to_add = (int)review.upvotes;
            review.upvotes = review.upvotes - 1;

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(review.reviewId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var updatedReview = await _context.Review.FindAsync(id);
            return updatedReview;
            // return NoContent();
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("/update/Score/{id}")]
        public async Task<ActionResult<Review>> PutupdateScore(int id, int new_score) // public async Task<IActionResult> PutupdateScore(int id, int new_score)
        {
            // update race info
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            // int score_to_add = (int)review.upvotes;
            int old_score = (int)review.reviewScore;
            int raceId = review.raceId;
            review.reviewScore = new_score;

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(review.reviewId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // update race score
            var race = await _context.Race.FindAsync(raceId);
            if (race == null)
            {
                return NotFound();
            }
            race.scoreSum = (int)(race.scoreSum - old_score + review.reviewScore);
            race.averageScore = race.scoreSum / race.totalReviews;
            _context.Entry(race).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceExists(race.raceId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var updatedReview = await _context.Review.FindAsync(id);
            return updatedReview;
            // return NoContent();
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("/update/Review/{id}")]
        public async Task<ActionResult<Review>> PutupdateScore(int id, string new_review) // public async Task<IActionResult> PutupdateScore(int id, int new_score)
        {
            // update race info
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            review.reviewText = new_review;

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(review.reviewId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var updatedReview = await _context.Review.FindAsync(id);
            return updatedReview;
            // return NoContent();
        }

        private bool RaceExists(int id)
        {
            return _context.Race.Any(e => e.raceId == id);
        }

        // POST: api/Reviews
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            // update race info
            var race = await _context.Race.FindAsync(review.raceId);
            if (race == null)
            {
                return NotFound();
            }
            int score_to_add = (int)review.reviewScore;
            race.totalReviews += 1;
            race.scoreSum = race.scoreSum + score_to_add;
            race.averageScore = race.scoreSum / race.totalReviews;

            _context.Entry(race).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceExists(race.raceId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            review.timeCreated = DateTime.Now;

            // add review
            _context.Review.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = review.reviewId }, review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Review>> DeleteReview(int id)
        {
            var review = await _context.Review.FindAsync(id);
            // update race info
            var race = await _context.Race.FindAsync(review.raceId);
            if (race == null)
            {
                return NotFound();
            }
            int score_to_minus = (int)review.reviewScore;
            race.totalReviews -= 1;
            if (race.totalReviews == 0) {
              race.scoreSum = 0;
              race.averageScore = 0;
            } else {
              race.scoreSum = (int)(race.scoreSum - score_to_minus);
              race.averageScore = race.scoreSum / race.totalReviews;
            }

            _context.Entry(race).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceExists(race.raceId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // delete review
            // var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();

            return review;
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.reviewId == id);
        }
    }
}

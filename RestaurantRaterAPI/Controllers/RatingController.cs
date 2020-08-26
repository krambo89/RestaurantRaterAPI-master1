using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        // Create New Ratings
        [HttpPost]
        public async Task<IHttpActionResult> CreateRating(Rating model)
        {
            //check to see if the target is not valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Find the target
            var restaurant = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurant == null)
            {
                return BadRequest($"The target resaurant with the Id of {model.RestaurantId} does not exist.");
            }

            _context.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($" You Rated {restaurant.Name} successfully!");
            }

            return InternalServerError();

        }

        //Get a rating by its ID?
        [HttpGet]
        public async Task<IHttpActionResult> GetbyId(int id)
        {
            Rating rating = await _context.Ratings.FindAsync(id);

            if (rating != null)
            {
                return Ok(rating);
            }

            return NotFound();
        }


        //Get All Ratings For a Specific Restaurant by the restaurant ID

        // Update Ratings
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRating([FromUri] int id, [FromBody] Rating updatedRating)
        {
            if (ModelState.IsValid)
            {
                //Find and update the appropriate rating
                Rating rating = await _context.Ratings.FindAsync(id);

                if (rating != null)
                {
                    //Update Ratings.
                    rating.FoodScore = updatedRating.FoodScore;
                    rating.EnvironmentScore = updatedRating.EnvironmentScore;
                    rating.CleanlinessScore = updatedRating.CleanlinessScore;


                    await _context.SaveChangesAsync();

                    return Ok();
                }

                // Didn't find the rating
                return NotFound();
            }

            return BadRequest(ModelState);
        }

            //Delete Ratings

            [HttpDelete]

            public async Task<IHttpActionResult> DeleteRatingsById(int id)
            {
                Rating rating = await _context.Ratings.FindAsync(id);

                if (rating == null)
                {
                    return NotFound();
                }

                _context.Ratings.Remove(rating);

                if (await _context.SaveChangesAsync() == 1)
                {
                    return Ok("The rating was deleted");
                }

                return InternalServerError();

            }
        }
    }


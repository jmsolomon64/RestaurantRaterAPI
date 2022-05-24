using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantRaterAPI.Models;

namespace RestaurantRaterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : Controller
    {
        private readonly ILogger<RatingController> _logger; //instance of logger idk what it do doe
        private RestaurantDbContext _context; //our standard named instance of the db

        public virtual List<Rating> Ratings {get; set;} = new List<Rating>(); 

        public double AverageRating
        {
            get
            {
                if (Ratings.Count == 0)
                {
                    return 0;
                }
                double total = 0.0;
                foreach(Rating rating in Ratings)
                {
                    total += rating.Score;
                }
                return total/ Ratings.Count;
            }
        }

        public RatingController (ILogger<RatingController> logger, RestaurantDbContext context) //first controller action and it requires context to be thrown in
        {
            _logger = logger;
            _context = context; //sets global context equal to the context thrown into the method at this moment (sort of like a refresh feature?)
        }

        [HttpPost]
        public async Task<IActionResult> RateRestaurant([FromForm] RatingEdit model) //post action that requires form data for the new rating
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Ratings.Add(new Rating() {
                Score = model.Score,
                RestaurantId = model.RestaurantId,
            });

            await _context.SaveChangesAsync();
            return Ok();
        }
        
    }
}
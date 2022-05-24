using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantRaterAPI.Models;

namespace RestaurantRaterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController: Controller
    {
        private readonly ILogger<RestaurantController> _logger;
        private RestaurantDbContext  _context; //creates an instance of our RestaurantDbContext class into this file

        public RestaurantController(ILogger<RestaurantController> logger, RestaurantDbContext context) //Method that takes in an instance of RestaurantDbContext
        {
            _logger = logger;
            _context = context; //turns the private global context into the context passed into the method
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpPost] //lets EF know that the method bellow is intended to hadnle a POST endpoint
        public async Task<IActionResult> PostRestaurant([FromForm] RestaurantEdit model) //method to post data to restaurant table
        { //method will return an IActionResult which will be defined in method
            if (!ModelState.IsValid) //checks to see if model state is not valid (will pass a fail if there is no valid name and location passed through)
            {
                return BadRequest(ModelState); //sends back an error message as well as returns info from the request
            }

            //grabs Database context, moves into Restaurant table, adds new information
            _context.Restaraunts.Add(new Restaurant() //Gives properties from the RestaurantEdit model
            {
                Name = model.Name,
                Location = model.Location
            });

            await _context.SaveChangesAsync(); //used to modifications to db made by user to have them persist
            return Ok(); // returns an okay message
        }

        [HttpGet] //lets ef know that method bellow is a GET endpoint
        public async Task<IActionResult> GetRestaurants() //new method for pulling information from db
        {
            var restaurants = await _context.Restaraunts.ToListAsync(); //steps through db context to the restaurantdb and then stores info into a list
            return Ok(restaurants); //returns an okay message
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRestaurant([FromForm] RestaurantEdit model, [FromRoute] int id)
        { //Creates a method to update restaurants by passing in a new name and location from the form and getting the id of the object being updated from the route 
            var oldRestaurant = await _context.Restaraunts.FindAsync(id); //creates instance of object about to be updated and finds it from the id in context

            if (oldRestaurant == null) //checks to see if the restaurant doesn't exist
            {
                return NotFound(); //if object doesn't exist we'll throw this method imherited from controller
            }

            if (!ModelState.IsValid) //checks to see if the information passed through is invalid
            {
                return BadRequest(); //if invalid it will throw another method from controller class
            }

            if (!string.IsNullOrEmpty(model.Name)) //makes sure new name isn't blank
            {
                oldRestaurant.Name = model.Name; // sets the old name equal to the new name
            }

            if (!string.IsNullOrEmpty(model.Location)) // makes sure new location isn't blank
            {
                oldRestaurant.Location = model.Location; // sets old location equal to new one
            }

            await _context.SaveChangesAsync(); // saves changes made to the DB context
            return Ok(); //if everything goes smoothly it will return an okay method from controller
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id) //method for deleting restaurant that takes in id from the uri
        {
            var restaurant = await _context.Restaraunts.FindAsync(id); //pulls restaurant object by id in db
            if(restaurant == null) //if restaurant doesn't exist, will send notfound
            {
                return NotFound();
            }

            _context.Restaraunts.Remove(restaurant); //removes restaurant from the context of db
            await _context.SaveChangesAsync(); //saves changes made to the db context into db
            return Ok();
        }

    }
}

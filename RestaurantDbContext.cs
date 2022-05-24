using Microsoft.EntityFrameworkCore; //necessary to gain access to database context
using RestaurantRaterAPI.Models;

namespace RestaurantRaterAPI
{
    public class RestaurantDbContext : DbContext //inherits from DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options) { }
        public DbSet<Restaurant> Restaraunts {get; set;} //represents the Restaurant table (spelled Restaraunt in the db cause I'm cool)
        public DbSet<Rating> Ratings {get; set;} //represents the Rating Table in db
    }
}
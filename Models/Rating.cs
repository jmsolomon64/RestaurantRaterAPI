using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; //needed for the ForeignKey annotation

namespace RestaurantRaterAPI.Models
{
    public class Rating
    {
        [Key]
        public int Id {get; set;}
        [Required]
        [ForeignKey("Restaurant")] //annotation for FOREIGN KEY (obviously)
        public int RestaurantId {get; set;}
        [Required]
        public double Score {get; set;}
    }
}
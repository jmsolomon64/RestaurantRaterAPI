using System.ComponentModel.DataAnnotations; //used so that code can understand annotations

namespace RestaurantRaterAPI.Models
{
    public class Restaurant
    {
        [Key] //annotation for PRIMARY KEY
        public int Id {get; set;}
        [Required] //annotation NOT NULL
        [MaxLength(100)] //annotation for character length
        public string Name {get; set;}
        [Required]
        [MaxLength(100)]
        public string Location {get; set;}
    }
}
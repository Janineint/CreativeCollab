using System.ComponentModel.DataAnnotations;

namespace CreativeCollab.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }
        
        public string ImageURL { get; set; }
        
        public string Review { get; set; }

        public User User { get; set; }

        public ICollection<CarCategory> CarCategories { get; set; } = new List<CarCategory>();

        public ICollection<OrderVehicle> OrderVehicles { get; set; } = new List<OrderVehicle>();
    }

}
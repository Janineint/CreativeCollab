using System.ComponentModel.DataAnnotations;

namespace CreativeCollab.Models
{

    public class OrderVehicle
    {
        [Key]
        public int OrderVehicleId { get; set; }

        [Required]
        public int OrderId { get; set; }
        

        public Order Order { get; set; }

        [Required]
        public int CarId { get; set; }

        public Car Car { get; set; }
    }
}
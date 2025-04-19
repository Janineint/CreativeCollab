using CreativeCollab.Models;
using System.Collections.Generic;

namespace CreativeCollab.ViewModels
{
    public class OrderDetailsViewModel
    {
        // The main Order object
        public Order Order { get; set; }

        // The User associated via OrderVehicle
        public User AssociatedUser { get; set; }

        // The Car associated via OrderVehicle
        public Car AssociatedCar { get; set; }

        // Constructor (optional, can be useful)
        public OrderDetailsViewModel()
        {
            // Initialize to avoid null reference issues in the view if needed
            Order = new Order();
            AssociatedUser = new User();
            AssociatedCar = new Car();
        }
    }
}

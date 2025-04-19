using System.ComponentModel.DataAnnotations;
using CreativeCollab.Models;

namespace CreativeCollab.ViewModels
{
    public class CarListViewModel
    {
        // This property will hold the list of cars to display
        public IEnumerable<Car> Cars { get; set; } = new List<Car>();

    }
}

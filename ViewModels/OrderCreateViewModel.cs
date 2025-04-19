using System.ComponentModel.DataAnnotations;

namespace CreativeCollab.ViewModels
{
    public class OrderCreateViewModel
    {
        
        public int? StoreID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now;

       
        public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();

    
        [Required(ErrorMessage = "Please select an owner/user.")]
        [Display(Name = "Owner/User")]
        public int SelectedUserId { get; set; }

        // Required: ID of the Car associated with this Order/Vehicle link
        [Required(ErrorMessage = "Please select a car.")]
        [Display(Name = "Car")]
        public int SelectedCarId { get; set; }

        public OrderCreateViewModel()
        {
            // Initialize defaults
            OrderDate = DateTime.Now;
            Items = new List<OrderItemViewModel>();
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CreativeCollab.Models
{
    public class Order
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public string? UserId { get; set; }

        public int? StoreID { get; set; }

        public decimal TotalCost { get; set; }

        public ApplicationUser User { get; set; }
        public Store Store { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public ICollection<User> Users { get; set; } = new List<User>();

        public ICollection<OrderVehicle> OrderVehicles { get; set; } = new List<OrderVehicle>();
    }
}

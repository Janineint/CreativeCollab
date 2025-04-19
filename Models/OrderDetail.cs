using System.ComponentModel.DataAnnotations;

namespace CreativeCollab.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}

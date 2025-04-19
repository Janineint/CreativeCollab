using System.ComponentModel.DataAnnotations;

namespace CreativeCollab.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required, MaxLength(100)]
        public string ProductName { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required, MaxLength(50)]
        public string UnitSize { get; set; }

        [Required, MaxLength(50)]
        public string Category { get; set; }

        public Supplier? Supplier { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CreativeCollab.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Contact { get; set; }

        [Required, MaxLength(10)]
        public string Phone { get; set; }

        [MaxLength(30)]
        public string? Email { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        [MaxLength(30)]
        public string? City { get; set; }

        [MaxLength(3)]
        public string? Province { get; set; }

        [MaxLength(6)]
        public string? Postal { get; set; }

        public ICollection<Product>? Products { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}

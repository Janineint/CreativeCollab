using System.ComponentModel.DataAnnotations;

namespace CreativeCollab.Models
{
    public class Store
    {
        [Key]
        public int StoreID { get; set; }

        [Required, MaxLength(100)]
        public string StoreName { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string? Phone { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}

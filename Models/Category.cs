using System.ComponentModel.DataAnnotations;

namespace CreativeCollab.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public ICollection<CarCategory> CarCategories { get; set; } = new List<CarCategory>();
    }

}
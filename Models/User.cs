using System.ComponentModel.DataAnnotations;

namespace CreativeCollab.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }

}
using System.ComponentModel.DataAnnotations;

namespace CreativeCollab.Models
{ 

	public class CarCategory
	{
		[Key]
		public int CarCategoryId { get; set; }
		
		[Required]
		public int CarID { get; set; }
		
		[Required]
		public int CategoryID { get; set; }
		
		[Required]
		public Car Car { get; set; }
		
		[Required]
		public Category Category { get; set; }
	}
}
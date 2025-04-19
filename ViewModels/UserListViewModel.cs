using System.ComponentModel.DataAnnotations;
using CreativeCollab.Models;

namespace CreativeCollab.ViewModels
{
    public class UserListViewModel
    {
        // This property will hold the list of users to display
        public IEnumerable<User> Users { get; set; } = new List<User>();

        // You could add other properties here if needed for the view
        // public int TotalUsers { get; set; }
    }
}

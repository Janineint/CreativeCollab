namespace CreativeCollab.ViewModels
{
    public class CustomerListViewModel
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public decimal LastOrderPrice { get; set; }
    }
}

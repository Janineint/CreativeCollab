namespace CreativeCollab.ViewModels
{
    public class StoreListViewModel
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public decimal LastOrderPrice { get; set; }
    }
}

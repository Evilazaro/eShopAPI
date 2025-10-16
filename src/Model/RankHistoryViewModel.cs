namespace eShopAPI.Model
{
    public class PriceHistoryViewModel
    {
        public Guid Id { get; set; }
        public decimal NewPrice { get; set; }
        public DateTime ChangeDate { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
    }
    public class PriceHistoryViewModelv2
    {
        public Guid Id { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public DateTime ChangeDate { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
    }
}

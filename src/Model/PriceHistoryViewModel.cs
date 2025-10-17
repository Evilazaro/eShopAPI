namespace eShopAPI.Model
{
    public class RankHistoryViewModel
    {
        public Guid Id { get; set; }
        public int Rank { get; set; }
        public DateTime Date { get; set; }
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
    }
}

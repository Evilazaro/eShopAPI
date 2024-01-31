namespace eShopAPI.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public List<PriceHistory> PriceHistories { get; set; }
        public List<RankHistory> RankHistories { get; set; }

    }
}

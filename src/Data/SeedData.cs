using Bogus;
using eShopAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShopAPI.Data
{
    public static class SeedData
    {
        public static IEnumerable<ProductCategory> ProductCategories { get; private set; }
        public static IEnumerable<Product> Products { get; private set; }
        public static IEnumerable<PriceHistory> PriceHistory { get; private set; }
        public static IEnumerable<RankHistory> RankHistory { get; private set; }
        static SeedData()
        {
            ProductCategories = GetProductCategories();
            Products = GetProducts();
            PriceHistory = GetPriceHistories();
            RankHistory = GetRankHistories();
        }
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasData(ProductCategories);
            modelBuilder.Entity<Product>().HasData(Products);
            modelBuilder.Entity<RankHistory>().HasData(RankHistory);
            modelBuilder.Entity<PriceHistory>().HasData(PriceHistory);

        }
        private static IEnumerable<ProductCategory> GetProductCategories()
        {
            var demoCategories = new Faker<ProductCategory>()
      .RuleFor(o => o.Id, f => Guid.NewGuid())
      .RuleFor(o => o.Name, f => f.Commerce.Categories(1).First())
      .RuleFor(o => o.Description, f => f.Lorem.Paragraph());
            return demoCategories.Generate(100);
        }
        private static IEnumerable<Product> GetProducts()
        {
            var categoryIds = ProductCategories.Select(z => z.Id);

            var demoProducts = new Faker<Product>()
      .RuleFor(o => o.Id, f => Guid.NewGuid())
      .RuleFor(o => o.Name, f => f.Commerce.Product())
      .RuleFor(o => o.Description, f => f.Commerce.ProductDescription())
      .RuleFor(o => o.Price, f => decimal.Parse(f.Commerce.Price()))
      .RuleFor(o => o.ProductCategoryId, f => f.PickRandom<Guid>(categoryIds))
      ;

            return demoProducts.Generate(1000);

        }
        private static IEnumerable<PriceHistory> GetPriceHistories()
        {
            var productIds = Products.Select(z => z.Id);

            var demoPriceHistory = new Faker<PriceHistory>()
      .RuleFor(o => o.Id, f => Guid.NewGuid())
      .RuleFor(o => o.NewPrice, f => decimal.Parse(f.Commerce.Price()))
      .RuleFor(o => o.OldPrice, f => decimal.Parse(f.Commerce.Price()))
      .RuleFor(o => o.ChangeDate, f => f.Date.Between(DateTime.Now.AddYears(-5), DateTime.Now))
      .RuleFor(o => o.ProductId, f => f.PickRandom<Guid>(productIds))
      ;

            return demoPriceHistory.Generate(5000);

        }
        private static IEnumerable<RankHistory> GetRankHistories()
        {
            var productIds = Products.Select(z => z.Id);

            var demoRankHistory = new Faker<RankHistory>()
      .RuleFor(o => o.Id, f => Guid.NewGuid())
      .RuleFor(o => o.Rank, f => f.Random.Number(1, 10000))
      .RuleFor(o => o.Date, f => f.Date.Between(DateTime.Now.AddYears(-5), DateTime.Now))
      .RuleFor(o => o.ProductId, f => f.PickRandom<Guid>(productIds))
      ;

            return demoRankHistory.Generate(10000);

        }

    }
}

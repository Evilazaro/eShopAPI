using eShopAPI.Data.Entities;
using System.Collections.Generic;

namespace eShopAPIUnitTests
{
    [TestClass]
    public class ProductsControllerTests
    {
        [TestMethod]
        public async Task GetProducts_ReturnsAllProducts()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Product>>();
            var mockContext = new Mock<YourDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            var products = new List<Product>
        {
            new Product { /* initialize properties */ },
            new Product { /* initialize properties */ },
            // Add more products as needed for testing
        };
            mockSet.SetSource(products);

            var controller = new ProductsController(mockContext.Object);

            // Act
            var result = await controller.GetProducts();

            // Assert
            var actionResult = Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<ProductViewModel>>));
            var viewResult = result.Value as IEnumerable<ProductViewModel>;
            Assert.AreEqual(products.Count, viewResult.Count());
        }
    }
}

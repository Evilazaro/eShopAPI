using Microsoft.VisualStudio.TestTools.UnitTesting;
using eShopAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using eShopAPI.Model;

namespace eShopAPI.Controllers.Tests
{
    [TestClass()]
    public class ProductsControllerTests
    {
        private static ProductsController _controller;
        private static ProductsController controller
        {
            get
            {
                return _controller;
            }
        }

        public ProductsControllerTests()
        {
            if (_controller == null)
            {
                _controller = new ProductsController(eShopDB.storeDbContext);
            }
        }

        [TestMethod()]
        public void GetProductsTest()
        {
            var products = controller.GetProducts();

            Assert.IsNotNull(eShopDB.storeDbContext);
            Assert.IsNotNull(controller);
            Assert.IsNotNull(products);
            Assert.IsTrue(products.Result.Value.Count() > 0);
        }

        [TestMethod()]
        public void GetProductTest()
        {
            var products = controller.GetProducts();
            var firstProduct = products.Result.Value.First<ProductViewModel>();
            var product = controller.GetProduct(firstProduct.Id);


            Assert.IsNotNull(eShopDB.storeDbContext);
            Assert.IsNotNull(controller);
            Assert.IsNotNull(products);
            Assert.IsTrue(products.Result.Value.Count() > 0);
            Assert.IsNotNull(firstProduct);
            Assert.IsNotNull(product.Result.Value.Id);
        }

        [TestMethod()]
        public void DeleteProductTest()
        {
            var products = controller.GetProducts();
            var firstProduct = products.Result.Value.First<ProductViewModel>();
            controller.DeleteProduct(firstProduct.Id);

            var product = controller.GetProduct(firstProduct.Id);

            Assert.IsNotNull(product);
            Assert.IsNull(product.Result.Value);
        }
    }
}
using CustomerSubscriptionAPI.Client.Configurations;
using CustomerSubscriptionAPI.Controllers;
using CustomerSubscriptionAPI.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace CustomerSubscriptionAPI.Client.Controllers
{
    public class ProductControllerTest
    {

        private ProductController _productController;

        [SetUp]
        public void Setup()
        {
            var dependencies = new SetupDependencies();
            _productController = dependencies.GetProductControllerInstance();
        }

        [Test]
        public void Test_GetAllProducts_NotNull_GreaterThanZero()
        {
            var products = _productController.Get();

            Assert.IsNotNull(products);
            Assert.IsTrue(products.Count() > 0);
        }

        [Test]
        public void Test_GetProductsById_IsBeingFound()
        {
            var products = _productController.Get();
            var product = products.First();

            var result = _productController.Get(product.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(product.Id, result.Id);
        }

        [Test]
        public void Test_CreateProduct_IsBeingCreated()
        {
            var id = Guid.NewGuid();
            var newProduct = new Product() { Id = id, Name = "TV Cable", Price = 314.75m };
            _productController.Create(newProduct);

            var result = _productController.Get(newProduct.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void Test_CreateEmptyProduct_Throws_ArgumentException()
        {
            var id = Guid.Empty;
            var newProduct = new Product() { Id = id, Name = "", Price = decimal.Zero };
            Assert.Throws<ArgumentNullException>(() => _productController.Create(newProduct));
        }

        [Test]
        public void Test_UpdateNameProduct_IsBeingUpdated()
        {
            var productNameToUpdate = "Mobile Messenger Only";
            var products = _productController.Get();
            var product = products.First();

            var result = _productController.Get(product.Id);

            Console.WriteLine($"Current Product Name: {result.Name}");

            result.Name = productNameToUpdate;

            _productController.Update(result);

            var newResult = _productController.Get(product.Id);

            Console.WriteLine($"Current Product new Name: {newResult.Name}");

            Assert.IsTrue(newResult.Name == productNameToUpdate);
        }

        [Test]
        public void Test_UpdatePriceProduct_IsBeingUpdated()
        {
            var priceToUpdate = 1520.32m;

            var products = _productController.Get();
            var product = products.First();

            var result = _productController.Get(product.Id);

            Console.WriteLine($"Current Customer Price: {result.Price}");

            result.Price = priceToUpdate;

            _productController.Update(result);

            var newResult = _productController.Get(product.Id);

            Console.WriteLine($"Current Product new Price: {newResult.Price}");

            Assert.IsTrue(newResult.Price == priceToUpdate);
        }

        [Test]
        public void Test_DeleteProductById_IsBeingDeleted()
        {
            var products = _productController.Get();
            var product = products.First();

            _productController.Delete(product.Id);

            products = _productController.Get();

            Assert.IsFalse(products.Any(c => c.Id == product.Id));
        }
    }
}

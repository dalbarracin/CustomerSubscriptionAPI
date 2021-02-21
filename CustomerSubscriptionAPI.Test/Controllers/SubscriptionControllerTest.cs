using CustomerSubscriptionAPI.Client.Configurations;
using CustomerSubscriptionAPI.Controllers;
using CustomerSubscriptionAPI.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace CustomerSubscriptionAPI.Client.Controllers
{
    public class SubscriptionControllerTest
    {

        private CustomerController _customerController;
        private SubscriptionController _subscriptionController;
        private ProductController _productController;

        [SetUp]
        public void Setup()
        {
            var dependencies = new SetupDependencies();
            _customerController = dependencies.GetCustomerControllerInstance();
            _productController = dependencies.GetProductControllerInstance();
            _subscriptionController = dependencies.GetSubscriptionControllerInstance();
        }

        [Test]
        public void Test_GetAllSubscriptions_NotNull_GreaterThanZero()
        {
            var subscritions = _subscriptionController.Get();

            Assert.IsNotNull(subscritions);
            Assert.IsTrue(subscritions.Count() > 0);
        }

        [Test]
        public void Test_GetSubscriptionsById_IsBeingFound()
        {
            var subscriptions = _subscriptionController.Get();
            var subscription = subscriptions.First();

            var result = _subscriptionController.Get(subscription.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(subscription.Id, result.Id);
        }

        [Test]
        public void Test_CreateSubscription_IsBeingCreated()
        {
            var id = Guid.NewGuid();
            var customer = _customerController.Get().First();
            var product = _productController.Get().First();

            var newSubscription = new Subscription() { Id = id, CustomerId = customer.Id, ProductId = product.Id };
            _subscriptionController.Create(newSubscription);

            var result = _subscriptionController.Get(newSubscription.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(customer.Id, result.CustomerId);
            Assert.AreEqual(product.Id, result.ProductId);
        }

        [Test]
        public void Test_CreateEmptySubscription_Throws_ArgumentException()
        {
            var id = Guid.Empty;
            var newSubscription = new Subscription() { Id = id, CustomerId = Guid.Empty, ProductId = Guid.Empty, Created = DateTime.MinValue };
            Assert.Throws<ArgumentNullException>(() => _subscriptionController.Create(newSubscription));
        }

        [Test]
        public void Test_UpdateSubscription_Product_IsBeingUpdated()
        {
            var subscription = _subscriptionController.Get().First();

            var customer = _customerController.Get().First(c => c.Id != subscription.CustomerId);
            var product = _productController.Get().First(c => c.Id != subscription.ProductId);

            var newSubscription = new Subscription() { Id = subscription.Id, CustomerId = customer.Id, ProductId = product.Id, Created = DateTime.Now };

            _subscriptionController.Update(newSubscription);

            var result = _subscriptionController.Get(newSubscription.Id);

            Assert.IsTrue(result.Id == newSubscription.Id);
            Assert.IsTrue(result.CustomerId == newSubscription.CustomerId);
            Assert.IsTrue(result.ProductId == newSubscription.ProductId);
            Assert.IsTrue(result.Created == newSubscription.Created);

            Console.WriteLine($"Subscription updated");
        }

        [Test]
        public void Test_DeleteSubscriptionById_IsBeingDeleted()
        {
            var subscriptions = _subscriptionController.Get();
            var subscription = subscriptions.First();

            _subscriptionController.Delete(subscription.Id);

            subscriptions = _subscriptionController.Get();

            Assert.IsFalse(subscriptions.Any(c => c.Id == subscription.Id));
        }
    }
}

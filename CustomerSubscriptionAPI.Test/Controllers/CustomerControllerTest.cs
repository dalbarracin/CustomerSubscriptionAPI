using CustomerSubscriptionAPI.Client.Configurations;
using CustomerSubscriptionAPI.Controllers;
using CustomerSubscriptionAPI.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace CustomerSubscriptionAPI.Client.Controllers
{
    public class CustomerControllerTest
    {

        private CustomerController _customerController;

        [SetUp]
        public void Setup()
        {
            var dependencies = new SetupDependencies();
            _customerController = dependencies.GetCustomerControllerInstance();
        }

        [Test]
        public void Test_GetAllCustomers_NotNull_GreaterThanZero()
        {
            var customers = _customerController.Get();

            Assert.IsNotNull(customers);
            Assert.IsTrue(customers.Count() > 0);
        }

        [Test]
        public void Test_GetCustomerById_IsBeingFound()
        {
            var customers = _customerController.Get();
            var customer = customers.First();

            var result = _customerController.Get(customer.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(customer.Id, result.Id);
        }

        [Test]
        public void Test_CreateCustomer_IsBeingCreated()
        {
            var id = Guid.NewGuid();
            var newCustomer = new Customer() { Id = id, Name = "Peter Jacobsen", Address = "Kaløgade 7, 8000 Aarhus" };
            _customerController.Create(newCustomer);

            var result = _customerController.Get(newCustomer.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void Test_CreateEmptyCustomer_Throws_ArgumentException()
        {
            var id = Guid.Empty;
            var newCustomer = new Customer() { Id = id, Name = "", Address = "" };
            Assert.Throws<ArgumentNullException>(() => _customerController.Create(newCustomer));
        }

        [Test]
        public void Test_UpdateNameCustomer_IsBeingUpdated()
        {
            var customerNameToUpdate = "Peter Jacobsen";
            var customers = _customerController.Get();
            var customer = customers.First();

            var result = _customerController.Get(customer.Id);

            Console.WriteLine($"Current Customer Name: {result.Name}");

            result.Name = customerNameToUpdate;

            _customerController.Update(result);

            var newResult = _customerController.Get(customer.Id);

            Console.WriteLine($"Current Customer new Name: {newResult.Name}");

            Assert.IsTrue(newResult.Name == customerNameToUpdate);
        }

        [Test]
        public void Test_UpdateAddressCustomer_IsBeingUpdated()
        {
            var addressToUpdate = "Kaløgade 7, 8000 Aarhus";

            var customers = _customerController.Get();
            var customer = customers.First();

            var result = _customerController.Get(customer.Id);

            Console.WriteLine($"Current Customer Address: {result.Address}");

            result.Address = addressToUpdate;

            _customerController.Update(result);

            var newResult = _customerController.Get(customer.Id);

            Console.WriteLine($"Current Customer new Address: {newResult.Address}");

            Assert.IsTrue(newResult.Address == addressToUpdate);
        }

        [Test]
        public void Test_DeleteCustomerById_IsBeingDeleted()
        {
            var customers = _customerController.Get();
            var customer = customers.First();

            _customerController.Delete(customer.Id);

            customers = _customerController.Get();

            Assert.IsFalse(customers.Any(c => c.Id == customer.Id));
        }
    }
}
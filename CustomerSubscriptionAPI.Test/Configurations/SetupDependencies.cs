using CustomerSubscriptionAPI.Controllers;
using CustomerSubscriptionAPI.Registers;
using CustomerSubscriptionAPI.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CustomerSubscriptionAPI.Client.Configurations
{
    public class SetupDependencies
    {
        private IServiceCollection _servicesCollection;
        private IServiceProvider _serviceProvider;
        private ICustomerRepository _customerRepository;
        private CustomerController _customerController;
        private ISubscriptionRepository _subscriptionRepository;
        private SubscriptionController _subscriptionController;
        private IProductRepository _productRepository;
        private ProductController _productController;

        public SetupDependencies()
        {
            _servicesCollection = new ServiceCollection();
            _servicesCollection.AddCustomerSubscriptionScoped();

            _serviceProvider = _servicesCollection.BuildServiceProvider();
            _subscriptionRepository = (ISubscriptionRepository)_serviceProvider.GetService(typeof(ISubscriptionRepository));
            _subscriptionController = new SubscriptionController(_subscriptionRepository);

            _customerRepository = (ICustomerRepository)_serviceProvider.GetService(typeof(ICustomerRepository));
            _customerController = new CustomerController(_customerRepository, _subscriptionRepository);

            _productRepository = (IProductRepository)_serviceProvider.GetService(typeof(IProductRepository));
            _productController = new ProductController(_productRepository, _subscriptionRepository);
        }

        public CustomerController GetCustomerControllerInstance() => _customerController;
        public ProductController GetProductControllerInstance() => _productController;
        public SubscriptionController GetSubscriptionControllerInstance() => _subscriptionController;
    }
}

using CustomerSubscriptionAPI.Models;
using CustomerSubscriptionAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerSubscriptionAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public ProductController(
            IProductRepository productRepository, 
            ISubscriptionRepository subscriptionRepository)
        {
            _productRepository = productRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productRepository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public Product Get(Guid id)
        {
            return _productRepository.GetById(id);
        }

        [HttpPost]
        public void Create([FromBody] Product product)
        {
            _productRepository.Create(product);
        }

        [HttpPut]
        public void Update([FromBody] Product product)
        {
            _productRepository.Update(product);
        }


        [HttpDelete]
        [Route("{id}")]
        public void Delete(Guid id)
        {
            var product = _productRepository.GetById(id);
            var subscriptions = _subscriptionRepository.GetByProductId(product.Id).ToList();

            foreach (var subscription in subscriptions)
            {
                _subscriptionRepository.Delete(subscription.Id);
            }

            _productRepository.Delete(id);
        }
    }
}

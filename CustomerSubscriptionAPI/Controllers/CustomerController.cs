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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public CustomerController(
            ICustomerRepository customerRepository,
            ISubscriptionRepository subscriptionRepository)
        {
            _customerRepository = customerRepository;
            _subscriptionRepository = subscriptionRepository;
        }


        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _customerRepository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public Customer Get(Guid id)
        {
            return _customerRepository.GetById(id);
        }

        [HttpPost]
        public void Create([FromBody] Customer customer)
        {
            _customerRepository.Create(customer);
        }

        [HttpPut]
        public void Update([FromBody] Customer customer)
        {
            _customerRepository.Update(customer);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(Guid id)
        {
            var customer = _customerRepository.GetById(id);
            var subscriptions = _subscriptionRepository.GetByCustomerId(customer.Id).ToList();

            foreach (var subscription in subscriptions)
            {
                _subscriptionRepository.Delete(subscription.Id);
            }

            _customerRepository.Delete(id);
        }
    }
}

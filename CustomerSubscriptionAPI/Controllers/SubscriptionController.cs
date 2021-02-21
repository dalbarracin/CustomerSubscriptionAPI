using CustomerSubscriptionAPI.Models;
using CustomerSubscriptionAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CustomerSubscriptionAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionController(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        [HttpGet]
        public IEnumerable<Subscription> Get()
        {
            return _subscriptionRepository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public Subscription Get(Guid id)
        {
            return _subscriptionRepository.GetById(id);
        }

        [HttpPost]
        public void Create([FromBody] Subscription subscription)
        {
            _subscriptionRepository.Create(subscription);
        }

        [HttpPut]
        public void Update([FromBody] Subscription subscription)
        {
            _subscriptionRepository.Update(subscription);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(Guid id)
        {
            _subscriptionRepository.Delete(id);
        }
    }
}

using CustomerSubscriptionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerSubscriptionAPI.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private HashSet<Subscription> _subscription;

        public SubscriptionRepository()
        {
            _subscription = new HashSet<Subscription>();
            _subscription.Add(new Subscription() { Id = Guid.NewGuid(), CustomerId = new Guid("8e8c1133-cd87-4541-8d2b-cf8771e5c4a5"), ProductId = new Guid("3eed6d77-068c-48cc-a4fb-3afe87339d78"), Created = DateTime.Now.AddDays(-15) });
            _subscription.Add(new Subscription() { Id = Guid.NewGuid(), CustomerId = new Guid("d55ddea0-b5b3-41b0-89b9-542b6ffa86f5"), ProductId = new Guid("20ae6e25-54c9-46f1-b678-ca715ab9669b"), Created = DateTime.Now.AddDays(-47) });
            _subscription.Add(new Subscription() { Id = Guid.NewGuid(), CustomerId = new Guid("8f5b99f4-97cb-40d4-bae3-2bd202866e02"), ProductId = new Guid("6f14b704-264b-429e-91eb-1edc56349add"), Created = DateTime.Now.AddDays(-93) });
        }                                          
                                                   
        public IEnumerable<Subscription> GetAll()  
        {
            return _subscription.AsEnumerable();
        }

        public Subscription GetById(Guid id)
        {
            VerifyEmptyId(id);

            if (Exist(id))
                return _subscription.FirstOrDefault(t => t.Id == id);

            return null;
        }

        public IEnumerable<Subscription> GetByCustomerId(Guid id)
        {
            VerifyEmptyId(id);
            return _subscription.Where(t => t.CustomerId == id);
        }

        public IEnumerable<Subscription> GetByProductId(Guid id)
        {
            VerifyEmptyId(id);
            return _subscription.Where(t => t.ProductId == id);
        }

        public void Create(Subscription subscription)
        {
            VerifyEmptyId(subscription.Id);

            lock (_subscription)
            {
                if (!Exist(subscription.Id))
                {
                    _subscription.Add(subscription);
                }
            }
        }

        public bool Update(Subscription subscription)
        {
            VerifyEmptyId(subscription.Id);

            lock (_subscription)
            {
                if (Exist(subscription.Id))
                {
                    var itemToRemove = _subscription.Single(item => item.Id == subscription.Id);
                    _subscription.Remove(itemToRemove);
                    _subscription.Add(subscription);
                    return true;
                }
            }

            return false;
        }

        public void Delete(Guid id)
        {
            VerifyEmptyId(id);

            lock (_subscription)
            {
                if (Exist(id))
                {
                    var itemToRemove = _subscription.Single(item => item.Id == id);
                    _subscription.Remove(itemToRemove);
                }
            }
        }

        public bool Exist(Guid id)
        {
            return _subscription.Any(t => t.Id == id);
        }

        public void VerifyEmptyId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException($"{nameof(Subscription)} Id must not be empty!!!");
            }
        }
    }
}

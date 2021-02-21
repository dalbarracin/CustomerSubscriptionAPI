using CustomerSubscriptionAPI.Models;
using System;
using System.Collections.Generic;

namespace CustomerSubscriptionAPI.Repositories
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        IEnumerable<Subscription> GetByCustomerId(Guid id);

        IEnumerable<Subscription> GetByProductId(Guid id);
    }
}

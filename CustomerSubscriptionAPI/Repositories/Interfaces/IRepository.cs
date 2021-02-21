using System;
using System.Collections.Generic;

namespace CustomerSubscriptionAPI.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(Guid id);

        void Create(T item);

        bool Update(T item);

        void Delete(Guid id);

        bool Exist(Guid id);

        void VerifyEmptyId(Guid id);
    }
}

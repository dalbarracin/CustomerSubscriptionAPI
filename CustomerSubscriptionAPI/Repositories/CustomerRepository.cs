using CustomerSubscriptionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerSubscriptionAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private HashSet<Customer> _customers;

        public CustomerRepository()
        {
            _customers = new HashSet<Customer>();
            _customers.Add(new Customer() { Id = new Guid("8e8c1133-cd87-4541-8d2b-cf8771e5c4a5"), Name = "John Doe", Address = "Nyhavn 89, 2100 Copenhagen" });
            _customers.Add(new Customer() { Id = new Guid("d55ddea0-b5b3-41b0-89b9-542b6ffa86f5"), Name = "Carmen Smith", Address = "Vesterbro 15, 2110 Copenhagen" });
            _customers.Add(new Customer() { Id = new Guid("8f5b99f4-97cb-40d4-bae3-2bd202866e02"), Name = "Nicklas Andersen", Address = "Østerbrogade 45, 2200 Copenhagen" });
        }

        public IEnumerable<Customer> GetAll()
        {
            lock (_customers)
            {
                return _customers.AsEnumerable();
            }
        }

        public Customer GetById(Guid id)
        {
            VerifyEmptyId(id);

            if (Exist(id))
                return _customers.FirstOrDefault(t => t.Id == id);

            return null;
        }

        public void Create(Customer customer)
        {
            VerifyEmptyId(customer.Id);

            lock (_customers)
            {
                if (!Exist(customer.Id))
                {
                    _customers.Add(customer);
                }
            }
        }

        public bool Update(Customer customer)
        {
            VerifyEmptyId(customer.Id);

            lock (_customers)
            {
                if (Exist(customer.Id))
                {
                    var itemToRemove = _customers.Single(item => item.Id == customer.Id);
                    _customers.Remove(itemToRemove);
                    _customers.Add(customer);
                    return true;
                }
            }

            return false;
        }

        public void Delete(Guid id)
        {
            VerifyEmptyId(id);

            lock (_customers)
            {
                if (Exist(id))
                {
                    var itemToRemove = _customers.Single(item => item.Id == id);
                    _customers.Remove(itemToRemove);
                }
            }
        }

        public bool Exist(Guid id)
        {
            return _customers.Any(t => t.Id == id);
        }

        public void VerifyEmptyId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException($"{nameof(Customer)} Id must not be empty!!!");
            }
        }
    }
}

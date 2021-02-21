using CustomerSubscriptionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerSubscriptionAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private HashSet<Product> _product;

        public ProductRepository()
        {
            _product = new HashSet<Product>();
            _product.Add(new Product() { Id = new Guid("8a40c03f-9540-47d2-8652-e1663a77a1d2"), Price = 135.50m, Name = "Streaming" });
            _product.Add(new Product() { Id = new Guid("d442fb33-1641-4626-b0cc-e964d4b3ca6e"), Price = 346.40m, Name = "TV" });
            _product.Add(new Product() { Id = new Guid("3eed6d77-068c-48cc-a4fb-3afe87339d78"), Price = 235.10m, Name = "Mobile phone" });
            _product.Add(new Product() { Id = new Guid("20ae6e25-54c9-46f1-b678-ca715ab9669b"), Price = 412.35m, Name = "Mobile broadband" });
            _product.Add(new Product() { Id = new Guid("6f14b704-264b-429e-91eb-1edc56349add"), Price = 586.99m, Name = "Broadband" });
            _product.Add(new Product() { Id = new Guid("c3746a7e-bb9a-4228-97f0-0312ed510a7a"), Price = 716.14m, Name = "Landline" });
        } 

        public IEnumerable<Product> GetAll()
        {
            return _product.AsEnumerable();
        }

        public Product GetById(Guid id)
        {
            VerifyEmptyId(id);

            if (Exist(id))
                return _product.FirstOrDefault(t => t.Id == id);

            return null;
        }

        public void Create(Product product)
        {
            VerifyEmptyId(product.Id);

            lock (_product)
            {
                if (!Exist(product.Id))
                {
                    _product.Add(product);
                }
            }
        }

        public bool Update(Product product)
        {
            VerifyEmptyId(product.Id);

            lock (_product)
            {
                if (Exist(product.Id))
                {
                    var itemToRemove = _product.Single(item => item.Id == product.Id);
                    _product.Remove(itemToRemove);
                    _product.Add(product);
                    return true;
                }
            }

            return false;
        }

        public void Delete(Guid id)
        {
            VerifyEmptyId(id);

            lock (_product)
            {
                if (Exist(id))
                {
                    var itemToRemove = _product.Single(item => item.Id == id);
                    _product.Remove(itemToRemove);
                }
            }
        }

        public bool Exist(Guid id)
        {
            return _product.Any(t => t.Id == id);
        }

        public void VerifyEmptyId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException($"{nameof(Product)} Id must not be empty!!!");
            }
        }
    }
}

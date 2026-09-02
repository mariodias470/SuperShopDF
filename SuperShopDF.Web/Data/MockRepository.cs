using SuperShopDF.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperShopDF.Web.Data
{
    public class MockRepository : IRepository
    {
        public void AddProduct(Product product)
        {
            throw new System.NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Um", Price = 10m },
                new Product { Id = 2, Name = "Dois", Price = 20m },
                new Product { Id = 3, Name = "Três", Price = 30m },
                new Product { Id = 4, Name = "Quatro", Price = 40m },
                new Product { Id = 5, Name = "Cinco", Price = 50m }
            };

            return products;
        } // end GetProducts()

        public bool ProductExists(int id)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveProduct(Product product)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void UpadateProduct(Product product)
        {
            throw new System.NotImplementedException();
        }
    } // end class MockRepository
} // end namespace SuperShopDF.Web.Data

using System.Linq;
using SuperShopDF.Web.Data.Entities; 

namespace SuperShopDF.Web.Data.Entities
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public IQueryable GetAllWithUsers();

    } // end interface IProductRepository
} // end namespace SuperShopDF.Web.Data

using System.Xml;

namespace SuperShopDF.Web.Data.Entities
{
    public class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
            
        }

    } // end class ProductRepository
} // end namespace SuperShopDF.Web.Data

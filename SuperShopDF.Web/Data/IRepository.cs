using System.Collections.Generic;
using System.Threading.Tasks;
using SuperShopDF.Web.Data.Entities;

// 16.30 do vídeo ASP.NET_MVC_07.

namespace SuperShopDF.Web.Data
{
    public interface IRepository
    {

        void AddProduct(Product product);
        
        Product GetProduct(int id);
        
        IEnumerable<Product> GetProducts();
        
        bool ProductExists(int id);
        
        void RemoveProduct(Product product);
        
        Task<bool> SaveAllAsync();
        
        void UpadateProduct(Product product);
    
    } // end interface IRepository

} // end namespace SuperShopDF.Web.Data
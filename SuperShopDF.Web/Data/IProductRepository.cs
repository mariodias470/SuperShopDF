using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering; // SelectListItem
using SuperShopDF.Web.Data.Entities; 

namespace SuperShopDF.Web.Data.Entities
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public IQueryable GetAllWithUsers();

        // 18.49 - vídeo ASP-NET_MVC_23:
        IEnumerable<SelectListItem> GetComboProducts();
    } // end interface IProductRepository
} // end namespace SuperShopDF.Web.Data

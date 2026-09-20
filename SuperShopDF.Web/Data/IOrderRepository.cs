using SuperShopDF.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShopDF.Web.Data
{
    // Questão para entrevista - 43.00 - vídeo ASP-NET_MVC_22
    public interface IOrderRepository : IGenericRepository<Order>
    {
        // 45.03 - vídeo ASP-NET_MVC_22
        Task<IQueryable<Order>> GetOrderAsync(string userName);



    } // end IOrderRepository
} // end namespace SuperShopDF.Web.Data

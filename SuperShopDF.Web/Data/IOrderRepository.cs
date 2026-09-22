using SuperShopDF.Web.Data.Entities;
using SuperShopDF.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShopDF.Web.Data
{
    // Questão para entrevista - 43.00 - vídeo ASP-NET_MVC_22
    public interface IOrderRepository : IGenericRepository<Order>
    {
        // 45.03 - vídeo ASP-NET_MVC_22:
        Task<IQueryable<Order>> GetOrderAsync(string userName);

        // 01.19 - vídeo ASP-NET_MVC_23:
        Task<IQueryable<OrderDetailTemp>> GetDetailsTempsAsync(string userName);

        // 03.28 - vídeo ASP-NET_MVC_24:
        Task AddItemToOrderAsync (AddItemViewModel model, string userName);

        Task ModifyOrderDetailTempQuantityAsync(int id, double quantity);

    } // end IOrderRepository
} // end namespace SuperShopDF.Web.Data

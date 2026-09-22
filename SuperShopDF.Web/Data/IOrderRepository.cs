using SuperShopDF.Web.Data.Entities;
using SuperShopDF.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShopDF.Web.Data
{    /*
        OrderRepository()
        GetOrderAsync()
        GetDetailsTempsAsync()
        AddItemToOrderAsync()
        ModifyOrderDetailTempQuantityAsync()
        DeleteDetailTempAsync()
     */

    // Questão para entrevista - 43.00 - vídeo ASP-NET_MVC_22
    public interface IOrderRepository : IGenericRepository<Order>
    {
        // 45.03 - vídeo ASP-NET_MVC_22:
        Task<IQueryable<Order>> GetOrderAsync(string userName);

        // 01.19 - vídeo ASP-NET_MVC_23:
        Task<IQueryable<OrderDetailTemp>> GetDetailsTempsAsync(string userName);

        // 03.28 - vídeo ASP-NET_MVC_24:
        Task AddItemToOrderAsync(AddItemViewModel model, string userName);

        Task ModifyOrderDetailTempQuantityAsync(int id, double quantity);

        // 01.21 - vídeo ASP-NET_MVC_25:
        // public async Task DeleteDetailTempAsync(int id); // IMPORTANTE: Error(active)  CS1994 The 'async' modifier can only be used in
        //                                                     IMPORTANTE: methods that have a body.
        public Task DeleteDetailTempAsync(int id);

        // 04.49 - vídeo ASP-NET_MVC_26:
        Task<bool> ConfirmOrderAsync(string userName);

    } // end IOrderRepository
} // end namespace SuperShopDF.Web.Data

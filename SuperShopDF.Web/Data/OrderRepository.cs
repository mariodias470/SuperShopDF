using Microsoft.EntityFrameworkCore;
using SuperShopDF.Web.Data.Entities;
using SuperShopDF.Web.Helpers;
using SuperShopDF.Web.Models;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace SuperShopDF.Web.Data
{   // IMPORTANTE: Explicações muitos importantes vídeo ASP-NET_MVC_22,
    // IMPORTANTE: de 41.30 a 44.24
    //
    // Questão para entrevista - 43.00 - vídeo ASP-NET_MVC_22
    // 43.28 - vídeo ASP-NET_MVC_22: de acordo com as regras da injecção de dependências....
    // 
    // services.AddScoped< IUserHelper, UserHelper >();
    //                     ^           ^        
    //                     |           |        
    //                1º a Interface   Depois a Classe
    // Ouvir, ouvir,
    // ouvir...
    // ouvir...
    // fim 44.24

    /*
        OrderRepository()
        GetOrderAsync()
        GetDetailsTempsAsync()
        AddItemToOrderAsync()
        ModifyOrderDetailTempQuantityAsync()
     */



    // 41.06 - vídeo ASP-NET_MVC_22:
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public OrderRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        } // end OrderRepository()


        public async Task<IQueryable<Order>> GetOrderAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }
            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(p => p.Product)
                    .OrderByDescending(o => o.OrderDate);
            }
            return _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(p => p.Product)
                    .Where(o => o.User == user)
                    .OrderByDescending(o => o.OrderDate);
        } // end GetOrderAsync()


        public async Task<IQueryable<OrderDetailTemp>> GetDetailsTempsAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            return _context.OrdersDetailsTemp
                            .Include(p => p.Product)
                            .Where(o => o.User == user)
                            .OrderBy(o => o.Product.Name);
        } // end GetDetailsTempsAsync()

        // 03.29 - vídeo ASP-NET_MVC_24:
        public async Task AddItemToOrderAsync(AddItemViewModel model, string userName)
        {   // 13.00 - revisão deste método:
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return;
            }

            var product = await _context.Products.FindAsync(model.ProductId);
            if (product == null) // alguém já o apagou
            {
                return;
            }
            // se chegarmos aqui temos user e temos produto.
            var orderDetailTemp = await _context.OrdersDetailsTemp
                                        .Where(odt => odt.User == user && odt.Product == product)
                                        .FirstOrDefaultAsync();

            if (orderDetailTemp == null) // é a 1ª vez que estamos lá a meter um item. Precisamos de criá-lo.
            {
                orderDetailTemp = new OrderDetailTemp
                {
                    Price = product.Price,
                    Product = product,
                    Quantity = model.Quantity,
                    User = user
                };
                _context.OrdersDetailsTemp.Add(orderDetailTemp);
            }
            else  // se já existir
            {
                orderDetailTemp.Quantity += model.Quantity;
                _context.OrdersDetailsTemp.Update(orderDetailTemp);
            }

            // 22.12 - vídeo ASP-NET_MVC_24 (gravar na base de dados):
            await _context.SaveChangesAsync();
        } // end AddItemToOrderAsync()

        public async Task ModifyOrderDetailTempQuantityAsync(int id, double quantity)
        {
            var orderDetailTemp = await _context.OrdersDetailsTemp.FindAsync(id);
            if (orderDetailTemp == null)
            {
                return;
            }

            orderDetailTemp.Quantity += quantity;

            if (orderDetailTemp.Quantity > 0)
            {
                _context.OrdersDetailsTemp.Update(orderDetailTemp);
                await _context.SaveChangesAsync();
            }
        } // end ModifyOrderDetailTempQuantityAsync()


    } // end class OrderRepository 
} // end namespace SuperShopDF.Web.Data

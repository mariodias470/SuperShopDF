using Microsoft.EntityFrameworkCore;
using SuperShopDF.Web.Data.Entities;
using SuperShopDF.Web.Helpers;
using SuperShopDF.Web.Models;
using System;
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
        DeleteDetailTempAsync()
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

            // Se estivermos na presença de um administrador vamos buscar isto:
            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return _context.Orders
                    .Include(o => o.User) // <-- 35.22  - vídeo ASP-NET_MVC_26.
                                          // MUITO IMPORTANTE:
                                          // É como se fosse mais um INNER JOIN:
                                          // Dá-me as encomendas todas e
                                          // dá-me também os users e
                                          // depois dá-me também os itens e
                                          // dá-me também os produtos.
                                          // ATTENÇÃO: ver bem a diferença entre o
                                          // Include
                                          // e o
                                          // ThenInclude
                    .Include(o => o.Items)
                    .ThenInclude(p => p.Product)
                    .OrderByDescending(o => o.OrderDate);
            }
            
            // Se não estivermos na presença de um administrador vamos buscar isto:
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



        // 01.47 - vídeo ASP-NET_MVC_25:
        public async Task DeleteDetailTempAsync(int id) 
        {
            var orderDetailTemp = await _context.OrdersDetailsTemp.FindAsync(id);
            if (orderDetailTemp == null)
            {
                return;
            }
            _context.OrdersDetailsTemp.Remove(orderDetailTemp);
            await _context.SaveChangesAsync();
        } // end DeleteDetailTempAsync()



        // 04.49 - vídeo ASP-NET_MVC_26:
        public async Task<bool> ConfirmOrderAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            
            if (user == null) {  return false; }
            
            var orderTmps = await _context.OrdersDetailsTemp
                .Include(o => o.Product)
                .Where(o => o.User == user)
                .ToListAsync();

            if (orderTmps == null || orderTmps.Count() == 0) { return false; }
            
            var details = orderTmps.Select(o => new OrderDetail
            {
                Price = o.Price,
                Product = o.Product,
                Quantity = o.Quantity
            }).ToList();

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                User = user, 
                Items = details
            };

            // CreateAsync(order); 
            // CS4014: Because this call is not awaited, execution of the current method continues
            //         before the call is completed.
            //         Consider applying the await operator to the result of the call.
            // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/async-await-errors?f1url=%3FappId%3Droslyn%26k%3Dk(CS4014)

            await CreateAsync(order);

            // 13.42 - vídeo ASP-NET_MVC_26, remover o outro:
            _context.OrdersDetailsTemp.RemoveRange(orderTmps);
            await _context.SaveChangesAsync();
            return true;

        } // end ConfirmOrderAsync()

    } // end class OrderRepository 
} // end namespace SuperShopDF.Web.Data

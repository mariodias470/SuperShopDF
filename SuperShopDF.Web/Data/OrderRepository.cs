using Microsoft.EntityFrameworkCore;
using SuperShopDF.Web.Data.Entities;
using SuperShopDF.Web.Helpers;
using System.Linq;
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


    // 41.06 - vídeo ASP-NET_MVC_22:
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public OrderRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

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
                    .ThenInclude(p => p.product)
                    .OrderByDescending(o => o.OrderDate);
            }
            return _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(p => p.product)
                    .Where(o => o.User == user)
                    .OrderByDescending(o => o.OrderDate);
        } // end GetOrderAsync()

    } // end OrderRepository
} // end namespace SuperShopDF.Web.Data

using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml;

namespace SuperShopDF.Web.Data.Entities
{
    public class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        // 1.11.27 vídeo ASP.NET_MVC_11 - Criação de um método que devolve uma lista de produtos, e
        //                                que vai ser utilizado no controlador ProductsController.cs
        public IQueryable GetAllWithUsers()
        {
            return _context.Products.Include(p => p.User); // 1.13.00 vídeo ASP.NET_MVC_11.
                                                           // INNER JOIN com a tabela Users, para que possamos ter
                                                           // acesso ao nome do utilizador que criou o produto.
                                                           // Este INNER JOIN é simples. Lida apenas com 2 tabelas
                                                           // a dos
                                                           // Produtos
                                                           // e a dos Users
        }


    } // end class ProductRepository
} // end namespace SuperShopDF.Web.Data

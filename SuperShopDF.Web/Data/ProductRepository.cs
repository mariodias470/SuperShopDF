using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        // 19.34 - vídeo ASP-NET_MVC_23:

        // public IEnumerable<SelectListItem> IProductRepository.GetComboProducts()
        public IEnumerable<SelectListItem> GetComboProducts()
        {
            // ... 20 objectos do tipo ListItem
            var list = _context.Products.Select(p => new SelectListItem // na prática, isto é como se fosse um foreach()
            {
                Text = p.Name,
                Value = p.Id.ToString()
            }
            ).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Escolha um produto...)",
                Value = "0"
            }
            );

            return list;
        } // end GetComboProducts()

        // 25.01 vídeo ASP-NET_MVC_23: Há até quem faça um helper para fazer os combo boxes.
    } // end class ProductRepository
} // end namespace SuperShopDF.Web.Data

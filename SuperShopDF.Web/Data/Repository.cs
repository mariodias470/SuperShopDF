using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperShopDF.Web.Data.Entities;

namespace SuperShopDF.Web.Data
{
    /*
        1)  void                    AddProduct      (Product product);
        2)  Product                 GetProduct      (int id);
        3)  IEnumerable<Product>    GetProducts     ();
        4)  bool                    ProductExists   (int id);
        5)  void                    RemoveProduct   (Product product);
        6)  Task<bool>              SaveAllAsync    ();
        7)  void                    UpadateProduct  (Product product);
     */

    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            // this._context = context;
            _context = context;
        }

        // 1) 
        // LÊ todos os produtos.
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.OrderBy(p => p.Name);
        }

        // 2) 
        // LÊ um produto específico.
        public Product GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        // 3) 
        // ADICIONA um novo produto, apenas em memória.
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        // 4) 
        // ACTUALIZA produto, apenas em memória.
        public void UpadateProduct(Product product)
        {
            _context.Products.Update(product); // muito importante: RS at vídeo ASP.NET_MVC_07, 8m 8s.
                                               // não temos associação directa à base de dados.
        }

        // 5) 
        // APAGA produto, apenas em memória.
        public void RemoveProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        // 6) 
        public async Task<bool> SaveAllAsync()
        {
            // 11.28 --> Se, pelos menos, uma operação for efectuada com successo, devolve true, caso contrário devolve false.
            return await _context.SaveChangesAsync() > 0;
        }


        // 7)             
        // 12.37 do vídeo ASP.NET_MVC_07:
        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        // 14.00 do vídeo ASP.NET_MVC_07: Como a classe dos produtos é feita por mim vou ter de pôr isto na injecção das dependências.

    } // end class Repository

} // end namespace SuperShopDF.Web.Data

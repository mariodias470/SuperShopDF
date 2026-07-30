using System;
using System.Linq;
using System.Threading.Tasks;
using SuperShopDF.Web.Data.Entities;

namespace SuperShopDF.Web.Data
{
    public class SeedDb
    {
        
        private readonly DataContext _context;
        private Random _random;

        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random(); // Vídeo 6 - 11.47
        }

        public async Task SeedAsync() // v6 - 11.48
        { 
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Products.Any()) 
            {
                AddProduct("iPhone X");
                AddProduct("Magic Mouse");
                AddProduct("iWatch Series 4");
                AddProduct("iPad Mini");

                await _context.SaveChangesAsync();
                // linha supra. Vídeo 6, 12.05 - temos de gravar o produto na base de dados
            }
        } // end SeedAsync()

        private void AddProduct(string name)
        {
            _context.Products.Add(new Product
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvailable = true,
                Stock = _random.Next(100) // vídeo 6 - 11.51
            });

        } // end AddProduct()

    } // end class SeedDb
} // end namespace SuperShopDF.Web.Data

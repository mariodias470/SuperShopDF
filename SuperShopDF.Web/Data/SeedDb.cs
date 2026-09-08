using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SuperShopDF.Web.Data.Entities;
using SuperShopDF.Web.Helpers;

namespace SuperShopDF.Web.Data
{
    public class SeedDb
    {
        
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper; // linha acrecentada aos 49.34 do vídeo ASP.NET_MVC_10
        // private readonly UserManager<User> _userManager; // Fora aos 49.58 do vídeo ASP.NET_MVC_10.
        private Random _random;

        // public SeedDb(DataContext context)
        // public SeedDb(DataContext context, UserManager<User> userManager) // linha comentada aos 49.34 do vídeo ASP.NET_MVC_10
        public SeedDb(DataContext context, IUserHelper userHelper) // linha acrecentada aos 49.34 do vídeo ASP.NET_MVC_10
        {
            _context = context;
            this._userHelper = userHelper;
            // _userManager = userManager; FORA aos 50.04    do vídeo ASP.NET_MVC_10.
            _random = new Random(); // Vídeo 6 - 11.47
        }

        public async Task SeedAsync() // v6 - 11.48
        {  
            await _context.Database.EnsureCreatedAsync(); // verifica se a base de dados existe 29.04 ASP.NET_MVC_10

            // var user = await _userManager.FindByEmailAsync("rafaaaa@gmail.com"); FORA aos 50.30 do vídeo ASP.NET_MVC_10.
            var user = await _userHelper.GetUserByEmailAsync("rafaaaa@gmail.com");  // acrecentado aos 50.35 do vídeo ASP.NET_MVC_10.

            ////---
            //if (result.Succeeded)
            //{
            //    // User was created
            //}
            //else
            //{
            //    foreach (var error in result.Errors)
            //    {
            //        Console.WriteLine($"{error.Code}: {error.Description}");
            //    }
            //}
            ////---

            if (user == null)
            {
                user = new User
                {
                    UserName= "rafaaaa@gmail.com",
                    FirstName = "Rafael",
                    LastName = "Silva",
                    Email = "rafaaaa@gmail.com",
                    PhoneNumber = "123456789"
                };

                // FORA aos 51.00 do vídeo ASP.NET_MVC_10:
                // var result = await _userManager.CreateAsync(user, "123456"); // 29.29 do vídeo ASP.NET_MVC_?? - O User Manager cria os Produtos 
                var result = await _userHelper.AddUserAsync(user, "123456"); // 29.29 - O User Manager cria os Produtos

                if (result != IdentityResult.Success)
                {
                    // return;
                    //foreach (var error in result.Errors)
                    //{
                    //    Console.WriteLine($"{error.Code}: {error.Description}");
                    //}
                    //return;
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!_context.Products.Any()) 
            {
                AddProduct("iPhone X", user);
                AddProduct("Magic Mouse", user);
                AddProduct("iWatch Series 4", user);
                AddProduct("iPad Mini", user);

                await _context.SaveChangesAsync();
                // linha supra. Vídeo 6, 12.05 - temos de gravar o produto na base de dados
            }
        } // end SeedAsync()

        private void AddProduct(string name, User user)
        {
            _context.Products.Add(new Product
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvailable = true,
                Stock = _random.Next(100), // vídeo 6 - 11.51
                User = user 
            });

        } // end AddProduct()

    } // end class SeedDb
} // end namespace SuperShopDF.Web.Data

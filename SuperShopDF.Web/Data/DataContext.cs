using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // <-- acrescentei à mão
using Microsoft.EntityFrameworkCore;
using SuperShopDF.Web.Data.Entities;

namespace SuperShopDF.Web.Data
{
    /*
        public DbSet<Product> 		    Products 		    { get; set; }
        public DbSet<Order> 		    Orders 			    { get; set; }
        public DbSet<OrderDetail> 	    OrdersDetails 		{ get; set; }
        public DbSet<OrderDetailTemp> 	OrdersDetailsTemp 	{ get; set; }
     */

    // public class DataContext : DbContext
    public class DataContext: IdentityDbContext<User>
    {
        // public DbSet<SuperShop105.Data.Entities.Product> Products { get; set; }
        public DbSet<Product> Products { get; set; }

        // 22.52 do vídeo ASP.NET_MVC_22 (acerscentar as tabelas)
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrdersDetails { get; set; }
        public DbSet<OrderDetailTemp> OrdersDetailsTemp { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

    } // end class Datacontext
} // end SuperShopDF.Web.Data

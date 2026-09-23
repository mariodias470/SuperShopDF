using System.Linq;
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
        } // end DataContext()

        
        
        // Activar a regra de apagar em cascata (Cascade delete rule)
        //---------------------------------------------------
        // 09.33 do vídeo ASP.NET_MVC_27:
        // (como é se se activa o 'apagar em cascata'):
        //---------------------------------------------------
/*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model
                  .GetEntityTypes()
                  .SelectMany(t => t.GetForeignKeys())
                  .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

              foreach(var fk in cascadeFKs)
              {
                  fk.DeleteBehavior = DeleteBehavior.Restrict;
              }

              base.OnModelCreating(modelBuilder);
            
            // 15.12 do vídeo ASP.NET_MVC_27:
            // a forma mais rápida de implementar esta funcionalidade é mandar a
            // base de dados abaixo e executar o seed().

        } // end OnModelCreating()
*/

    } // end class Datacontext
} // end SuperShopDF.Web.Data

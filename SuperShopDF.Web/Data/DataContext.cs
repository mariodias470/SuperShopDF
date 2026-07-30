using Microsoft.EntityFrameworkCore;
using SuperShopDF.Web.Data.Entities;

namespace SuperShopDF.Web.Data
{
    public class DataContext : DbContext
    {
        // public DbSet<SuperShop105.Data.Entities.Product> Products { get; set; }
        public DbSet<Product> Products { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

    } // end class Datacontext
} // end SuperShopDF.Web.Data

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperShopDF.Web.Data;
using SuperShopDF.Web.Data.Entities;

// Aos 30.40 do vídeo ASP.NET_MVC_08, o professor apaga a interface IRepository e a classe Repository, porque vamos usar a GenericRepository.
// Passamos a usar apenas o genérico.


namespace SuperShopDF.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) { Configuration = configuration; }

        
        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<DataContext>(cfg =>
            {
                // 1h 13m do vídeo (leitura deste excerto de código)
                // 2026_m07_JUL_d21_3F_[ASP.NET_MVC_04]_.mp4
                // Atenção à ressalva do professor à 1h 1m do vídeo.
                cfg.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient<SeedDb>(); // O seed é criado e desaparece 31.30 do vídeo ASP.NET_MVC_07.

            // 14.00 do vídeo ASP.NET_MVC_07: Como a classe dos produtos é feita por mim vou ter de pôr isto na injecção das dependências.


            // 30.46 do vídeo ASP.NET_MVC_07: injecção de dependências para a classe Repository:
            services.AddTransient<SeedDb>();
            // services.AddScoped<IRepository, Repository>(); 


            // 31.17 do vídeo ASP.NET_MVC_08:
            services.AddScoped<IProductRepository, ProductRepository>(); 

            // MOCK REPOSITORY:
            // services.AddScoped<IRepository, MockRepository>(); // 72.49 do vídeo ASP.NET_MVC_07: utilização do MockRepository.
            // services.AddSingleton<>

            // services.AddSingleton
            // services.AddScoped();

            services.AddControllersWithViews();
        
        } // end ConfigureServices(()




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        } // end Configure()

        // 33.06 --> ASP.NET_MVC_07 acedêmos através do repositório e não acedo directamente ao contexto (i.e., aos dados).

    } // end class public class Startup
} // end namespace SuperShopDF.Web

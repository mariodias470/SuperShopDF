using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperShopDF.Web.Data;
using SuperShopDF.Web.Data.Entities;
using SuperShopDF.Web.Helpers;

        
        /*
            ConfigureServices()
            Configure()
         */


// Aos 30.40 do vídeo ASP.NET_MVC_08, o professor apaga a interface IRepository e a classe Repository, porque vamos usar a GenericRepository.
// Passamos a usar apenas o genérico.




// Dependency Injection in .NET Step by Step Tutorial
// https://www.youtube.com/watch?v=VXb3dirvL3I
// Lifecycles: transient, scoped, singleton.
// 19.30:
// --> No data shared across requests --> Transient 
// --> Data shared within the same request but not across requests --> Scoped 
// --> Data shared across requests --> singleton



namespace SuperShopDF.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) { Configuration = configuration; }

        public IConfiguration Configuration { get; }

        
        /*--------------------------------
         | ConfigureServices()
         +--------------------------------*/
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 30.05 --> 3º passo: configuração do serviço de autentivação (ASP.NET_MVC_10)
            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 0;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<DataContext>();


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
            // services.AddTransient<SeedDb>();
            // services.AddScoped<IRepository, Repository>(); 

            // 48.56 do vídeo ASP.NET_MVC_10:
            services.AddScoped<IUserHelper, UserHelper>();

            // 09.42 do vídeo ASP.NET_MVC_12:
            services.AddScoped<IImageHelper, ImageHelper>();

            // 24.13 do vídeo ASP.NET_MVC_12:
            services.AddScoped<IConverterHelper, ConverterHelper>();

            // 57.51 do vídeo ASP.NET_MVC_22:
            services.AddScoped<IProductRepository, ProductRepository>();


            services.AddScoped<IOrderRepository, OrderRepository>();

            // MOCK REPOSITORY:
            // services.AddScoped<IRepository, MockRepository>(); // 72.49 do vídeo ASP.NET_MVC_07: utilização do MockRepository.
            // services.AddSingleton<>

            // services.AddSingleton
            // services.AddScoped();

            // 06.41 do vídeo ASP.NET_MVC_21:
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/NotAuthorized";
                options.AccessDeniedPath = "/Account/NotAuthorized";
            }
            );
            // FIM - 06.41 do vídeo ASP.NET_MVC_21:

            services.AddControllersWithViews();
        
        } // end ConfigureServices(()



        /*--------------------------------
         | Configure()
         +--------------------------------*/
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


            // 22.29 ASP.NET_MVC_21. Middleware. Como é que eçe responde quando pesquisamos por uma página não encontrada.
            app.UseStatusCodePagesWithReExecute("/error/{0}");


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            // app.UseAuthentication(); só preciso disto para o login, logout e register.
            // Não preciso disto para o CRUD dos produtos. 57.10 ASP.NET_MVC_10. Middleware de autenticação, adicionado por mim no dia 5
            app.UseAuthentication(); // 57.10 ASP.NET_MVC_10. Middleware de autenticação, adicionado por mim no dia 5


            app.UseAuthorization(); // adicionado por mim no dia 5

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

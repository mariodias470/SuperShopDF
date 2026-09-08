using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperShopDF.Web.Data;
using SuperShopDF.Web.Data.Entities;
using SuperShopDF.Web.Helpers;

namespace SuperShopDF.Web.Controllers
{
    /* 

    LINHA SAGRADA:

     D:\CppCet105\RS2026\Projs\SuperShopDF\SuperShopDF.Web>
                                   dotnet aspnet-codegenerator 
                                   controller -name ProductsController 
                                   -m Product 
                                   -dc DataContext 
                                   --relativeFolderPath Controllers 
                                   --useDefaultLayout 
                                   --referenceScriptLibraries
    */

    /*
            public async Task<IActionResult>    1) Index()                         GET
            public async Task<IActionResult>    2) Details(int? id)                GET

            public IActionResult                3) Create()                        GET
            public async Task<IActionResult>    4) Create(Product product)         POST

            public async Task<IActionResult>    5) Edit(int? id)                   GET
            public async Task<IActionResult>    6) Edit(int id, Product product)   POST

            public async Task<IActionResult>    7) Delete(int? id)                 GET
            public async Task<IActionResult>    8) DeleteConfirmed(int id)         POST

            private bool                        9) ProductExists(int id)           
    */


    public class ProductsController : Controller
    {
        // -- private readonly IRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IUserHelper _userHelper;

        // FORA:
        // private readonly DataContext _context;

        // FORA: 
        // public ProductsController(DataContext context) // Este DataContext acabou: 18m 50s do vídeo 2026_m07_JUL_d21_3F_[ASP.NET_MVC_07] _.mp4
        // {
        //     _context = context;
        // }

        // -- public ProductsController(IRepository repository)  // aos 30.20 do vídeo 2026_m07_JUL_d21_3F_[ASP.NET_MVC_07]_.mp4: o objecto repository não foi instanciado.
        // --                                                    // O injector de dependências não está a injectar isto aqui. Temos de ir ao ficheiro Startup.cs e dizer
        // --                                                    // vou aqui criar um serviço,e, quando fôr preciso, tu compilas o Interface do repositório e, depois, quando fôr necessário, ele
        // --                                                    // vai ser instânciado (injectado). A classe Repository será injectada quando fôr necessário:
        // --                                                    // services.AddScoped<IRepository, Repository>();  

        // -- {
        // --     _repository = repository;
        // -- }


        // Aos 52.26 do vídeo ASP.NET_MVC_10.mp4:
        // Primeiro, é sempre o mesmo procedimento, Injectar o nosso UserHelper, para depois podermos utilizá-lo.
        public ProductsController
            (
                IProductRepository productRepository, 
                IUserHelper userHelper
            )

        // public ProductsController(IProductRepository productRepository)
        {
            // _repository = repository;
            _productRepository = productRepository;
            _userHelper = userHelper;
        }



        //----------------------------------------------
        // 1)
        //----------------------------------------------
        // GET: Products
        // public async Task<IActionResult> Index()  // go to View --> ^MG
        public IActionResult Index()
        {
            // -- return View(_repository.GetProducts());
            return View(_productRepository.GetAll().OrderBy(p => p.Name));
        } // end Index() [1]


        //----------------------------------------------
        // 2)
        //----------------------------------------------
        // GET: Products/Details/5
        // public async Task<IActionResult> Details(int? id)
        // -- public IActionResult Details(int? id) // pode aceitar null.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var product = await _context.Products
            //     .FirstOrDefaultAsync(m => m.Id == id);
            // -- var product = _repository.GetProduct(id.Value); 
            var product = await _productRepository.GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        } // end Details(int? id) [2]


        //----------------------------------------------
        // 3)
        //----------------------------------------------
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        } // end Create() [3]


        //----------------------------------------------
        // 4)
        //----------------------------------------------
        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvailable,Stock")] Product product)
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // TODO: modificar para o user que estiver logado (é o user indentity.Name) [Vide janela "Task List"].
                // Aos 53.26 do vídeo ASP.NET_MVC_10.mp4: Antes de gravar o produto na base de dados, temos
                // de associar o produto ao utilizador que está a criar o produto.
                product.User = await _userHelper.GetUserByEmailAsync("rafaaaa@gmail.com");


                // _context.Add(product); // muito importante: RS at vídeo ASP.NET_MVC_07, 8m 8s.
                // não temos associação directa à base de dados.
                // -- _repository.AddProduct(product);
                await _productRepository.CreateAsync(product);

                // await _context.SaveChangesAsync();
                // -- await _repository.SaveAllAsync();
                // 37.15 --> Vídeo 2026_m07_JUL_d21_3F_[ASP.NET_MVC_08]_.mp4: Não precisamos de gravar aqui nada, porque o método CreateAsync já
                // faz isso.
                // O método CreateAsync já chama o SaveChangesAsync() internamente.

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        } // ends Create(Product product) [4]


        //----------------------------------------------
        // 5)
        //----------------------------------------------
        // GET: Products/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // -- public IActionResult Edit(int? id)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var product = await _context.Products.FindAsync(id);
            // -- var product = _repository.GetProduct(id.Value);
            var product = await _productRepository.GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        } // end Edit(int? id) [5]


        //----------------------------------------------
        // 6)
        //----------------------------------------------
        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvailable,Stock")] Product product)
        // public async Task<IActionResult> Edit(int id, Product product)
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // _context.Update(product);
                    // -- _repository.UpadateProduct(product); 

                    // 1.02.10 do vídeo ASP.NET_MVC_10:
                    // TODO: modificar para o user que estiver logado (é o user indentity.Name) [Vide janela "Task List"]
                    product.User = await _userHelper.GetUserByEmailAsync("rafaaaa@gmail.com");

                    await _productRepository.UpdateAsync(product); // 39.02 do vídeo ASP.NET_MVC_08.mp3


                    // await _context.SaveChangesAsync();
                    // -- await _repository.SaveAllAsync(); 39.12 do vídeo 2026_m07_JUL_d21_3F_[ASP.NET_MVC_08]_.mp4: Também
                    // não precisamos de gravar aqui nada.
                }
                catch (DbUpdateConcurrencyException)
                {
                    // if (!ProductExists(product.Id)) { return NotFound(); }
                    // -- if (!_repository.ProductExists(product.Id))
                    if (!await _productRepository.ExistAsync(product.Id))   // 39.42 do vídeo 2026_m07_JUL_d21_3F_[ASP.NET_MVC_08]_.mp4
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        } // end Edit(int id, Product product) [6]


        //----------------------------------------------
        // 7)
        //----------------------------------------------
        // GET: Products/Delete/5
        // public async Task<IActionResult> Delete(int? id) // 26.00 do vídeo ASP.NET_MVC_07.mp3
        // -- public IActionResult Delete(int? id) // 26.00 do vídeo ASP.NET_MVC_07.mp3
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            // -- var product = _repository.GetProduct(id.Value);
            var product = await _productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        } // end Delete(int? id) [7]


        //----------------------------------------------
        // 8)
        //----------------------------------------------
        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // var product = await _context.Products.FindAsync(id);
            // -- var product = _repository.GetProduct(id);
            var product = await _productRepository.GetByIdAsync(id);

            // _context.Products.Remove(product);   // 1.31.40 --> Remove da memória!...
            // -- _repository.RemoveProduct(product);
            await _productRepository.DeleteAsync(product);

            // await _context.SaveChangesAsync();
            // -- await _repository.SaveAllAsync();
            // 41.16 do vídeo 2026_m07_JUL_d21_3F_[ASP.NET_MVC_08]_.mp4: Também não precisamos de gravar aqui nada, porque 

            return RedirectToAction(nameof(Index));
            /*
                10.14-- > return RedirectToAction(nameof(Index)); <=> return RedirectToAction("Index"));
                                                   à antiga                               à moderna
            */
        } // end DeleteConfirmed(int id) [8]


        //----------------------------------------------
        // 9)
        //----------------------------------------------
        // FORA: 27.35 do vídeo ASP.NET_MVC_07.mp4
        // private bool ProductExists(int id)
        // {
        //return _context.Products.Any(e => e.Id == id);
        // } // end ProductExists(int id) [9]

    } // end class ProductsController 

} // end namespace SuperShopDF.Web.Controllers




/*
    ================================================
    Conteúdo deste ficheiro até aos 18m 50s do
    vídeo 2026_m07_JUL_d21_3F_[ASP.NET_MVC_07]_.mp4:
    ================================================
*/

//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SuperShopDF.Web.Data;
//using SuperShopDF.Web.Data.Entities;

//namespace SuperShopDF.Web.Controllers
//{
//    /* 

//    LINHA SAGRADA:

//     D:\CppCet105\RS2026\Projs\SuperShopDF\SuperShopDF.Web>
//                                   dotnet aspnet-codegenerator 
//                                   controller -name ProductsController 
//                                   -m Product 
//                                   -dc DataContext 
//                                   --relativeFolderPath Controllers 
//                                   --useDefaultLayout 
//                                   --referenceScriptLibraries
//    */

///*
//        public async Task<IActionResult>    1) Index()                         GET
//        public async Task<IActionResult>    2) Details(int? id)                GET

//        public IActionResult                3) Create()                        GET
//        public async Task<IActionResult>    4) Create(Product product)         POST

//        public async Task<IActionResult>    5) Edit(int? id)                   GET
//        public async Task<IActionResult>    6) Edit(int id, Product product)   POST

//        public async Task<IActionResult>    7) Delete(int? id)                 GET
//        public async Task<IActionResult>    8) DeleteConfirmed(int id)         POST

//        private bool                        9) ProductExists(int id)           
//*/


//public class ProductsController : Controller
//{

//    private readonly DataContext _context;


//    public ProductsController(DataContext context)
//    {
//        _context = context;
//    }


//    // 1)
//    // GET: Products
//    public async Task<IActionResult> Index()  // go to View --> ^MG
//    {
//        return View(await _context.Products.ToListAsync());
//    }


//    // 2)
//    // GET: Products/Details/5
//    public async Task<IActionResult> Details(int? id)
//    {
//        if (id == null)
//        {
//            return NotFound();
//        }

//        var product = await _context.Products
//            .FirstOrDefaultAsync(m => m.Id == id);
//        if (product == null)
//        {
//            return NotFound();
//        }

//        return View(product);
//    }


//    // 3)
//    // GET: Products/Create
//    public IActionResult Create()
//    {
//        return View();
//    }


//    // 4)
//    // POST: Products/Create
//    // To protect from overposting attacks, enable the specific properties you want to bind to.
//    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    // public async Task<IActionResult> Create([Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvailable,Stock")] Product product)
//    public async Task<IActionResult> Create(Product product)
//    {
//        if (ModelState.IsValid)
//        {
//            _context.Add(product); // muito importante: RS at vídeo ASP.NET_MVC_07, 8m 8s.
//                                   // não temos associação directa à base de dados.
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }
//        return View(product);
//    }


//    // 5)
//    // GET: Products/Edit/5
//    public async Task<IActionResult> Edit(int? id)
//    {
//        if (id == null)
//        {
//            return NotFound();
//        }

//        var product = await _context.Products.FindAsync(id);
//        if (product == null)
//        {
//            return NotFound();
//        }
//        return View(product);
//    }



//    // 6)
//    // POST: Products/Edit/5
//    // To protect from overposting attacks, enable the specific properties you want to bind to.
//    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    // public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvailable,Stock")] Product product)
//    public async Task<IActionResult> Edit(int id, Product product)
//    {
//        if (id != product.Id)
//        {
//            return NotFound();
//        }

//        if (ModelState.IsValid)
//        {
//            try
//            {
//                _context.Update(product);
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ProductExists(product.Id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }
//            return RedirectToAction(nameof(Index));
//        }
//        return View(product);
//    }


//    // 7)
//    // GET: Products/Delete/5
//    public async Task<IActionResult> Delete(int? id)
//    {
//        if (id == null)
//        {
//            return NotFound();
//        }

//        var product = await _context.Products
//            .FirstOrDefaultAsync(m => m.Id == id);
//        if (product == null)
//        {
//            return NotFound();
//        }

//        return View(product);
//    }


//    // 8)
//    // POST: Products/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> DeleteConfirmed(int id)
//    {
//        var product = await _context.Products.FindAsync(id);
//        _context.Products.Remove(product);   // 1.31.40 --> Remove da memória!...
//        await _context.SaveChangesAsync();
//        return RedirectToAction(nameof(Index));
//        /*
//            10.14-- > return RedirectToAction(nameof(Index)); <=> return RedirectToAction("Index"));
//                                               à antiga                               à moderna
//        */
//    }


//    // 9)
//    private bool ProductExists(int id)
//    {
//        return _context.Products.Any(e => e.Id == id);
//    }

//} // end class ProductsController 
//} // end namespace SuperShopDF.Web.Controllers











/*

29.28 do vídeo ASP.NET_MVC_07.mp4 :

(A reter deste erro: 
 Unable to resolve service for type 'SuperShopDF.Web.Data.IRepository' while attempting to activate 
'SuperShopDF.Web.Controllers.ProductsController'.)

https://localhost:44334/Products
 
 An unhandled exception occurred while processing the request.
InvalidOperationException: Unable to resolve service for type 'SuperShopDF.Web.Data.IRepository' while attempting to activate 'SuperShopDF.Web.Controllers.ProductsController'.
Microsoft.Extensions.DependencyInjection.ActivatorUtilities.GetService(IServiceProvider sp, Type type, Type requiredBy, bool isDefaultParameterRequired)

Stack Query Cookies Headers Routing
InvalidOperationException: Unable to resolve service for type 'SuperShopDF.Web.Data.IRepository' while attempting to activate 'SuperShopDF.Web.Controllers.ProductsController'.
Microsoft.Extensions.DependencyInjection.ActivatorUtilities.GetService(IServiceProvider sp, Type type, Type requiredBy, bool isDefaultParameterRequired)
lambda_method21(Closure , IServiceProvider , object[] )
Microsoft.AspNetCore.Mvc.Controllers.ControllerActivatorProvider+<>c__DisplayClass4_0.<CreateActivator>b__0(ControllerContext controllerContext)
Microsoft.AspNetCore.Mvc.Controllers.ControllerFactoryProvider+<>c__DisplayClass5_0.<CreateControllerFactory>g__CreateController|0(ControllerContext controllerContext)
Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(ref State next, ref Scope scope, ref object state, ref bool isCompleted)
Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextResourceFilter>g__Awaited|24_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, object state, bool isCompleted)
Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Rethrow(ResourceExecutedContextSealed context)
Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Next(ref State next, ref Scope scope, ref object state, ref bool isCompleted)
Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.InvokeFilterPipelineAsync()
Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
Microsoft.AspNetCore.Routing.EndpointMiddleware.<Invoke>g__AwaitRequestTask|6_0(Endpoint endpoint, Task requestTask, ILogger logger)
Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware.Invoke(HttpContext context)

Show raw exception details

*/




















/*
    ================================================
    Conteúdo deste ficheiro até aos 31m 55s do
    vídeo 2026_m07_JUL_d21_3F_[ASP.NET_MVC_08]_.mp4:
    ================================================
*/

//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SuperShopDF.Web.Data;
//using SuperShopDF.Web.Data.Entities;

//namespace SuperShopDF.Web.Controllers
//{
//    /* 

//    LINHA SAGRADA:

//     D:\CppCet105\RS2026\Projs\SuperShopDF\SuperShopDF.Web>
//                                   dotnet aspnet-codegenerator 
//                                   controller -name ProductsController 
//                                   -m Product 
//                                   -dc DataContext 
//                                   --relativeFolderPath Controllers 
//                                   --useDefaultLayout 
//                                   --referenceScriptLibraries
//    */

///*
//        public async Task<IActionResult>    1) Index()                         GET
//        public async Task<IActionResult>    2) Details(int? id)                GET

//        public IActionResult                3) Create()                        GET
//        public async Task<IActionResult>    4) Create(Product product)         POST

//        public async Task<IActionResult>    5) Edit(int? id)                   GET
//        public async Task<IActionResult>    6) Edit(int id, Product product)   POST

//        public async Task<IActionResult>    7) Delete(int? id)                 GET
//        public async Task<IActionResult>    8) DeleteConfirmed(int id)         POST

//        private bool                        9) ProductExists(int id)           
//*/


//public class ProductsController : Controller
//{
//    private readonly IRepository _repository;

//    // FORA:
//    // private readonly DataContext _context;

//    // FORA: 
//    // public ProductsController(DataContext context) // Este DataContext acabou: 18m 50s do vídeo 2026_m07_JUL_d21_3F_[ASP.NET_MVC_07] _.mp4
//    // {
//    //     _context = context;
//    // }

//    // -- public ProductsController(IRepository repository)  // aos 30.20 do vídeo 2026_m07_JUL_d21_3F_[ASP.NET_MVC_07]_.mp4: o objecto repository não foi instanciado.
//    // --                                                    // O injector de dependências não está a injectar isto aqui. Temos de ir ao ficheiro Startup.cs e dizer
//    // --                                                    // vou aqui criar um serviço,e, quando fôr preciso, tu compilas o Interface do repositório e, depois, quando fôr necessário, ele
//    // --                                                    // vai ser instânciado (injectado). A classe Repository será injectada quando fôr necessário:
//    // --                                                    // services.AddScoped<IRepository, Repository>();  

//    // -- {
//    // --     _repository = repository;
//    // -- }

//    public ProductsController(IRepository repository)
//    {
//        _repository = repository;
//    }



//    //----------------------------------------------
//    // 1)
//    //----------------------------------------------
//    // GET: Products
//    // public async Task<IActionResult> Index()  // go to View --> ^MG
//    public IActionResult Index()
//    {
//        return View(_repository.GetProducts());
//    } // end Index() [1]


//    //----------------------------------------------
//    // 2)
//    //----------------------------------------------
//    // GET: Products/Details/5
//    // public async Task<IActionResult> Details(int? id)
//    public IActionResult Details(int? id) // pode aceitar null.
//    {
//        if (id == null)
//        {
//            return NotFound();
//        }

//        // var product = await _context.Products
//        //     .FirstOrDefaultAsync(m => m.Id == id);
//        var product = _repository.GetProduct(id.Value);

//        if (product == null)
//        {
//            return NotFound();
//        }

//        return View(product);
//    } // end Details(int? id) [2]


//    //----------------------------------------------
//    // 3)
//    //----------------------------------------------
//    // GET: Products/Create
//    public IActionResult Create()
//    {
//        return View();
//    } // end Create() [3]


//    //----------------------------------------------
//    // 4)
//    //----------------------------------------------
//    // POST: Products/Create
//    // To protect from overposting attacks, enable the specific properties you want to bind to.
//    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    // public async Task<IActionResult> Create([Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvailable,Stock")] Product product)
//    public async Task<IActionResult> Create(Product product)
//    {
//        if (ModelState.IsValid)
//        {
//            // _context.Add(product); // muito importante: RS at vídeo ASP.NET_MVC_07, 8m 8s.
//            // não temos associação directa à base de dados.
//            _repository.AddProduct(product);

//            // await _context.SaveChangesAsync();
//            await _repository.SaveAllAsync();
//            return RedirectToAction(nameof(Index));
//        }
//        return View(product);
//    } // ends Create(Product product) [4]


//    //----------------------------------------------
//    // 5)
//    //----------------------------------------------
//    // GET: Products/Edit/5
//    // public async Task<IActionResult> Edit(int? id)
//    public IActionResult Edit(int? id)
//    {
//        if (id == null)
//        {
//            return NotFound();
//        }

//        // var product = await _context.Products.FindAsync(id);
//        var product = _repository.GetProduct(id.Value);

//        if (product == null)
//        {
//            return NotFound();
//        }
//        return View(product);
//    } // end Edit(int? id) [5]


//    //----------------------------------------------
//    // 6)
//    //----------------------------------------------
//    // POST: Products/Edit/5
//    // To protect from overposting attacks, enable the specific properties you want to bind to.
//    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    // public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvailable,Stock")] Product product)
//    // public async Task<IActionResult> Edit(int id, Product product)
//    public async Task<IActionResult> Edit(int id, Product product)
//    {
//        if (id != product.Id)
//        {
//            return NotFound();
//        }

//        if (ModelState.IsValid)
//        {
//            try
//            {
//                // _context.Update(product);
//                _repository.UpadateProduct(product);

//                // await _context.SaveChangesAsync();
//                await _repository.SaveAllAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                // if (!ProductExists(product.Id)) { return NotFound(); }
//                if (!_repository.ProductExists(product.Id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }
//            return RedirectToAction(nameof(Index));
//        }
//        return View(product);
//    } // end Edit(int id, Product product) [6]


//    //----------------------------------------------
//    // 7)
//    //----------------------------------------------
//    // GET: Products/Delete/5
//    // public async Task<IActionResult> Delete(int? id) // 26.00 do vídeo ASP.NET_MVC_07.mp3
//    public IActionResult Delete(int? id) // 26.00 do vídeo ASP.NET_MVC_07.mp3
//    {
//        if (id == null)
//        {
//            return NotFound();
//        }

//        // var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
//        var product = _repository.GetProduct(id.Value);
//        if (product == null)
//        {
//            return NotFound();
//        }

//        return View(product);
//    } // end Delete(int? id) [7]


//    //----------------------------------------------
//    // 8)
//    //----------------------------------------------
//    // POST: Products/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    // public async Task<IActionResult> DeleteConfirmed(int id)
//    public async Task<IActionResult> DeleteConfirmed(int id)
//    {
//        // var product = await _context.Products.FindAsync(id);
//        var product = _repository.GetProduct(id);

//        // _context.Products.Remove(product);   // 1.31.40 --> Remove da memória!...
//        _repository.RemoveProduct(product);

//        // await _context.SaveChangesAsync();
//        await _repository.SaveAllAsync();

//        return RedirectToAction(nameof(Index));
//        /*
//            10.14-- > return RedirectToAction(nameof(Index)); <=> return RedirectToAction("Index"));
//                                               à antiga                               à moderna
//        */
//    } // end DeleteConfirmed(int id) [8]


//    //----------------------------------------------
//    // 9)
//    //----------------------------------------------
//    // FORA: 27.35 do vídeo ASP.NET_MVC_07.mp4
//    // private bool ProductExists(int id)
//    // {
//    //return _context.Products.Any(e => e.Id == id);
//    // } // end ProductExists(int id) [9]

//} // end class ProductsController 

//} // end namespace SuperShopDF.Web.Controllers



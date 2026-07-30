using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperShopDF.Web.Data;
using SuperShopDF.Web.Data.Entities;

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

        private readonly DataContext _context;


        public ProductsController(DataContext context)
        {
            _context = context;
        }


        // 1)
        // GET: Products
        public async Task<IActionResult> Index()  // go to View --> ^MG
        {
            return View(await _context.Products.ToListAsync());
        }


        // 2)
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // 3)
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }


        // 4)
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
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        // 5)
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }



        // 6)
        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvailable,Stock")] Product product)
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
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
        }


        // 7)
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // 8)
        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);   // 1.31.40 --> Remove da memória!...
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            /*
                10.14-- > return RedirectToAction(nameof(Index)); <=> return RedirectToAction("Index"));
                                                   à antiga                               à moderna
            */
        }


        // 9)
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

    } // end class ProductsController 
} // end namespace SuperShopDF.Web.Controllers

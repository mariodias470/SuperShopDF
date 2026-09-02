using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SuperShopDF.Web.Data.Entities;


// Ficheiro da class criada às 11.35 do vídeo ASP.NET_MVC_08.
// Classe terminada às 25.02 do vídeo ASP.NET_MVC_08.

namespace SuperShopDF.Web.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }


        // 1)
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking(); // AsNoTracking() is used to improve performance when yo
            // 15.55 --> Set<T> é a tabela. ASP.NET_MVC_08.mp4
        }


        // 2)
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        // 3)
        public async Task CreateAsync(T entity) 
        {
            // 20.49, técnica do by-pass" Vídeo ASP.NET_MVC_08.mp4
            await _context.Set<T>().AddAsync(entity);
            await SaveAllAsync();
        }

        // 4)
        public async Task UpdateAsync(T entity)
        {
            // 20.49, técnica do by-pass" Vídeo ASP.NET_MVC_08.mp4
            // -- -- _context.Set<T>().AddAsync(entity);
            _context.Set<T>().Update(entity);
            await SaveAllAsync();
        }


        // 5)
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await SaveAllAsync();
        }

        // 6)
        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Set<T>().AnyAsync(e => e.Id == id);
        }


        // 7)
        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    } // end class GenericRepository
} // end namespace SuperShopDF.Web.Data

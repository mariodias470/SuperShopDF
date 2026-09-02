using System.Linq;
using System.Threading.Tasks;

namespace SuperShopDF.Web.Data
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(); 

        Task<T> GetByIdAsync(int id);

        Task CreateAsync (T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(int id);

        // 10.49 do vídeo ASP.NET_MVC_08:
        // Não é aqui que definimos a assinatura do método que grava os dados, porque depede dos detalhes da implementação.

    } // end interface IGenericRepository
} // end namespace SuperShopDF.Web.Data

using System.Linq;
using System.Threading.Tasks;

namespace UrlShorten.EntityFrameworkCore.Repositories
{
    public interface IRepository<T, in TKey> where T: class, IEntityBase<TKey>
    {
        IQueryable<T> GetAll();
        T Get(TKey id);
        Task<T> GetAsync(TKey id);

        T Create(T entity);
        Task<T> CreateAsync(T entity);

        T Update(T entity);
        Task<T> UpdateAsync(T entity);

        void Delete(T entity);
        Task DeleteAsync(T entity);

        void Delete(TKey id);
        Task DeleteAsync(TKey id);
    }
}
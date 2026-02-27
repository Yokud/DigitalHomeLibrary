using DigitalHomeLibrary.TrackingBooks.Infractructure;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.TrackingBooks.DataAccess.Repositories.Abstract
{
    public interface IAsyncRepository<T>
    {
        Task<T?> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, PaginationInfo? paginationInfo = null);
        Task<Guid> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task SaveAsync();
    }
}

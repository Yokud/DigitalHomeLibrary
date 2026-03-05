using DigitalHomeLibrary.BookService.Infractructure;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.BookService.Domain.Repositories
{
    public interface IAsyncRepository<T>
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, PaginationInfo? paginationInfo = null);
        Task<Guid> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task SaveAsync();
    }
}

using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Infractructure;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.BookService.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(Guid id);
        Task<IEnumerable<Book>> FindAsync(Expression<Func<Book, bool>>? filter = null, PaginationInfo? paginationInfo = null);
        Task<Guid> AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Guid id);
    }
}
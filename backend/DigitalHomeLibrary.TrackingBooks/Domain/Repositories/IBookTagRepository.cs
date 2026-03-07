using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Infractructure;

namespace DigitalHomeLibrary.BookService.Domain.Repositories
{
    public interface IBookTagRepository
    {
        Task<BookTag?> GetByIdAsync(Guid id);
        Task<BookTag?> FindByNameAsync(string name);
        Task<IEnumerable<BookTag>> GetAllAsync(PaginationInfo? paginationInfo = null);
        Task<Guid> AddAsync(BookTag book);
        Task UpdateAsync(BookTag book);
        Task DeleteAsync(Guid id);
    }
}

using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Domain.Repositories
{
    public interface IBookTagRepository
    {
        Task<BookTag?> GetByIdAsync(Guid id);
        Task<BookTag?> FindByNameAsync(string name);
        Task<IEnumerable<BookTag>> GetAllAsync(PaginationParams? paginationInfo = null);
        Task<Guid> AddAsync(BookTag tag);
        Task UpdateAsync(BookTag tag);
        Task DeleteAsync(Guid id);
    }
}

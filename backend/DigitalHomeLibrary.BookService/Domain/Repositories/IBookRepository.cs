using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<bool> Exists(Guid id);
        Task<Book?> GetByIdAsync(Guid id);
        Task<Book?> FindByTitleAsync(string name);
        Task<IReadOnlyList<Book>> GetAllAsync(PaginationParams? paginationInfo = null);
        Task<Guid> AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Guid id);
    }
}
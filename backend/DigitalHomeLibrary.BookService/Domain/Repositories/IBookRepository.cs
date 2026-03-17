using DigitalHomeLibrary.BookService.Application.DTO.Info;
using DigitalHomeLibrary.BookService.Domain.Entities;

namespace DigitalHomeLibrary.BookService.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<bool> Exists(Guid id);
        Task<Book?> GetByIdAsync(Guid id);
        Task<Book?> FindByTitleAsync(string name);
        Task<IReadOnlyList<Book>> GetAllAsync(PaginationInfo? paginationInfo = null);
        Task<Guid> AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Guid id);
    }
}
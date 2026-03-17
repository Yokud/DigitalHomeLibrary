using DigitalHomeLibrary.BookService.Application.DTO.Info;
using DigitalHomeLibrary.BookService.Domain.Entities;

namespace DigitalHomeLibrary.BookService.Domain.Repositories
{
    public interface IBookTagRepository
    {
        Task<BookTag?> GetByIdAsync(Guid id);
        Task<BookTag?> FindByNameAsync(string name);
        Task<IEnumerable<BookTag>> GetAllAsync(PaginationInfo? paginationInfo = null);
        Task<Guid> AddAsync(BookTag tag);
        Task UpdateAsync(BookTag tag);
        Task DeleteAsync(Guid id);
    }
}

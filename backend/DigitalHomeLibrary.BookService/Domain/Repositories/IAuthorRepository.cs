using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Domain.Repositories
{
    public interface IAuthorRepository
    {
        Task<IReadOnlyList<Author>> GetAllAsync(PaginationParams? paginationInfo = null);
        Task<Author?> GetByIdAsync(Guid id);
        Task<Author?> FindByFullNameAsync(FullName fullName);
        Task<Guid> AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(Guid id);
    }
}

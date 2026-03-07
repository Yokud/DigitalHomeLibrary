using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Domain.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author?> GetByIdAsync(Guid id);
        Task<Author?> FindByFullNameAsync(FullName fullName);
        Task<Guid> AddAsync(Author book);
        Task UpdateAsync(Author book);
        Task DeleteAsync(Guid id);
    }
}

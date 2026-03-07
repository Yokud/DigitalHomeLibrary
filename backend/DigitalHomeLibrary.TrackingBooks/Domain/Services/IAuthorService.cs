using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Infractructure;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.BookService.Domain.Services
{
    public interface IAuthorService
    {
        Task<Author?> GetAuthor(Guid authorId);

        Task<IEnumerable<Author>> FindAuthors(Expression<Func<Author, bool>>? filter = null, PaginationInfo? paginationInfo = null);

        Task UpdateAuthor(Author author);
    }
}

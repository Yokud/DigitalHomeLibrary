using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using DigitalHomeLibrary.TrackingBooks.Infractructure;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.TrackingBooks.Services.Abstract
{
    public interface IBooksService
    {
        Task<Book> GetBook(Guid bookId);

        Task<Author> GetAuthor(Guid bookId);

        Task<IEnumerable<Book>> GetBooks(Expression<Func<Book, bool>>? filter = null, PaginationInfo? paginationInfo = null);

        Task<IEnumerable<Author>> GetAuthors(Expression<Func<Book, bool>>? filter = null, PaginationInfo? paginationInfo = null);

        Task<Guid> AddBook(Book book, IEnumerable<Author> authors);

        Task UpdateBook(Book book);

        Task UpdateAuthor(Author author);

        Task DeleteBook(Guid bookId);

        Task DeleteAuthor(Guid bookId);

        Task ChangeBookState(Guid bookId, ReadingState readingState);
    }
}

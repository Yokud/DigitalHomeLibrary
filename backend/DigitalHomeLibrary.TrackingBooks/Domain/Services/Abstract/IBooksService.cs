using DigitalHomeLibrary.TrackingBooks.DataAccess.Entities;
using DigitalHomeLibrary.TrackingBooks.Domain.Models;
using DigitalHomeLibrary.TrackingBooks.Infractructure;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.TrackingBooks.DataAccess.Services.Abstract
{
    public interface IBooksService
    {
        Task<BookEntity?> GetBook(Guid bookId);

        Task<Author?> GetAuthor(Guid authorId);

        Task<IEnumerable<Book>> GetBooks(Expression<Func<Book, bool>>? filter = null, PaginationInfo? paginationInfo = null);

        Task<IEnumerable<Author>> GetAuthors(Expression<Func<Author, bool>>? filter = null, PaginationInfo? paginationInfo = null);

        Task<Guid> AddBook(Book book, IEnumerable<Author> authors);

        Task UpdateBook(Book book);

        Task UpdateAuthor(Author author);

        Task DeleteBook(Guid bookId);

        Task SetBookStateReading(Guid bookId);

        Task SetBookStateRead(Guid bookId);
    }
}

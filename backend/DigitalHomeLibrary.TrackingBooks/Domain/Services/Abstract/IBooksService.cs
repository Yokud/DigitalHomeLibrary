using DigitalHomeLibrary.BookService.Domain.Models;
using DigitalHomeLibrary.BookService.Infractructure;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.BookService.DataAccess.Services.Abstract
{
    public interface IBooksService
    {
        Task<Book?> GetBook(Guid bookId);

        Task<Author?> GetAuthor(Guid authorId);

        Task<IEnumerable<Book>> GetBooks(Expression<Func<Book, bool>>? filter = null, PaginationInfo? paginationInfo = null);

        Task<IEnumerable<Author>> GetAuthors(Expression<Func<Author, bool>>? filter = null, PaginationInfo? paginationInfo = null);

        Task<Guid> AddBook(Book book, IEnumerable<Author> authors);

        Task UpdateBook(Book book);

        Task UpdateAuthor(Author author);

        Task DeleteBook(Guid bookId);

        Task SetBookStateReading(Guid bookId);

        Task SetBookStateRead(Guid bookId);

        Task<IEnumerable<Review>> GetBookReviews(Guid bookId);

        Task<Guid> AddReviewToBook(Review review);
    }
}

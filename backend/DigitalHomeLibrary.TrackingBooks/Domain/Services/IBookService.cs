using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Infractructure;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.BookService.Domain.Services
{
    public interface IBookService
    {
        Task<Book?> GetBookById(Guid bookId);

        Task<IEnumerable<Book>> FindBooks(Expression<Func<Book, bool>>? filter = null, PaginationInfo? paginationInfo = null);

        Task<Guid> AddBook(Book book, IEnumerable<Guid> authorIds);

        Task UpdateBook(Book book);

        Task DeleteBook(Guid bookId);

        Task SetBookStateReading(Guid bookId);

        Task SetBookStateRead(Guid bookId);

        Task<IEnumerable<Review>> GetBookReviews(Guid bookId);

        Task<Guid> AddReviewToBook(Review review);
    }
}

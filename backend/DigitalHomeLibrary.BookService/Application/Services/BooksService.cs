using CSharpFunctionalExtensions;
using DigitalHomeLibrary.BookService.Application.DTO.Info;
using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Domain.Services;
using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Application.Services
{
    public class BooksService(IBookRepository booksRepository, BookReviewsService bookReviewsService)
    {
        readonly IBookRepository _booksRepository = booksRepository;
        readonly BookReviewsService _bookReviewsService = bookReviewsService;

        public async Task<Guid> AddBook(BookDetails bookInfo)
        {
            var book = new Book(bookInfo);

            var bookId = await _booksRepository.AddAsync(book);

            return bookId;
        }

        public async Task DeleteBook(Guid bookId)
        {
            await _booksRepository.DeleteAsync(bookId);
        }

        public async Task<Result<Book>> GetBookById(Guid bookId)
        {
            var book = await _booksRepository.GetByIdAsync(bookId);

            return book is null ? Result.Failure<Book>($"Book with ID = {bookId} does not exist") : Result.Success(book);
        }

        public async Task<Result> SetBookStateRead(Guid bookId)
        {
            var book = (await _booksRepository.GetByIdAsync(bookId));

            if (book is null)
                return Result.Failure("Not found book");

            book.SetStateToRead(DateOnly.FromDateTime(DateTime.UtcNow));

            await _booksRepository.UpdateAsync(book);
            return Result.Success();
        }

        public async Task<Result> SetBookStateReading(Guid bookId)
        {
            var book = (await _booksRepository.GetByIdAsync(bookId));

            if (book is null)
                return Result.Failure("Not found book");

            book.SetStateToReading(DateOnly.FromDateTime(DateTime.UtcNow));

            await _booksRepository.UpdateAsync(book);
            return Result.Success();
        }

        public async Task<Result> UpdateBookInfo(Guid bookId, BookDetails newBookInfo)
        {
            var book = await _booksRepository.GetByIdAsync(bookId);

            if (book is null)
                return Result.Failure("Not found book");

            var updatedBook = new Book(book.Id, newBookInfo);

            await _booksRepository.UpdateAsync(updatedBook);
            return Result.Success();
        }

        public async Task<IEnumerable<Book>> GetAllBooks(PaginationInfo? paginationInfo = null)
        {
            return await _booksRepository.GetAllAsync(paginationInfo);
        }

        public async Task<IEnumerable<Book>> GetAuthorBooks(Guid authorId)
        {
            return (await _booksRepository.GetAllAsync()).Where(book => book.Details.AuthorIds.Contains(authorId));
        }

        public async Task<Result<AverageScore>> GetBookAverageScore(Guid bookId) => await _bookReviewsService.GetBookAverageScore(bookId);
    }
}

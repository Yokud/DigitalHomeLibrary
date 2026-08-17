using CSharpFunctionalExtensions;
using DigitalHomeLibrary.BookService.Application.DTO;
using DigitalHomeLibrary.BookService.Application.Responses;
using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Application.Services
{
    public class BooksService(IBookRepository booksRepository, IAuthorRepository authorRepository)
    {
        readonly IBookRepository _booksRepository = booksRepository;
        readonly IAuthorRepository _authorRepository = authorRepository;

        public async Task<Guid> AddBook(string title, string description, IEnumerable<Guid> authorIds, int releaseYear, string publisher, string isbn, string genre, string language)
        {
            var bookInfo = new BookDetails(title, description, authorIds, releaseYear, publisher, new(isbn), genre, language);
            var book = new Book(bookInfo);

            var bookId = await _booksRepository.AddAsync(book);

            return bookId;
        }

        public async Task DeleteBook(Guid bookId)
        {
            await _booksRepository.DeleteAsync(bookId);
        }

        public async Task<Result<BookDto>> GetBookById(Guid bookId)
        {
            var book = await _booksRepository.GetByIdAsync(bookId);

            return book is null ? Result.Failure<BookDto>($"Book with ID = {bookId} does not exist") : Result.Success(BookDto.FromDomainEntity(book));
        }

        public async Task<Result<IReadOnlyList<AuthorDto>>> GetBookAuthors(Guid bookId)
        {
            var book = await _booksRepository.GetByIdAsync(bookId);

            if (book is null)
                return Result.Failure<IReadOnlyList<AuthorDto>>($"Book with ID = {bookId} does not exist");

            var getAuthorTasks = book.Details.AuthorIds.Select(async id => await _authorRepository.GetByIdAsync(id));
            var authors = (await Task.WhenAll(getAuthorTasks))?.Where(e => e is not null).Cast<Author>();

            return authors is null ? Result.Failure<IReadOnlyList<AuthorDto>>($"Authors for book with ID = {bookId} does not exist") : Result.Success<IReadOnlyList<AuthorDto>>([.. authors.Select(AuthorDto.FromDomainEntity)]);
        }

        public async Task<Result> SetBookStateRead(Guid bookId)
        {
            var book = await _booksRepository.GetByIdAsync(bookId);

            if (book is null)
                return Result.Failure("Not found book");

            book.SetStateToRead(DateOnly.FromDateTime(DateTime.UtcNow));

            await _booksRepository.UpdateAsync(book);
            return Result.Success();
        }

        public async Task<Result> SetBookStateReading(Guid bookId)
        {
            var book = await _booksRepository.GetByIdAsync(bookId);

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

        public async Task<PaginationResponse<BookDto>> GetAllBooks(int page, int size)
        {
            var paginationInfo = new PaginationParams(page, size);
            var res = await _booksRepository.GetAllAsync(paginationInfo);

            return new PaginationResponse<BookDto>(paginationInfo.PageNum, paginationInfo.PageSize, res.Count(), res.Select(BookDto.FromDomainEntity));
        }

        public async Task<IReadOnlyList<BookDto>> GetAuthorBooks(Guid authorId)
        {
            var authorBooks = (await _booksRepository.GetAllAsync()).Where(book => book.Details.AuthorIds.Contains(authorId));

            return [.. authorBooks.Select(BookDto.FromDomainEntity)];
        }
    }
}

using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using DigitalHomeLibrary.TrackingBooks.Infractructure;
using DigitalHomeLibrary.TrackingBooks.Repositories.Abstract;
using DigitalHomeLibrary.TrackingBooks.Services.Abstract;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.TrackingBooks.Services
{
    public class BooksService(IAsyncRepository<Book> booksRepository, IAsyncRepository<Author> authorsRepository, IAsyncRepository<Status> statusesRepository) : IBooksService
    {
        readonly IAsyncRepository<Book> _booksRepository = booksRepository;
        readonly IAsyncRepository<Author> _authorsRepository = authorsRepository;
        readonly IAsyncRepository<Status> _statusesRepository = statusesRepository;

        public async Task<Guid> AddBook(Book book, IEnumerable<Author> authors)
        {
            if (!authors.Any())
                throw new ArgumentException("Book has to have author(s)");

            await Task.WhenAll(authors.Select(author => new Task(() => _authorsRepository.CreateAsync(author))));
            await _authorsRepository.SaveAsync();

            var bookStatus = new Status()
            {
                AdditionDateTime = DateTime.UtcNow,
                ReadingState = ReadingState.NotRead
            };

            var statusId = await _statusesRepository.CreateAsync(bookStatus);
            await _statusesRepository.SaveAsync();

            book.StatusId = statusId;
            book.Authors = [.. authors];

            var bookId = await _booksRepository.CreateAsync(book);
            await _booksRepository.SaveAsync();

            return bookId;
        }

        public async Task ChangeBookState(Guid bookId, ReadingState readingState)
        {
            var bookStatus = (await _booksRepository.GetAsync(bookId))?.Status ?? throw new Exception("Not found book");

            if (bookStatus.ReadingState == ReadingState.NotRead && readingState == ReadingState.Reading)
            {
                bookStatus.ReadingState = ReadingState.Reading;
                bookStatus.ReadingStartDate = DateOnly.FromDateTime(DateTime.UtcNow);
            }
            else if (bookStatus.ReadingState == ReadingState.Reading && readingState == ReadingState.Read)
            {
                bookStatus.ReadingState = ReadingState.Read;
                bookStatus.ReadingEndDate = DateOnly.FromDateTime(DateTime.UtcNow);
            }
            else
                throw new InvalidOperationException($"Wrong new reading state. Book with with reading state {bookStatus.ReadingState} cannot become {readingState}");

            await _statusesRepository.UpdateAsync(bookStatus);
            await _statusesRepository.SaveAsync();
        }

        public async Task DeleteBook(Guid bookId)
        {
            await _booksRepository.DeleteAsync(bookId);
            await _booksRepository.SaveAsync();
        }

        public async Task<Author?> GetAuthor(Guid authorId)
        {
            return await _authorsRepository.GetAsync(authorId);
        }

        public async Task<IEnumerable<Author>> GetAuthors(Expression<Func<Author, bool>>? filter = null, PaginationInfo? paginationInfo = null)
        {
            return await _authorsRepository.GetAllAsync(filter, paginationInfo) ?? [];
        }

        public async Task<Book?> GetBook(Guid bookId)
        {
            return await _booksRepository.GetAsync(bookId);
        }

        public async Task<IEnumerable<Book>> GetBooks(Expression<Func<Book, bool>>? filter = null, PaginationInfo? paginationInfo = null)
        {
            return await _booksRepository.GetAllAsync(filter, paginationInfo) ?? [];
        }

        public async Task UpdateAuthor(Author author)
        {
            if (author.DeathDate < author.BirthDate)
                throw new ArgumentException("Author cannot die before own birth day");

            await _authorsRepository.UpdateAsync(author);
            await _authorsRepository.SaveAsync();
        }

        public async Task UpdateBook(Book book)
        {
            await _booksRepository.UpdateAsync(book);
            await _booksRepository.SaveAsync();
        }
    }
}

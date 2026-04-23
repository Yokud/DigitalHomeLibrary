using DigitalHomeLibrary.BookService.Application.DTO.Info;
using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHomeLibrary.BookService.Infractructure.Repositories
{
    public class EFCoreBookRepository(BookServiceDbContext context) : IBookRepository
    {
        readonly BookServiceDbContext _context = context;

        public async Task<Guid> AddAsync(Book book)
        {
            var daEntity = new EFBook()
            {
                Id = book.Id,
                Title = book.Details.Title,
                Description = book.Details.Description,
                Authors = [.. _context.Authors.Where(e => book.Details.AuthorIds.Contains(e.Id))],
                ReleaseYear = book.Details.ReleaseYear,
                Publisher = book.Details.Publisher,
                ISBN = book.Details.ISBN.Value,
                Genre = book.Details.Genre,
                Language = book.Details.Language,
                Status = new()
                {
                    AdditionDateTime = book.Status.AdditionDateTime,
                    ReadingState = book.Status.ReadingState,
                    ReadingStartDate = book.Status.ReadingStartDate,
                    ReadingEndDate = book.Status.ReadingEndDate,
                }
            };

            await _context.Books.AddAsync(daEntity);
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Books.Where(e => e.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return (await _context.Books.Where(e => e.Id == id).AsNoTracking().SingleOrDefaultAsync()) is not null;
        }

        public async Task<Book?> FindByTitleAsync(string title)
        {
            var bookEntity = await _context.Books.Where(e => e.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase))
                .Include(e => e.Authors)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return bookEntity is not null ? new(bookEntity.Id, new(bookEntity.Title, bookEntity.Description, bookEntity.Authors.Select(a => a.Id), bookEntity.ReleaseYear, bookEntity.Publisher, new(bookEntity.ISBN), bookEntity.Genre, bookEntity.Language)) : null;
        }

        public async Task<IReadOnlyList<Book>> GetAllAsync(PaginationInfo? paginationInfo = null)
        {
            IQueryable<EFBook> res = _context.Books;

            if (paginationInfo is not null)
                res = res.Skip(paginationInfo.PageNum * paginationInfo.PageSize).Take(paginationInfo.PageSize);

            var bookEntities = await res
                .Include(e => e.Authors)
                .Include(e => e.Reviews)
                .Include(e => e.Tags)
                .AsNoTracking()
                .ToListAsync();

            return [.. bookEntities.Select(e => new Book(e.Id, new(e.Title, e.Description, e.Authors.Select(a => a.Id), e.ReleaseYear, e.Publisher, new(e.ISBN), e.Genre, e.Language)))];
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            var bookEntity = await _context.Books.Where(e => e.Id == id)
                .Include(e => e.Authors)
                .AsNoTracking().SingleOrDefaultAsync();

            return bookEntity is not null ? new(bookEntity.Id, new(bookEntity.Title, bookEntity.Description, bookEntity.Authors.Select(a => a.Id), bookEntity.ReleaseYear, bookEntity.Publisher, new(bookEntity.ISBN), bookEntity.Genre, bookEntity.Language)) : null;
        }

        public async Task UpdateAsync(Book book)
        {
            var updatingBookQuery = _context.Books.Where(e => e.Id == book.Id);

            await updatingBookQuery.ExecuteUpdateAsync(s => s.
            SetProperty(e => e.Title, book.Details.Title).
            SetProperty(e => e.Description, book.Details.Description).
            SetProperty(e => e.ReleaseYear, book.Details.ReleaseYear).
            SetProperty(e => e.Publisher, book.Details.Publisher).
            SetProperty(e => e.ISBN, book.Details.ISBN.Value).
            SetProperty(e => e.Genre, book.Details.Genre).
            SetProperty(e => e.Language, book.Details.Language).
            SetProperty(e => e.Status, EFStatus.FromDomain(book.Status))
            );

            await _context.SaveChangesAsync();
        }
    }
}

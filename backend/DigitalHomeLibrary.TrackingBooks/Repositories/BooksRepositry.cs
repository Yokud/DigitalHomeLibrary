using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using DigitalHomeLibrary.TrackingBooks.Domain.Models;
using DigitalHomeLibrary.TrackingBooks.Infractructure;
using DigitalHomeLibrary.TrackingBooks.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.TrackingBooks.Repositories
{
    public class BooksRepositry(TrackingBooksDbContext context) : IBooksRepository
    {
        readonly TrackingBooksDbContext _context = context;

        public async Task<Guid> CreateAsync(Book entity)
        {
            await _context.Books.AddAsync(entity);
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Books.Where(e => e.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync(Expression<Func<Book, bool>>? filter = null, PaginationInfo? paginationInfo = null)
        {
            IQueryable<Book> res = _context.Books;

            if (filter is not null)
                res = res.Where(filter);

            if (paginationInfo is not null)
                res = res.Skip(paginationInfo.PageNum * paginationInfo.PageSize).Take(paginationInfo.PageSize);

            return await res.Include(e => e.Genre)
                            .Include(e => e.Authors).ThenInclude(a => a.Country)
                            .Include(e => e.Status)
                            .Include(e => e.Language)
                            .Include(e => e.Reviews)
                            .Include(e => e.Tags)
                            .AsNoTracking().ToListAsync();
        }

        public async Task<Book?> GetAsync(Guid id)
        {
            return await _context.Books.Where(e => e.Id == id)
                .Include(e => e.Genre)
                .Include(e => e.Authors).ThenInclude(a => a.Country)
                .Include(e => e.Status)
                .Include(e => e.Language)
                .Include(e => e.Reviews)
                .Include(e => e.Tags)
                .AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book entity)
        {
            await _context.Books.Where(e => e.Id == entity.Id).
                ExecuteUpdateAsync(s => s.
                    SetProperty(e => e.Title, entity.Title).
                    SetProperty(e => e.ISBN, entity.ISBN).
                    SetProperty(e => e.Description, entity.Description).
                    SetProperty(e => e.ReleaseYear, entity.ReleaseYear).
                    SetProperty(e => e.Publisher, entity.Publisher).
                    SetProperty(e => e.GenreId, entity.GenreId).
                    SetProperty(e => e.StatusId, entity.StatusId).
                    SetProperty(e => e.LanguageId, entity.LanguageId)
                );
        }
    }
}

using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.BookService.Infractructure.Repositories
{
    public class EFCoreBookRepository(BookServiceDbContext context) : IBookRepository
    {
        readonly BookServiceDbContext _context = context;

        public async Task<Guid> AddAsync(Book entity)
        {
            await _context.Books.AddAsync(entity);
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Books.Where(e => e.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> FindAsync(Expression<Func<Book, bool>>? filter = null, PaginationInfo? paginationInfo = null)
        {
            IQueryable<BookEntity> res = _context.Books;

            if (filter is not null)
                res = res.Where(filter);

            if (paginationInfo is not null)
                res = res.Skip(paginationInfo.PageNum * paginationInfo.PageSize).Take(paginationInfo.PageSize);

            return await res.Include(e => e.Authors)
                            .Include(e => e.Status)
                            .Include(e => e.Reviews)
                            .Include(e => e.Tags)
                            .AsNoTracking().ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            var bookEntity = await _context.Books.Where(e => e.Id == id)
                .Include(e => e.Authors)
                .Include(e => e.Status)
                .Include(e => e.Reviews)
                .Include(e => e.Tags)
                .AsNoTracking().SingleOrDefaultAsync();


        }

        public Task UpdateAsync(Book entity)
        {
            throw new NotImplementedException();

            await _context.SaveChangesAsync();
        }
    }
}

using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using DigitalHomeLibrary.TrackingBooks.Domain.Models;
using DigitalHomeLibrary.TrackingBooks.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DigitalHomeLibrary.TrackingBooks.Repositories
{
    public class BooksRepositry(TrackingBooksDbContext dbContext) : IAsyncRepository<Book>
    {
        readonly TrackingBooksDbContext _context = dbContext;

        public async Task<Guid> CreateAsync(Book entity)
        {
            await _context.Books.AddAsync(entity);
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Books.Where(e => e.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.AsNoTracking().ToListAsync();
        }

        public async Task<Book?> GetAsync(Guid id)
        {
            return await _context.Books.AsNoTracking().Where(e => e.Id == id).SingleOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book entity)
        {
            await _context.Books.Where(e => e.Id == entity.Id).ExecuteUpdateAsync(s => s.SetProperty(e => e.Title, entity.Title));
        }
    }
}

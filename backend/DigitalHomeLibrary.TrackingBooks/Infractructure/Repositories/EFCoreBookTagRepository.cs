using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHomeLibrary.BookService.Infractructure.Repositories
{
    public class EFCoreBookTagRepository(BookServiceDbContext context) : IBookTagRepository
    {
        readonly BookServiceDbContext _context = context;

        public async Task<Guid> AddAsync(BookTag entity)
        {
            await _context.Tags.AddAsync(entity);
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Tags.Where(e => e.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public Task<BookTag?> FindByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookTag>> GetAllAsync(PaginationInfo? paginationInfo = null)
        {
            IQueryable<TagEntity> res = _context.Tags;

            if (paginationInfo is not null)
                res = res.Skip(paginationInfo.PageNum * paginationInfo.PageSize).Take(paginationInfo.PageSize);

            return await res.AsNoTracking().ToListAsync();
        }

        public async Task<BookTag?> GetByIdAsync(Guid id)
        {
            return await _context.Tags.Where(e => e.Id == id).AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(BookTag entity)
        {
            await _context.Tags.Where(e => e.Id == entity.Id).
                ExecuteUpdateAsync(s => s.
                    SetProperty(e => e.Name, entity.Name).
                    SetProperty(e => e.Description, entity.Description)
                );

            await _context.SaveChangesAsync();
        }
    }
}

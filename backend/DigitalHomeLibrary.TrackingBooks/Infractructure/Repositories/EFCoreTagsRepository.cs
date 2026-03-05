using DigitalHomeLibrary.BookService.DataAccess.Repositories;
using DigitalHomeLibrary.BookService.Domain.Models;
using DigitalHomeLibrary.BookService.Infractructure;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.BookService.Infractructure.Repositories
{
    public class EFCoreTagsRepository(BookServiceDbContext context) : ITagsRepository
    {
        readonly BookServiceDbContext _context = context;

        public async Task<Guid> AddAsync(Tag entity)
        {
            await _context.Tags.AddAsync(entity);
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Tags.Where(e => e.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(Expression<Func<Tag, bool>>? filter = null, PaginationInfo? paginationInfo = null)
        {
            IQueryable<TagEntity> res = _context.Tags;

            if (filter is not null)
                res = res.Where(filter);

            if (paginationInfo is not null)
                res = res.Skip(paginationInfo.PageNum * paginationInfo.PageSize).Take(paginationInfo.PageSize);

            return await res.AsNoTracking().ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(Guid id)
        {
            return await _context.Tags.Where(e => e.Id == id).AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tag entity)
        {
            await _context.Tags.Where(e => e.Id == entity.Id).
                ExecuteUpdateAsync(s => s.
                    SetProperty(e => e.Name, entity.Name).
                    SetProperty(e => e.Description, entity.Description)
                );
        }
    }
}

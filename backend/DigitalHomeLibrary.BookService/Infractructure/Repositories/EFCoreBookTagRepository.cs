using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Domain.ValueObjects;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.DBO;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHomeLibrary.BookService.Infractructure.Repositories
{
    public class EFCoreBookTagRepository(BookServiceDbContext context) : IBookTagRepository
    {
        readonly BookServiceDbContext _context = context;

        public async Task<Guid> AddAsync(BookTag tag)
        {
            var daEntity = new TagDbo()
            {
                Id = tag.Id,
                Name = tag.Name,
                Description = tag.Description,
            };

            await _context.Tags.AddAsync(daEntity);
            await _context.SaveChangesAsync();
            return tag.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Tags.Where(e => e.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<BookTag?> FindByNameAsync(string name)
        {
            var entity = await _context.Tags.Where(e => e.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)).AsNoTracking().SingleOrDefaultAsync();

            return entity is not null ? new BookTag(entity.Id, entity.Name, entity.Description) : null;
        }

        public async Task<IEnumerable<BookTag>> GetAllAsync(PaginationParams? paginationInfo = null)
        {
            IQueryable<TagDbo> res = _context.Tags;

            if (paginationInfo is not null)
                res = res.Skip(paginationInfo.PageNum * paginationInfo.PageSize).Take(paginationInfo.PageSize);

            var entities = await res.AsNoTracking().ToListAsync();

            return entities.Select(e => new BookTag(e.Id, e.Name, e.Description));
        }

        public async Task<BookTag?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Tags.Where(e => e.Id == id).AsNoTracking().SingleOrDefaultAsync();

            return entity is not null ? new BookTag(entity.Id, entity.Name, entity.Description) : null;
        }

        public async Task UpdateAsync(BookTag tag)
        {
            await _context.Tags.Where(e => e.Id == tag.Id).
                ExecuteUpdateAsync(s => s.
                    SetProperty(e => e.Name, tag.Name).
                    SetProperty(e => e.Description, tag.Description)
                );

            await _context.SaveChangesAsync();
        }
    }
}

using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using DigitalHomeLibrary.TrackingBooks.Domain.Models;
using DigitalHomeLibrary.TrackingBooks.Infractructure;
using DigitalHomeLibrary.TrackingBooks.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.TrackingBooks.Repositories
{
    public class LanguagesRepository(TrackingBooksDbContext context) : ILanguagesRepository
    {
        readonly TrackingBooksDbContext _context = context;

        public async Task<Guid> CreateAsync(Language entity)
        {
            await _context.Languages.AddAsync(entity);
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Languages.Where(e => e.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Language>> GetAllAsync(Expression<Func<Language, bool>>? filter = null, PaginationInfo? paginationInfo = null)
        {
            IQueryable<Language> res = _context.Languages;

            if (filter is not null)
                res = res.Where(filter);

            if (paginationInfo is not null)
                res = res.Skip(paginationInfo.PageNum * paginationInfo.PageSize).Take(paginationInfo.PageSize);

            return await res.AsNoTracking().ToListAsync();
        }

        public async Task<Language?> GetAsync(Guid id)
        {
            return await _context.Languages.Where(e => e.Id == id).AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Language entity)
        {
            await _context.Languages.Where(e => e.Id == entity.Id).
                ExecuteUpdateAsync(s => s.
                    SetProperty(e => e.Name, entity.Name)
                );
        }
    }
}

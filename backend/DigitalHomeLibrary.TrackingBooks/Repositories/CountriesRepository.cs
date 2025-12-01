using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using DigitalHomeLibrary.TrackingBooks.Domain.Models;
using DigitalHomeLibrary.TrackingBooks.Infractructure;
using DigitalHomeLibrary.TrackingBooks.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.TrackingBooks.Repositories
{
    public class CountriesRepository(TrackingBooksDbContext context) : ICountriesRepository
    {
        readonly TrackingBooksDbContext _context = context;

        public async Task<Guid> CreateAsync(Country entity)
        {
            await _context.Countries.AddAsync(entity);
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Countries.Where(e => e.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Country>> GetAllAsync(Expression<Func<Country, bool>>? filter = null, PaginationInfo? paginationInfo = null)
        {
            IQueryable<Country> res = _context.Countries;

            if (filter is not null)
                res = res.Where(filter);

            if (paginationInfo is not null)
                res = res.Skip(paginationInfo.PageNum * paginationInfo.PageSize).Take(paginationInfo.PageSize);

            return await res.Include(e => e.Authors)
                            .AsNoTracking().ToListAsync();
        }

        public async Task<Country?> GetAsync(Guid id)
        {
            return await _context.Countries.Where(e => e.Id == id)
                .Include(e => e.Authors)
                .AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Country entity)
        {
            await _context.Countries.Where(e => e.Id == entity.Id).
                ExecuteUpdateAsync(s => s.
                    SetProperty(e => e.Name, entity.Name)
                );
        }
    }
}

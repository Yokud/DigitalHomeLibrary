using DigitalHomeLibrary.TrackingBooks.DataAccess.Models;
using DigitalHomeLibrary.TrackingBooks.DataAccess.Repositories.Abstract;
using DigitalHomeLibrary.TrackingBooks.Domain.Models;
using DigitalHomeLibrary.TrackingBooks.Infractructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.TrackingBooks.Domain.Repositories
{
    public class AuthorsRepository(TrackingBooksDbContext context) : IAsyncRepository<Author>
    {
        readonly TrackingBooksDbContext _context = context;

        public async Task<Guid> CreateAsync(Author entity)
        {
            await _context.Authors.AddAsync(entity);
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Authors.Where(e => e.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAsync(Expression<Func<Author, bool>>? filter = null, PaginationInfo? paginationInfo = null)
        {
            IQueryable<Author> res = _context.Authors;

            if (filter is not null)
                res = res.Where(filter);

            if (paginationInfo is not null)
                res = res.Skip(paginationInfo.PageNum * paginationInfo.PageSize).Take(paginationInfo.PageSize);

            return await res.Include(e => e.Books).AsNoTracking().ToListAsync();
        }

        public async Task<Author?> GetAsync(Guid id)
        {
            return await _context.Authors.Where(e => e.Id == id).Include(e => e.Books).AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author entity)
        {
            await _context.Authors.Where(e => e.Id == entity.Id).
                ExecuteUpdateAsync(s => s.
                    SetProperty(e => e.FirstName, entity.FirstName).
                    SetProperty(e => e.MiddleName, entity.MiddleName).
                    SetProperty(e => e.LastName, entity.LastName).
                    SetProperty(e => e.BirthDate, entity.BirthDate).
                    SetProperty(e => e.DeathDate, entity.DeathDate).
                    SetProperty(e => e.CountryName, entity.CountryName).
                    SetProperty(e => e.LifeStory, entity.LifeStory)
                );
        }
    }
}

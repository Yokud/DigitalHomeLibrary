using DigitalHomeLibrary.TrackingBooks.DataAccess.Entities;
using DigitalHomeLibrary.TrackingBooks.DataAccess.Models;
using DigitalHomeLibrary.TrackingBooks.DataAccess.Repositories.Abstract;
using DigitalHomeLibrary.TrackingBooks.Domain.Models;
using DigitalHomeLibrary.TrackingBooks.Infractructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.TrackingBooks.DataAccess.Repositories
{
    public class StatusesRepository(TrackingBooksDbContext context) : IAsyncRepository<Status>
    {
        readonly TrackingBooksDbContext _context = context;

        public async Task<Guid> CreateAsync(Status entity)
        {
            await _context.BookStatuses.AddAsync(entity);
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.BookStatuses.Where(e => e.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Status>> GetAllAsync(Expression<Func<Status, bool>>? filter = null, PaginationInfo? paginationInfo = null)
        {
            IQueryable<StatusEntity> res = _context.BookStatuses;

            if (filter is not null)
                res = res.Where(filter);

            if (paginationInfo is not null)
                res = res.Skip(paginationInfo.PageNum * paginationInfo.PageSize).Take(paginationInfo.PageSize);

            return await res.AsNoTracking().ToListAsync();
        }

        public async Task<Status?> GetAsync(Guid id)
        {
            return await _context.BookStatuses.Where(e => e.Id == id).AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Status entity)
        {
            await _context.BookStatuses.Where(e => e.Id == entity.Id).
                ExecuteUpdateAsync(s => s.
                    SetProperty(e => e.AdditionDateTime, entity.AdditionDateTime).
                    SetProperty(e => e.ReadingStartDate, entity.ReadingStartDate).
                    SetProperty(e => e.ReadingEndDate, entity.ReadingEndDate).
                    SetProperty(e => e.ReadingState, entity.ReadingState)
                );
        }
    }
}

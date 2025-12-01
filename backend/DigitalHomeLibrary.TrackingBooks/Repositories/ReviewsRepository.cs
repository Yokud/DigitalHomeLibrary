using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using DigitalHomeLibrary.TrackingBooks.Domain.Models;
using DigitalHomeLibrary.TrackingBooks.Infractructure;
using DigitalHomeLibrary.TrackingBooks.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DigitalHomeLibrary.TrackingBooks.Repositories
{
    public class ReviewsRepository(TrackingBooksDbContext context) : IReviewsRepository
    {
        readonly TrackingBooksDbContext _context = context;

        public async Task<Guid> CreateAsync(Review entity)
        {
            await _context.Reviews.AddAsync(entity);
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Reviews.Where(e => e.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Review>> GetAllAsync(Expression<Func<Review, bool>>? filter = null, PaginationInfo? paginationInfo = null)
        {
            IQueryable<Review> res = _context.Reviews;

            if (filter is not null)
                res = res.Where(filter);

            if (paginationInfo is not null)
                res = res.Skip(paginationInfo.PageNum * paginationInfo.PageSize).Take(paginationInfo.PageSize);

            return await res.AsNoTracking().ToListAsync();
        }

        public async Task<Review?> GetAsync(Guid id)
        {
            return await _context.Reviews.Where(e => e.Id == id).AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Review entity)
        {
            await _context.Reviews.Where(e => e.Id == entity.Id).
                ExecuteUpdateAsync(s => s.
                    SetProperty(e => e.Note, entity.Note).
                    SetProperty(e => e.Score, entity.Score).
                    SetProperty(e => e.BookId, entity.BookId)
                );
        }
    }
}

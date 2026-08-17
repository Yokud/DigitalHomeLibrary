using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Domain.ValueObjects;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.DBO;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHomeLibrary.BookService.Infractructure.Repositories
{
    public class EFCoreReviewRepository(BookServiceDbContext context) : IReviewRepository
    {
        readonly BookServiceDbContext _context = context;

        public async Task<Guid> AddAsync(Review review)
        {
            var entity = new ReviewDbo()
            {
                Id = review.Id,
                BookId = review.ReviewedBookId,
                Score = (byte)review.Score.ScoreValue,
                Note = review.Note,
            };

            await _context.Reviews.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Reviews.Where(e => e.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Review>> GetAllAsync(PaginationParams? paginationInfo = null)
        {
            IQueryable<ReviewDbo> res = _context.Reviews;

            if (paginationInfo is not null)
                res = res.Skip(paginationInfo.PageNum * paginationInfo.PageSize).Take(paginationInfo.PageSize);

            var reviewEntities = await res.AsNoTracking().ToListAsync();

            return reviewEntities.Select(e => new Review(e.Id, e.BookId, new(e.Score), e.Note));
        }

        public async Task<Review?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Reviews.Where(e => e.Id == id).AsNoTracking().SingleOrDefaultAsync();

            return entity is null ? null : new Review(entity.Id, entity.BookId, new(entity.Score), entity.Note);
        }

        public async Task UpdateAsync(Review review)
        {
            await _context.Reviews.Where(e => e.Id == review.Id).ExecuteUpdateAsync(s => s.
            SetProperty(e => e.Score, review.Score.ScoreValue).
            SetProperty(e => e.Note, review.Note)
            );

            await _context.SaveChangesAsync();
        }
    }
}

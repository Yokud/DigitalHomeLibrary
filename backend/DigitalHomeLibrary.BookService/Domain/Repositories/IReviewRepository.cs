using DigitalHomeLibrary.BookService.Application.DTO.Info;
using DigitalHomeLibrary.BookService.Domain.Entities;

namespace DigitalHomeLibrary.BookService.Domain.Repositories
{
    public interface IReviewRepository
    {
        Task<Review?> GetByIdAsync(Guid id);
        Task<IEnumerable<Review>> GetAllAsync(PaginationInfo? paginationInfo = null);
        Task<Guid> AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(Guid id);
    }
}

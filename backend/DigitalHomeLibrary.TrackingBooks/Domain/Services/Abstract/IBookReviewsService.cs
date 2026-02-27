using DigitalHomeLibrary.TrackingBooks.Domain.Models;

namespace DigitalHomeLibrary.TrackingBooks.DataAccess.Services.Abstract
{
    public interface IBookReviewsService
    {
        Task<IEnumerable<Review>> GetBookReviews(Guid bookId);

        Task<Guid> AddReviewToBook(Review review);

        Task DeleteReview(Guid reviewId);
    }
}

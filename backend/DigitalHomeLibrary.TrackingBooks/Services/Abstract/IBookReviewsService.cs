using DigitalHomeLibrary.TrackingBooks.Domain.Entities;

namespace DigitalHomeLibrary.TrackingBooks.Services.Abstract
{
    public interface IBookReviewsService
    {
        Task<IEnumerable<Review>> GetBookReviews(Book book);

        Task<Guid> AddReviewToBook(Review review);

        Task DeleteReview(Guid reviewId);
    }
}

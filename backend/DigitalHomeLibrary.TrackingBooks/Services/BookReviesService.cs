using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using DigitalHomeLibrary.TrackingBooks.Repositories.Abstract;
using DigitalHomeLibrary.TrackingBooks.Services.Abstract;

namespace DigitalHomeLibrary.TrackingBooks.Services
{
    public class BookReviesService(IAsyncRepository<Review> reviewsRepository) : IBookReviewsService
    {
        readonly IAsyncRepository<Review> _reviewsRepository = reviewsRepository;

        public async Task<Guid> AddReviewToBook(Review review)
        {
            var reviewId = await _reviewsRepository.CreateAsync(review);
            await _reviewsRepository.SaveAsync();

            return reviewId;
        }

        public async Task DeleteReview(Guid reviewId)
        {
            await _reviewsRepository.DeleteAsync(reviewId);
            await _reviewsRepository.SaveAsync();
        }

        public async Task<IEnumerable<Review>> GetBookReviews(Guid bookId)
        {
            return await _reviewsRepository.GetAllAsync((review) => review.BookId == bookId);
        }
    }
}

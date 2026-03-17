using CSharpFunctionalExtensions;
using DigitalHomeLibrary.BookService.Application.DTO.Info;
using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.Repositories;

namespace DigitalHomeLibrary.BookService.Application.Services
{
    public class ReviewService(IBookRepository bookRepository, IReviewRepository reviewRepository)
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IReviewRepository _reviewRepository = reviewRepository;

        public async Task<Result<Guid>> AddReviewToBook(Review review)
        {
            if (!await _bookRepository.Exists(review.ReviewedBookId))
                return Result.Failure<Guid>($"Book with ID = {review.ReviewedBookId} does not exist");

            await _reviewRepository.AddAsync(review);

            return Result.Success(review.Id);
        }

        public async Task DeleteReview(Guid reviewId)
        {
            await _reviewRepository.DeleteAsync(reviewId);
        } 

        public async Task<IEnumerable<Review>> GetBookReviews(Guid bookId, PaginationInfo? paginationInfo = null)
        {
            var reviews = (await _reviewRepository.GetAllAsync()).Where(e => e.ReviewedBookId == bookId);

            if (paginationInfo is not null)
                reviews = reviews.Skip((paginationInfo.PageNum - 1) * paginationInfo.PageSize).Take(paginationInfo.PageSize);

            return reviews;
        }
    }
}

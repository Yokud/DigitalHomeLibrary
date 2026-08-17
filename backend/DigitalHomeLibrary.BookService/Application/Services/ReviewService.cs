using CSharpFunctionalExtensions;
using DigitalHomeLibrary.BookService.Application.DTO;
using DigitalHomeLibrary.BookService.Application.Responses;
using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Application.Services
{
    public class ReviewService(IBookRepository bookRepository, IReviewRepository reviewRepository)
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IReviewRepository _reviewRepository = reviewRepository;

        public async Task<Result<Guid>> AddReviewToBook(Guid bookId, byte score, string note)
        {
            if (!await _bookRepository.Exists(bookId))
                return Result.Failure<Guid>($"Book with ID = {bookId} does not exist");

            var review = new Review(bookId, new(score), note);
            await _reviewRepository.AddAsync(review);

            return Result.Success(review.Id);
        }

        public async Task DeleteReview(Guid reviewId)
        {
            await _reviewRepository.DeleteAsync(reviewId);
        }

        public async Task<PaginationResponse<ReviewDto>> GetBookReviews(Guid bookId, int page, int size)
        {
            var reviews = (await _reviewRepository.GetAllAsync()).Where(e => e.ReviewedBookId == bookId);

            var paginationInfo = new PaginationParams(page, size);
            reviews = reviews.Skip((paginationInfo.PageNum - 1) * paginationInfo.PageSize).Take(paginationInfo.PageSize);

            return new PaginationResponse<ReviewDto>(page, size, reviews.Count(), reviews.Select(ReviewDto.FromDomainEntity));
        }
    }
}

using CSharpFunctionalExtensions;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Domain.Services
{
    public class BookReviewsService(IBookRepository bookRepository, IReviewRepository reviewRepository)
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IReviewRepository _reviewRepository = reviewRepository;

        public async Task<Result<AverageScore>> GetBookAverageScore(Guid bookId)
        {
            if (!await _bookRepository.Exists(bookId))
                return Result.Failure<AverageScore>($"Book with ID = {bookId} does not exist");

            var reviews = (await _reviewRepository.GetAllAsync()).Where(e => e.ReviewedBookId == bookId);
            var averageScore = reviews.Any() ? new AverageScore(reviews.Select(e => e.Score)) : AverageScore.ZeroScore;

            return Result.Success(averageScore);
        }
    }
}

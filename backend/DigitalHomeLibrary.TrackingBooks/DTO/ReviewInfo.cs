using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities;

namespace DigitalHomeLibrary.BookService.DTO
{
    public record ReviewInfo(byte Score, string Note, BookInfo ReviewedBook)
    {
        public static ReviewInfo FromEntity(ReviewEntity review) => new(review.Score, review.Note, review.ReviewedBook is not null ? BookInfo.FromEntity(review.ReviewedBook) : throw new InvalidCastException($"Review with id = {review.Id} has no book"));
    }
}

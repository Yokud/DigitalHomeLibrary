using DigitalHomeLibrary.TrackingBooks.Domain.Entities;

namespace DigitalHomeLibrary.TrackingBooks.DTO
{
    public record ReviewInfo(byte Score, string Note, BookInfo ReviewedBook)
    {
        public static ReviewInfo FromEntity(Review review) => new(review.Score, review.Note, review.ReviewedBook is not null ? BookInfo.FromEntity(review.ReviewedBook) : throw new InvalidCastException($"Review with id = {review.Id} has no book"));
    }
}

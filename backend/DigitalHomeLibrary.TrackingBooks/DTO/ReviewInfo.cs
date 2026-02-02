using DigitalHomeLibrary.TrackingBooks.Domain.Entities;

namespace DigitalHomeLibrary.TrackingBooks.DTO
{
    public record ReviewInfo(byte Score, string Note, BookInfo ReviewedBook)
    {
        public static ReviewInfo FromEntity(Review review) => new(review.Score, review.Note, BookInfo.FromEntity(review.ReviewedBook));
    }
}

using DigitalHomeLibrary.BookService.Domain.Entities;

namespace DigitalHomeLibrary.BookService.Application.DTO
{
    public record ReviewDto(byte Score, string Note)
    {
        public static ReviewDto FromDomainEntity(Review review) => new((byte)review.Score.ScoreValue, review.Note);
    }
}

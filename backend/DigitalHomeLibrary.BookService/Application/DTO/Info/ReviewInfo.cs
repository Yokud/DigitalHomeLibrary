using DigitalHomeLibrary.BookService.Domain.Entities;

namespace DigitalHomeLibrary.BookService.Application.DTO.Info
{
    public record ReviewInfo(byte Score, string Note)
    {
        public static ReviewInfo FromDomainEntity(Review review) => new((byte)review.Score.ScoreValue, review.Note);
    }
}

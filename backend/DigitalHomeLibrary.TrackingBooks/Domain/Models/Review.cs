using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Domain.Models
{
    public sealed class Review
    {
        public Review(Guid reviewedBookId, Score score, string note)
        {
            if (string.IsNullOrWhiteSpace(note))
                throw new ArgumentException("Note cannot be null, empty or contains only whitespaces", nameof(note));

            ReviewedBookId = reviewedBookId;
            Score = score;
            Note = note;
        }

        public Guid ReviewedBookId { get; private set; }
        public Score Score { get; private set; }
        public string Note { get; private set; }
    }
}

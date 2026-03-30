using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Domain.Entities
{
    public sealed class Review : DomainEntity
    {
        public Review(Guid id, Guid reviewedBookId, Score score, string note) : base(id)
        {
            if (string.IsNullOrWhiteSpace(note))
                throw new ArgumentException("Note cannot be null, empty or contains only whitespaces", nameof(note));

            ReviewedBookId = reviewedBookId;
            Score = score;
            Note = note;
        }

        public Review(Guid reviewedBookId, Score score, string note) : this(Guid.NewGuid(), reviewedBookId, score, note)
        {

        }

        public Guid ReviewedBookId { get; private set; }
        public Score Score { get; private set; }
        public string Note { get; private set; }
    }
}

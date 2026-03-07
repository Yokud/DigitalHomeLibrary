using DigitalHomeLibrary.BookService.Domain.ValueObjects;
using BookInfo = DigitalHomeLibrary.BookService.Domain.ValueObjects.BookInfo;

namespace DigitalHomeLibrary.BookService.Domain.Entities
{
    public class Book : DomainEntity
    {
        private readonly List<Review> _reviews = [];
        private readonly List<BookTag> _tags = [];

        public Book(Guid id, BookInfo bookInfo) : base(id)
        {
            ArgumentNullException.ThrowIfNull(bookInfo, nameof(bookInfo));

            BookInfo = bookInfo;
        }

        public Book(BookInfo bookInfo) : this(Guid.NewGuid(), bookInfo)
        {

        }

        public BookInfo BookInfo { get; }
        public IReadOnlyCollection<Review> Reviews => _reviews;
        public IReadOnlyCollection<BookTag> Tags => _tags;
        public Status? Status { get; private set; }

        public void InitState(DateTime additionDateTime)
        {
            Status = Status.GetSourceStatus(Id, additionDateTime);
        }

        public void SetStateToReading(DateOnly readingStartDate)
        {
            if (Status is null)
                throw new InvalidOperationException("Book status is not inited");

            Status = Status.GetReadingStatus(Status.BookId, Status.AdditionDateTime, readingStartDate);
        }

        public void SetStateToRead(DateOnly readingEndDate)
        {
            if (Status?.ReadingStartDate is null)
                throw new InvalidOperationException("Book cannot be read before reading");

            Status = Status.GetReadStatus(Status.BookId, Status.AdditionDateTime, Status.ReadingStartDate.Value, readingEndDate);
        }

        public void AddReview(Review review)
        {
            _reviews.Add(review);
        }

        public void AddTag(BookTag tag)
        {
            _tags.Add(tag);
        }

        public double GetAverageScore() => Reviews.Select(e => e.Score.ScoreValue).Average();
    }
}

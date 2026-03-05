using BookInfo = DigitalHomeLibrary.BookService.Domain.ValueObjects.BookInfo;

namespace DigitalHomeLibrary.BookService.Domain.Models
{
    public class Book
    {
        private readonly List<Review> _reviews = [];
        private readonly List<Tag> _tags = [];

        public Book(BookInfo bookInfo)
        {
            ArgumentNullException.ThrowIfNull(bookInfo, nameof(bookInfo));

            Id = Guid.NewGuid();
            BookInfo = bookInfo;
        }

        public Guid Id { get; }
        public BookInfo BookInfo { get; }
        public IReadOnlyCollection<Review> Reviews => _reviews;
        public IReadOnlyCollection<Tag> Tags => _tags;
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

        public void AddTag(Tag tag)
        {
            _tags.Add(tag);
        }
    }
}

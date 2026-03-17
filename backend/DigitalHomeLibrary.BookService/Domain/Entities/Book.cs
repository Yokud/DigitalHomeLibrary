using DigitalHomeLibrary.BookService.Domain.ValueObjects;
using BookDetails = DigitalHomeLibrary.BookService.Domain.ValueObjects.BookDetails;

namespace DigitalHomeLibrary.BookService.Domain.Entities
{
    public class Book : DomainEntity
    {
        private readonly List<Guid> _bookTagIds = [];

        public Book(Guid id, BookDetails bookInfo) : base(id)
        {
            ArgumentNullException.ThrowIfNull(bookInfo, nameof(bookInfo));

            Details = bookInfo;
            Status = Status.GetSourceStatus(Id, DateTime.UtcNow);
        }

        public Book(BookDetails bookInfo) : this(Guid.NewGuid(), bookInfo)
        {

        }

        public BookDetails Details { get; }
        public IReadOnlyList<Guid> BookTagIds => _bookTagIds;
        public Status Status { get; private set; }

        public void SetStateToReading(DateOnly readingStartDate)
        {
            Status = Status.GetReadingStatus(Id, Status.AdditionDateTime, readingStartDate);
        }

        public void SetStateToRead(DateOnly readingEndDate)
        {
            if (Status?.ReadingStartDate is null)
                throw new InvalidOperationException("Book cannot be read before reading");

            Status = Status.GetReadStatus(Id, Status.AdditionDateTime, Status.ReadingStartDate.Value, readingEndDate);
        }

        public void AddTag(BookTag tag)
        {
            _bookTagIds.Add(tag.Id);
        }

        public void DeleteTag(BookTag tag)
        {
            _bookTagIds.Remove(tag.Id);
        }
    }
}

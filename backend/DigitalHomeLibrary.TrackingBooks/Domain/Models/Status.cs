namespace DigitalHomeLibrary.BookService.Domain.Models
{
    public enum ReadingState
    {
        NotRead,
        Reading,
        Read
    }

    public sealed class Status
    {
        public Guid BookId { get; }
        public DateTime AdditionDateTime { get; }
        public ReadingState ReadingState { get; }
        public DateOnly? ReadingStartDate { get; }
        public DateOnly? ReadingEndDate { get; }

        private Status(Guid bookId, DateTime additionDateTime, ReadingState readingState, DateOnly? readingStartDate, DateOnly? readingEndDate)
        {
            if (readingStartDate is not null)
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(readingStartDate.Value, DateOnly.FromDateTime(additionDateTime));

                if (readingEndDate is not null)
                    ArgumentOutOfRangeException.ThrowIfLessThan(readingEndDate.Value, readingStartDate.Value);
            }

            BookId = bookId;
            AdditionDateTime = additionDateTime;
            ReadingState = readingState;
            ReadingStartDate = readingStartDate;
            ReadingEndDate = readingEndDate;
        }

        public static Status GetSourceStatus(Guid bookId, DateTime additionDateTime)
        {
            return new(bookId, additionDateTime, ReadingState.NotRead, null, null);
        }

        public static Status GetReadingStatus(Guid bookId, DateTime additionDateTime, DateOnly readingStartDate)
        {
            return new(bookId, additionDateTime, ReadingState.Reading, readingStartDate, null);
        }

        public static Status GetReadStatus(Guid bookId, DateTime additionDateTime, DateOnly readingStartDate, DateOnly readingEndDate)
        {
            return new(bookId, additionDateTime, ReadingState.Read, readingStartDate, readingEndDate);
        }
    }
}

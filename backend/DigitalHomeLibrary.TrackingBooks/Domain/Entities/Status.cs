namespace DigitalHomeLibrary.TrackingBooks.Domain.Entities
{
    public enum ReadingState
    {
        NotRead,
        Reading,
        Readed
    }

    public class Status
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Book? Book { get; set; }
        public DateTime AdditionDateTime { get; set; }
        public ReadingState ReadingState { get; set; }
        public DateOnly? ReadingStartDate { get; set; }
        public DateOnly? ReadingEndDate { get; set; }
    }
}

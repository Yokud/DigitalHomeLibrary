namespace DigitalHomeLibrary.Core.Entities
{
    public enum BookState
    {
        NotRead,
        Reading,
        Readed
    }

    public class BookStatus
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public DateTime AdditionDateTime { get; set; }
        public BookState Status { get; set; }
        public DateOnly? ReadingStartDate { get; set; }
        public DateOnly? ReadingEndDate { get; set; }
    }
}

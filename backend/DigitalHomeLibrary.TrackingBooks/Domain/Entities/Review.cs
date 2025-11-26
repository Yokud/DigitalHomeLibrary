namespace DigitalHomeLibrary.TrackingBooks.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public byte Score { get; set; }
        public string Note { get; set; } = string.Empty;
        public Book? ReviewedBook { get; set; }
    }
}

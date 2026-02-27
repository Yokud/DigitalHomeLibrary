namespace DigitalHomeLibrary.TrackingBooks.DataAccess.Entities
{
    public class ReviewEntity
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public byte Score { get; set; }
        public string Note { get; set; } = string.Empty;
        public BookEntity? ReviewedBook { get; set; }
    }
}

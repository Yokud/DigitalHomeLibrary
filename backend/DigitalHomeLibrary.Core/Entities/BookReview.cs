namespace DigitalHomeLibrary.Core.Entities
{
    public class BookReview
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public byte Score { get; set; }
        public string Note { get; set; } 
    }
}

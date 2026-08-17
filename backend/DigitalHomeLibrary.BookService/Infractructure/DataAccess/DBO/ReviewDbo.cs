namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.DBO
{
    public class ReviewDbo
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public byte Score { get; set; }
        public string Note { get; set; } = string.Empty;
        public BookDbo? ReviewedBook { get; set; }
    }
}

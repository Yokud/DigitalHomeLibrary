namespace DigitalHomeLibrary.TrackingBooks.Domain.Entities
{
    public class Language
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Book> Books { get; set; } = [];
    }
}

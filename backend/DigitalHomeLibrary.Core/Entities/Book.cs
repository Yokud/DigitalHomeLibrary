namespace DigitalHomeLibrary.Core.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public int ReleaseYear { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public Guid GenreId { get; set; }
        public Guid LanguageId { get; set; }
    }
}

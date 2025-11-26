namespace DigitalHomeLibrary.TrackingBooks.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<Author> Authors { get; set; } = [];
        public int ReleaseYear { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public Guid GenreId { get; set; }
        public Genre? Genre { get; set; }
        public Guid LanguageId { get; set; }
        public Language? Language { get; set; }
        public IEnumerable<Review> Reviews { get; set; } = [];
        public IEnumerable<Tag> Tags { get; set; } = [];
        public Guid StatusId { get; set; }
        public Status? Status { get; set; }
    }
}

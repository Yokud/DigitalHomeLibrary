namespace DigitalHomeLibrary.TrackingBooks.Domain.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Author> Authors { get; set; } = [];
        public int ReleaseYear { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public ICollection<Review> Reviews { get; set; } = [];
        public ICollection<Tag> Tags { get; set; } = [];
        public Status? Status { get; set; }
    }
}

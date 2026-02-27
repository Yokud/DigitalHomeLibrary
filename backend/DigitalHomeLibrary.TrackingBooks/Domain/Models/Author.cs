namespace DigitalHomeLibrary.TrackingBooks.Domain.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public DateOnly? DeathDate { get; set; }
        public string? LifeStory { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public ICollection<Book> Books { get; set; } = [];
    }
}

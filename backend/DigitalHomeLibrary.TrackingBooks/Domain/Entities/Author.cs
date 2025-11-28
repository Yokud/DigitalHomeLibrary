namespace DigitalHomeLibrary.TrackingBooks.Domain.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public string? LifeStory { get; set; }
        public Guid CountryId { get; set; }
        public Country? Country { get; set; }
        public IEnumerable<Book> Books { get; set; } = [];
    }
}

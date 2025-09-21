namespace DigitalHomeLibrary.Core.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public string? LifeStory { get; set; }
    }
}

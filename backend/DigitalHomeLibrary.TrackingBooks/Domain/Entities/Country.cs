namespace DigitalHomeLibrary.TrackingBooks.Domain.Entities
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Author> Authors { get; set; } = [];
    }
}

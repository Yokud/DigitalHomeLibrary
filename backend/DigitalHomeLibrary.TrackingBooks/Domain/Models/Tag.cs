namespace DigitalHomeLibrary.BookService.Domain.Models
{
    public class Tag
    {
        public Tag(string name, string description)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
    }
}

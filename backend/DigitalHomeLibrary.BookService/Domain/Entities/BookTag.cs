namespace DigitalHomeLibrary.BookService.Domain.Entities
{
    public class BookTag : DomainEntity
    {
        public BookTag(Guid id, string name, string? description = null) : base(id)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            Name = name;
            Description = description ?? string.Empty;
        }

        public BookTag(string name, string? description = null) : this(Guid.NewGuid(), name, description)
        {

        }

        public string Name { get; }
        public string Description { get; }
    }
}

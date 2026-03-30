namespace DigitalHomeLibrary.BookService.Domain.Entities
{
    public class BookTag : DomainEntity
    {
        public BookTag(Guid id, string name, string description) : base(id)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            Name = name;
            Description = description;
        }

        public BookTag(string name, string description) : this(Guid.NewGuid(), name, description)
        {

        }

        public string Name { get; }
        public string Description { get; }
    }
}

namespace DigitalHomeLibrary.BookService.Domain.Entities
{
    public abstract class DomainEntity
    {
        public DomainEntity(Guid id) => Id = id;

        public Guid Id { get; }
    }
}

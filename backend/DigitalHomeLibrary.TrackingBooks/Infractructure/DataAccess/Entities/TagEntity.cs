namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities
{
    public class TagEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<BookEntity> TaggedBooks { get; set; } = [];
    }
}

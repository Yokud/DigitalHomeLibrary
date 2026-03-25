namespace DigitalHomeLibrary.ContentService.Domain.Entities
{
    public class BookContentData(Guid bookId, string contentUri)
    {
        public Guid BookId { get; } = bookId;

        public string ContentUri { get; } = contentUri;
    }
}

namespace DigitalHomeLibrary.TrackingBooks.DTO
{
    public record TagsResponse(Guid Id, string Name, string? Description, IEnumerable<BookInfo> BookInfo)
    {
    }
}

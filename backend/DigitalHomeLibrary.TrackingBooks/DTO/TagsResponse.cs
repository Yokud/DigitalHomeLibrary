namespace DigitalHomeLibrary.BookService.DTO
{
    public record TagsResponse(Guid Id, string Name, string? Description, IEnumerable<BookInfo> BookInfo)
    {
    }
}

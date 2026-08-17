namespace DigitalHomeLibrary.BookService.Application.Requests
{
    public record UpdateTagRequest(Guid TagId, string Name, string? Description);
}

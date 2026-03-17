namespace DigitalHomeLibrary.BookService.Application.DTO.Requests
{
    public record UpdateTagRequest(Guid TagId, string Name, string? Description);
}

namespace DigitalHomeLibrary.BookService.Application.Requests
{
    public record UpdateBookRequest(string? Title, string? Description, int? ReleaseYear, string? Publisher, string? ISBN, string? Genre, string? Language);
}

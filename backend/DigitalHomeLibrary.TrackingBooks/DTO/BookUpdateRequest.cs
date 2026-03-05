namespace DigitalHomeLibrary.BookService.DTO
{
    public record BookUpdateRequest(string? Title, string? Description, int? ReleaseYear, string? Publisher, string? ISBN, string? Genre, string? Language);
}

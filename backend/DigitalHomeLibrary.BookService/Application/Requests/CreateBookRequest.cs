namespace DigitalHomeLibrary.BookService.Application.Requests
{
    public record CreateBookRequest(string Title, string Description, IReadOnlyList<Guid> AuthorIds, int ReleaseYear, string Publisher, string ISBN, string Genre, string Language);
}

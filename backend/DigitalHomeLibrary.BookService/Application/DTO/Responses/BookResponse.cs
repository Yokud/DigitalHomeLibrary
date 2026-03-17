namespace DigitalHomeLibrary.BookService.Application.DTO.Responses
{
    public record BookResponse(Guid Id, string Title, IReadOnlyList<string> AuthorNames, string Genre, double AverageScore, int ReviewCount)
    {
    }
}

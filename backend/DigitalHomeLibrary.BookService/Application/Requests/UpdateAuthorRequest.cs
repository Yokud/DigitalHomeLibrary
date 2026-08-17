namespace DigitalHomeLibrary.BookService.Application.Requests
{
    public record UpdateAuthorRequest(Guid Id, string? FirstName, string? MiddleName, string? LastName, DateOnly? BirthDate, DateOnly? DeathDate, string? LifeStory, string? CountryName);
}

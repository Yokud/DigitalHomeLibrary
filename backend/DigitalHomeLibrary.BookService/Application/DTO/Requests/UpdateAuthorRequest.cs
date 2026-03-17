namespace DigitalHomeLibrary.BookService.Application.DTO.Requests
{
    public record UpdateAuthorRequest(Guid Id, string? FirstName, string? MiddleName, string? LastName, DateOnly? BirthDate, DateOnly? DeathDate, string? LifeStory, string? CountryName);
}

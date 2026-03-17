namespace DigitalHomeLibrary.BookService.Application.DTO.Requests
{
    public record CreateAuthorRequest(string FirstName, string LastName, DateOnly BirthDate, string CountryName, string? MiddleName, DateOnly? DeathDate, string? LifeStory);
}

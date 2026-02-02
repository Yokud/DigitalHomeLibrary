namespace DigitalHomeLibrary.TrackingBooks.DTO
{
    public record AuthorUpdateRequest(string? FirstName, string? MiddleName, string? LastName, DateOnly? BirthDate, DateOnly? DeathDate, string? LifeStory, string? CountryName);
}

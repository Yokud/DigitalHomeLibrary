namespace DigitalHomeLibrary.TrackingBooks.DTO
{
    public record TagCreateRequest(string Name, string? Description)
    {
    }

    public record TagUpdateRequest(Guid TagId, string Name, string? Description);
}

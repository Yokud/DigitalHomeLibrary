namespace DigitalHomeLibrary.TrackingBooks.DTO
{
    public record ReviewCreateRequest(Guid BookId, byte Score, string Note)
    {
    }
}

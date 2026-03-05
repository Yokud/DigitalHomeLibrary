namespace DigitalHomeLibrary.BookService.DTO
{
    public record ReviewCreateRequest(Guid BookId, byte Score, string Note)
    {
    }
}

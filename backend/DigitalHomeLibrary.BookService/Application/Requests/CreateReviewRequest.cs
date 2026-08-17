namespace DigitalHomeLibrary.BookService.Application.Requests
{
    public record CreateReviewRequest(Guid BookId, byte Score, string Note)
    {
    }
}

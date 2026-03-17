namespace DigitalHomeLibrary.BookService.Application.DTO.Requests
{
    public record CreateReviewRequest(Guid BookId, byte Score, string Note)
    {
    }
}

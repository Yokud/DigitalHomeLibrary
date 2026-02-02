namespace DigitalHomeLibrary.TrackingBooks.DTO
{
    public record PaginationResponse<T>(int Page, int PageSize, int TotalElements, IEnumerable<T> Items) where T : class
    {

    }
}

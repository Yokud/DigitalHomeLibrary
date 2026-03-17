namespace DigitalHomeLibrary.BookService.Application.DTO.Info
{
    public sealed class PaginationInfo(int pageNum, int pageSize)
    {
        public int PageNum { get; init; } = pageNum >= 0 ? pageNum : throw new ArgumentException("Page number cannot be less 0");
        public int PageSize { get; init; } = pageSize > 0 ? pageSize : throw new ArgumentException("Page size cannot be less or equal 0");
    }
}

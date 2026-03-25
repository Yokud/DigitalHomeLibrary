using DigitalHomeLibrary.ContentService.Domain.Entities;

namespace DigitalHomeLibrary.ContentService.Domain.Repositories
{
    public interface IBookContentDataRepository
    {
        Task AddBookContentData(BookContentData bookContentData);

        Task<BookContentData> GetBookContentData(Guid bookId);

        Task DeleteBookContent(Guid bookId);
    }
}

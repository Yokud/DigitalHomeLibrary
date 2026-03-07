using DigitalHomeLibrary.BookService.Domain.Entities;

namespace DigitalHomeLibrary.BookService.Domain.Services
{
    public interface IBookTagService
    {
        Task<IEnumerable<BookTag>> GetBookTags(Guid bookId);

        Task<Guid> AddTagToBook(Guid bookId, BookTag tag);

        Task UpdateTag(BookTag tag);

        Task DeleteTag(Guid tagId);
    }
}

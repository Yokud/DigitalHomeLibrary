using DigitalHomeLibrary.TrackingBooks.Domain.Entities;

namespace DigitalHomeLibrary.TrackingBooks.Services.Abstract
{
    public interface IBookTagsService
    {
        Task<IEnumerable<Tag>> GetBookTags(Book book);

        Task<Guid> AddTagToBook(Guid bookId, Tag tag);

        Task UpdateTag(Tag tag);

        Task DeleteTag(Guid tagId);
    }
}

using DigitalHomeLibrary.TrackingBooks.Domain.Models;

namespace DigitalHomeLibrary.TrackingBooks.DataAccess.Services.Abstract
{
    public interface IBookTagsService
    {
        Task<IEnumerable<Tag>> GetBookTags(Guid bookId);

        Task<Guid> AddTagToBook(Guid bookId, Tag tag);

        Task UpdateTag(Tag tag);

        Task DeleteTag(Guid tagId);
    }
}

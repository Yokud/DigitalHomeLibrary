using DigitalHomeLibrary.TrackingBooks.DataAccess.Repositories.Abstract;
using DigitalHomeLibrary.TrackingBooks.DataAccess.Services.Abstract;
using DigitalHomeLibrary.TrackingBooks.Domain.Models;

namespace DigitalHomeLibrary.TrackingBooks.DataAccess.Services
{
    public class BooksTagsService(IAsyncRepository<Book> booksRepository, IAsyncRepository<Tag> tagsRepository) : IBookTagsService
    {
        readonly IAsyncRepository<Book> _booksRepository = booksRepository;
        readonly IAsyncRepository<Tag> _tagsRepository = tagsRepository;

        public async Task<Guid> AddTagToBook(Guid bookId, Tag tag)
        {
            var book = await _booksRepository.GetAsync(bookId) ?? throw new Exception("Not found book");

            var tagId = await _tagsRepository.CreateAsync(tag);
            book.Tags.Add(tag);
            await _booksRepository.UpdateAsync(book);

            return tagId;
        }

        public async Task DeleteTag(Guid tagId)
        {
            await _tagsRepository.DeleteAsync(tagId);
            await _tagsRepository.SaveAsync();
        }

        public async Task<IEnumerable<Tag>> GetBookTags(Guid bookId)
        {
            return (await _booksRepository.GetAsync(bookId))?.Tags ?? [];
        }

        public async Task UpdateTag(Tag tag)
        {
            await _tagsRepository.UpdateAsync(tag);
            await _tagsRepository.SaveAsync();
        }
    }
}

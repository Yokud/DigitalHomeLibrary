using DigitalHomeLibrary.BookService.DataAccess.Repositories;
using DigitalHomeLibrary.BookService.DataAccess.Services.Abstract;
using DigitalHomeLibrary.BookService.Domain.Models;
using DigitalHomeLibrary.BookService.Domain.Repositories;

namespace DigitalHomeLibrary.BookService.Infractructure.Services
{
    public class BooksTagsService(IBooksRepository booksRepository, ITagsRepository tagsRepository) : IBookTagsService
    {
        readonly IAsyncRepository<Book> _booksRepository = booksRepository;
        readonly IAsyncRepository<Tag> _tagsRepository = tagsRepository;

        public async Task<Guid> AddTagToBook(Guid bookId, Tag tag)
        {
            var book = await _booksRepository.GetByIdAsync(bookId) ?? throw new Exception("Not found book");

            var tagId = await _tagsRepository.AddAsync(tag);
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
            return (await _booksRepository.GetByIdAsync(bookId))?.Tags ?? [];
        }

        public async Task UpdateTag(Tag tag)
        {
            await _tagsRepository.UpdateAsync(tag);
            await _tagsRepository.SaveAsync();
        }
    }
}

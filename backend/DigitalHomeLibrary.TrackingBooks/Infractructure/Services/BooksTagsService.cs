using DigitalHomeLibrary.BookService.DataAccess.Repositories;
using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Domain.Services;

namespace DigitalHomeLibrary.BookService.Infractructure.Services
{
    public class BooksTagsService(IBookRepository booksRepository, ITagsRepository tagsRepository) : IBookTagService
    {
        readonly IAsyncRepository<Book> _booksRepository = booksRepository;
        readonly IAsyncRepository<BookTag> _tagsRepository = tagsRepository;

        public async Task<Guid> AddTagToBook(Guid bookId, BookTag tag)
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

        public async Task<IEnumerable<BookTag>> GetBookTags(Guid bookId)
        {
            return (await _booksRepository.GetByIdAsync(bookId))?.Tags ?? [];
        }

        public async Task UpdateTag(BookTag tag)
        {
            await _tagsRepository.UpdateAsync(tag);
            await _tagsRepository.SaveAsync();
        }
    }
}

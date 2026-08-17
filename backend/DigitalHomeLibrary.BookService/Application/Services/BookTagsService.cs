using CSharpFunctionalExtensions;
using DigitalHomeLibrary.BookService.Application.DTO;
using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.Repositories;

namespace DigitalHomeLibrary.BookService.Application.Services
{
    public class BookTagsService(IBookRepository booksRepository, IBookTagRepository tagsRepository)
    {
        readonly IBookRepository _booksRepository = booksRepository;
        readonly IBookTagRepository _tagsRepository = tagsRepository;

        public async Task<Result<Guid>> AddTagToBook(Guid bookId, string tagName, string? tagDescription)
        {
            var book = await _booksRepository.GetByIdAsync(bookId);

            if (book is null)
                return Result.Failure<Guid>("Not found book");

            var tag = await _tagsRepository.FindByNameAsync(tagName);
            if (tag is null)
            {
                tag = new BookTag(tagName, tagDescription);
                await _tagsRepository.AddAsync(tag);
            }

            book.AddTag(tag);
            await _booksRepository.UpdateAsync(book);

            return Result.Success(tag.Id);
        }

        public async Task<Result> DeleteTag(Guid tagId)
        {
            var tag = await _tagsRepository.GetByIdAsync(tagId);

            if (tag is null)
                return Result.Failure("Not found tag");

            var books = (await _booksRepository.GetAllAsync()).Where(book => book.BookTagIds.Contains(tagId));

            foreach (var book in books)
                book.DeleteTag(tag);

            await _tagsRepository.DeleteAsync(tagId);
            return Result.Success();
        }

        public async Task<IReadOnlyList<BookTagDto>> GetBookTags(Guid bookId)
        {
            var tagIds = (await _booksRepository.GetByIdAsync(bookId))?.BookTagIds ?? [];

            var bookTagTasks = tagIds.Select(async tagId => await _tagsRepository.GetByIdAsync(tagId));
            var bookTags = await Task.WhenAll(bookTagTasks);

            return [.. bookTags.Where(e => e is not null).Select(e => BookTagDto.FromDomainEntity(e!))];
        }

        public async Task<Result> UpdateTag(Guid tagId, string tagName, string? description = null)
        {
            if (await _tagsRepository.GetByIdAsync(tagId) is null)
                return Result.Failure("Not found tag");

            var tag = new BookTag(tagId, tagName, description);
            await _tagsRepository.UpdateAsync(tag);

            return Result.Success();
        }
    }
}

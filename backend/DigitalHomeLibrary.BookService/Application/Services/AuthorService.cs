using CSharpFunctionalExtensions;
using DigitalHomeLibrary.BookService.Application.DTO.Info;
using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Application.Services
{
    public class AuthorService(IAuthorRepository authorRepository)
    {
        readonly IAuthorRepository _authorRepository = authorRepository;

        public async Task<IReadOnlyList<Author>> GetAllAuthors(PaginationInfo? paginationInfo)
        {
            return await _authorRepository.GetAllAsync(paginationInfo);
        }

        public async Task<Result<Author>> GetAuthorById(Guid authorId)
        {
            var res = await _authorRepository.GetByIdAsync(authorId);

            return res is not null ? Result.Success(res) : Result.Failure<Author>($"Author with ID = {authorId} does not exist");
        }

        public async Task<Result<Author>> FindAuthorByFullName(FullName fullName)
        {
            var res = await _authorRepository.FindByFullNameAsync(fullName);

            return res is not null ? Result.Success(res) : Result.Failure<Author>($"Author with full name \"{fullName}\" does not exist");
        }

        public async Task<Result> UpdateAuthor(Author author)
        {
            if (await _authorRepository.GetByIdAsync(author.Id) is null)
                return Result.Failure($"Author with ID = {author.Id} does not exist");

            await _authorRepository.UpdateAsync(author);
            return Result.Success();
        }

        public async Task<Result<Guid>> AddAuthor(Author author)
        {
            if (await _authorRepository.FindByFullNameAsync(author.FullName) is not null)
                return Result.Failure<Guid>($"Author with fullname \"{author.FullName}\" already exists");

            var id = await _authorRepository.AddAsync(author);

            return Result.Success(id);
        }

        public async Task DeleteAuthor(Guid id)
        {
            await _authorRepository.DeleteAsync(id);
        }
    }
}

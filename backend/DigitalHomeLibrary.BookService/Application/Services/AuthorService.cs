using CSharpFunctionalExtensions;
using DigitalHomeLibrary.BookService.Application.DTO;
using DigitalHomeLibrary.BookService.Application.Responses;
using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Application.Services
{
    public class AuthorService(IAuthorRepository authorRepository)
    {
        readonly IAuthorRepository _authorRepository = authorRepository;

        public async Task<PaginationResponse<AuthorDto>> GetAllAuthors(int page, int size)
        {
            var paginationParams = new PaginationParams(page, size);

            var res = await _authorRepository.GetAllAsync(paginationParams);

            return new PaginationResponse<AuthorDto>(page, size, res.Count, res.Select(AuthorDto.FromDomainEntity));
        }

        public async Task<Result<AuthorDto>> GetAuthorById(Guid authorId)
        {
            var res = await _authorRepository.GetByIdAsync(authorId);

            return res is not null ? Result.Success(AuthorDto.FromDomainEntity(res)) : Result.Failure<AuthorDto>($"Author with ID = {authorId} does not exist");
        }

        public async Task<Result<AuthorDto>> FindAuthorByFullName(FullName fullName)
        {
            var res = await _authorRepository.FindByFullNameAsync(fullName);

            return res is not null ? Result.Success(AuthorDto.FromDomainEntity(res)) : Result.Failure<AuthorDto>($"Author with full name \"{fullName}\" does not exist");
        }

        public async Task<Result> UpdateAuthor(Guid id, string? firstName, string? middleName, string? lastName, DateOnly? birthDate, DateOnly? deathDate, string? lifeStory, string? countryName)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author is null)
                return Result.Failure($"Author with ID = {id} does not exist");

            var updatedAuthor = new Author(id,
                new(firstName ?? author.FullName.FirstName,
                    lastName ?? author.FullName.LastName,
                    middleName ?? author.FullName.MiddleName),
                birthDate ?? author.BirthDate,
                countryName is not null ? new(countryName) : author.Country,
                deathDate ?? author.DeathDate,
                lifeStory ?? author.LifeStory);

            await _authorRepository.UpdateAsync(updatedAuthor);
            return Result.Success();
        }

        public async Task<Result<Guid>> AddAuthor(string? firstName, string? middleName, string? lastName, DateOnly? birthDate, DateOnly? deathDate, string? lifeStory, string? countryName)
        {
            var fullName = new FullName(firstName, lastName, middleName);
            if (await _authorRepository.FindByFullNameAsync(fullName) is not null)
                return Result.Failure<Guid>($"Author with fullname \"{fullName}\" already exists");

            var newAuthor = new Author(fullName, birthDate.Value, new Country(countryName), deathDate, lifeStory);
            var id = await _authorRepository.AddAsync(newAuthor);

            return Result.Success(id);
        }

        public async Task DeleteAuthor(Guid id) => await _authorRepository.DeleteAsync(id);
    }
}

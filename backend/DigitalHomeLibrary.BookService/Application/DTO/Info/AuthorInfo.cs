using DigitalHomeLibrary.BookService.Domain.Entities;

namespace DigitalHomeLibrary.BookService.Application.DTO.Info
{
    public record AuthorInfo(Guid Id, string FirstName, string? MiddleName, string LastName, DateOnly BirthDate, DateOnly? DeathDate, string? LifeStory, string CountryName)
    {
        public static AuthorInfo FromDomainEntity(Author author) => new(author.Id, author.FullName.FirstName, author.FullName.MiddleName, author.FullName.LastName, author.BirthDate, author.DeathDate, author.LifeStory, author.Country.Name);

        public static Author ToDomainEntity(AuthorInfo author) => new(
            new(author.FirstName, author.LastName, author.MiddleName),
            author.BirthDate,
            new(author.CountryName),
            author.DeathDate,
            author.LifeStory
        );
    }
}

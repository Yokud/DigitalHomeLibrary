using DigitalHomeLibrary.BookService.Domain.Entities;

namespace DigitalHomeLibrary.BookService.Application.DTO
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="FirstName"></param>
    /// <param name="MiddleName"></param>
    /// <param name="LastName"></param>
    /// <param name="BirthDate"></param>
    /// <param name="DeathDate"></param>
    /// <param name="LifeStory"></param>
    /// <param name="CountryName"></param>
    public record AuthorDto(Guid Id, string FirstName, string? MiddleName, string LastName, DateOnly BirthDate, DateOnly? DeathDate, string? LifeStory, string CountryName)
    {
        public static AuthorDto FromDomainEntity(Author author) => new(author.Id, author.FullName.FirstName, author.FullName.MiddleName, author.FullName.LastName, author.BirthDate, author.DeathDate, author.LifeStory, author.Country.Name);

        public static Author ToDomainEntity(AuthorDto author) => new(
            new(author.FirstName, author.LastName, author.MiddleName),
            author.BirthDate,
            new(author.CountryName),
            author.DeathDate,
            author.LifeStory
        );
    }
}

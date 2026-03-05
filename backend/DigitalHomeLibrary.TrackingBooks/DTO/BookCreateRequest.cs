using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities;

namespace DigitalHomeLibrary.BookService.DTO
{
    public record BookInfo(string Title, string Description, int ReleaseYear, string Publisher, string ISBN,  string Genre, string Language)
    {
        public static BookInfo FromEntity(BookEntity book) => new(book.Title, book.Description, book.ReleaseYear, book.Publisher, book.ISBN, book.Genre, book.Language);

        public static BookEntity ToEntity(BookInfo bookInfo) => new()
        {
            Title = bookInfo.Title,
            Description = bookInfo.Description,
            ReleaseYear = bookInfo.ReleaseYear,
            Publisher = bookInfo.Publisher,
            ISBN = bookInfo.ISBN,
            Genre = bookInfo.Genre,
            Language = bookInfo.Language
        };
    }

    public record AuthorInfo(string FirstName, string? MiddleName, string LastName, DateOnly BirthDate, DateOnly? DeathDate, string? LifeStory, string CountryName)
    {
        public static AuthorInfo FromEntity(Author author) => new(author.FirstName, author.MiddleName, author.LastName, author.BirthDate, author.DeathDate, author.LifeStory, author.CountryName);

        public static Author ToEntity(AuthorInfo author) => new()
        {
            FirstName = author.FirstName,
            MiddleName = author.MiddleName,
            LastName = author.LastName,
            BirthDate = author.BirthDate,
            DeathDate = author.DeathDate,
            LifeStory = author.LifeStory,
            CountryName = author.CountryName
        };
    }

    public record BookCreateRequest(BookInfo BookInfo, IEnumerable<AuthorInfo> BookAuthorsInfo);
}

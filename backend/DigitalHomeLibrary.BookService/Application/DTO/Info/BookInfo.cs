using DigitalHomeLibrary.BookService.Domain.Entities;

namespace DigitalHomeLibrary.BookService.Application.DTO.Info
{
    public record BookInfo(Guid Id, string Title, string Description, IReadOnlyList<Guid> AuthorIds, int ReleaseYear, string Publisher, string ISBN,  string Genre, string Language)
    {
        public static BookInfo FromDomainEntity(Book book) => new(book.Id, book.Details.Title, book.Details.Description, book.Details.AuthorIds, book.Details.ReleaseYear, book.Details.Publisher, book.Details.ISBN.Value, book.Details.Genre, book.Details.Language);

        public static Book ToDomainEntity(BookInfo bookInfo) => new(
            new(bookInfo.Title,
                bookInfo.Description,
                bookInfo.AuthorIds,
                bookInfo.ReleaseYear,
                bookInfo.Publisher,
                new(bookInfo.ISBN),
                bookInfo.Genre,
                bookInfo.Language)
            );
    }
}

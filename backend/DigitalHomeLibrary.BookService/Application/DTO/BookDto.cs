using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Application.DTO
{
    public record BookDto(Guid? Id, string Title, string Description, IReadOnlyList<Guid> AuthorIds, int ReleaseYear, string Publisher, string ISBN, string Genre, string Language)
    {
        public static BookDto FromDomainEntity(Book book) => new(book.Id, book.Details.Title, book.Details.Description, book.Details.AuthorIds, book.Details.ReleaseYear, book.Details.Publisher, book.Details.ISBN.Value, book.Details.Genre, book.Details.Language);

        public static Book ToDomainEntity(BookDto bookInfo)
        {
            var bookDetails = new BookDetails(
                bookInfo.Title,
                bookInfo.Description,
                bookInfo.AuthorIds,
                bookInfo.ReleaseYear,
                bookInfo.Publisher,
                new(bookInfo.ISBN),
                bookInfo.Genre,
                bookInfo.Language
                );

            return bookInfo.Id.HasValue ? new(bookInfo.Id.Value, bookDetails) : new(bookDetails);
        }
    }
}

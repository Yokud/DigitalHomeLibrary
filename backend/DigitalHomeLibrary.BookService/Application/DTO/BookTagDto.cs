using DigitalHomeLibrary.BookService.Domain.Entities;

namespace DigitalHomeLibrary.BookService.Application.DTO
{
    public record BookTagDto(Guid Id, string Name, string Description)
    {
        public static BookTagDto FromDomainEntity(BookTag bookTag) => new(bookTag.Id, bookTag.Name, bookTag.Description);
    }
}

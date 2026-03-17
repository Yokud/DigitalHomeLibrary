using DigitalHomeLibrary.BookService.Domain.Entities;

namespace DigitalHomeLibrary.BookService.Application.DTO.Info
{
    public record BookTagInfo(Guid Id, string Name, string Description)
    {
        public static BookTagInfo FromDomainEntity(BookTag bookTag) => new(bookTag.Id, bookTag.Name, bookTag.Description);
    }
}

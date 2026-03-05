using DigitalHomeLibrary.BookService.Domain.Models;

namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities
{
    public class StatusEntity
    {
        public Guid Id { get; set; }
        public BookEntity? Book { get; set; }
        public DateTime AdditionDateTime { get; set; }
        public ReadingState ReadingState { get; set; }
        public DateOnly? ReadingStartDate { get; set; }
        public DateOnly? ReadingEndDate { get; set; }
    }
}

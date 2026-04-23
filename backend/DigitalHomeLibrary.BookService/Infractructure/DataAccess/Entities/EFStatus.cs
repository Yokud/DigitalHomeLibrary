using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities
{
    public class EFStatus
    {
        public DateTime AdditionDateTime { get; set; }
        public ReadingState ReadingState { get; set; }
        public DateOnly? ReadingStartDate { get; set; }
        public DateOnly? ReadingEndDate { get; set; }

        public static EFStatus FromDomain(Status status) => new() { AdditionDateTime = status.AdditionDateTime, ReadingState = status.ReadingState, ReadingStartDate = status.ReadingStartDate, ReadingEndDate = status.ReadingEndDate };
    }
}

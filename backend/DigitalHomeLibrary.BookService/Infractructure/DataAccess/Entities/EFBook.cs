using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities
{
    public class EFBook
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public ICollection<EFAuthor> Authors { get; set; } = [];
        public ICollection<EFReview> Reviews { get; set; } = [];
        public ICollection<EFTag> Tags { get; set; } = [];
        public DateTime AdditionDateTime { get; set; }
        public ReadingState ReadingState { get; set; }
        public DateOnly? ReadingStartDate { get; set; }
        public DateOnly? ReadingEndDate { get; set; }
    }
}

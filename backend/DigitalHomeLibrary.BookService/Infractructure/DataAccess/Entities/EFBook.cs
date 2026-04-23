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
        public required EFStatus Status { get; set; }
        public ICollection<EFAuthor> Authors { get; set; } = [];
        public ICollection<EFReview> Reviews { get; set; } = [];
        public ICollection<EFTag> Tags { get; set; } = [];
    }
}

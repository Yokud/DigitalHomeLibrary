namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.DBO
{
    public class BookDbo
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public required StatusDbo Status { get; set; }
        public ICollection<AuthorDbo> Authors { get; set; } = [];
        public ICollection<ReviewDbo> Reviews { get; set; } = [];
        public ICollection<TagDbo> Tags { get; set; } = [];
    }
}

using DigitalHomeLibrary.BookService.Domain.Entities;

namespace DigitalHomeLibrary.BookService.Domain.ValueObjects
{
    public sealed class BookInfo
    {
        public BookInfo(string title, string description, IEnumerable<Guid> authorIds, int releaseYear, string publisher, ISBN isbn, string genre, string language)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(title, nameof(title));
            ArgumentOutOfRangeException.ThrowIfLessThan(title.Length, 2, nameof(title));

            if (authorIds is null || !authorIds.Any())
                throw new ArgumentNullException(nameof(authorIds));

            ArgumentNullException.ThrowIfNullOrWhiteSpace(publisher, nameof(publisher));
            ArgumentOutOfRangeException.ThrowIfLessThan(publisher.Length, 2, nameof(publisher));

            Title = title;
            Description = description;
            AuthorIds = [.. authorIds.Distinct()];
            ReleaseYear = releaseYear;
            Publisher = publisher;
            ISBN = isbn;
            Genre = genre;
            Language = language;
        }

        public string Title { get; }
        public string Description { get; }
        public IReadOnlyCollection<Guid> AuthorIds { get; }
        public int ReleaseYear { get; }
        public string Publisher { get; }
        public ISBN ISBN { get; }
        public string Genre { get; }
        public string Language { get; }
    }
}

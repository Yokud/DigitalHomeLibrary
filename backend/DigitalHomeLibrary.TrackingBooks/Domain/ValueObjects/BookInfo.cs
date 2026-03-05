using DigitalHomeLibrary.BookService.Domain.Models;

namespace DigitalHomeLibrary.BookService.Domain.ValueObjects
{
    public sealed class BookInfo
    {
        public BookInfo(string title, string description, IEnumerable<Author> authors, int releaseYear, string publisher, string isbn, string genre, string language)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(title, nameof(title));
            ArgumentOutOfRangeException.ThrowIfLessThan(title.Length, 2, nameof(title));

            if (authors is null || !authors.Any())
                throw new ArgumentNullException(nameof(authors));

            ArgumentNullException.ThrowIfNullOrWhiteSpace(publisher, nameof(title));
            ArgumentOutOfRangeException.ThrowIfLessThan(title.Length, 2, nameof(title));

            Title = title;
            Description = description;
            Authors = [.. authors];
            ReleaseYear = releaseYear;
            Publisher = publisher;
            ISBN = isbn;
            Genre = genre;
            Language = language;
        }

        public string Title { get; }
        public string Description { get; }
        public IReadOnlyCollection<Author> Authors { get; }
        public int ReleaseYear { get; }
        public string Publisher { get; }
        public string ISBN { get; }
        public string Genre { get; }
        public string Language { get; }
    }
}

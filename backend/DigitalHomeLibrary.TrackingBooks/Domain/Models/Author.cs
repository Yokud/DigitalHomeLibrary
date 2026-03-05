using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Domain.Models
{
    public sealed class Author
    {
        public Author(FullName fullName, DateOnly birthDate, Country country, DateOnly? deathDate, string? lifeStory)
        {
            ArgumentNullException.ThrowIfNull(fullName, nameof(fullName));
            ArgumentNullException.ThrowIfNull(country, nameof(country));

            if (deathDate is not null && deathDate.Value <= BirthDate)
                throw new ArgumentException("Author cannot be dead before own birth");

            Id = Guid.NewGuid();
            FullName = fullName;
            BirthDate = birthDate;
            Country = country;
            DeathDate = deathDate;
            LifeStory = lifeStory;
        }

        public Author(FullName fullName, DateOnly birthDate, Country country) : this(fullName, birthDate, country, null, null)
        {

        }

        public Guid Id { get; }
        public FullName FullName { get; private set; }
        public DateOnly BirthDate { get; private set; }
        public DateOnly? DeathDate { get; private set; }
        public string? LifeStory { get; private set; }
        public Country Country { get; private set; }
    }
}

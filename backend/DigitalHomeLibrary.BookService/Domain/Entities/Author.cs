using DigitalHomeLibrary.BookService.Domain.ValueObjects;

namespace DigitalHomeLibrary.BookService.Domain.Entities
{
    public sealed class Author : DomainEntity
    {
        public Author(Guid id, FullName fullName, DateOnly birthDate, Country country, DateOnly? deathDate, string? lifeStory) : base(id)
        {
            ArgumentNullException.ThrowIfNull(fullName, nameof(fullName));
            ArgumentNullException.ThrowIfNull(country, nameof(country));

            if (deathDate is not null && deathDate.Value <= BirthDate)
                throw new ArgumentException("Author cannot be dead before own birth");

            FullName = fullName;
            BirthDate = birthDate;
            Country = country;
            DeathDate = deathDate;
            LifeStory = lifeStory;
        }

        public Author(FullName fullName, DateOnly birthDate, Country country, DateOnly? deathDate, string? lifeStory) : this(Guid.NewGuid(), fullName, birthDate, country, deathDate, lifeStory)
        {

        }

        public FullName FullName { get; private set; }
        public DateOnly BirthDate { get; private set; }
        public DateOnly? DeathDate { get; private set; }
        public string? LifeStory { get; private set; }
        public Country Country { get; private set; }
    }
}

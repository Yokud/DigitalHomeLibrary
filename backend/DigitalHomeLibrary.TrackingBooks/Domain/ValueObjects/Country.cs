namespace DigitalHomeLibrary.BookService.Domain.ValueObjects
{
    public sealed class Country
    {
        public Country(string name)
        {
            if (name.Length < 3 || !name.All(char.IsLetter))
                throw new ArgumentException("Country name is too short or has digits in name");

            Name = name;
        }

        public string Name { get; }
    }
}

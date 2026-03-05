using System.ComponentModel.DataAnnotations;

namespace DigitalHomeLibrary.BookService.Domain.ValueObjects
{
    public sealed class FullName : IEquatable<FullName>
    {
        private const int MinNameLen = 3;

        public FullName(string firstName, string lastName, string? middleName = null)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < MinNameLen || !firstName.All(char.IsLetter))
                throw new ArgumentException($"First name cannot be null, white space, must contains only letters and len greater or equal {MinNameLen}");

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < MinNameLen || !lastName.All(char.IsLetter))
                throw new ArgumentException($"Last name cannot be null, white space, must contains only letters and len greater or equal {MinNameLen}");

            if (middleName is not null && (!middleName.All(char.IsLetter) || middleName.Length < MinNameLen))
                throw new ArgumentException($"Middle name must contains only letters and len greater or equal {MinNameLen}");

            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        public string FirstName { get; }
        public string? MiddleName { get; }
        public string LastName { get; }

        public bool Equals(FullName? other)
        {
            if (other is null)
                return false;

            return FirstName == other.FirstName && LastName == other.LastName && MiddleName == other.MiddleName;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as FullName);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, MiddleName);
        }
    }
}

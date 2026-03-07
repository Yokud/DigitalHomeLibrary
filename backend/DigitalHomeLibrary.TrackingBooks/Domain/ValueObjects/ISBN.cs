namespace DigitalHomeLibrary.BookService.Domain.ValueObjects
{
    public partial class ISBN
    {
        public ISBN(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("ISBN не может быть пустым.");

            // Убираем дефисы и пробелы для проверки
            var sanitized = value.Replace("-", "").Replace(" ", "");

            if (!IsValidIsbn(sanitized))
                throw new ArgumentException($"Некорректный формат ISBN: {value}");

            Value = sanitized;
        }

        public string Value { get; }

        public int EAN { get; }

        public int Group { get; }

        public int Publisher { get; }

        public int Publication { get; }

        public char CheckDigit { get; }

        private static bool IsValidIsbn(string isbn)
        {
            if (isbn.Length == 10)
                return IsValidIsbn10(isbn);

            if (isbn.Length == 13)
                return IsValidIsbn13(isbn);

            return false;
        }

        private static bool IsValidIsbn10(string isbn)
        {
            // 9 цифр + контрольная цифра (может быть 'X')
            if (!ISBN10Regex().IsMatch(isbn))
                return false;

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (isbn[i] - '0') * (10 - i);

            char last = isbn[9];
            sum += (last == 'X') ? 10 : (last - '0');

            return sum % 11 == 0;
        }

        private static bool IsValidIsbn13(string isbn)
        {
            if (!ISBN13Regex().IsMatch(isbn))
                return false;

            int sum = 0;
            for (int i = 0; i < 13; i++)
            {
                int digit = isbn[i] - '0';
                sum += (i % 2 == 0) ? digit : digit * 3;
            }

            return sum % 10 == 0;
        }

        [System.Text.RegularExpressions.GeneratedRegex(@"^\d{9}[\dX]$")]
        private static partial System.Text.RegularExpressions.Regex ISBN10Regex();

        [System.Text.RegularExpressions.GeneratedRegex(@"^\d{13}$")]
        private static partial System.Text.RegularExpressions.Regex ISBN13Regex();
    }
}

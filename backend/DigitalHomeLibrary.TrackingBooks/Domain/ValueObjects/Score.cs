namespace DigitalHomeLibrary.BookService.Domain.ValueObjects
{
    public class Score : IEquatable<Score>
    {
        private const int MinScoreValue = 0, MaxScoreValue = 5;
        private readonly int _scoreValue;

        public Score(int scoreValue)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(scoreValue, MinScoreValue);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(scoreValue, MaxScoreValue);

            _scoreValue = scoreValue;
        }

        public int ScoreValue => _scoreValue;

        public bool Equals(Score? other)
        {
            return other is not null && _scoreValue == other._scoreValue;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Score);
        }

        public override int GetHashCode()
        {
            return _scoreValue.GetHashCode();
        }
    }
}

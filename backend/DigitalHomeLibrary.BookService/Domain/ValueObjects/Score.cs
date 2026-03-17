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

    public sealed class AverageScore
    {
        public AverageScore(IEnumerable<Score> scores)
        {
            var isNullOrEmpty = scores is null || !scores.Any();
            
            ScoresCount = !isNullOrEmpty ? scores!.Count() : 0;
            AverageScoreValue = !isNullOrEmpty ? (float)scores!.Select(s => s.ScoreValue).Sum() / ScoresCount : 0;
        }

        public int ScoresCount { get; }

        public float AverageScoreValue { get; }

        public static AverageScore ZeroScore => new([]);
    }
}

using System.Runtime.CompilerServices;

namespace DigitalHomeLibrary.BookService.Infractructure
{
    public class Result
    {
        public bool IsSuccess { get; }

        public string? ErrorMessage { get; }

        protected Result(bool isSuccess, string? errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static Result Success() => new(true, null);

        public static Result Failure(string errorMessage) => new(false, errorMessage);
    }

    public class Result<T> : Result where T : class
    {
        public T? Value { get; }

        private Result(bool isSuccess, string? errorMessage, T? value) : base(isSuccess, errorMessage)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new(true, null, value);

        public static new Result<T> Failure(string errorMessage) => new(false, errorMessage, null);
    }
}
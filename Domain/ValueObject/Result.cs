namespace Biokudi_Backend.Domain.ValueObject
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, string errorMessage)
        {
            if (isSuccess && !string.IsNullOrEmpty(errorMessage))
                throw new InvalidOperationException();

            if (!isSuccess && string.IsNullOrEmpty(errorMessage))
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static Result Success() => new Result(true, string.Empty);
        public static Result Failure(string errorMessage) => new Result(false, errorMessage);
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        protected Result(T value, bool isSuccess, string errorMessage) : base(isSuccess, errorMessage)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new Result<T>(value, true, string.Empty);
        public static Result<T> Failure(string errorMessage) => new Result<T>(default(T), false, errorMessage);
    }
}

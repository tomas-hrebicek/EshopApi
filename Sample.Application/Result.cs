namespace Sample.Application
{
    public class Result
    {
        private readonly Error _error;

        public Result(Error error)
        {
            _error = error;
        }

        public static Result Success() => new Result(null);

        public static Result Failure(Error error) => new Result(error ?? throw new ArgumentNullException(nameof(error)));

        public static Result<TData> Success<TData>(TData value) => new Result<TData>(value, null);

        public static Result<TData> Failure<TData>(Error error) => new Result<TData>(default, error ?? throw new ArgumentNullException(nameof(error)));

        public bool IsSuccess => this.Error is null;
        public Error Error => _error;
    }

    public class Result<TData> : Result
    {
        private readonly TData _data;

        public Result(TData data, Error error)
            : base(error)
        {
            _data = data;
        }

        public static implicit operator Result<TData>(TData data)
        {
            return Success(data);
        }

        public TData Data => _data;
    }
}
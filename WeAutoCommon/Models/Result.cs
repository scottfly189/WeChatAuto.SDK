namespace WeAutoCommon.Models
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        protected Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static Result Ok() => new Result(true, null);
        public static Result Fail(string error) => new Result(false, error);
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }
        private Result(bool success, string message, T data) : base(success, message)
        {
            Value = data;
        }

        public static Result<T> Ok(T value) => new Result<T>(true, null, value);
        public new static Result<T> Fail(string error) => new Result<T>(false, error, default);
    }

}
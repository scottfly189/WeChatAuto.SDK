using System;

namespace WeAutoCommon.Models
{

    /// <summary>
    /// Result类，用于封装操作结果
    /// </summary>
    public class Result
    {
        public bool Success { get; set; }
        public string Error { get; set; }

        protected Result(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        public static Result Ok() => new Result(true, null);
        public static Result Fail(string error) => new Result(false, error);
    }
    /// <summary>
    /// Result的泛型类，用于封装操作结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : Result
    {
        public T Value { get; set; }
        private Result(bool success, string error, T data) : base(success, error)
        {
            Value = data;
        }

        public static Result<T> Ok(T value) => new Result<T>(true, null, value);
        public new static Result<T> Fail(string error) => new Result<T>(false, error, default);

        public Result<U> Map<U>(Func<T, U> mapper)
            => Success ? Result<U>.Ok(mapper(Value)) : Result<U>.Fail(Error);

        public Result<U> Bind<U>(Func<T, Result<U>> binder)
            => Success ? binder(Value) : Result<U>.Fail(Error);

        public U Match<U>(Func<T, U> ok, Func<string, U> fail)
            => Success ? ok(Value) : fail(Error);
    }
}
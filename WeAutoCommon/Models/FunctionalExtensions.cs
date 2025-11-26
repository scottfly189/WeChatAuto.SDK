using System;
using System.Threading.Tasks;

namespace WeAutoCommon.Models
{
    /// <summary>
    /// 函数式扩展方法
    /// </summary>
    public static class FunctionalExtensions
    {
        /// <summary>
        /// 将Maybe转换为Result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maybe"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Result<T> ToResult<T>(this Maybe<T> maybe, string errorMessage)
            => maybe.HasValue
                ? Result<T>.Ok(maybe.Value)
                : Result<T>.Fail(errorMessage);
        /// <summary>
        /// 将值转换为Maybe
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Maybe<T> ToMaybe<T>(this T value)
            => value == null ? Maybe<T>.None() : Maybe<T>.Some(value);
        /// <summary>
        /// 尝试执行函数，如果失败则返回失败，否则返回成功的结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result<T> Try<T>(Func<T> func)
        {
            try
            {
                return Result<T>.Ok(func());
            }
            catch (Exception ex)
            {
                return Result<T>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// Result Bind，将Result转换为Result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="result"></param>
        /// <param name="binder"></param>
        /// <returns></returns>
        public static Result<U> Bind<T, U>(this Result<T> result, Func<T, Result<U>> binder)
            => result.Success
                ? binder(result.Value)
                : Result<U>.Fail(result.Error);

        /// <summary>
        /// Result Map，将Result转换为Result
        /// 如果你要加工值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="result"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static Result<U> Map<T, U>(this Result<T> result, Func<T, U> mapper)
            => result.Success
                ? Result<U>.Ok(mapper(result.Value))
                : Result<U>.Fail(result.Error);

        /// <summary>
        /// Result Match，将Result转换为Result
        /// 最终收敛
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="result"></param>
        /// <param name="ok"></param>
        /// <param name="fail"></param>
        /// <returns></returns>
        public static U Match<T, U>(this Result<T> result, Func<T, U> ok, Func<string, U> fail)
            => result.Success
                ? ok(result.Value)
                : fail(result.Error);
        /// <summary>
        /// Async Result Bind，将Result转换为Result
        /// 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="result"></param>
        /// <param name="binder"></param>
        /// <returns></returns>
        public static async Task<Result<U>> BindAsync<T, U>(this Result<T> result, Func<T, Task<Result<U>>> binder)
            => result.Success ? await binder(result.Value) : Result<U>.Fail(result.Error);


        /// <summary>
        /// Async Result Map，将Result转换为Result
        /// 如果你要加工值
        /// 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="result"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static async Task<Result<U>> MapAsync<T, U>(this Result<T> result, Func<T, Task<U>> mapper)
            => result.Success ? Result<U>.Ok(await mapper(result.Value)) : Result<U>.Fail(result.Error);
    }

}
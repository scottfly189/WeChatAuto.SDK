using System;
using System.ComponentModel;

namespace WeAutoCommon.Models
{
    /// <summary>
    /// Maybe类，用于封装可能存在的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Maybe<T>
    {
        public T Value { get; set; }
        public bool HasValue { get; set; }
        private Maybe(T value, bool hasValue)
        {
            Value = value;
            HasValue = hasValue;
        }
        public static Maybe<T> Some(T value) => new Maybe<T>(value, true);
        public static Maybe<T> None() => new Maybe<T>(default, false);
        public Maybe<U> Bind<U>(Func<T, Maybe<U>> binder)
            => HasValue ? binder(Value) : Maybe<U>.None();
        public Maybe<U> Map<U>(Func<T, U> mapper)
            => HasValue ? Maybe<U>.Some(mapper(Value)) : Maybe<U>.None();

        public U Match<U>(Func<T, U> some, Func<U> none)
            => HasValue ? some(Value) : none();
        public T GetValueOrDefault(T defaultValue) => HasValue ? Value : defaultValue;

        /// <summary>
        /// 当Maybe为None时，执行一个委托来提供备用的Maybe值
        /// </summary>
        /// <param name="orElse">当为None时执行的委托</param>
        /// <returns>如果有值则返回自身，否则返回orElse的结果</returns>
        public Maybe<T> OrElse(Func<Maybe<T>> orElse)
            => HasValue ? this : orElse();

        /// <summary>
        /// 当Maybe为None时，执行一个Action（副作用操作，如日志记录）
        /// </summary>
        /// <param name="onNone">当为None时执行的Action</param>
        /// <returns>返回自身，便于链式调用</returns>
        public Maybe<T> OnNone(Action onNone)
        {
            if (!HasValue)
            {
                onNone();
            }
            return this;
        }

        /// <summary>
        /// Bind的扩展版本：如果有值则执行binder，如果为None则执行orElse
        /// </summary>
        /// <typeparam name="U">目标类型</typeparam>
        /// <param name="binder">当有值时的转换函数</param>
        /// <param name="orElse">当为None时的备用函数</param>
        /// <returns>转换后的Maybe</returns>
        public Maybe<U> BindOrElse<U>(Func<T, Maybe<U>> binder, Func<Maybe<U>> orElse)
            => HasValue ? binder(Value) : orElse();
    }
}
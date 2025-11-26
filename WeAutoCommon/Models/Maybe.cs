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
        public T GetValueOrNull() => HasValue ? Value : default;
    }
}
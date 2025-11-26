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

        public Maybe<T> Bind(Func<T, Maybe<T>> func) => HasValue ? func(Value) : Maybe<T>.None();


    }
}
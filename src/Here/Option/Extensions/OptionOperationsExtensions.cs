using System;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Option{T}"/> for operations.
    /// </summary>
    public static class OptionOperationsExtensions
    {
        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <param name="then">Treatment to do.</param>
        /// <returns>This <see cref="Option{T}"/>.</returns>
        [PublicAPI]
        public static Option<T> If<T>(in this Option<T> option, [NotNull, InstantHandle] in Action<T> then)
        {
            if (option.HasValue)
                then(option.Value);
            return option;
        }

        /// <summary>
        /// Calls the <paramref name="else"/> function if this <see cref="Option{T}"/> has no value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <param name="else">Treatment to do.</param>
        /// <returns>This <see cref="Option{T}"/>.</returns>
        [PublicAPI]
        public static Option<T> Else<T>(in this Option<T> option, [NotNull, InstantHandle] in Action @else)
        {
            if (option.HasNoValue)
                @else();
            return option;
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value, otherwise calls <paramref name="else"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <param name="then">Treatment to do with this <see cref="Option{T}"/> value.</param>
        /// <param name="else">Treatment to do if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>This <see cref="Option{T}"/>.</returns>
        [PublicAPI]
        public static Option<T> IfElse<T>(in this Option<T> option, [NotNull, InstantHandle] in Action<T> then, [NotNull, InstantHandle] in Action @else)
        {
            if (option.HasValue)
                then(option.Value);
            else
                @else();

            return option;
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <param name="then">Treatment to do with this <see cref="Option{T}"/> value.</param>
        /// <param name="else">Treatment to do if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>Result of the executed callback.</returns>
        [PublicAPI]
        public static TResult IfElse<T, TResult>(in this Option<T> option, [NotNull, InstantHandle] in Func<T, TResult> then, [NotNull, InstantHandle] in Func<TResult> @else)
        {
            if (option.HasValue)
                return then(option.Value);
            return @else();
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value, otherwise returns the <paramref name="orValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <param name="then">Treatment to do with this <see cref="Option{T}"/> value.</param>
        /// <param name="orValue">Value to return if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>Result of the executed treatment, otherwise the <paramref name="orValue"/>.</returns>
        [PublicAPI]
        public static TResult IfOr<T, TResult>(in this Option<T> option, [NotNull, InstantHandle] in Func<T, TResult> then, [NotNull] in TResult orValue)
        {
            if (option.HasValue)
                return then(option.Value);
            return orValue;
        }

        /// <summary>
        /// Calls the <paramref name="else"/> function if this <see cref="Option{T}"/> has no value, otherwise returns the <paramref name="orValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <param name="else">Treatment to compute result value.</param>
        /// <param name="orValue">Value to return if this <see cref="Option{T}"/> has a value.</param>
        /// <returns>Result of the executed treatment, otherwise the <paramref name="orValue"/>.</returns>
        [PublicAPI]
        public static TResult ElseOr<T, TResult>(in this Option<T> option, [NotNull, InstantHandle] in Func<TResult> @else, [NotNull] in TResult orValue)
        {
            if (option.HasNoValue)
                return @else();
            return orValue;
        }

        /// <summary>
        /// Returns this <see cref="Option{T}"/> value if it has value, otherwise returns the <paramref name="orValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to check.</param>
        /// <param name="orValue">Value to use if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>This <see cref="Option{T}"/> value, otherwise the <paramref name="orValue"/>.</returns>
        [PublicAPI, Pure, NotNull]
        public static T Or<T>(in this Option<T> option, [NotNull] in T orValue)
        {
            return option.Unwrap(orValue);
        }

        /// <summary>
        /// Returns this <see cref="Option{T}"/> value if it has one, otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to check.</param>
        /// <param name="orFunc">Function called if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>This <see cref="Option{T}"/> value, otherwise the result of <paramref name="orFunc"/>.</returns>
        [PublicAPI, Pure]
        public static T Or<T>(in this Option<T> option, [NotNull, InstantHandle] in Func<T> orFunc)
        {
            return option.Unwrap(orFunc);
        }

        /// <summary>
        /// Returns this <see cref="Option{T}"/> value if it has one, otherwise the default value of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to check.</param>
        /// <returns>This <see cref="Option{T}"/> value, otherwise the default of <typeparamref name="T"/> type.</returns>
        [PublicAPI, Pure, CanBeNull]
        public static T OrDefault<T>(in this Option<T> option)
        {
            return option.Unwrap();
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value, otherwise throws the given exception.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing treatment.</param>
        /// <param name="then">Treatment to do with this <see cref="Option{T}"/> value.</param>
        /// <param name="exception">The exception to throw if this <see cref="Option{T}"/> has no value.</param>
        [PublicAPI]
        public static void IfOrThrows<T>(in this Option<T> option, [NotNull, InstantHandle] in Action<T> then, [NotNull] in Exception exception)
        {
            if (option.HasNoValue)
                throw exception;

            then(option.Value);
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value, otherwise throws the given exception.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing treatment.</param>
        /// <param name="then">Treatment to do with this <see cref="Option{T}"/> value.</param>
        /// <param name="exceptionFunc">The factory exception method used to throw if this <see cref="Option{T}"/> has no value.</param>
        [PublicAPI]
        public static void IfOrThrows<T>(in this Option<T> option, [NotNull, InstantHandle] in Action<T> then, [NotNull, InstantHandle] in Func<Exception> exceptionFunc)
        {
            if (option.HasNoValue)
                throw exceptionFunc();

            then(option.Value);
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value, otherwise throws the given exception.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing treatment.</param>
        /// <param name="then">Treatment to do with this <see cref="Option{T}"/> value.</param>
        /// <param name="exception">The exception to throw if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>Result of the treatment.</returns>
        [PublicAPI]
        public static TResult IfOrThrows<T, TResult>(in this Option<T> option, [NotNull, InstantHandle] in Func<T, TResult> then, [NotNull] in Exception exception)
        {
            if (option.HasValue)
                return then(option.Value);
            throw exception;
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value, otherwise throws the exception from factory method.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing treatment.</param>
        /// <param name="then">Treatment to do with this <see cref="Option{T}"/> value.</param>
        /// <param name="exceptionFunc">The factory exception method used to throw if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>Result of the treatment.</returns>
        [PublicAPI]
        public static TResult IfOrThrows<T, TResult>(in this Option<T> option, [NotNull, InstantHandle] in Func<T, TResult> then, [NotNull, InstantHandle] in Func<Exception> exceptionFunc)
        {
            if (option.HasValue)
                return then(option.Value);
            throw exceptionFunc();
        }

        /// <summary>
        /// Returns this <see cref="Option{T}"/> value if it has one, otherwise throws the given exception.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to check.</param>
        /// <param name="exception">The exception to throw if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>This <see cref="Option{T}"/> value.</returns>
        [PublicAPI, NotNull]
        public static T OrThrows<T>(in this Option<T> option, [NotNull] in Exception exception)
        {
            if (option.HasValue)
                return option.Value;
            throw exception;
        }

        /// <summary>
        /// Returns this <see cref="Option{T}"/> value if it has one, otherwise throws the given exception.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to check.</param>
        /// <param name="exceptionFunc">The factory exception method used to throw if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>This <see cref="Option{T}"/> value.</returns>
        [PublicAPI, NotNull]
        public static T OrThrows<T>(in this Option<T> option, [NotNull, InstantHandle] in Func<Exception> exceptionFunc)
        {
            if (option.HasValue)
                return option.Value;
            throw exceptionFunc();
        }

        /// <summary>
        /// Returns this <see cref="Option{T}"/> if it has a value, otherwise returns a <see cref="Option{T}"/> returned by <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to check.</param>
        /// <param name="orFunc">Function called if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>This <see cref="Option{T}"/>, otherwise the result of <paramref name="orFunc"/>.</returns>
        [PublicAPI, Pure]
        public static Option<T> Or<T>(in this Option<T> option, [NotNull, InstantHandle] in Func<Option<T>> orFunc)
        {
            if (option.HasValue)
                return option;
            return orFunc();
        }

        /// <summary>
        /// Unwraps this <see cref="Option{T}"/> value if it has one, otherwise returns the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to unwrap value.</param>
        /// <param name="defaultValue">Default value to use.</param>
        /// <returns>The unwrapped value, otherwise the default one.</returns>
        [PublicAPI, Pure]
        public static T Unwrap<T>(in this Option<T> option, [CanBeNull] in T defaultValue = default)
        {
            if (option.HasValue)
                return option.Value;
            return defaultValue;
        }

        /// <summary>
        /// Unwraps this <see cref="Option{T}"/> value if it has one, otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to unwrap value.</param>
        /// <param name="orFunc">Default value factory method.</param>
        /// <returns>The unwrapped value from this <see cref="Option{T}"/> if it has value, otherwise the result from <paramref name="orFunc"/>.</returns>
        [PublicAPI, Pure]
        public static T Unwrap<T>(in this Option<T> option, [NotNull, InstantHandle] in Func<T> orFunc)
        {
            if (option.HasValue)
                return option.Value;
            return orFunc();
        }

        /// <summary>
        /// Unwraps this <see cref="Option{T}"/> value if it has one, 
        /// uses the <paramref name="converter"/> to convert the value,
        /// otherwise returns the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TOut">Output value type.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to unwrap value.</param>
        /// <param name="converter">Function called to convert this <see cref="Option{T}"/> value.</param>
        /// <param name="defaultValue">Default value to use.</param>
        /// <returns>The converted unwrapped value from this <see cref="Option{T}"/>, otherwise the default one.</returns>
        [PublicAPI, Pure]
        public static TOut Unwrap<T, TOut>(in this Option<T> option, 
            [NotNull, InstantHandle] in Func<T, TOut> converter, 
            [CanBeNull] in TOut defaultValue = default)
        {
            if (option.HasValue)
                return converter(option.Value);
            return defaultValue;
        }

        /// <summary>
        /// Unwraps this <see cref="Option{T}"/> value if it has one, 
        /// uses the <paramref name="converter"/> to convert the value,
        /// otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TOut">Output value type.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to unwrap value.</param>
        /// <param name="converter">Function called to convert this <see cref="Option{T}"/> value.</param>
        /// <param name="orFunc">Default value factory method.</param>
        /// <returns>The converted unwrapped value from this <see cref="Option{T}"/>, otherwise the result from <paramref name="orFunc"/>.</returns>
        [PublicAPI, Pure]
        public static TOut Unwrap<T, TOut>(in this Option<T> option, 
            [NotNull, InstantHandle] in Func<T, TOut> converter, 
            [NotNull, InstantHandle] in Func<TOut> orFunc)
        {
            if (option.HasValue)
                return converter(option.Value);
            return orFunc();
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <typeparam name="TFrom">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TTo">Type of the value embedded in the converted <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to convert.</param>
        /// <param name="converter">Function called to convert this <see cref="Option{T}"/> value.</param>
        /// <returns>Converted <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Option<TTo> Cast<TFrom, TTo>(in this Option<TFrom> option, [NotNull, InstantHandle] in Func<TFrom, TTo> converter)
        {
            if (option.HasValue)
                return converter(option.Value);
            return Option<TTo>.None;
        }

        /// <summary>
        /// Checks if this <see cref="Option{T}"/> matches the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <param name="predicate">Condition to match.</param>
        /// <returns>True if this <see cref="Option{T}"/> matches the <paramref name="predicate"/>, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool Exists<T>(in this Option<T> option, [NotNull, InstantHandle] in Predicate<T> predicate)
        {
            if (option.HasValue)
                return predicate(option.Value);
            return false;
        }
    }
}

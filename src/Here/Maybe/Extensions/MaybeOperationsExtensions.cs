using System;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Maybe{T}"/> for operations.
    /// </summary>
    public static class MaybeOperationsExtensions
    {
        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Maybe{T}"/> has a value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="then">Treatment to do.</param>
        /// <returns>This <see cref="Maybe{T}"/>.</returns>
        [PublicAPI]
        public static Maybe<T> If<T>(in this Maybe<T> maybe, [NotNull, InstantHandle] in Action<T> then)
        {
            if (maybe.HasValue)
                then(maybe.Value);
            return maybe;
        }

        /// <summary>
        /// Calls the <paramref name="else"/> function if this <see cref="Maybe{T}"/> has no value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="else">Treatment to do.</param>
        /// <returns>This <see cref="Maybe{T}"/>.</returns>
        [PublicAPI]
        public static Maybe<T> Else<T>(in this Maybe<T> maybe, [NotNull, InstantHandle] in Action @else)
        {
            if (maybe.HasNoValue)
                @else();
            return maybe;
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Maybe{T}"/> has a value, otherwise calls <paramref name="else"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="then">Treatment to do with this <see cref="Maybe{T}"/> value.</param>
        /// <param name="else">Treatment to do if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>This <see cref="Maybe{T}"/>.</returns>
        [PublicAPI]
        public static Maybe<T> IfElse<T>(in this Maybe<T> maybe, [NotNull, InstantHandle] in Action<T> then, [NotNull, InstantHandle] in Action @else)
        {
            if (maybe.HasValue)
                then(maybe.Value);
            else
                @else();

            return maybe;
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Maybe{T}"/> has a value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="then">Treatment to do with this <see cref="Maybe{T}"/> value.</param>
        /// <param name="else">Treatment to do if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>Result of the executed callback.</returns>
        [PublicAPI]
        public static TResult IfElse<T, TResult>(in this Maybe<T> maybe, [NotNull, InstantHandle] in Func<T, TResult> then, [NotNull, InstantHandle] in Func<TResult> @else)
        {
            if (maybe.HasValue)
                return then(maybe.Value);
            return @else();
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Maybe{T}"/> has a value, otherwise returns the <paramref name="orValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="then">Treatment to do with this <see cref="Maybe{T}"/> value.</param>
        /// <param name="orValue">Value to return if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>Result of the executed treatment, otherwise the <paramref name="orValue"/>.</returns>
        [PublicAPI]
        public static TResult IfOr<T, TResult>(in this Maybe<T> maybe, [NotNull, InstantHandle] in Func<T, TResult> then, [NotNull] in TResult orValue)
        {
            if (maybe.HasValue)
                return then(maybe.Value);
            return orValue;
        }

        /// <summary>
        /// Calls the <paramref name="else"/> function if this <see cref="Maybe{T}"/> has no value, otherwise returns the <paramref name="orValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="else">Treatment to compute result value.</param>
        /// <param name="orValue">Value to return if this <see cref="Maybe{T}"/> has a value.</param>
        /// <returns>Result of the executed treatment, otherwise the <paramref name="orValue"/>.</returns>
        [PublicAPI]
        public static TResult ElseOr<T, TResult>(in this Maybe<T> maybe, [NotNull, InstantHandle] in Func<TResult> @else, [NotNull] in TResult orValue)
        {
            if (maybe.HasNoValue)
                return @else();
            return orValue;
        }

        /// <summary>
        /// Returns this <see cref="Maybe{T}"/> value if it has value, otherwise returns the <paramref name="orValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to check.</param>
        /// <param name="orValue">Value to use if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>This <see cref="Maybe{T}"/> value, otherwise the <paramref name="orValue"/>.</returns>
        [PublicAPI, Pure, NotNull]
        public static T Or<T>(in this Maybe<T> maybe, [NotNull] in T orValue)
        {
            return maybe.Unwrap(orValue);
        }

        /// <summary>
        /// Returns this <see cref="Maybe{T}"/> value if it has one, otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to check.</param>
        /// <param name="orFunc">Function called if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>This <see cref="Maybe{T}"/> value, otherwise the result of <paramref name="orFunc"/>.</returns>
        [PublicAPI, Pure]
        public static T Or<T>(in this Maybe<T> maybe, [NotNull, InstantHandle] in Func<T> orFunc)
        {
            return maybe.Unwrap(orFunc);
        }

        /// <summary>
        /// Returns this <see cref="Maybe{T}"/> value if it has one, otherwise the default value of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to check.</param>
        /// <returns>This <see cref="Maybe{T}"/> value, otherwise the default of <typeparamref name="T"/> type.</returns>
        [PublicAPI, Pure, CanBeNull]
        public static T OrDefault<T>(in this Maybe<T> maybe)
        {
            return maybe.Unwrap();
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Maybe{T}"/> has a value, otherwise throws the given exception.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="then">Treatment to do with this <see cref="Maybe{T}"/> value.</param>
        /// <param name="exception">The exception to throw if this <see cref="Maybe{T}"/> has no value.</param>
        [PublicAPI]
        public static void IfOrThrows<T>(in this Maybe<T> maybe, [NotNull, InstantHandle] in Action<T> then, [NotNull] in Exception exception)
        {
            if (maybe.HasNoValue)
                throw exception;

            then(maybe.Value);
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Maybe{T}"/> has a value, otherwise throws the given exception.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="then">Treatment to do with this <see cref="Maybe{T}"/> value.</param>
        /// <param name="exceptionFunc">The factory exception method used to throw if this <see cref="Maybe{T}"/> has no value.</param>
        [PublicAPI]
        public static void IfOrThrows<T>(in this Maybe<T> maybe, [NotNull, InstantHandle] in Action<T> then, [NotNull, InstantHandle] in Func<Exception> exceptionFunc)
        {
            if (maybe.HasNoValue)
                throw exceptionFunc();

            then(maybe.Value);
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Maybe{T}"/> has a value, otherwise throws the given exception.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="then">Treatment to do with this <see cref="Maybe{T}"/> value.</param>
        /// <param name="exception">The exception to throw if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>Result of the treatment.</returns>
        [PublicAPI]
        public static TResult IfOrThrows<T, TResult>(in this Maybe<T> maybe, [NotNull, InstantHandle] in Func<T, TResult> then, [NotNull] in Exception exception)
        {
            if (maybe.HasValue)
                return then(maybe.Value);
            throw exception;
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Maybe{T}"/> has a value, otherwise throws the exception from factory method.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="then">Treatment to do with this <see cref="Maybe{T}"/> value.</param>
        /// <param name="exceptionFunc">The factory exception method used to throw if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>Result of the treatment.</returns>
        [PublicAPI]
        public static TResult IfOrThrows<T, TResult>(in this Maybe<T> maybe, [NotNull, InstantHandle] in Func<T, TResult> then, [NotNull, InstantHandle] in Func<Exception> exceptionFunc)
        {
            if (maybe.HasValue)
                return then(maybe.Value);
            throw exceptionFunc();
        }

        /// <summary>
        /// Returns this <see cref="Maybe{T}"/> value if it has one, otherwise throws the given exception.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to check.</param>
        /// <param name="exception">The exception to throw if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>This <see cref="Maybe{T}"/> value.</returns>
        [PublicAPI, NotNull]
        public static T OrThrows<T>(in this Maybe<T> maybe, [NotNull] in Exception exception)
        {
            if (maybe.HasValue)
                return maybe.Value;
            throw exception;
        }

        /// <summary>
        /// Returns this <see cref="Maybe{T}"/> value if it has one, otherwise throws the given exception.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to check.</param>
        /// <param name="exceptionFunc">The factory exception method used to throw if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>This <see cref="Maybe{T}"/> value.</returns>
        [PublicAPI, NotNull]
        public static T OrThrows<T>(in this Maybe<T> maybe, [NotNull, InstantHandle] in Func<Exception> exceptionFunc)
        {
            if (maybe.HasValue)
                return maybe.Value;
            throw exceptionFunc();
        }

        /// <summary>
        /// Returns this <see cref="Maybe{T}"/> if it has a value, otherwise returns a <see cref="Maybe{T}"/> returned by <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to check.</param>
        /// <param name="orFunc">Function called if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>This <see cref="Maybe{T}"/>, otherwise the result of <paramref name="orFunc"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> Or<T>(in this Maybe<T> maybe, [NotNull, InstantHandle] in Func<Maybe<T>> orFunc)
        {
            if (maybe.HasValue)
                return maybe;
            return orFunc();
        }

        /// <summary>
        /// Unwraps this <see cref="Maybe{T}"/> value if it has one, otherwise returns the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to unwrap value.</param>
        /// <param name="defaultValue">Default value to use.</param>
        /// <returns>The unwrapped value, otherwise the default one.</returns>
        [PublicAPI, Pure]
        public static T Unwrap<T>(in this Maybe<T> maybe, [CanBeNull] in T defaultValue = default)
        {
            if (maybe.HasValue)
                return maybe.Value;
            return defaultValue;
        }

        /// <summary>
        /// Unwraps this <see cref="Maybe{T}"/> value if it has one, otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to unwrap value.</param>
        /// <param name="orFunc">Default value factory method.</param>
        /// <returns>The unwrapped value from this <see cref="Maybe{T}"/> if it has value, otherwise the result from <paramref name="orFunc"/>.</returns>
        [PublicAPI, Pure]
        public static T Unwrap<T>(in this Maybe<T> maybe, [NotNull, InstantHandle] in Func<T> orFunc)
        {
            if (maybe.HasValue)
                return maybe.Value;
            return orFunc();
        }

        /// <summary>
        /// Unwraps this <see cref="Maybe{T}"/> value if it has one, 
        /// uses the <paramref name="converter"/> to convert the value,
        /// otherwise returns the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TOut">Output value type.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to unwrap value.</param>
        /// <param name="converter">Function called to convert this <see cref="Maybe{T}"/> value.</param>
        /// <param name="defaultValue">Default value to use.</param>
        /// <returns>The converted unwrapped value from this <see cref="Maybe{T}"/>, otherwise the default one.</returns>
        [PublicAPI, Pure]
        public static TOut Unwrap<T, TOut>(in this Maybe<T> maybe, 
            [NotNull, InstantHandle] in Func<T, TOut> converter, 
            [CanBeNull] in TOut defaultValue = default)
        {
            if (maybe.HasValue)
                return converter(maybe.Value);
            return defaultValue;
        }

        /// <summary>
        /// Unwraps this <see cref="Maybe{T}"/> value if it has one, 
        /// uses the <paramref name="converter"/> to convert the value,
        /// otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TOut">Output value type.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to unwrap value.</param>
        /// <param name="converter">Function called to convert this <see cref="Maybe{T}"/> value.</param>
        /// <param name="orFunc">Default value factory method.</param>
        /// <returns>The converted unwrapped value from this <see cref="Maybe{T}"/>, otherwise the result from <paramref name="orFunc"/>.</returns>
        [PublicAPI, Pure]
        public static TOut Unwrap<T, TOut>(in this Maybe<T> maybe, 
            [NotNull, InstantHandle] in Func<T, TOut> converter, 
            [NotNull, InstantHandle] in Func<TOut> orFunc)
        {
            if (maybe.HasValue)
                return converter(maybe.Value);
            return orFunc();
        }

        /// <summary>
        /// Converts this <see cref="Maybe{TFrom}"/> to a <see cref="Maybe{TTo}"/>.
        /// </summary>
        /// <typeparam name="TFrom">Type of the value embedded in this <see cref="Maybe{TFrom}"/>.</typeparam>
        /// <typeparam name="TTo">Type of the value embedded in the converted <see cref="Maybe{TTo}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{TFrom}"/> to convert.</param>
        /// <param name="converter">Function called to convert this <see cref="Maybe{TFrom}"/> value.</param>
        /// <returns>Converted <see cref="Maybe{TTo}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<TTo> Cast<TFrom, TTo>(in this Maybe<TFrom> maybe, [NotNull, InstantHandle] in Func<TFrom, TTo> converter)
        {
            if (maybe.HasValue)
                return converter(maybe.Value);
            return Maybe<TTo>.None;
        }

        /// <summary>
        /// Checks if this <see cref="Maybe{T}"/> matches the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="predicate">Condition to match.</param>
        /// <returns>True if this <see cref="Maybe{T}"/> matches the <paramref name="predicate"/>, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool Exists<T>(in this Maybe<T> maybe, [NotNull, InstantHandle] in Predicate<T> predicate)
        {
            if (maybe.HasValue)
                return predicate(maybe.Value);
            return false;
        }
    }
}

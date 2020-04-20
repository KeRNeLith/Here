using System;
#if SUPPORTS_AGGRESSIVE_INLINING
using System.Runtime.CompilerServices;
#endif
using JetBrains.Annotations;

namespace Here
{
    // Operations
    public partial struct Option<T>
    {
        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value.
        /// </summary>
        /// <param name="then">Treatment to do.</param>
        /// <returns>This <see cref="Option{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="then"/> is null.</exception>
        [PublicAPI]
        public Option<T> If([NotNull, InstantHandle] in Action<T> then)
        {
            Throw.IfArgumentNull(then, nameof(then));

            if (HasValue)
                then(_value);
            return this;
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value.
        /// </summary>
        /// <typeparam name="TResult">Type of the output value.</typeparam>
        /// <param name="then">Treatment to do.</param>
        /// <returns>Output value of <paramref name="then"/> function, or the default value of <typeparamref name="TResult"/>.</returns>
        [PublicAPI, Pure, CanBeNull]
        public TResult If<TResult>([NotNull, InstantHandle] in Func<T, TResult> then)
        {
            Throw.IfArgumentNull(then, nameof(then));

            if (HasValue)
                return then(_value);
            return default;
        }

        /// <summary>
        /// Calls the <paramref name="else"/> function if this <see cref="Option{T}"/> has no value.
        /// </summary>
        /// <param name="else">Treatment to do.</param>
        /// <returns>This <see cref="Option{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="else"/> is null.</exception>
        [PublicAPI]
        public Option<T> Else([NotNull, InstantHandle] in Action @else)
        {
            Throw.IfArgumentNull(@else, nameof(@else));

            if (HasNoValue)
                @else();
            return this;
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value, otherwise calls <paramref name="else"/>.
        /// </summary>
        /// <param name="then">Treatment to do with this <see cref="Option{T}"/> value.</param>
        /// <param name="else">Treatment to do if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>This <see cref="Option{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="then"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="else"/> is null.</exception>
        [PublicAPI]
        public Option<T> IfElse([NotNull, InstantHandle] in Action<T> then, [NotNull, InstantHandle] in Action @else)
        {
            Throw.IfArgumentNull(then, nameof(then));
            Throw.IfArgumentNull(@else, nameof(@else));

            if (HasValue)
                then(_value);
            else
                @else();

            return this;
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value.
        /// </summary>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="then">Treatment to do with this <see cref="Option{T}"/> value.</param>
        /// <param name="else">Treatment to do if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>Result of the executed callback.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="then"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="else"/> is null.</exception>
        [PublicAPI, Pure]
        public TResult IfElse<TResult>([NotNull, InstantHandle] in Func<T, TResult> then, [NotNull, InstantHandle] in Func<TResult> @else)
        {
            Throw.IfArgumentNull(then, nameof(then));
            Throw.IfArgumentNull(@else, nameof(@else));

            if (HasValue)
                return then(_value);
            return @else();
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value, otherwise returns the <paramref name="orValue"/>.
        /// </summary>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="then">Treatment to do with this <see cref="Option{T}"/> value.</param>
        /// <param name="orValue">Value to return if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>Result of the executed treatment, otherwise the <paramref name="orValue"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="then"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="orValue"/> is null.</exception>
        /// <exception cref="NullResultException">If this <see cref="Option{T}"/> has a value and <paramref name="then"/> returns null.</exception>
        [PublicAPI, Pure, NotNull]
        public TResult IfOr<TResult>([NotNull, InstantHandle] in Func<T, TResult> then, [NotNull] in TResult orValue)
        {
            Throw.IfArgumentNull(then, nameof(then));
            Throw.IfArgumentNull(orValue, nameof(orValue));

            if (HasValue)
                return Throw.IfResultNull(then(_value));
            return orValue;
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value, otherwise returns the default value of <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="then">Treatment to do with this <see cref="Option{T}"/> value.</param>
        /// <returns>Result of the executed treatment, otherwise the default one.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="then"/> is null.</exception>
        [PublicAPI, Pure, CanBeNull]
        public TResult IfOrDefault<TResult>([NotNull, InstantHandle] in Func<T, TResult> then)
        {
            return Unwrap(then);
        }

        /// <summary>
        /// Calls the <paramref name="else"/> function if this <see cref="Option{T}"/> has no value, otherwise returns the <paramref name="orValue"/>.
        /// </summary>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="else">Treatment to compute result value.</param>
        /// <param name="orValue">Value to return if this <see cref="Option{T}"/> has a value.</param>
        /// <returns>Result of the executed treatment, otherwise the <paramref name="orValue"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="else"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="orValue"/> is null.</exception>
        /// <exception cref="NullResultException">If this <see cref="Option{T}"/> has no value and <paramref name="else"/> returns null.</exception>
        [PublicAPI, Pure, NotNull]
        public TResult ElseOr<TResult>([NotNull, InstantHandle] in Func<TResult> @else, [NotNull] in TResult orValue)
        {
            Throw.IfArgumentNull(@else, nameof(@else));
            Throw.IfArgumentNull(orValue, nameof(orValue));

            if (HasNoValue)
                return Throw.IfResultNull(@else());
            return orValue;
        }

        /// <summary>
        /// Calls the <paramref name="else"/> function if this <see cref="Option{T}"/> has no value, otherwise returns the default value of <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="else">Treatment to compute result value.</param>
        /// <returns>Result of the executed treatment, otherwise the default one.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="else"/> is null.</exception>
        [PublicAPI, Pure, CanBeNull]
        public TResult ElseOrDefault<TResult>([NotNull, InstantHandle] in Func<TResult> @else)
        {
            Throw.IfArgumentNull(@else, nameof(@else));

            if (HasNoValue)
                return @else();
            return default;
        }

        /// <summary>
        /// Returns this <see cref="Option{T}"/> value if it has value, otherwise returns the <paramref name="orValue"/>.
        /// </summary>
        /// <param name="orValue">Value to use if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>This <see cref="Option{T}"/> value, otherwise the <paramref name="orValue"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="orValue"/> is null.</exception>
        [PublicAPI, Pure, NotNull]
        public T Or([NotNull] in T orValue)
        {
            Throw.IfArgumentNull(orValue, nameof(orValue));

            if (HasValue)
                return _value;
            return orValue;
        }

        /// <summary>
        /// Returns this <see cref="Option{T}"/> value if it has one, otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <param name="orFunc">Function called if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>This <see cref="Option{T}"/> value, otherwise the result of <paramref name="orFunc"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="orFunc"/> is null.</exception>
        /// <exception cref="NullResultException">If the <see cref="Option{T}"/> has no value and <paramref name="orFunc"/> returns null.</exception>
        [PublicAPI, Pure, NotNull]
        public T Or([NotNull, InstantHandle] in Func<T> orFunc)
        {
            Throw.IfArgumentNull(orFunc, nameof(orFunc));

            if (HasValue)
                return _value;

            T orValue = orFunc();
            return Throw.IfResultNull(orValue);
        }

        /// <summary>
        /// Returns this <see cref="Option{T}"/> if it has a value, otherwise returns a <see cref="Option{T}"/> returned by <paramref name="orFunc"/>.
        /// </summary>
        /// <param name="orFunc">Function called if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>This <see cref="Option{T}"/>, otherwise the result of <paramref name="orFunc"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="orFunc"/> is null.</exception>
        [PublicAPI, Pure]
        public Option<T> Or([NotNull, InstantHandle] in Func<Option<T>> orFunc)
        {
            Throw.IfArgumentNull(orFunc, nameof(orFunc));

            if (HasValue)
                return this;
            return orFunc();
        }

        /// <summary>
        /// Returns this <see cref="Option{T}"/> value if it has one, otherwise the default value of <typeparamref name="T"/>.
        /// </summary>
        /// <returns>This <see cref="Option{T}"/> value, otherwise the default of <typeparamref name="T"/> type.</returns>
        [PublicAPI, Pure, CanBeNull]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public T OrDefault()
        {
            return Unwrap();
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value, otherwise throws the given exception.
        /// </summary>
        /// <param name="then">Treatment to do with this <see cref="Option{T}"/> value.</param>
        /// <param name="exception">The exception to throw if this <see cref="Option{T}"/> has no value.</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="then"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="exception"/> is null.</exception>
        [PublicAPI]
        public void IfOrThrows([NotNull, InstantHandle] in Action<T> then, [NotNull] in Exception exception)
        {
            Throw.IfArgumentNull(then, nameof(then));
            Throw.IfArgumentNull(exception, nameof(exception));

            if (HasNoValue)
                throw exception;

            then(_value);
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value, otherwise throws the given exception.
        /// </summary>
        /// <param name="then">Treatment to do with this <see cref="Option{T}"/> value.</param>
        /// <param name="exceptionFunc">The factory exception method used to throw if this <see cref="Option{T}"/> has no value.</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="then"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="exceptionFunc"/> is null.</exception>
        [PublicAPI]
        public void IfOrThrows([NotNull, InstantHandle] in Action<T> then, [NotNull, InstantHandle] in Func<Exception> exceptionFunc)
        {
            Throw.IfArgumentNull(then, nameof(then));
            Throw.IfArgumentNull(exceptionFunc, nameof(exceptionFunc));

            if (HasNoValue)
                throw exceptionFunc();

            then(_value);
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value, otherwise throws the given exception.
        /// </summary>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="then">Treatment to do with this <see cref="Option{T}"/> value.</param>
        /// <param name="exception">The exception to throw if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>Result of the treatment.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="then"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="exception"/> is null.</exception>
        [PublicAPI]
        public TResult IfOrThrows<TResult>([NotNull, InstantHandle] in Func<T, TResult> then, [NotNull] in Exception exception)
        {
            Throw.IfArgumentNull(then, nameof(then));
            Throw.IfArgumentNull(exception, nameof(exception));

            if (HasValue)
                return then(_value);
            throw exception;
        }

        /// <summary>
        /// Calls the <paramref name="then"/> function if this <see cref="Option{T}"/> has a value, otherwise throws the exception from factory method.
        /// </summary>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="then">Treatment to do with this <see cref="Option{T}"/> value.</param>
        /// <param name="exceptionFunc">The factory exception method used to throw if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>Result of the treatment.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="then"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="exceptionFunc"/> is null.</exception>
        [PublicAPI]
        public TResult IfOrThrows<TResult>([NotNull, InstantHandle] in Func<T, TResult> then, [NotNull, InstantHandle] in Func<Exception> exceptionFunc)
        {
            Throw.IfArgumentNull(then, nameof(then));
            Throw.IfArgumentNull(exceptionFunc, nameof(exceptionFunc));

            if (HasValue)
                return then(_value);
            throw exceptionFunc();
        }

        /// <summary>
        /// Returns this <see cref="Option{T}"/> value if it has one, otherwise throws the given exception.
        /// </summary>
        /// <param name="exception">The exception to throw if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>This <see cref="Option{T}"/> value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="exception"/> is null.</exception>
        [PublicAPI, NotNull]
        public T OrThrows([NotNull] in Exception exception)
        {
            Throw.IfArgumentNull(exception, nameof(exception));

            if (HasValue)
                return _value;
            throw exception;
        }

        /// <summary>
        /// Returns this <see cref="Option{T}"/> value if it has one, otherwise throws the given exception.
        /// </summary>
        /// <param name="exceptionFunc">The factory exception method used to throw if this <see cref="Option{T}"/> has no value.</param>
        /// <returns>This <see cref="Option{T}"/> value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="exceptionFunc"/> is null.</exception>
        [PublicAPI, NotNull]
        public T OrThrows([NotNull, InstantHandle] in Func<Exception> exceptionFunc)
        {
            Throw.IfArgumentNull(exceptionFunc, nameof(exceptionFunc));

            if (HasValue)
                return _value;
            throw exceptionFunc();
        }

        /// <summary>
        /// Unwraps this <see cref="Option{T}"/> value if it has one, otherwise returns the <paramref name="defaultValue"/>.
        /// </summary>
        /// <param name="defaultValue">Default value to use.</param>
        /// <returns>The unwrapped value, otherwise the default one.</returns>
        [PublicAPI, Pure, CanBeNull]
        public T Unwrap([CanBeNull] in T defaultValue = default)
        {
            if (HasValue)
                return _value;
            return defaultValue;
        }

        /// <summary>
        /// Unwraps this <see cref="Option{T}"/> value if it has one, otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <param name="orFunc">Default value factory method.</param>
        /// <returns>The unwrapped value from this <see cref="Option{T}"/> if it has value, otherwise the result from <paramref name="orFunc"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="orFunc"/> is null.</exception>
        [PublicAPI, Pure, CanBeNull]
        public T Unwrap([NotNull, InstantHandle] in Func<T> orFunc)
        {
            Throw.IfArgumentNull(orFunc, nameof(orFunc));

            if (HasValue)
                return _value;
            return orFunc();
        }

        /// <summary>
        /// Unwraps this <see cref="Option{T}"/> value if it has one, 
        /// uses the <paramref name="converter"/> to convert the value,
        /// otherwise returns the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="TOut">Output value type.</typeparam>
        /// <param name="converter">Function called to convert this <see cref="Option{T}"/> value.</param>
        /// <param name="defaultValue">Default value to use.</param>
        /// <returns>The converted unwrapped value from this <see cref="Option{T}"/>, otherwise the default one.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="converter"/> is null.</exception>
        [PublicAPI, Pure, CanBeNull]
        public TOut Unwrap<TOut>(
            [NotNull, InstantHandle] in Func<T, TOut> converter, 
            [CanBeNull] in TOut defaultValue = default)
        {
            Throw.IfArgumentNull(converter, nameof(converter));

            if (HasValue)
                return converter(_value);
            return defaultValue;
        }

        /// <summary>
        /// Unwraps this <see cref="Option{T}"/> value if it has one, 
        /// uses the <paramref name="converter"/> to convert the value,
        /// otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="TOut">Output value type.</typeparam>
        /// <param name="converter">Function called to convert this <see cref="Option{T}"/> value.</param>
        /// <param name="orFunc">Default value factory method.</param>
        /// <returns>The converted unwrapped value from this <see cref="Option{T}"/>, otherwise the result from <paramref name="orFunc"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="converter"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="orFunc"/> is null.</exception>
        [PublicAPI, Pure, CanBeNull]
        public TOut Unwrap<TOut>(
            [NotNull, InstantHandle] in Func<T, TOut> converter, 
            [NotNull, InstantHandle] in Func<TOut> orFunc)
        {
            Throw.IfArgumentNull(converter, nameof(converter));
            Throw.IfArgumentNull(orFunc, nameof(orFunc));

            if (HasValue)
                return converter(_value);
            return orFunc();
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <typeparam name="TTo">Type of the value embedded in the converted <see cref="Option{T}"/>.</typeparam>
        /// <param name="converter">Function called to convert this <see cref="Option{T}"/> value.</param>
        /// <returns>Converted <see cref="Option{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="converter"/> is null.</exception>
        [PublicAPI, Pure]
        public Option<TTo> Cast<TTo>([NotNull, InstantHandle] in Func<T, TTo> converter)
        {
            Throw.IfArgumentNull(converter, nameof(converter));

            if (HasValue)
                return converter(_value);
            return Option<TTo>.None;
        }

        /// <summary>
        /// Checks if this <see cref="Option{T}"/> matches the <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">Condition to match.</param>
        /// <returns>True if this <see cref="Option{T}"/> matches the <paramref name="predicate"/>, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
        public bool Exists([NotNull, InstantHandle] in Predicate<T> predicate)
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));

            if (HasValue)
                return predicate(_value);
            return false;
        }

        /// <summary>
        /// Checks if this <see cref="Option{T}"/> has a value and matches the <paramref name="predicate"/>,
        /// if yes returns <see cref="None"/>, otherwise this <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="predicate">Condition to match.</param>
        /// <returns>This <see cref="Option{T}"/> if it has a value and doesn't match the <paramref name="predicate"/>, otherwise <see cref="None"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
        public Option<T> NoneIf([NotNull, InstantHandle] in Predicate<T> predicate)
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));

            if (HasValue && predicate(_value))
                return None; 
            return this;
        }

        /// <summary>
        /// Checks if this <see cref="Option{T}"/> has a value and matches the <paramref name="predicate"/>,
        /// if yes returns <see cref="Option{TOut}.None"/>, otherwise an <see cref="Option{TOut}"/> initialized
        /// with <paramref name="converter"/> based on this <see cref="Option{T}"/> value.
        /// </summary>
        /// <typeparam name="TOut">Type of the value embedded in the output <see cref="Option{TOut}"/>.</typeparam>
        /// <param name="predicate">Condition to match.</param>
        /// <param name="converter">Function called to convert this <see cref="Option{T}"/> value to <typeparamref name="TOut"/>.</param>
        /// <returns>An <see cref="Option{TOut}"/> if it has value and doesn't match the <paramref name="predicate"/>, otherwise <see cref="Option{TOut}.None"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="converter"/> is null.</exception>
        [PublicAPI, Pure]
        public Option<TOut> NoneIf<TOut>([NotNull, InstantHandle] in Predicate<T> predicate, [NotNull, InstantHandle] in Func<T, TOut> converter)
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));
            Throw.IfArgumentNull(converter, nameof(converter));

            if (HasValue && !predicate(_value))
                return converter(_value);

            return Option<TOut>.None;
        }
    }
}

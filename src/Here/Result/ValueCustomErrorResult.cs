using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// <see cref="Result{T, TError}"/> is an object that represents the result/state of a treatment.
    /// This <see cref="Result{T, TError}"/> embed a <see cref="Value"/> resulting of the treatment
    /// or a custom error object in case of failed.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay("{" + nameof(IsSuccess) + " ? \"IsSuccess\" + (" + nameof(IsWarning) + " ? \" with warning\" : System.String.Empty) + \", Value = \" + " + nameof(_value) + " : \"IsFailure\"}")]
    public readonly partial struct Result<T, TError> : IResult<T>, IResultError<TError>, IEquatable<T>, IEquatable<Result<T, TError>>, IComparable, IComparable<Result<T, TError>>
    {
        /// <inheritdoc />
        public bool IsSuccess => Logic.IsSuccess;

        /// <inheritdoc />
        public bool IsWarning => Logic.IsWarning;

        /// <inheritdoc />
        public bool IsFailure => Logic.IsFailure;

        /// <inheritdoc />
        public string Message => Logic.Message;

        /// <inheritdoc />
        public Exception Exception => Logic.Exception;

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">If the result is not a failure.</exception>
        public TError Error => Logic.Error;

        [CanBeNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        // ReSharper disable once InconsistentNaming
        internal readonly T _value;

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">If the result is not a success.</exception>
        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException("Cannot get the value of a failed Result.");

                return _value;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly ResultLogic<TError> Logic;

        /// <summary>
        /// <see cref="Result{T, TError}"/> "Ok" constructor.
        /// </summary>
        /// <param name="value">Result value.</param>
        internal Result([CanBeNull] in T value)
        {
            Logic = new ResultLogic<TError>();
            _value = value;
        }

        /// <summary>
        /// <see cref="Result{T, TError}"/> "warning" constructor.
        /// </summary>
        /// <param name="value">Embedded value.</param>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embedded exception.</param>
        internal Result([CanBeNull] in T value, [NotNull] in string message, [CanBeNull] in Exception exception)
        {
            Logic = new ResultLogic<TError>(true, message, default, exception);
            _value = value;
        }

        /// <summary>
        /// <see cref="Result{T, TError}"/> "failure" constructor.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <param name="error">Result error object.</param>
        /// <param name="exception">Result embedded exception.</param>
        internal Result([NotNull] in string message, [NotNull] in TError error, [CanBeNull] in Exception exception)
        {
            Logic = new ResultLogic<TError>(false, message, error, exception);
            _value = default;
        }

        /// <summary>
        /// <see cref="Result{T, TError}"/> constructor.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <param name="logic">Result logic.</param>
        internal Result([CanBeNull] in T value, in ResultLogic<TError> logic)
        {
            Logic = logic;
            _value = value;
        }

        #region Cast

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a <see cref="Result{TOut, TError}"/> via the "as" operator.
        /// </summary>
        /// <typeparam name="TOut">Type of the output result value.</typeparam>
        /// <returns>A <see cref="Result{TOut, TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<TOut, TError> Cast<TOut>()
            where TOut : class 
        {
            if (IsFailure)
                return ToFailValueCustomResult<TOut>();
            if (IsWarning)
                return Result.Warn<TOut, TError>(_value as TOut, Logic.Message, Logic.Exception);
            return Result.Ok<TOut, TError>(_value as TOut);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a <see cref="Result{TOut, TError}"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the output result value.</typeparam>
        /// <param name="converter">Function that convert this result value from input type to output type.</param>
        /// <returns>A <see cref="Result{TOut, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="converter"/> is null.</exception>
        [PublicAPI, Pure]
        public Result<TOut, TError> Cast<TOut>([NotNull, InstantHandle] in Func<T, TOut> converter)
        {
            Throw.IfArgumentNull(converter, nameof(converter));

            if (IsFailure)
                return ToFailValueCustomResult<TOut>();
            if (IsWarning)
                return Result.Warn<TOut, TError>(converter(_value), Logic.Message, Logic.Exception);
            return Result.Ok<TOut, TError>(converter(_value));
        }

        #endregion

        #region Internal helpers

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a failure <see cref="Result"/>.
        /// This <see cref="Result{T, TError}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result"/>.</returns>
        [Pure]
        internal Result ToFailResult()
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result<T, TError> to a failure Result.");
            return Result.Fail(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a failure <see cref="Result{TOut}"/>.
        /// This <see cref="Result{T, TError}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{TOut}"/>.</returns>
        [Pure]
        internal Result<TOut> ToFailValueResult<TOut>()
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result<TIn, TError> to a failure Result<TOut>.");
            return Result.Fail<TOut>(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a failure <see cref="CustomResult{TError}"/>.
        /// This <see cref="Result{T, TError}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        internal CustomResult<TError> ToFailCustomResult()
        {
            Debug.Assert(Logic.IsFailure, "Cannot convert a success Result<T, TError> to a failure CustomResult<TError>.");
            return this;
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a failure <see cref="CustomResult{TError}"/>.
        /// This <see cref="Result{T, TError}"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Custom error object to use.</param>
        /// <returns>A failed <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        internal CustomResult<TError> ToFailCustomResult([NotNull] in TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result<T, TError> to a failure CustomResult<TError>.");
            return Result.CustomFail(Logic.Message, errorObject, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a failure <see cref="Result{TOut, TError}"/>.
        /// This <see cref="Result{T, TError}"/> should be a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{TOut, TError}"/>.</returns>
        [Pure]
        internal Result<TOut, TError> ToFailValueCustomResult<TOut>()
        {
            Debug.Assert(Logic.IsFailure, "Cannot convert a success Result<TIn, TError> to a failure Result<TOut, TError>.");
            return Result.Fail<TOut, TError>(Logic.Message, Logic.Error, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a failure <see cref="Result{TOut, TError}"/>.
        /// This <see cref="Result{T, TError}"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Custom error object to use.</param>
        /// <returns>A failed <see cref="Result{TOut, TError}"/>.</returns>
        [Pure]
        internal Result<TOut, TError> ToFailValueCustomResult<TOut>([NotNull] in TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result<TIn, TError> to a failure Result<TOut, TError>.");
            return Result.Fail<TOut, TError>(Logic.Message, errorObject, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a failure <see cref="Result{TOut, TError}"/>.
        /// This <see cref="Result{T, TError}"/> should be a failure.
        /// </summary>
        /// <param name="additionalMessage">Message to add as suffix of this <see cref="Result{TOut, TError}"/> message.</param>
        /// <param name="exception">Exception to set in the failure <see cref="Result{TOut, TError}"/>.</param>
        /// <returns>A failed <see cref="Result{TOut, TError}"/>.</returns>
        [Pure]
        internal Result<TOut, TError> ToFailValueCustomResult<TOut>([NotNull] in string additionalMessage, [CanBeNull] in Exception exception = null)
        {
            Debug.Assert(Logic.IsFailure, "Cannot convert a success Result<TIn, TError> to a failure Result<TOut, TError>.");
            return Result.Fail<TOut, TError>(Logic.Message + additionalMessage, Logic.Error, exception);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a warning <see cref="Result{T, TError}"/>.
        /// This <see cref="Result{T, TError}"/> should be a Ok or warning.
        /// </summary>
        /// <param name="message">Message to set in the warning <see cref="Result{T, TError}"/>.</param>
        /// <param name="exception">Exception to set in the warning <see cref="Result{T, TError}"/>.</param>
        /// <returns>A warning <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        internal Result<T, TError> ToWarnValueCustomResult([NotNull] in string message, [CanBeNull] in Exception exception = null)
        {
            Debug.Assert(ResultLogic.IsConvertibleToWarning(Logic), "Cannot convert a warning Result<T, TError> to a warning Result<T, TError>.");
            return Result.Warn<T, TError>(_value, message, exception);
        }

        #endregion

        #region Equality / IEquatable<T>

        /// <summary>
        /// Checks that this <see cref="Result{T, TError}"/> is equals to the given one and that they are successful.
        /// </summary>
        /// <param name="other"><see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>True if both <see cref="Result{T, TError}"/> are equal and successful, otherwise false.</returns>
        [PublicAPI, Pure]
        public bool SuccessEquals(in Result<T, TError> other)
        {
            return AreSuccessEqual(this, other);
        }

        /// <inheritdoc />
        public bool Equals(T other)
        {
            return AreEqual(this, other);
        }

        /// <inheritdoc />
        public bool Equals(Result<T, TError> other)
        {
            return AreEqual(this, other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj is Result<T, TError> result)
                return AreEqual(this, result);
            return obj is T value && AreEqual(this, value);
        }

        /// <summary>
        /// Indicates whether <see cref="Result{T, TError}"/> value is equals to the given value.
        /// </summary>
        /// <param name="result"><see cref="Result{T, TError}"/> that may embed a value to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if the <see cref="Result{T, TError}"/> value is equals to the given value, otherwise false.</returns>
        [PublicAPI, Pure]
        internal static bool AreEqual(in Result<T, TError> result, [CanBeNull] in T value, [CanBeNull] in IEqualityComparer<T> equalityComparer = null)
        {
            if (result.IsSuccess)
                return (equalityComparer ?? EqualityComparer<T>.Default).Equals(result._value, value);
            return false;
        }

        /// <summary>
        /// Indicates whether both <see cref="Result{T, TError}"/> are equal.
        /// </summary>
        /// <param name="result1">First <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if both <see cref="Result{T, TError}"/> are equal, otherwise false.</returns>
        [Pure]
        internal static bool AreEqual(in Result<T, TError> result1, in Result<T, TError> result2, [CanBeNull] in IEqualityComparer<T> equalityComparer = null)
        {
            return result1.Logic.Equals(result2.Logic)
                   && (equalityComparer ?? EqualityComparer<T>.Default).Equals(result1._value, result2._value);
        }

        /// <summary>
        /// Indicates whether both <see cref="Result{T, TError}"/> are equal.
        /// </summary>
        /// <param name="result1">First <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if both <see cref="Result{T, TError}"/> are equal, otherwise false.</returns>
        [Pure]
        internal static bool AreSuccessEqual(in Result<T, TError> result1, in Result<T, TError> result2, [CanBeNull] in IEqualityComparer<T> equalityComparer = null)
        {
            if (result1.IsSuccess && result2.IsSuccess)
                return AreEqual(result1, result2);
            return false;
        }

        /// <summary>
        /// Indicates whether both <see cref="Result{T, TError}"/> are equal.
        /// </summary>
        /// <param name="result1">First <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>True if both <see cref="Result{T, TError}"/> are equal, otherwise false.</returns>
        public static bool operator ==(in Result<T, TError> result1, in Result<T, TError> result2)
        {
            return AreEqual(result1, result2);
        }

        /// <summary>
        /// Indicates whether both <see cref="Result{T, TError}"/> are not equal.
        /// </summary>
        /// <param name="result1">First <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>True if both <see cref="Result{T, TError}"/> are not equal, otherwise false.</returns>
        public static bool operator !=(in Result<T, TError> result1, in Result<T, TError> result2)
        {
            return !(result1 == result2);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (EqualityComparer<T>.Default.GetHashCode(_value) * 397) ^ Logic.GetHashCode();
        }

        /// <summary>
        /// Indicates whether <see cref="Result{T, TError}"/> value is equals to the given value.
        /// </summary>
        /// <param name="result"><see cref="Result{T, TError}"/> that may embed a value to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <returns>True if the <see cref="Result{T, TError}"/> value is equals to the given value, otherwise false.</returns>
        public static bool operator ==(in Result<T, TError> result, in T value)
        {
            return AreEqual(result, value);
        }

        /// <summary>
        /// Indicates whether <see cref="Result{T, TError}"/> value is not equals to the given value.
        /// </summary>
        /// <param name="result"><see cref="Result{T, TError}"/> that may embed a value to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <returns>True if the <see cref="Result{T, TError}"/> value is not equals to the given value, otherwise false.</returns>
        public static bool operator !=(in Result<T, TError> result, in T value)
        {
            return !(result == value);
        }

        /// <summary>
        /// Indicates whether <see cref="Result{T, TError}"/> value is equals to the given value.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="result"><see cref="Result{T, TError}"/> that may embed a value to compare.</param>
        /// <returns>True if the <see cref="Result{T, TError}"/> value is equals to the given value, otherwise false.</returns>
        public static bool operator ==(in T value, in Result<T, TError> result)
        {
            return result == value;
        }

        /// <summary>
        /// Indicates whether <see cref="Result{T, TError}"/> value is not equals to the given value.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="result"><see cref="Result{T, TError}"/> that may embed a value to compare.</param>
        /// <returns>True if the <see cref="Result{T, TError}"/> value is not equals to the given value, otherwise false.</returns>
        public static bool operator !=(in T value, in Result<T, TError> result)
        {
            return !(result == value);
        }

        #endregion

        #region IComparable / IComparable<T>

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj is null)
                return 1;
            if (obj is Result<T, TError> other)
                return CompareTo(other);

            throw new ArgumentException($"Cannot compare an object of type {obj.GetType()} with a {typeof(Result<T, TError>)}.");
        }

        /// <inheritdoc />
        /// <summary>
        /// Compare this <see cref="Result{T, TError}"/> with the given one.
        /// Order keeps failures first, then warnings and finally successes.
        /// </summary>
        public int CompareTo(Result<T, TError> other)
        {
            int logicCompare = Logic.CompareTo(other.Logic);
            if (logicCompare == 0)
            {
                if (_value is IComparable comparable)
                    return comparable.CompareTo(other._value);
                else if (_value is IComparable<T> comparableT)
                    return comparableT.CompareTo(other._value);
            }

            return logicCompare;
        }

        /// <summary>
        /// Determines if this <see cref="Result{T, TError}"/> is "less" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(in Result<T, TError> left, in Result<T, TError> right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="Result{T, TError}"/> is "less" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(in Result<T, TError> left, in Result<T, TError> right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="Result{T, TError}"/> is "greater" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(in Result<T, TError> left, in Result<T, TError> right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="Result{T, TError}"/> is "greater" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(in Result<T, TError> left, in Result<T, TError> right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return Logic.ToString();
        }
    }
}
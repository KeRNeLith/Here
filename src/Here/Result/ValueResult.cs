using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Here.Results
{
    /// <summary>
    /// <see cref="Result{T}"/> is an object that represents the result/state of a treatment.
    /// This <see cref="Result{T}"/> embed a <see cref="Value"/> resulting of the treatment.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay("{" + nameof(IsSuccess) + " ? \"IsSuccess\" + (" + nameof(IsWarning) + " ? \" with warning\" : System.String.Empty) + \", Value = \" + " + nameof(Value) + " : \"IsFailure\"}")]
    public partial struct Result<T> : IResult<T>, IEquatable<Result<T>>, IComparable, IComparable<Result<T>>
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

        [CanBeNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly T _value;

        /// <inheritdoc />
        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException("Cannot get the value of a failed Result.");

                return _value;
            }
        }

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly ResultLogic Logic;

        /// <summary>
        /// <see cref="Result{T}"/> "Ok" constructor.
        /// </summary>
        /// <param name="value">Result value.</param>
        internal Result([CanBeNull] in T value)
        {
            Logic = new ResultLogic();
            _value = value;
        }

        /// <summary>
        /// <see cref="Result{T}"/> "warning"/"error" constructor.
        /// </summary>
        /// <param name="isWarning">Result warning flag.</param>
        /// <param name="value">Embedded value.</param>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embedded exception.</param>
        internal Result(bool isWarning, [CanBeNull] in T value, [NotNull] in string message, [CanBeNull] in Exception exception)
        {
            Logic = new ResultLogic(isWarning, message, exception);
            _value = value;
        }

        /// <summary>
        /// <see cref="Result{T}"/> constructor.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <param name="logic">Result logic.</param>
        internal Result([CanBeNull] in T value, [NotNull] in ResultLogic logic)
        {
            Logic = logic;
            _value = value;
        }

        #region Cast

        /// <summary>
        /// Converts this <see cref="Result{T}"/> to a <see cref="Result{TOut}"/>
        /// </summary>
        /// <typeparam name="TOut">Type of the output result value.</typeparam>
        /// <param name="converter">Function that converts this result value from input type to output type.</param>
        /// <returns>A <see cref="Result{TOut}"/>.</returns>
        [PublicAPI, Pure]
        public Result<TOut> Cast<TOut>([NotNull, InstantHandle] in Func<T, TOut> converter)
        {
            if (IsFailure)
                return ToFailValueResult<TOut>();
            return new Result<TOut>(converter(Value), Logic);
        }

        /// <summary>
        /// Converts this <see cref="Result{T}"/> to a <see cref="CustomResult{TError}"/>
        /// </summary>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public CustomResult<TError> CustomCast<TError>([NotNull] in TError errorObject)
        {
            if (IsFailure)
                return ToFailCustomResult(errorObject);
            if (IsWarning)
                // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
                return Result.CustomWarn<TError>(Logic.Message, Logic.Exception);
            return Result.CustomOk<TError>();
        }

        /// <summary>
        /// Converts this <see cref="Result{T}"/> to a <see cref="CustomResult{TError}"/>
        /// </summary>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="errorFactory">Factory method that creates a custom error object.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public CustomResult<TError> CustomCast<TError>([NotNull, InstantHandle] in Func<TError> errorFactory)
        {
            if (IsFailure)
                return ToFailCustomResult(errorFactory());
            if (IsWarning)
                // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
                return Result.CustomWarn<TError>(Logic.Message, Logic.Exception);
            return Result.CustomOk<TError>();
        }

        /// <summary>
        /// Converts this <see cref="Result{T}"/> to a <see cref="Result{TOut, TError}"/>
        /// </summary>
        /// <typeparam name="TOut">Type of the output result value.</typeparam>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="converter">Function that converts this result value from input type to output type.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A <see cref="Result{TOut, TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<TOut, TError> Cast<TOut, TError>([NotNull, InstantHandle] in Func<T, TOut> converter, [NotNull] in TError errorObject)
        {
            if (IsFailure)
                return ToFailCustomValueResult<TOut, TError>(errorObject);
            if (IsWarning)
                // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
                return Result.Warn<TOut, TError>(converter(Value), Logic.Message, Logic.Exception);
            return Result.Ok<TOut, TError>(converter(Value));
        }

        /// <summary>
        /// Converts this <see cref="Result{T}"/> to a <see cref="Result{TOut, TError}"/>
        /// </summary>
        /// <typeparam name="TOut">Type of the output result value.</typeparam>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="converter">Function that converts this result value from input type to output type.</param>
        /// <param name="errorFactory">Factory method that creates a custom error object.</param>
        /// <returns>A <see cref="Result{TOut, TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<TOut, TError> Cast<TOut, TError>([NotNull, InstantHandle] in Func<T, TOut> converter, [NotNull, InstantHandle] in Func<TError> errorFactory)
        {
            if (IsFailure)
                return ToFailCustomValueResult<TOut, TError>(errorFactory());
            if (IsWarning)
                // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
                return Result.Warn<TOut, TError>(converter(Value), Logic.Message, Logic.Exception);
            return Result.Ok<TOut, TError>(converter(Value));
        }

        #endregion

        #region Internal helpers

        /// <summary>
        /// Converts this <see cref="Result{T}"/> to a failure <see cref="Result"/>.
        /// This <see cref="Result{T}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result"/>.</returns>
        [Pure]
        internal Result ToFailResult()
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result<T> to a Result failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail(Logic.Message, Logic.Exception);
        }
        
        /// <summary>
        /// Converts this <see cref="Result{T}"/> to a failure <see cref="Result{TOut}"/>.
        /// This <see cref="Result{T}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{TOut}"/>.</returns>
        [Pure]
        internal Result<TOut> ToFailValueResult<TOut>()
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result<T> to a Result<T> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail<TOut>(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result{T}"/> to a failure <see cref="Result{TOut}"/>.
        /// This <see cref="Result{T}"/> should be a warning or a failure.
        /// </summary>
        /// <param name="additionalMessage">Message to add as suffix of this <see cref="Result{T}"/> message.</param>
        /// <param name="exception">Exception to set in the failure <see cref="Result{T}"/>.</param>
        /// <returns>A failed <see cref="Result{TOut}"/>.</returns>
        [Pure]
        internal Result<TOut> ToFailValueResult<TOut>([NotNull] in string additionalMessage, [CanBeNull] in Exception exception = null)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result<T> to a Result<T> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail<TOut>(Logic.Message + additionalMessage, exception);
        }

        /// <summary>
        /// Converts this <see cref="Result{T}"/> to a failure <see cref="CustomResult{TError}"/>.
        /// This <see cref="Result{T}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        internal CustomResult<TError> ToFailCustomResult<TError>([NotNull] in TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result<T> to a CustomResult<TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.CustomFail(Logic.Message, errorObject, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result{T}"/> to a failure <see cref="Result{TOut, TError}"/>.
        /// This <see cref="Result{T}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{TOut, TError}"/>.</returns>
        [Pure]
        internal Result<TOut, TError> ToFailCustomValueResult<TOut, TError>([NotNull] in TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result<T> to a Result<T, TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail<TOut, TError>(Logic.Message, errorObject, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result{T}"/> to a warning <see cref="Result{T}"/>.
        /// This <see cref="Result{T}"/> should be a Ok or warning.
        /// </summary>
        /// <param name="message">Message to set in the warning <see cref="Result{T}"/>.</param>
        /// <param name="exception">Exception to set in the warning <see cref="Result{T}"/>.</param>
        /// <returns>A warning <see cref="Result{T}"/>.</returns>
        [Pure]
        internal Result<T> ToWarnValueResult([NotNull] in string message, [CanBeNull] in Exception exception = null)
        {
            Debug.Assert(ResultLogic.IsConvertibleToWarning(Logic), "Cannot convert a warning Result<T> to a Result<T> warning.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Warn(Value, message, exception);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Checks that this <see cref="Result{T}"/> is equals to the given one and that they are successful.
        /// </summary>
        /// <param name="other"><see cref="Result{T}"/> to compare.</param>
        /// <returns>True if both <see cref="Result{T}"/> are equal and successful, otherwise false.</returns>
        [PublicAPI, Pure]
        public bool SuccessEquals(in Result<T> other)
        {
            return AreSuccessEqual(this, other);
        }

        /// <inheritdoc />
        public bool Equals(Result<T> other)
        {
            return AreEqual(this, other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            return obj is Result<T> result && AreEqual(this, result);
        }

        /// <summary>
        /// Indicates whether both <see cref="Result{T}"/> are equal.
        /// </summary>
        /// <param name="result1">First <see cref="Result{T}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T}"/> to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if both <see cref="Result{T}"/> are equal, otherwise false.</returns>
        [Pure]
        internal static bool AreEqual(in Result<T> result1, in Result<T> result2, [CanBeNull] in IEqualityComparer<T> equalityComparer = null)
        {
            return result1.Logic.Equals(result2.Logic)
                && (equalityComparer ?? EqualityComparer<T>.Default).Equals(result1._value, result2._value);
        }

        /// <summary>
        /// Indicates whether both <see cref="Result{T}"/> are equal.
        /// </summary>
        /// <param name="result1">First <see cref="Result{T}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T}"/> to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if both <see cref="Result{T}"/> are equal, otherwise false.</returns>
        [Pure]
        internal static bool AreSuccessEqual(in Result<T> result1, in Result<T> result2, [CanBeNull] in IEqualityComparer<T> equalityComparer = null)
        {
            if (result1.IsSuccess && result2.IsSuccess)
                return AreEqual(result1, result2);
            return false;
        }

        /// <summary>
        /// Indicates whether both <see cref="Result{T}"/> are equal.
        /// </summary>
        /// <param name="result1">First <see cref="Result{T}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T}"/> to compare.</param>
        /// <returns>True if both <see cref="Result{T}"/> are equal, otherwise false.</returns>
        public static bool operator ==(in Result<T> result1, in Result<T> result2)
        {
            return AreEqual(result1, result2);
        }

        /// <summary>
        /// Indicates whether both <see cref="Result{T}"/> are not equal.
        /// </summary>
        /// <param name="result1">First <see cref="Result{T}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T}"/> to compare.</param>
        /// <returns>True if both <see cref="Result{T}"/> are not equal, otherwise false.</returns>
        public static bool operator !=(in Result<T> result1, in Result<T> result2)
        {
            return !(result1 == result2);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (EqualityComparer<T>.Default.GetHashCode(_value) * 397) ^ Logic.GetHashCode();
        }

        /// <summary>
        /// Indicates whether <see cref="Result{T}"/> value is equals to the given value.
        /// </summary>
        /// <param name="result"><see cref="Result{T}"/> that may embed a value to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <returns>True if the <see cref="Result{T}"/> value is equals to the given value, otherwise false.</returns>
        public static bool operator ==(in Result<T> result, in T value)
        {
            if (result.IsSuccess)
                return EqualityComparer<T>.Default.Equals(result.Value, value);
            return false;
        }

        /// <summary>
        /// Indicates whether <see cref="Result{T}"/> value is not equals to the given value.
        /// </summary>
        /// <param name="result"><see cref="Result{T}"/> that may embed a value to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <returns>True if the <see cref="Result{T}"/> value is not equals to the given value, otherwise false.</returns>
        public static bool operator !=(in Result<T> result, in T value)
        {
            return !(result == value);
        }

        /// <summary>
        /// Indicates whether <see cref="Result{T}"/> value is equals to the given value.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="result"><see cref="Result{T}"/> that may embed a value to compare.</param>
        /// <returns>True if the <see cref="Result{T}"/> value is equals to the given value, otherwise false.</returns>
        public static bool operator ==(in T value, in Result<T> result)
        {
            return result == value;
        }

        /// <summary>
        /// Indicates whether <see cref="Result{T}"/> value is not equals to the given value.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="result"><see cref="Result{T}"/> that may embed a value to compare.</param>
        /// <returns>True if the <see cref="Result{T}"/> value is not equals to the given value, otherwise false.</returns>
        public static bool operator !=(in T value, in Result<T> result)
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
            if (obj is Result<T> other)
                return CompareTo(other);

            throw new ArgumentException($"Cannot compare an object of type {obj.GetType()} with a {typeof(Result<T>)}");
        }

        /// <inheritdoc />
        /// <summary>
        /// Compares this <see cref="Result{T}"/> with the given one.
        /// Order keeps failures first, then warnings and finally successes.
        /// </summary>
        public int CompareTo(Result<T> other)
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
        /// Determines if this <see cref="Result{T}"/> is "less" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result{T}"/> to compare.</param>
        /// <param name="right">The second <see cref="Result{T}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(in Result<T> left, in Result<T> right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="Result{T}"/> is "less" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result{T}"/> to compare.</param>
        /// <param name="right">The second <see cref="Result{T}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(in Result<T> left, in Result<T> right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="Result{T}"/> is "greater" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result{T}"/> to compare.</param>
        /// <param name="right">The second <see cref="Result{T}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(in Result<T> left, in Result<T> right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="Result{T}"/> is "greater" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result{T}"/> to compare.</param>
        /// <param name="right">The second <see cref="Result{T}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(in Result<T> left, in Result<T> right)
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
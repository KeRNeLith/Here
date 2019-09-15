using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static Here.ResultHelpers;

namespace Here
{
    /// <summary>
    /// The result interaction logic.
    /// </summary>
    internal struct ResultLogic : IResult, IEquatable<ResultLogic>, IComparable<ResultLogic>
    {
        /// <inheritdoc />
        public bool IsSuccess => !IsFailure;

        /// <inheritdoc />
        public bool IsWarning { get; }

        /// <inheritdoc />
        public bool IsFailure { get; }

        /// <inheritdoc />
        public string Message { get; }

        /// <inheritdoc />
        public Exception Exception { get; }

        /// <summary>
        /// <see cref="ResultLogic"/> "warning"/"failure" constructor.
        /// </summary>
        /// <param name="isWarning">Warning flag.</param>
        /// <param name="message">Message.</param>
        /// <param name="exception">Result embedded exception.</param>
        public ResultLogic(bool isWarning, [NotNull] string message, [CanBeNull] Exception exception)
        {
            // Warning & Failure must have a message
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message), "Cannot initialize a warning or failure Result with a null or empty message.");

            IsWarning = isWarning;
            IsFailure = !isWarning;
            Message = message;
            Exception = exception;
        }

        #region Converter

        /// <summary>
        /// Checks if the given <see cref="IResult"/> can be converted to a failure result.
        /// </summary>
        /// <param name="logic"><see cref="IResult"/> to check.</param>
        /// <returns>True if the <see cref="IResult"/> is convertible, otherwise false.</returns>
        [Pure]
        internal static bool IsConvertibleToFailure(IResult logic)
        {
            return logic.IsFailure || logic.IsWarning;
        }

        /// <summary>
        /// Checks if the given <see cref="IResult"/> can be converted to a warning result.
        /// </summary>
        /// <param name="logic"><see cref="IResult"/> to check.</param>
        /// <returns>True if the <see cref="IResult"/> is convertible, otherwise false.</returns>
        [Pure]
        internal static bool IsConvertibleToWarning(IResult logic)
        {
            return !logic.IsFailure;
        }

        /// <summary>
        /// Converts a <see cref="ResultLogic{TError}"/> into a <see cref="ResultLogic"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the custom error object.</typeparam>
        /// <param name="logic"><see cref="ResultLogic{TError}"/> to convert.</param>
        /// <returns>A <see cref="ResultLogic"/>.</returns>
        [Pure]
        internal static ResultLogic ToResultLogic<TError>(ResultLogic<TError> logic)
        {
            if (logic.IsSuccess && !logic.IsWarning)
                return new ResultLogic();

            return new ResultLogic(logic.IsWarning, logic.Message, logic.Exception);
        }

        #endregion

        #region Equality / IEquatable

        /// <inheritdoc />
        public bool Equals(ResultLogic other)
        {
            return IsFailure == other.IsFailure // Do not check IsSuccess as it's always the opposite of IsFailure
                && IsWarning == other.IsWarning
                && string.Equals(Message, other.Message, StringComparison.Ordinal)
                && Equals(Exception, other.Exception);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            return obj is ResultLogic logic && Equals(logic);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            int hashCode = IsWarning.GetHashCode();
            hashCode = (hashCode * 397) ^ IsFailure.GetHashCode();
            hashCode = (hashCode * 397) ^ (Message != null ? Message.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Exception != null ? Exception.GetHashCode() : 0);
            return hashCode;
        }

        /// <summary>
        /// Indicates whether both <see cref="ResultLogic"/> are equal.
        /// </summary>
        /// <param name="result1">First <see cref="ResultLogic"/> to compare.</param>
        /// <param name="result2">Second <see cref="ResultLogic"/> to compare.</param>
        /// <returns>True if both <see cref="ResultLogic"/> are equal, otherwise false.</returns>
        public static bool operator ==(ResultLogic result1, ResultLogic result2)
        {
            return Equals(result1, result2);
        }

        /// <summary>
        /// Indicates whether both <see cref="ResultLogic"/> are not equal.
        /// </summary>
        /// <param name="result1">First <see cref="ResultLogic"/> to compare.</param>
        /// <param name="result2">Second <see cref="ResultLogic"/> to compare.</param>
        /// <returns>True if both <see cref="ResultLogic"/> are not equal, otherwise false.</returns>
        public static bool operator !=(ResultLogic result1, ResultLogic result2)
        {
            return !(result1 == result2);
        }

        #endregion

        #region IComparable<T>

        /// <inheritdoc />
        /// <summary>
        /// Compares this <see cref="ResultLogic"/> with the given one.
        /// Order keeps failures first, then warning and finally successes.
        /// </summary>
        public int CompareTo(ResultLogic other)
        {
            return CompareResults(this, other);
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic"/> is "less" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(ResultLogic left, ResultLogic right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic"/> is "less" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(ResultLogic left, ResultLogic right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic"/> is "greater" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(ResultLogic left, ResultLogic right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic"/> is "greater" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(ResultLogic left, ResultLogic right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return ResultToString(this);
        }
    }

    /// <summary>
    /// The result interaction logic (with error).
    /// </summary>
    internal struct ResultLogic<TError> : IResultError<TError>, IEquatable<ResultLogic<TError>>, IComparable<ResultLogic<TError>>
    {
        /// <inheritdoc />
        public bool IsSuccess => !IsFailure;

        /// <inheritdoc />
        public bool IsWarning { get; }

        /// <inheritdoc />
        public bool IsFailure { get; }

        /// <inheritdoc />
        public string Message { get; }

        /// <inheritdoc />
        public Exception Exception { get; }

        [CanBeNull]
        private readonly TError _error;

        /// <inheritdoc />
        public TError Error
        {
            get
            {
                if (IsSuccess || IsWarning)
                    throw new InvalidOperationException("There is no error object for a success or warning Result.");

                return _error;
            }
        }

        /// <summary>
        /// <see cref="ResultLogic{TError}"/> "warning"/"failure" constructor.
        /// </summary>
        /// <param name="isWarning">Warning flag.</param>
        /// <param name="message">Message.</param>
        /// <param name="error">Error object.</param>
        /// <param name="exception">Result embedded exception.</param>
        public ResultLogic(bool isWarning, [NotNull] string message, [CanBeNull] TError error, [CanBeNull] Exception exception)
        {
            // Warning & Failure must have a message
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message), "Cannot initialize a warning or failure Result with a null or empty message.");
            if (!isWarning && error == null)
                throw new ArgumentNullException(nameof(error), "Cannot initialize a failure Result with a null error object.");

            IsWarning = isWarning;
            IsFailure = !isWarning;
            Message = message;
            _error = error;
            Exception = exception;
        }

        #region Equality / IEquatable

        /// <inheritdoc />
        public bool Equals(ResultLogic<TError> other)
        {
            return IsFailure == other.IsFailure // Do not check IsSuccess as it's always the opposite of IsFailure
                && IsWarning == other.IsWarning
                && string.Equals(Message, other.Message, StringComparison.Ordinal)
                && EqualityComparer<TError>.Default.Equals(_error, other._error)
                && Equals(Exception, other.Exception);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            return obj is ResultLogic<TError> logic && Equals(logic);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            int hashCode = EqualityComparer<TError>.Default.GetHashCode(_error);
            hashCode = (hashCode * 397) ^ IsWarning.GetHashCode();
            hashCode = (hashCode * 397) ^ IsFailure.GetHashCode();
            hashCode = (hashCode * 397) ^ (Message != null ? Message.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Exception != null ? Exception.GetHashCode() : 0);
            return hashCode;
        }

        /// <summary>
        /// Indicates whether both <see cref="ResultLogic{TError}"/> are equal.
        /// </summary>
        /// <param name="result1">First <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <returns>True if both <see cref="ResultLogic{TError}"/> are equal, otherwise false.</returns>
        public static bool operator ==(ResultLogic<TError> result1, ResultLogic<TError> result2)
        {
            return Equals(result1, result2);
        }

        /// <summary>
        /// Indicates whether both <see cref="ResultLogic{TError}"/> are not equal.
        /// </summary>
        /// <param name="result1">First <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <returns>True if both <see cref="ResultLogic{TError}"/> are not equal, otherwise false.</returns>
        public static bool operator !=(ResultLogic<TError> result1, ResultLogic<TError> result2)
        {
            return !(result1 == result2);
        }

        #endregion

        #region IComparable<T>

        /// <inheritdoc />
        /// <summary>
        /// Compares this <see cref="ResultLogic{TError}"/> with the given one.
        /// Order keeps failures first, then warning and finally successes.
        /// </summary>
        public int CompareTo(ResultLogic<TError> other)
        {
            return CompareResults(this, other);
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic{TError}"/> is "less" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(ResultLogic<TError> left, ResultLogic<TError> right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic{TError}"/> is "less" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(ResultLogic<TError> left, ResultLogic<TError> right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic{TError}"/> is "greater" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(ResultLogic<TError> left, ResultLogic<TError> right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic{TError}"/> is "greater" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(ResultLogic<TError> left, ResultLogic<TError> right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return ResultToString(this);
        }
    }
}

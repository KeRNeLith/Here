using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// The result interaction logic.
    /// </summary>
    internal class ResultLogic<TError> : IEquatable<ResultLogic<TError>>, IComparable<ResultLogic<TError>>
    {
        /// <summary>
        /// Indicates if it is a success or not.
        /// </summary>
        public bool IsSuccess => !IsFailure;

        /// <summary>
        /// Indicates if it has a warning or not.
        /// </summary>
        public bool IsWarning { get; }

        /// <summary>
        /// Indicates if it is a failure or not.
        /// </summary>
        public bool IsFailure { get; }

        /// <summary>
        /// Result message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Result exception.
        /// </summary>
        [CanBeNull]
        public Exception Exception { get; }

        [CanBeNull]
        private readonly TError _error;

        /// <summary>
        /// Result error object.
        /// </summary>
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
        /// <see cref="ResultLogic{TError}"/> "Ok" constructor.
        /// </summary>
        public ResultLogic()
        {
            IsWarning = false;
            IsFailure = false;
            Message = null;
            _error = default;
            Exception = null;
        }

        /// <summary>
        /// <see cref="ResultLogic{TError}"/> "warning"/"failure" constructor.
        /// </summary>
        /// <param name="isWarning">Warning flag.</param>
        /// <param name="message">Message.</param>
        /// <param name="error">Error object.</param>
        /// <param name="exception">Result embedded exception.</param>
        public ResultLogic(in bool isWarning, [NotNull] in string message, [CanBeNull] in TError error, [CanBeNull] in Exception exception)
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
            if (other is null)
                return false;
            return IsSuccess == other.IsSuccess // Do not check IsFailure as it's always the opposite of IsSuccess
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
        public static bool operator ==(in ResultLogic<TError> result1, in ResultLogic<TError> result2)
        {
            if (ReferenceEquals(result1, result2))
                return true;
            return Equals(result1, result2);
        }

        /// <summary>
        /// Indicates whether both <see cref="ResultLogic{TError}"/> are not equal.
        /// </summary>
        /// <param name="result1">First <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <returns>True if both <see cref="ResultLogic{TError}"/> are not equal, otherwise false.</returns>
        public static bool operator !=(in ResultLogic<TError> result1, in ResultLogic<TError> result2)
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
            if (IsSuccess && !other.IsSuccess)
                return 1;

            if (!IsSuccess && other.IsSuccess)
                return -1;

            // Both success
            if (IsSuccess && other.IsSuccess)
            {
                if (IsWarning && !other.IsWarning)
                    return -1;
                if (!IsWarning && other.IsWarning)
                    return 1;
            }

            // Both success with or without warning or both failure
            return 0;
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic{TError}"/> is "less" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(in ResultLogic<TError> left, in ResultLogic<TError> right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic{TError}"/> is "less" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(in ResultLogic<TError> left, in ResultLogic<TError> right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic{TError}"/> is "greater" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(in ResultLogic<TError> left, in ResultLogic<TError> right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic{TError}"/> is "greater" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic{TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(in ResultLogic<TError> left, in ResultLogic<TError> right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            if (IsFailure)
                return "Failure";
            if (IsWarning)
                return "Warning";
            return "Success";
        }
    }

    /// <summary>
    /// The Result interaction logic specialized to only provide a message (for warning and error).
    /// </summary>
    internal sealed class ResultLogic : ResultLogic<string>, IEquatable<ResultLogic>, IComparable<ResultLogic>
    {
        /// <summary>
        /// <see cref="ResultLogic"/> "Ok" constructor.
        /// </summary>
        public ResultLogic()
        {
        }

        /// <summary>
        /// <see cref="ResultLogic"/> "warning"/"error" constructor.
        /// </summary>
        /// <param name="isWarning">Warning flag.</param>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embedded exception.</param>
        public ResultLogic(in bool isWarning, [NotNull] in string message, [CanBeNull] in Exception exception)
            : base(isWarning, message, isWarning ? null : message, exception)
        {
        }

        #region Converter

        /// <summary>
        /// Checks if the given <see cref="ResultLogic{TError}"/> can be converted to a failure result.
        /// </summary>
        /// <param name="logic"><see cref="ResultLogic{TError}"/> to check.</param>
        /// <returns>True if the <see cref="ResultLogic{TError}"/> is convertible, otherwise false.</returns>
        [Pure]
        internal static bool IsConvertibleToFailure<TError>(in ResultLogic<TError> logic)
        {
            return !logic.IsSuccess || logic.IsWarning;
        }

        /// <summary>
        /// Checks if the given <see cref="ResultLogic{TError}"/> can be converted to a warning result.
        /// </summary>
        /// <param name="logic"><see cref="ResultLogic{TError}"/> to check.</param>
        /// <returns>True if the <see cref="ResultLogic{TError}"/> is convertible, otherwise false.</returns>
        internal static bool IsConvertibleToWarning<TError>(in ResultLogic<TError> logic)
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
        internal static ResultLogic ToResultLogic<TError>(in ResultLogic<TError> logic)
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
            if (other is null)
                return false;
            return IsSuccess == other.IsSuccess // Do not check IsFailure as it's always the opposite of IsSuccess
                && IsWarning == other.IsWarning
                && string.Equals(Message, other.Message, StringComparison.Ordinal)  // Do not check the error field as it is not used
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
        public static bool operator ==(in ResultLogic result1, in ResultLogic result2)
        {
            if (ReferenceEquals(result1, result2))
                return true;
            return Equals(result1, result2);
        }

        /// <summary>
        /// Indicates whether both <see cref="ResultLogic"/> are not equal.
        /// </summary>
        /// <param name="result1">First <see cref="ResultLogic"/> to compare.</param>
        /// <param name="result2">Second <see cref="ResultLogic"/> to compare.</param>
        /// <returns>True if both <see cref="ResultLogic"/> are not equal, otherwise false.</returns>
        public static bool operator !=(in ResultLogic result1, in ResultLogic result2)
        {
            return !(result1 == result2);
        }

        #endregion

        #region IComparable<T>

        /// <inheritdoc />
        /// <summary>
        /// Compare this <see cref="ResultLogic"/> with the given one.
        /// Order keeps failures first, then warnings and finally successes.
        /// </summary>
        public int CompareTo(ResultLogic other)
        {
            return base.CompareTo(other);
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic"/> is "less" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(in ResultLogic left, in ResultLogic right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic"/> is "less" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(in ResultLogic left, in ResultLogic right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic"/> is "greater" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(in ResultLogic left, in ResultLogic right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="ResultLogic"/> is "greater" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="ResultLogic"/> to compare.</param>
        /// <param name="right">The second <see cref="ResultLogic"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(in ResultLogic left, in ResultLogic right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion
    }
}

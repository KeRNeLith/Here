using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Here.Results
{
    /// <summary>
    /// The Result interaction logic.
    /// </summary>
    internal class ResultLogic<TError> : IEquatable<ResultLogic<TError>>, IComparable<ResultLogic<TError>>
    {
        /// <summary>
        /// Indicate if it is a success.
        /// </summary>
        public bool IsSuccess => !IsFailure;

        /// <summary>
        /// Indicate if it is a warning.
        /// </summary>
        public bool IsWarning { get; }

        /// <summary>
        /// Indicate if it is a failure.
        /// </summary>
        public bool IsFailure { get; }

        /// <summary>
        /// Result message.
        /// </summary>
        [CanBeNull]
        public string Message { get; }

        /// <summary>
        /// Result exception.
        /// </summary>
        [CanBeNull]
        public Exception Exception { get; }

        private readonly TError _error;

        /// <summary>
        /// Result error object.
        /// </summary>
        [CanBeNull]
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
        /// <see cref="ResultLogic{TError}"/> "ok" constructor.
        /// </summary>
        public ResultLogic()
        {
            IsWarning = false;
            IsFailure = false;
            Message = null;
            _error = default(TError);
            Exception = null;
        }

        /// <summary>
        /// <see cref="ResultLogic{TError}"/> "warning"/"failure" constructor.
        /// </summary>
        /// <param name="isWarning">Warning flag.</param>
        /// <param name="message">Message.</param>
        /// <param name="error">Error object.</param>
        /// <param name="exception">Result embeded exception.</param>
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

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            return obj is ResultLogic<TError> logic && Equals(logic);
        }

        public override int GetHashCode()
        {
            int hashCode = EqualityComparer<TError>.Default.GetHashCode(_error);
            hashCode = (hashCode * 397) ^ IsWarning.GetHashCode();
            hashCode = (hashCode * 397) ^ IsFailure.GetHashCode();
            hashCode = (hashCode * 397) ^ (Message != null ? Message.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Exception != null ? Exception.GetHashCode() : 0);
            return hashCode;
        }

        public static bool operator ==(ResultLogic<TError> result1, ResultLogic<TError> result2)
        {
            if (ReferenceEquals(result1, result2))
                return true;
            return Equals(result1, result2);
        }

        public static bool operator !=(ResultLogic<TError> result1, ResultLogic<TError> result2)
        {
            return !(result1 == result2);
        }

        #endregion

        #region IComparable<T>

        /// <summary>
        /// Compare this <see cref="ResultLogic{TError}"/> with the given one.
        /// Order keeps <see cref="IsFailure"/> first, then <see cref="IsWarning"/> and finally <see cref="IsSuccess"/>.
        /// </summary>
        /// <param name="other"><see cref="ResultLogic{TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
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
        /// <see cref="ResultLogic"/> "ok" constructor.
        /// </summary>
        public ResultLogic()
        {
        }

        /// <summary>
        /// <see cref="ResultLogic"/> "warning"/"error" constructor.
        /// </summary>
        /// <param name="isWarning">Warning flag.</param>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embeded exception.</param>
        public ResultLogic(bool isWarning, [NotNull] string message, [CanBeNull] Exception exception)
            : base(isWarning, message, isWarning ? null : message, exception)
        {
        }

        #region Converter

        /// <summary>
        /// Check if the given <see cref="ResultLogic{TError}"/> can be converted to a failure result.
        /// </summary>
        /// <param name="logic"><see cref="ResultLogic{TError}"/> to check.</param>
        /// <returns>True if the <see cref="ResultLogic{TError}"/> is convertable, otherwise false.</returns>
        internal static bool IsConvertableToFailure<TError>(ResultLogic<TError> logic)
        {
            return !logic.IsSuccess || logic.IsWarning;
        }

        /// <summary>
        /// Convert a <see cref="ResultLogic{TError}"/> into a <see cref="ResultLogic"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the custom error object.</typeparam>
        /// <param name="logic"><see cref="ResultLogic{TError}"/> to convert.</param>
        /// <returns>A corresponding <see cref="ResultLogic"/>.</returns>
        [Pure]
        internal static ResultLogic ToResultLogic<TError>(ResultLogic<TError> logic)
        {
            if (logic.IsSuccess && !logic.IsWarning)
                return new ResultLogic();
            
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return new ResultLogic(logic.IsWarning, logic.Message, logic.Exception);
        }

        #endregion

        #region Equality / IEquatable

        public bool Equals(ResultLogic other)
        {
            if (other is null)
                return false;
            return IsSuccess == other.IsSuccess // Do not check IsFailure as it's always the opposite of IsSuccess
                && IsWarning == other.IsWarning
                && string.Equals(Message, other.Message, StringComparison.Ordinal)  // Do not check the error field as it is not used
                && Equals(Exception, other.Exception);
        }

        public override bool Equals(object other)
        {
            if (other is null)
                return false;
            return other is ResultLogic logic && Equals(logic);
        }

        public override int GetHashCode()
        {
            int hashCode = IsWarning.GetHashCode();
            hashCode = (hashCode * 397) ^ IsFailure.GetHashCode();
            hashCode = (hashCode * 397) ^ (Message != null ? Message.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Exception != null ? Exception.GetHashCode() : 0);
            return hashCode;
        }

        public static bool operator ==(ResultLogic result1, ResultLogic result2)
        {
            if (ReferenceEquals(result1, result2))
                return true;
            return Equals(result1, result2);
        }

        public static bool operator !=(ResultLogic result1, ResultLogic result2)
        {
            return !(result1 == result2);
        }

        #endregion

        #region IComparable<T>

        /// <summary>
        /// Compare this <see cref="ResultLogic"/> with the given one.
        /// Order keeps <see cref="IsFailure"/> first, then <see cref="IsWarning"/> and finally <see cref="IsSuccess"/>.
        /// </summary>
        /// <param name="other"><see cref="ResultLogic"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public int CompareTo(ResultLogic other)
        {
            return base.CompareTo(other);
        }

        #endregion
    }
}

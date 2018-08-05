using System;
using JetBrains.Annotations;

namespace Here.Results
{
    /// <summary>
    /// The Result interaction logic.
    /// </summary>
    internal class ResultLogic<TError>
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

        /// <summary>
        /// Check if the given <see cref="ResultLogic{TError}"/> can be converted to a failure result.
        /// </summary>
        /// <param name="logic"><see cref="ResultLogic{TError}"/> to check.</param>
        /// <returns>True if the <see cref="ResultLogic{TError}"/> is convertable, otherwise false.</returns>
        public static bool IsConvertableToFailure(ResultLogic<TError> logic)
        {
            return !logic.IsSuccess || logic.IsWarning;
        }

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
    internal sealed class ResultLogic : ResultLogic<string>
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
        /// Convert a <see cref="ResultLogic{TError}"/> into a <see cref="ResultLogic"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the custom error object.</typeparam>
        /// <param name="logic"><see cref="ResultLogic{TError}"/> to convert.</param>
        /// <returns>A corresponding <see cref="ResultLogic"/>.</returns>
        [Pure]
        public static ResultLogic ToResultLogic<TError>(ResultLogic<TError> logic)
        {
            if (logic.IsSuccess && !logic.IsWarning)
                return new ResultLogic();
            
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return new ResultLogic(logic.IsWarning, logic.Message, logic.Exception);
        }

        #endregion
    }
}

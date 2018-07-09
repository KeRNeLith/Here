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
        }

        /// <summary>
        /// <see cref="ResultLogic{TError}"/> "warning"/"failure" constructor.
        /// </summary>
        /// <param name="isWarning">Warning flag.</param>
        /// <param name="message">Message.</param>
        /// <param name="error">Error object.</param>
        public ResultLogic(bool isWarning, [NotNull] string message, [CanBeNull] TError error)
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
            : base()
        {
        }

        /// <summary>
        /// <see cref="ResultLogic"/> "warning"/"error" constructor.
        /// </summary>
        /// <param name="isWarning">Warning flag.</param>
        /// <param name="message">Result message.</param>
        public ResultLogic(bool isWarning, [NotNull] string message)
            : base(isWarning, message, isWarning ? null : message)
        {
        }
    }
}

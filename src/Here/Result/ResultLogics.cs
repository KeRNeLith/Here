using JetBrains.Annotations;
using System;

namespace Here.Results
{
    /// <summary>
    /// The <see cref="Result"/> interaction logic.
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

        private readonly TError _error;

        /// <summary>
        /// Result error object.
        /// </summary>
        [NotNull]
        public TError Error
        {
            get
            {
                if (IsSuccess && !IsWarning)
                    throw new InvalidOperationException("There is no error object for a success or warning Result.");

                return _error;
            }
        }

        /// <summary>
        /// <see cref="ResultLogic{TError}"/> constructor.
        /// </summary>
        /// <param name="isWarning">Warning flag.</param>
        /// <param name="isFailure">Failure flag.</param>
        /// <param name="error">Error object.</param>
        public ResultLogic(bool isWarning, bool isFailure, [CanBeNull] TError error)
        {
            // Warnings & Errors should have a message
            if ((isWarning || isFailure) && error == null)
                throw new ArgumentNullException(nameof(error), "Cannot initialize a warning or failure Result with a null error object.");

            IsWarning = isWarning;
            IsFailure = isFailure;
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
    /// The <see cref="Result"/> interaction logic for error object as string message.
    /// </summary>
    internal sealed class ResultLogic : ResultLogic<string>
    {
        /// <summary>
        /// Result message.
        /// </summary>
        [NotNull]
        public string Message => Error;

        /// <summary>
        /// <see cref="ResultLogic"/> constructor.
        /// </summary>
        /// <param name="isWarning">Warning flag.</param>
        /// <param name="isFailure">Failure flag.</param>
        /// <param name="message">Message.</param>
        public ResultLogic(bool isWarning, bool isFailure, [CanBeNull] string message)
            : base(isWarning, isFailure, message)
        {
            // Warnings & Errors should have a message
            if ((isWarning || isFailure) && string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message), "Cannot initialize a warning or error Result with a null or empty message.");
        }
    }
}

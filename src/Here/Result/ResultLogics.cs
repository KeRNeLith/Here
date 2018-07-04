using JetBrains.Annotations;
using System;

namespace Here.Results
{
    /// <summary>
    /// The <see cref="IResult"/> interaction logic.
    /// </summary>
    internal class ResultLogic
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

        private readonly string _message;

        /// <summary>
        /// Result message.
        /// </summary>
        [CanBeNull]
        public string Message
        {
            get
            {
                if (IsSuccess && !IsWarning)
                    throw new InvalidOperationException("There is no message for a success Result.");

                return _message;
            }
        }

        /// <summary>
        /// <see cref="ResultLogic"/> constructor.
        /// </summary>
        /// <param name="isWarning">Warning flag.</param>
        /// <param name="isFailure">Failure flag.</param>
        /// <param name="message">Message.</param>
        public ResultLogic(bool isWarning, bool isFailure, [CanBeNull] string message)
        {
            // Warnings & Errors should have a message
            if ((isWarning || isFailure) && string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message), "Cannot initialize a not-success Result with a null message.");

            IsWarning = isWarning;
            IsFailure = isFailure;
            _message = message;
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
}

using JetBrains.Annotations;

namespace Here.Results
{
    /// <summary>
    /// Represents the result of an operation/treatment.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Indicate if this Result is a success.
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Indicate if this Result succeed with warning.
        /// </summary>
        bool IsWarning { get; }

        /// <summary>
        /// Indicate if this Result is a failure.
        /// </summary>
        bool IsFailure { get; }

        /// <summary>
        /// Result message.
        /// </summary>
        [CanBeNull]
        string Message { get; }
    }

    /// <summary>
    /// Represents the result of an operation/treatment with a custom error object.
    /// </summary>
    public interface IResultError<out TError> : IResult
    {
        /// <summary>
        /// Error attached to a Result.
        /// </summary>
        [CanBeNull]
        TError Error { get; }
    }

    /// <summary>
    /// Represents the result of an operation/treatment with a <see cref="Value"/>.
    /// </summary>
    /// <typeparam name="T">Type of the embeded value.</typeparam>
    public interface IResult<out T> : IResult
    {
        /// <summary>
        /// This Result value.
        /// </summary>
        [CanBeNull]
        T Value { get; }
    }
}

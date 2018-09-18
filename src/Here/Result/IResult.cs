using System;
using JetBrains.Annotations;

namespace Here.Results
{
    /// <summary>
    /// Represents the result of an operation/treatment.
    /// </summary>
    [PublicAPI]
    public interface IResult
    {
        /// <summary>
        /// Indicate if this Result is a success.
        /// </summary>
        [PublicAPI]
        bool IsSuccess { get; }

        /// <summary>
        /// Indicate if this Result succeed with warning.
        /// </summary>
        [PublicAPI]
        bool IsWarning { get; }

        /// <summary>
        /// Indicate if this Result is a failure.
        /// </summary>
        [PublicAPI]
        bool IsFailure { get; }

        /// <summary>
        /// Result message.
        /// </summary>
        [PublicAPI, CanBeNull]
        string Message { get; }

        /// <summary>
        /// Result exception.
        /// </summary>
        [PublicAPI, CanBeNull]
        Exception Exception { get; }
    }

    /// <summary>
    /// Represents the result of an operation/treatment with a custom error object.
    /// </summary>
    [PublicAPI]
    public interface IResultError<out TError> : IResult
    {
        /// <summary>
        /// Error attached to a Result.
        /// </summary>
        [PublicAPI, CanBeNull]
        TError Error { get; }
    }

    /// <summary>
    /// Represents the result of an operation/treatment with a <see cref="Value"/>.
    /// </summary>
    /// <typeparam name="T">Type of the embeded value.</typeparam>
    [PublicAPI]
    public interface IResult<out T> : IResult
    {
        /// <summary>
        /// This Result value.
        /// </summary>
        [PublicAPI, CanBeNull]
        T Value { get; }
    }
}

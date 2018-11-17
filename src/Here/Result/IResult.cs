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
        /// Indicates if this result is a success or not.
        /// </summary>
        [PublicAPI]
        bool IsSuccess { get; }

        /// <summary>
        /// Indicates if this result succeed with warning or not.
        /// </summary>
        [PublicAPI]
        bool IsWarning { get; }

        /// <summary>
        /// Indicates if this result is a failure.
        /// </summary>
        [PublicAPI]
        bool IsFailure { get; }

        /// <summary>
        /// Result message.
        /// </summary>
        [PublicAPI]
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
        /// Error attached to this result.
        /// </summary>
        [PublicAPI]
        TError Error { get; }
    }

    /// <summary>
    /// Represents the result of an operation/treatment with a <see cref="Value"/>.
    /// </summary>
    /// <typeparam name="T">Type of the embedded value.</typeparam>
    [PublicAPI]
    public interface IResult<out T> : IResult
    {
        /// <summary>
        /// Result value.
        /// </summary>
        [PublicAPI]
        T Value { get; }
    }
}

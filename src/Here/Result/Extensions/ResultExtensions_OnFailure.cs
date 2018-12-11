using System;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> (On failure).
    /// </summary>
    public static partial class ResultExtensions
    {
        #region Result

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result"/>.</returns>
        [PublicAPI]
        public static Result OnFailure(in this Result result, [NotNull, InstantHandle] in Action onFailure, in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result"/>.</returns>
        [PublicAPI]
        public static Result OnFailure(in this Result result, [NotNull, InstantHandle] in Action<Result> onFailure, in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure, otherwise return the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is failure.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnFailure<TOut>(in this Result result, 
            [NotNull, InstantHandle] in Func<Result, TOut> onFailure, 
            [CanBeNull] in TOut defaultValue,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);
            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure,
        /// otherwise return the <paramref name="valueFactory"/> return value.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is failure.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnFailure<TOut>(in this Result result,
            [NotNull, InstantHandle] in Func<Result, TOut> onFailure,
            [NotNull, InstantHandle] in Func<TOut> valueFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);
            return valueFactory();
        }

        #endregion

        #region Result<T>

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result{T}"/>.</returns>
        [PublicAPI]
        public static Result<T> OnFailure<T>(in this Result<T> result, [NotNull, InstantHandle] in Action onFailure, in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result{T}"/>.</returns>
        [PublicAPI]
        public static Result<T> OnFailure<T>(in this Result<T> result, [NotNull, InstantHandle] in Action<Result<T>> onFailure, in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure, otherwise return the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="TIn">Result value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is failure.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnFailure<TIn, TOut>(in this Result<TIn> result,
            [NotNull, InstantHandle] in Func<Result<TIn>, TOut> onFailure,
            [CanBeNull] in TOut defaultValue,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);
            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure,
        /// otherwise return the <paramref name="valueFactory"/> return value.
        /// </summary>
        /// <typeparam name="TIn">Result value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is failure.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnFailure<TIn, TOut>(in this Result<TIn> result,
            [NotNull, InstantHandle] in Func<Result<TIn>, TOut> onFailure,
            [NotNull, InstantHandle] in Func<TIn, TOut> valueFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);
            return valueFactory(result.Value);
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(in this CustomResult<TError> result, 
            [NotNull, InstantHandle] in Action onFailure,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(in this CustomResult<TError> result, 
            [NotNull, InstantHandle] in Action<CustomResult<TError>> onFailure,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure, otherwise return the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnFailure<TOut, TError>(in this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<CustomResult<TError>, TOut> onFailure,
            [CanBeNull] in TOut defaultValue,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);
            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure,
        /// otherwise return the <paramref name="valueFactory"/> return value.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnFailure<TOut, TError>(in this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<CustomResult<TError>, TOut> onFailure,
            [NotNull, InstantHandle] in Func<TOut> valueFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);
            return valueFactory();
        }

        #endregion

        #region Result<T, TError>

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(in this Result<T, TError> result,
            [NotNull, InstantHandle] in Action onFailure,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(in this Result<T, TError> result,
            [NotNull, InstantHandle] in Action<Result<T, TError>> onFailure,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure, otherwise return the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="TIn">Result value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is failure.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnFailure<TIn, TOut, TError>(in this Result<TIn, TError> result,
            [NotNull, InstantHandle] in Func<Result<TIn, TError>, TOut> onFailure,
            [CanBeNull] in TOut defaultValue,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);
            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure
        /// otherwise return the <paramref name="valueFactory"/> return value.
        /// </summary>
        /// <typeparam name="TIn">Result value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is failure.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnFailure<TIn, TOut, TError>(in this Result<TIn, TError> result,
            [NotNull, InstantHandle] in Func<Result<TIn, TError>, TOut> onFailure,
            [NotNull, InstantHandle] in Func<TIn, TOut> valueFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);
            return valueFactory(result.Value);
        }

        #endregion
    }
}

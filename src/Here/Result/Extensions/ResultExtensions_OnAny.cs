using System;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> (On any).
    /// </summary>
    public static partial class ResultExtensions
    {
        #region Result

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <param name="result"><see cref="Result"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <returns>This <see cref="Result"/>.</returns>
        [PublicAPI]
        public static Result OnAny(this Result result, [NotNull, InstantHandle] in Action onAny)
        {
            onAny();
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <param name="result"><see cref="Result"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <returns>This <see cref="Result"/>.</returns>
        [PublicAPI]
        public static Result OnAny(this Result result, [NotNull, InstantHandle] in Action<Result> onAny)
        {
            onAny(result);
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> function all the time.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnAny<TOut>(this Result result, [NotNull, InstantHandle] in Func<Result, TOut> onAny)
        {
            return onAny(result);
        }

        #endregion

        #region Result<T>

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <returns>This <see cref="Result{T}"/>.</returns>
        [PublicAPI]
        public static Result<T> OnAny<T>(this Result<T> result, [NotNull, InstantHandle] in Action onAny)
        {
            onAny();
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <returns>This <see cref="Result{T}"/>.</returns>
        [PublicAPI]
        public static Result<T> OnAny<T>(this Result<T> result, [NotNull, InstantHandle] in Action<Result<T>> onAny)
        {
            onAny(result);
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> function all the time.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnAny<TIn, TOut>(this Result<TIn> result, [NotNull, InstantHandle] in Func<Result<TIn>, TOut> onAny)
        {
            return onAny(result);
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <returns>This <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnAny<TError>(this CustomResult<TError> result, [NotNull, InstantHandle] in Action onAny)
        {
            onAny();
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <returns>This <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnAny<TError>(this CustomResult<TError> result, [NotNull, InstantHandle] in Action<CustomResult<TError>> onAny)
        {
            onAny(result);
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> function all the time.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnAny<TOut, TError>(this CustomResult<TError> result, [NotNull, InstantHandle] in Func<CustomResult<TError>, TOut> onAny)
        {
            return onAny(result);
        }

        #endregion

        #region Result<T, TError>

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <returns>This <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI]
        public static Result<T, TError> OnAny<T, TError>(this Result<T, TError> result, [NotNull, InstantHandle] in Action onAny)
        {
            onAny();
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <returns>This <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI]
        public static Result<T, TError> OnAny<T, TError>(this Result<T, TError> result, [NotNull, InstantHandle] in Action<Result<T, TError>> onAny)
        {
            onAny(result);
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> function all the time.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnAny<TIn, TOut, TError>(this Result<TIn, TError> result, [NotNull, InstantHandle] in Func<Result<TIn, TError>, TOut> onAny)
        {
            return onAny(result);
        }

        #endregion
    }
}

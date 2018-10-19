using System;
using JetBrains.Annotations;

namespace Here.Results.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/>.
    /// </summary>
    public static partial class ResultExtensions
    {
        #region Result

        /// <summary>
        /// Ensure that this <see cref="Result"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/>.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <returns>A corresponding <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public static Result Ensure(this Result result, [NotNull, InstantHandle] Func<bool> predicate, [NotNull] string errorMessage)
        {
            if (result.IsFailure)
                return result;

            if (!predicate())
                return Result.Fail(errorMessage);

            return result;
        }

        #endregion

        #region Result<T>

        /// <summary>
        /// Ensure that this <see cref="Result{T}"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <returns>A corresponding <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Result<T> Ensure<T>(this Result<T> result, [NotNull, InstantHandle] Predicate<T> predicate, [NotNull] string errorMessage)
        {
            if (result.IsFailure)
                return result;

            if (!predicate(result.Value))
                return Result.Fail<T>(errorMessage);

            return result;
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Ensure that this <see cref="CustomResult{TError}"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="TError">Error type of the result.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/>.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A corresponding <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public static CustomResult<TError> Ensure<TError>(this CustomResult<TError> result, 
            [NotNull, InstantHandle] Func<bool> predicate, 
            [NotNull] string errorMessage, 
            [NotNull] TError errorObject)
        {
            if (result.IsFailure)
                return result;

            if (!predicate())
                return Result.CustomFail(errorMessage, errorObject);

            return result;
        }

        /// <summary>
        /// Ensure that this <see cref="CustomResult{TError}"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="TError">Error type of the result.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/>.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <returns>A corresponding <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public static CustomResult<TError> Ensure<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<bool> predicate,
            [NotNull] string errorMessage,
            [NotNull, InstantHandle] Func<TError> errorFactory)
        {
            if (result.IsFailure)
                return result;

            if (!predicate())
                return Result.CustomFail(errorMessage, errorFactory());

            return result;
        }

        #endregion

        #region Result<T, TError>

        /// <summary>
        /// Ensure that this <see cref="Result{T, TError}"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Error type of the result.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A corresponding <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public static Result<T, TError> Ensure<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Predicate<T> predicate,
            [NotNull] string errorMessage,
            [NotNull] TError errorObject)
        {
            if (result.IsFailure)
                return result;

            if (!predicate(result.Value))
                return Result.Fail<T, TError>(errorMessage, errorObject);

            return result;
        }

        /// <summary>
        /// Ensure that this <see cref="Result{T, TError}"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Error type of the result.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <returns>A corresponding <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public static Result<T, TError> Ensure<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Predicate<T> predicate,
            [NotNull] string errorMessage,
            [NotNull, InstantHandle] Func<TError> errorFactory)
        {
            if (result.IsFailure)
                return result;

            if (!predicate(result.Value))
                return Result.Fail<T, TError>(errorMessage, errorFactory());

            return result;
        }

        #endregion
    }
}

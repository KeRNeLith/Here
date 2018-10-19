using System;
using JetBrains.Annotations;

namespace Here.Results.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/>.
    /// </summary>
    public static partial class ResultExtensions
    {
        #region IResult<T>

        /// <summary>
        /// Unwrap this <see cref="IResult{T}"/> value if it <see cref="IResult.IsSuccess"/>, 
        /// otherwise returns the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="IResult{T}"/>.</typeparam>
        /// <param name="result"><see cref="IResult{T}"/> to unwrap value.</param>
        /// <param name="defaultValue">Default value to use.</param>
        /// <returns>The unwrapped value from this <see cref="IResult{T}"/> if it has value, otherwise the default value.</returns>
        [PublicAPI, Pure, CanBeNull]
        public static T Unwrap<T>(this IResult<T> result, [CanBeNull] T defaultValue = default(T))
        {
            if (result.IsSuccess)
                return result.Value;
            return defaultValue;
        }

        /// <summary>
        /// Unwrap this <see cref="IResult{T}"/> value if it <see cref="IResult.IsSuccess"/>, 
        /// otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="IResult{T}"/>.</typeparam>
        /// <param name="result"><see cref="IResult{T}"/> to unwrap value.</param>
        /// <param name="orFunc">Default value factory method.</param>
        /// <returns>The unwrapped value from this <see cref="IResult{T}"/> if it has value, otherwise the default value.</returns>
        [PublicAPI, Pure, CanBeNull]
        public static T Unwrap<T>(this IResult<T> result, [NotNull, InstantHandle] Func<T> orFunc)
        {
            if (result.IsSuccess)
                return result.Value;
            return orFunc();
        }

        /// <summary>
        /// Unwrap this <see cref="IResult{T}"/> value if it <see cref="IResult.IsSuccess"/>, 
        /// use the <paramref name="converter"/> to convert the value, 
        /// otherwise returns the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="IResult{T}"/>.</typeparam>
        /// <typeparam name="TOut">Output value type.</typeparam>
        /// <param name="result"><see cref="IResult{T}"/> to unwrap value.</param>
        /// <param name="converter">Function called to convert this <see cref="IResult{T}"/> value.</param>
        /// <param name="defaultValue">Default value to use.</param>
        /// <returns>The unwrapped value from this <see cref="IResult{T}"/> if it has value, otherwise the default value.</returns>
        [PublicAPI, Pure, CanBeNull]
        public static TOut Unwrap<T, TOut>(this IResult<T> result,
            [NotNull, InstantHandle] Func<T, TOut> converter,
            [CanBeNull] TOut defaultValue = default(TOut))
        {
            if (result.IsSuccess)
                return converter(result.Value);
            return defaultValue;
        }

        /// <summary>
        /// Unwrap this <see cref="IResult{T}"/> value if it <see cref="IResult.IsSuccess"/>, 
        /// use the <paramref name="converter"/> to convert the value, 
        /// otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="IResult{T}"/>.</typeparam>
        /// <typeparam name="TOut">Output value type.</typeparam>
        /// <param name="result"><see cref="IResult{T}"/> to unwrap value.</param>
        /// <param name="converter">Function called to convert this <see cref="IResult{T}"/> value.</param>
        /// <param name="orFunc">Default value factory method.</param>
        /// <returns>The unwrapped value from this <see cref="IResult{T}"/> if it has value, otherwise the default value.</returns>
        [PublicAPI, Pure, CanBeNull]
        public static TOut Unwrap<T, TOut>(this IResult<T> result,
            [NotNull, InstantHandle] Func<T, TOut> converter,
            [NotNull] Func<TOut> orFunc)
        {
            if (result.IsSuccess)
                return converter(result.Value);
            return orFunc();
        }

        #endregion

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

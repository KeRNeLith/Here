using System;
using JetBrains.Annotations;

namespace Here.Results.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/>.
    /// </summary>
    public static class ResultExtensions
    {
        #region Result

        /// <summary>
        /// Call the <paramref name="onSuccess"/> action when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result OnSuccess(this Result result, [NotNull, InstantHandle] Action onSuccess, bool treatWarningAsError = false)
        {
            if (result.IsSuccess)
            {
                if (!result.IsWarning || (result.IsWarning && !treatWarningAsError))
                    onSuccess();
                else
                    return Result.Fail(result.Message);
            }

            return result;
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> computed with <paramref name="onSuccess"/>, or a failure corresponding to this one.</returns>
        [PublicAPI]
        public static Result OnSuccess(this Result result, [NotNull, InstantHandle] Func<Result> onSuccess, bool treatWarningAsError = false)
        {
            if (result.IsFailure || (treatWarningAsError && result.IsWarning))
                return Result.Fail(result.Message);

            return onSuccess();
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<T> OnSuccess<T>(this Result result, [NotNull, InstantHandle] Func<T> onSuccess, bool treatWarningAsError = false)
        {
            if (result.IsFailure || (treatWarningAsError && result.IsWarning))
                return Result.Fail<T>(result.Message);

            return new Result<T>(onSuccess(), result._logic);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<T> OnSuccess<T>(this Result result, [NotNull, InstantHandle] Func<Result<T>> onSuccess, bool treatWarningAsError = false)
        {
            if (result.IsFailure || (treatWarningAsError && result.IsWarning))
                return Result.Fail<T>(result.Message);

            return onSuccess();
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// Use the <paramref name="errorObject"/> if needed to create a <see cref="CustomResult{TError}"/> in cases 
        /// the <see cref="Result"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="errorObject">Custom error object to use if this <see cref="Result.IsFailure"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this Result result,
            [NotNull, InstantHandle] Func<CustomResult<TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (result.IsFailure || (treatWarningAsError && result.IsWarning))
                return Result.CustomFail(result.Message, errorObject);

            return onSuccess();
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// Call the <paramref name="errorFactory"/> function if needed to create a custom error object in cases 
        /// the <see cref="Result"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this Result result,
            [NotNull, InstantHandle] Func<CustomResult<TError>> onSuccess,
            [NotNull, InstantHandle] Func<TError> errorFactory,
            bool treatWarningAsError = false)
        {
            if (result.IsFailure || (treatWarningAsError && result.IsWarning))
                return Result.CustomFail(result.Message, errorFactory());

            return onSuccess();
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// Use the <paramref name="errorObject"/> if needed to create a Failure <see cref="Result{T, TError}"/> in cases 
        /// the <see cref="Result"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="T">Input result value type.</typeparam>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="errorObject">Custom error object to use if this <see cref="Result.IsFailure"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this Result result,
            [NotNull, InstantHandle] Func<Result<T, TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (result.IsFailure || (treatWarningAsError && result.IsWarning))
                return Result.Fail<T, TError>(result.Message, errorObject);

            return onSuccess();
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// Call the <paramref name="errorFactory"/> function if needed to create a custom error object in cases 
        /// the <see cref="Result"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="T">Input result value type.</typeparam>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this Result result, 
            [NotNull, InstantHandle] Func<Result<T, TError>> onSuccess, 
            [NotNull, InstantHandle] Func<TError> errorFactory, 
            bool treatWarningAsError = false)
        {
            if (result.IsFailure || (treatWarningAsError && result.IsWarning))
                return Result.Fail<T, TError>(result.Message, errorFactory());

            return onSuccess();
        }

        #endregion

        #region Result<T>

        // TODO
        
        #endregion

        #region CustomResult<TError>

        // TODO

        #endregion

        #region Result<T, TError>

        // TODO

        #endregion
    }
}

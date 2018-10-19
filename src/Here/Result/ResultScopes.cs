using System;
using JetBrains.Annotations;

namespace Here.Results
{
    /// <summary>
    /// Scopes to safely return a <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> or <see cref="Result{T,TError}"/>.
    /// </summary>
    [PublicAPI]
    public static class ResultScope
    {
        /// <summary>
        /// Run the given <paramref name="action"/> in a safe scope that always return a <see cref="Result"/>.
        /// </summary>
        /// <param name="action">Function to call.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        [PublicAPI]
        public static Result SafeResult([NotNull, InstantHandle] Action action)
        {
            try
            {
                action();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(
                    string.Format(ResultConstants.ResultScopeErrorMessage, ex.Message),
                    ex);
            }
        }

        /// <summary>
        /// Run the given <paramref name="action"/> in a safe scope that always return a <see cref="Result"/>.
        /// </summary>
        /// <param name="action">Function to call.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        [PublicAPI]
        public static Result SafeResult([NotNull, InstantHandle] Func<Result> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return Result.Fail(
                    string.Format(ResultConstants.ResultScopeErrorMessage, ex.Message), 
                    ex);
            }
        }

        /// <summary>
        /// Run the given <paramref name="action"/> in a safe scope that always return a <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="action">Function to call.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI]
        public static Result<T> SafeValueResult<T>([NotNull, InstantHandle] Func<Result<T>> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return Result.Fail<T>(
                    string.Format(ResultConstants.ValueResultScopeErrorMessage, ex.Message),
                    ex);
            }
        }

        /// <summary>
        /// Run the given <paramref name="action"/> in a safe scope that always return a <see cref="Result"/>.
        /// </summary>
        /// <param name="action">Function to call.</param>
        /// <param name="errorObject">Error object to return in case an exception is thrown.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI]
        public static CustomResult<TError> SafeCustomResult<TError>([NotNull, InstantHandle] Func<CustomResult<TError>> action, [NotNull] TError errorObject)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return Result.CustomFail(
                    string.Format(ResultConstants.CustomResultScopeErrorMessage, ex.Message),
                    errorObject,
                    ex);
            }
        }

        /// <summary>
        /// Run the given <paramref name="action"/> in a safe scope that always return a <see cref="Result"/>.
        /// </summary>
        /// <param name="action">Function to call.</param>
        /// <param name="errorFactory">Function to create a custom error object in case an exception is thrown.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI]
        public static CustomResult<TError> SafeCustomResult<TError>([NotNull, InstantHandle] Func<CustomResult<TError>> action, [NotNull] Func<TError> errorFactory)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return Result.CustomFail(
                    string.Format(ResultConstants.CustomResultScopeErrorMessage, ex.Message),
                    errorFactory(),
                    ex);
            }
        }

        /// <summary>
        /// Run the given <paramref name="action"/> in a safe scope that always return a <see cref="Result"/>.
        /// </summary>
        /// <param name="action">Function to call.</param>
        /// <param name="errorObject">Error object to return in case an exception is thrown.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI]
        public static Result<T, TError> SafeValueCustomResult<T, TError>([NotNull, InstantHandle] Func<Result<T, TError>> action, [NotNull] TError errorObject)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return Result.Fail<T, TError>(
                    string.Format(ResultConstants.ValueCustomResultScopeErrorMessage, ex.Message),
                    errorObject,
                    ex);
            }
        }

        /// <summary>
        /// Run the given <paramref name="action"/> in a safe scope that always return a <see cref="Result"/>.
        /// </summary>
        /// <param name="action">Function to call.</param>
        /// <param name="errorFactory">Function to create a custom error object in case an exception is thrown.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI]
        public static Result<T, TError> SafeValueCustomResult<T, TError>([NotNull, InstantHandle] Func<Result<T, TError>> action, [NotNull] Func<TError> errorFactory)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return Result.Fail<T, TError>(
                    string.Format(ResultConstants.ValueCustomResultScopeErrorMessage, ex.Message),
                    errorFactory(),
                    ex);
            }
        }
    }
}
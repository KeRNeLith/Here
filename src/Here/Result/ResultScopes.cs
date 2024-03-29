using System;
using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// Scopes to safely return a <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> or <see cref="Result{T,TError}"/>.
    /// </summary>
    [PublicAPI]
    public static class ResultScope
    {
        /// <summary>
        /// Runs the given <paramref name="action"/> in a safe scope that always returns a <see cref="Result"/>.
        /// </summary>
        /// <param name="action">Function to call.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="action"/> is null.</exception>
        [PublicAPI]
        public static Result SafeResult([NotNull, InstantHandle] in Action action)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));

            try
            {
                action();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex);
            }
        }

        /// <summary>
        /// Runs the given <paramref name="action"/> in a safe scope that always returns a <see cref="Result"/>.
        /// </summary>
        /// <param name="action">Function to call.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="action"/> is null.</exception>
        [PublicAPI]
        public static Result SafeResult([NotNull, InstantHandle] in Func<Result> action)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));

            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex);
            }
        }

        /// <summary>
        /// Runs the given <paramref name="action"/> in a safe scope that always returns a <see cref="Result{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="action">Function to call.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="action"/> is null.</exception>
        [PublicAPI]
        public static Result<T> SafeValueResult<T>([NotNull, InstantHandle] in Func<Result<T>> action)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));

            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return Result.Fail<T>(ex);
            }
        }

        /// <summary>
        /// Runs the given <paramref name="action"/> in a safe scope that always returns a <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="action">Function to call.</param>
        /// <param name="errorObject">Error object to return in case an exception is thrown.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="action"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> SafeCustomResult<TError>([NotNull, InstantHandle] in Func<CustomResult<TError>> action, [NotNull] in TError errorObject)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));
            if (errorObject == null)
                throw new ArgumentNullException(nameof(errorObject));

            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return Result.CustomFail(errorObject, ex);
            }
        }

        /// <summary>
        /// Runs the given <paramref name="action"/> in a safe scope that always returns a <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="action">Function to call.</param>
        /// <param name="errorFactory">Function to create a custom error object in case an exception is thrown.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="action"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> SafeCustomResult<TError>([NotNull, InstantHandle] in Func<CustomResult<TError>> action, [NotNull] in Func<TError> errorFactory)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));
            if (errorFactory is null)
                throw new ArgumentNullException(nameof(errorFactory));

            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return Result.CustomFail(errorFactory(), ex);
            }
        }

        /// <summary>
        /// Runs the given <paramref name="action"/> in a safe scope that always returns a <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="action">Function to call.</param>
        /// <param name="errorObject">Error object to return in case an exception is thrown.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="action"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> SafeValueCustomResult<T, TError>([NotNull, InstantHandle] in Func<Result<T, TError>> action, [NotNull] in TError errorObject)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));
            if (errorObject == null)
                throw new ArgumentNullException(nameof(errorObject));

            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return Result.Fail<T, TError>(errorObject, ex);
            }
        }

        /// <summary>
        /// Runs the given <paramref name="action"/> in a safe scope that always returns a <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="action">Function to call.</param>
        /// <param name="errorFactory">Function to create a custom error object in case an exception is thrown.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="action"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> SafeValueCustomResult<T, TError>([NotNull, InstantHandle] in Func<Result<T, TError>> action, [NotNull] in Func<TError> errorFactory)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));
            if (errorFactory is null)
                throw new ArgumentNullException(nameof(errorFactory));

            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return Result.Fail<T, TError>(errorFactory(), ex);
            }
        }
    }
}
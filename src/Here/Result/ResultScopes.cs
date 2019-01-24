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
        /// <exception cref="ArgumentNullException">If the <paramref name="action"/> is null.</exception>
        [PublicAPI]
        public static Result SafeResult([NotNull, InstantHandle] Action action)
        {
            Throw.IfArgumentNull(action, nameof(action));

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
        /// <exception cref="ArgumentNullException">If the <paramref name="action"/> is null.</exception>
        [PublicAPI]
        public static Result SafeResult([NotNull, InstantHandle] Func<Result> action)
        {
            Throw.IfArgumentNull(action, nameof(action));

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
        /// <param name="action">Function to call.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="action"/> is null.</exception>
        [PublicAPI]
        public static Result<T> SafeValueResult<T>([NotNull, InstantHandle] Func<Result<T>> action)
        {
            Throw.IfArgumentNull(action, nameof(action));

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
        /// <param name="action">Function to call.</param>
        /// <param name="errorObject">Error object to return in case an exception is thrown.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="action"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> SafeCustomResult<TError>([NotNull, InstantHandle] Func<CustomResult<TError>> action, [NotNull] TError errorObject)
        {
            Throw.IfArgumentNull(action, nameof(action));
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

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
        /// <param name="action">Function to call.</param>
        /// <param name="errorFactory">Function to create a custom error object in case an exception is thrown.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="action"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> SafeCustomResult<TError>([NotNull, InstantHandle] Func<CustomResult<TError>> action, [NotNull] Func<TError> errorFactory)
        {
            Throw.IfArgumentNull(action, nameof(action));
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

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
        /// <param name="action">Function to call.</param>
        /// <param name="errorObject">Error object to return in case an exception is thrown.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="action"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> SafeValueCustomResult<T, TError>([NotNull, InstantHandle] Func<Result<T, TError>> action, [NotNull] TError errorObject)
        {
            Throw.IfArgumentNull(action, nameof(action));
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

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
        /// <param name="action">Function to call.</param>
        /// <param name="errorFactory">Function to create a custom error object in case an exception is thrown.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="action"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> SafeValueCustomResult<T, TError>([NotNull, InstantHandle] Func<Result<T, TError>> action, [NotNull] Func<TError> errorFactory)
        {
            Throw.IfArgumentNull(action, nameof(action));
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

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
using System;
using JetBrains.Annotations;
using Here.Results;

namespace Here.Maybes
{
    // Operators
    public partial struct Maybe<T>
    {
        /// <summary>
        /// Checks if the <see cref="Maybe{T}"/> state is empty.
        /// It means that it <see cref="HasNoValue"/>.
        /// </summary>
        /// <param name="maybe"><see cref="Maybe{T}"/> to check.</param>
        /// <returns>True if <see cref="Maybe{T}"/> <see cref="HasNoValue"/>.</returns>
        [PublicAPI, Pure]
        public static bool operator !(in Maybe<T> maybe) => maybe.HasNoValue;

        /// <summary>
        /// Converts this <see cref="Maybe{T}"/> if it has a value to a <see cref="Maybe{TOut}"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the value embedded in the converted <see cref="Maybe{TOut}"/>.</typeparam>
        /// <returns>Converted <see cref="Maybe{TOut}"/>.</returns>
        [PublicAPI, Pure]
        public Maybe<TOut> Cast<TOut>()
            where TOut : class
        {
            if (HasValue)
                return Value as TOut;
            return Maybe<TOut>.None;
        }

        /// <summary>
        /// Returns this <see cref="Maybe{T}"/> as a <see cref="Maybe{TOut}"/> if it has a value and is of type <typeparamref name="TOut"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the value embedded in the resulting <see cref="Maybe{TOut}"/>.</typeparam>
        /// <returns>Cast <see cref="Maybe{TOut}"/>.</returns>
        [PublicAPI, Pure]
        public Maybe<TOut> OfType<TOut>()
            where TOut : class
        {
            return Cast<TOut>();
        }

        #region Gateway to Result

        /// <summary>
        /// Converts this <see cref="Maybe{T}"/> to a success <see cref="Result"/> if it has a value, or a failure if not.
        /// </summary>
        /// <param name="failureMessage">Failure message in case this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public Result ToResult([CanBeNull] in string failureMessage = null)
        {
            if (HasValue)
                return Result.Ok();
            return Result.Fail(failureMessage ?? string.Format(Maybe.FailedToResultMessage, typeof(T)));
        }

        /// <summary>
        /// Converts this <see cref="Maybe{T}"/> to a success <see cref="Result"/> if it has a value, or a failure if not.
        /// </summary>
        /// <param name="failureMessage">Failure message in case the <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T> ToValueResult([CanBeNull] in string failureMessage = null)
        {
            if (HasValue)
                return Result.Ok(Value);
            return Result.Fail<T>(failureMessage ?? string.Format(Maybe.FailedToResultMessage, typeof(T)));
        }

        /// <summary>
        /// Converts this <see cref="Maybe{T}"/> to a success <see cref="CustomResult{TError}"/> if it has a value, or a failure if not.
        /// </summary>
        /// <param name="errorFactory">Function that create the custom error object to use to construct the result in case this <see cref="Maybe{T}"/> has no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public CustomResult<TError> ToCustomResult<TError>([NotNull, InstantHandle] in Func<TError> errorFactory, [CanBeNull] in string failureMessage = null)
        {
            if (HasValue)
                return Result.CustomOk<TError>();
            return Result.CustomFail(failureMessage ?? string.Format(Maybe.FailedToResultMessage, typeof(T)), errorFactory());
        }

        /// <summary>
        /// Converts this <see cref="Maybe{T}"/> to a success <see cref="CustomResult{TError}"/> if it has a value, or a failure if not.
        /// </summary>
        /// <param name="errorObject">Custom error object to use to construct the result in case this <see cref="Maybe{T}"/> has no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public CustomResult<TError> ToCustomResult<TError>([NotNull] in TError errorObject, [CanBeNull] in string failureMessage = null)
        {
            if (HasValue)
                return Result.CustomOk<TError>();
            return Result.CustomFail(failureMessage ?? string.Format(Maybe.FailedToResultMessage, typeof(T)), errorObject);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{T}"/> to a success <see cref="Result{T, TError}"/> if it has a value, or a failure if not.
        /// </summary>
        /// <param name="errorFactory">Function that create the custom error object to use to construct the result in case this <see cref="Maybe{T}"/> has no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T, TError> ToValueCustomResult<TError>([NotNull, InstantHandle] in Func<TError> errorFactory, [CanBeNull] in string failureMessage = null)
        {
            if (HasValue)
                return Result.Ok<T, TError>(Value);
            return Result.Fail<T, TError>(failureMessage ?? string.Format(Maybe.FailedToResultMessage, typeof(T)), errorFactory());
        }

        /// <summary>
        /// Converts this <see cref="Maybe{T}"/> to a success <see cref="Result{T, TError}"/> if it has a value, or a failure if not.
        /// </summary>
        /// <param name="errorObject">Custom error object to use to construct the result in case this <see cref="Maybe{T}"/> has no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T, TError> ToValueCustomResult<TError>([NotNull] in TError errorObject, [CanBeNull] in string failureMessage = null)
        {
            if (HasValue)
                return Result.Ok<T, TError>(Value);
            return Result.Fail<T, TError>(failureMessage ?? string.Format(Maybe.FailedToResultMessage, typeof(T)), errorObject);
        }

        #endregion
    }
}

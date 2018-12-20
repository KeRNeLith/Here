using System;
using JetBrains.Annotations;

namespace Here
{
    // Operators
    public partial struct Option<T>
    {
        /// <summary>
        /// Checks if the <see cref="Option{T}"/> state is empty.
        /// It means that it <see cref="HasNoValue"/>.
        /// </summary>
        /// <param name="option"><see cref="Option{T}"/> to check.</param>
        /// <returns>True if <see cref="Option{T}"/> <see cref="HasNoValue"/>.</returns>
        [PublicAPI, Pure]
        public static bool operator !(in Option<T> option) => option.HasNoValue;

        /// <summary>
        /// Converts this <see cref="Option{T}"/> if it has a value to an <see cref="Option{T}"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the value embedded in the converted <see cref="Option{T}"/>.</typeparam>
        /// <returns>Converted <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure]
        public Option<TOut> Cast<TOut>()
            where TOut : class
        {
            if (HasValue)
                return Value as TOut;
            return Option<TOut>.None;
        }

        /// <summary>
        /// Returns this <see cref="Option{T}"/> as an <see cref="Option{T}"/> if it has a value and is of type <typeparamref name="TOut"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the value embedded in the resulting <see cref="Option{T}"/>.</typeparam>
        /// <returns>Cast <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure]
        public Option<TOut> OfType<TOut>()
            where TOut : class
        {
            return Cast<TOut>();
        }

        #region Gateway to Result

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to a success <see cref="Result"/> if it has a value, or a failure if not.
        /// </summary>
        /// <param name="failureMessage">Failure message in case this <see cref="Option{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public Result ToResult([CanBeNull] in string failureMessage = null)
        {
            if (HasValue)
                return Result.Ok();
            return Result.Fail(failureMessage ?? string.Format(Option.FailedToResultMessage, typeof(T)));
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to a success <see cref="Result"/> if it has a value, or a failure if not.
        /// </summary>
        /// <param name="failureMessage">Failure message in case the <see cref="Option{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T> ToValueResult([CanBeNull] in string failureMessage = null)
        {
            if (HasValue)
                return Result.Ok(Value);
            return Result.Fail<T>(failureMessage ?? string.Format(Option.FailedToResultMessage, typeof(T)));
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to a success <see cref="CustomResult{TError}"/> if it has a value, or a failure if not.
        /// </summary>
        /// <param name="errorFactory">Function that create the custom error object to use to construct the result in case this <see cref="Option{T}"/> has no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Option{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI, Pure]
        public CustomResult<TError> ToCustomResult<TError>([NotNull, InstantHandle] in Func<TError> errorFactory, [CanBeNull] in string failureMessage = null)
        {
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (HasValue)
                return Result.CustomOk<TError>();
            return Result.CustomFail(failureMessage ?? string.Format(Option.FailedToResultMessage, typeof(T)), errorFactory());
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to a success <see cref="CustomResult{TError}"/> if it has a value, or a failure if not.
        /// </summary>
        /// <param name="errorObject">Custom error object to use to construct the result in case this <see cref="Option{T}"/> has no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Option{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI, Pure]
        public CustomResult<TError> ToCustomResult<TError>([NotNull] in TError errorObject, [CanBeNull] in string failureMessage = null)
        {
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

            if (HasValue)
                return Result.CustomOk<TError>();
            return Result.CustomFail(failureMessage ?? string.Format(Option.FailedToResultMessage, typeof(T)), errorObject);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to a success <see cref="Result{T, TError}"/> if it has a value, or a failure if not.
        /// </summary>
        /// <param name="errorFactory">Function that create the custom error object to use to construct the result in case this <see cref="Option{T}"/> has no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Option{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI, Pure]
        public Result<T, TError> ToValueCustomResult<TError>([NotNull, InstantHandle] in Func<TError> errorFactory, [CanBeNull] in string failureMessage = null)
        {
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (HasValue)
                return Result.Ok<T, TError>(Value);
            return Result.Fail<T, TError>(failureMessage ?? string.Format(Option.FailedToResultMessage, typeof(T)), errorFactory());
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to a success <see cref="Result{T, TError}"/> if it has a value, or a failure if not.
        /// </summary>
        /// <param name="errorObject">Custom error object to use to construct the result in case this <see cref="Option{T}"/> has no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Option{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI, Pure]
        public Result<T, TError> ToValueCustomResult<TError>([NotNull] in TError errorObject, [CanBeNull] in string failureMessage = null)
        {
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

            if (HasValue)
                return Result.Ok<T, TError>(Value);
            return Result.Fail<T, TError>(failureMessage ?? string.Format(Option.FailedToResultMessage, typeof(T)), errorObject);
        }

        #endregion
    }
}

using System;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Option{T}"/> for conversions.
    /// </summary>
    public static class OptionOperatorsExtensions
    {
        #region Gateway to Result

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to a success <see cref="Result"/> if it has a value, or a failure if not.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to convert.</param>
        /// <param name="failureMessage">Failure message in case this <see cref="Option{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public static Result ToResult<T>(in this Option<T> option, [CanBeNull] in string failureMessage = null)
        {
            if (option.HasValue)
                return Result.Ok();
            return Result.Fail(failureMessage ?? string.Format(Option.FailedToResultMessage, typeof(T)));
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to a success <see cref="Result"/> if it has a value, or a failure if not.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to convert.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Option{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Result<T> ToValueResult<T>(in this Option<T> option, [CanBeNull] in string failureMessage = null)
        {
            if (option.HasValue)
                return Result.Ok(option._value);
            return Result.Fail<T>(failureMessage ?? string.Format(Option.FailedToResultMessage, typeof(T)));
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to a success <see cref="CustomResult{TError}"/> if it has a value, or a failure if not.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TError">Type of the <see cref="CustomResult{TError}"/> error.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to convert.</param>
        /// <param name="errorFactory">Function that create the custom error object to use to construct the result in case this <see cref="Option{T}"/> has no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Option{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI, Pure]
        public static CustomResult<TError> ToCustomResult<T, TError>(in this Option<T> option, 
            [NotNull, InstantHandle] in Func<TError> errorFactory, 
            [CanBeNull] in string failureMessage = null)
        {            if (errorFactory is null)
                throw new ArgumentNullException(nameof(errorFactory));

            if (option.HasValue)
                return Result.CustomOk<TError>();
            return Result.CustomFail(failureMessage ?? string.Format(Option.FailedToResultMessage, typeof(T)), errorFactory());
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to a success <see cref="CustomResult{TError}"/> if it has a value, or a failure if not.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TError">Type of the <see cref="CustomResult{TError}"/> error.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to convert.</param>
        /// <param name="errorObject">Custom error object to use to construct the result in case this <see cref="Option{T}"/> has no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Option{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI, Pure]
        public static CustomResult<TError> ToCustomResult<T, TError>(in this Option<T> option, 
            [NotNull] in TError errorObject, 
            [CanBeNull] in string failureMessage = null)
        {            if (errorObject == null)
                throw new ArgumentNullException(nameof(errorObject));

            if (option.HasValue)
                return Result.CustomOk<TError>();
            return Result.CustomFail(failureMessage ?? string.Format(Option.FailedToResultMessage, typeof(T)), errorObject);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to a success <see cref="Result{T, TError}"/> if it has a value, or a failure if not.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TError">Type of the <see cref="Result{T, TError}"/> error.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to convert.</param>
        /// <param name="errorFactory">Function that create the custom error object to use to construct the result in case this <see cref="Option{T}"/> has no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Option{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI, Pure]
        public static Result<T, TError> ToValueCustomResult<T, TError>(in this Option<T> option, 
            [NotNull, InstantHandle] in Func<TError> errorFactory, 
            [CanBeNull] in string failureMessage = null)
        {            if (errorFactory is null)
                throw new ArgumentNullException(nameof(errorFactory));

            if (option.HasValue)
                return Result.Ok<T, TError>(option._value);
            return Result.Fail<T, TError>(failureMessage ?? string.Format(Option.FailedToResultMessage, typeof(T)), errorFactory());
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to a success <see cref="Result{T, TError}"/> if it has a value, or a failure if not.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TError">Type of the <see cref="Result{T, TError}"/> error.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to convert.</param>
        /// <param name="errorObject">Custom error object to use to construct the result in case this <see cref="Option{T}"/> has no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Option{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI, Pure]
        public static Result<T, TError> ToValueCustomResult<T, TError>(in this Option<T> option, 
            [NotNull] in TError errorObject, 
            [CanBeNull] in string failureMessage = null)
        {            if (errorObject == null)
                throw new ArgumentNullException(nameof(errorObject));

            if (option.HasValue)
                return Result.Ok<T, TError>(option._value);
            return Result.Fail<T, TError>(failureMessage ?? string.Format(Option.FailedToResultMessage, typeof(T)), errorObject);
        }

        #endregion

        #region Gateway to Either

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Either{TLeft,T}"/> in
        /// <see cref="EitherStates.Right"/> state if it has a value, otherwise in a <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to convert.</param>
        /// <param name="defaultLeftValue">Default left value to use if the <see cref="Option{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="Either{TLeft,T}"/>.</returns>
        [PublicAPI, Pure]
        public static Either<TLeft, T> ToEither<T, TLeft>(in this Option<T> option, [NotNull] in TLeft defaultLeftValue)
        {            if (defaultLeftValue == null)
                throw new ArgumentNullException(nameof(defaultLeftValue));

            if (option.HasValue)
                return Either<TLeft, T>.Right(option._value);
            return Either<TLeft, T>.Left(defaultLeftValue);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Either{TLeft,T}"/> in
        /// <see cref="EitherStates.Right"/> state if it has a value, otherwise in a <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to convert.</param>
        /// <param name="leftValueFactory">Function called to create a left value if the
        /// <see cref="Option{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="Either{TLeft,T}"/>.</returns>
        [PublicAPI, Pure]
        public static Either<TLeft, T> ToEither<T, TLeft>(in this Option<T> option, [NotNull, InstantHandle] in Func<TLeft> leftValueFactory)
        {            if (leftValueFactory is null)
                throw new ArgumentNullException(nameof(leftValueFactory));

            if (option.HasValue)
                return Either<TLeft, T>.Right(option._value);
            return Either<TLeft, T>.Left(leftValueFactory());
        }

        #endregion
    }
}

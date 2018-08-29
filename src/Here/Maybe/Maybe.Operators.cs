using System;
using JetBrains.Annotations;
using Here.Results;

namespace Here.Maybes
{
    // Operators
    public partial struct Maybe<T>
    {
        /// <summary>
        /// Check the <see cref="Maybe{T}"/> state, it matches true if it <see cref="HasValue"/>.
        /// </summary>
        /// <param name="maybe">Maybe to check.</param>
        /// <returns>True if <see cref="Maybe{T}"/> <see cref="HasValue"/>.</returns>
        [PublicAPI, Pure]
        public static bool operator true(Maybe<T> maybe) => maybe.HasValue;

        /// <summary>
        /// Check the <see cref="Maybe{T}"/> state, it matches false if it <see cref="HasNoValue"/>.
        /// </summary>
        /// <param name="maybe">Maybe to check.</param>
        /// <returns>True if <see cref="Maybe{T}"/> <see cref="HasNoValue"/>.</returns>
        [PublicAPI, Pure]
        public static bool operator false(Maybe<T> maybe) => maybe.HasNoValue;

        /// <summary>
        /// Check if the <see cref="Maybe{T}"/> state is empty.
        /// It means it <see cref="HasNoValue"/>).
        /// </summary>
        /// <param name="maybe">Maybe to check.</param>
        /// <returns>True if <see cref="Maybe{T}"/> <see cref="HasNoValue"/>.</returns>
        [PublicAPI, Pure]
        public static bool operator !(Maybe<T> maybe) => maybe.HasNoValue;
        
        /// <summary>
        /// Convert this <see cref="Maybe{T}"/> if it has a value to a <see cref="Maybe{TOut}"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the value embedded in the converted <see cref="Maybe{TOut}"/>.</typeparam>
        /// <returns>The conversion of this <see cref="Maybe{T}"/> to <see cref="Maybe{TOut}"/>.</returns>
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
		/// <returns>This casted as <see cref="Maybe{TOut}"/>.</returns>
        [PublicAPI, Pure]
        public Maybe<TOut> OfType<TOut>()
            where TOut : class
        {
            return Cast<TOut>();
        }

        #region Gateway to Result

        /// <summary>
        /// Convert this <see cref="Maybe{T}"/> to a <see cref="Result.IsSuccess"/> if it has a value, or a <see cref="Result.IsFailure"/> if not.
        /// </summary>
        /// <param name="failureMessage">Failure message in case the <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>The corresponding <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public Result ToResult([CanBeNull] string failureMessage = null)
        {
            if (HasValue)
                return Result.Ok();
            return Result.Fail(failureMessage ?? string.Format(Maybe.FailedToResultMessage, typeof(T)));
        }

        /// <summary>
		/// Convert this <see cref="Maybe{T}"/> to a <see cref="Result{T}.IsSuccess"/> if it has a value, or a <see cref="Result{T}.IsFailure"/> if not.
		/// </summary>
        /// <param name="failureMessage">Failure message in case the <see cref="Maybe{T}"/> has no value.</param>
		/// <returns>The corresponding <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T> ToValueResult([CanBeNull] string failureMessage = null)
        {
            if (HasValue)
                return Result.Ok(Value);
            return Result.Fail<T>(failureMessage ?? string.Format(Maybe.FailedToResultMessage, typeof(T)));
        }

        /// <summary>
		/// Convert this <see cref="Maybe{T}"/> to a <see cref="CustomResult{TError}.IsSuccess"/> if it has a value, or a <see cref="CustomResult{TError}.IsFailure"/> if not.
		/// </summary>
        /// <param name="errorFactory">Function to create a custom error object in case this <see cref="Maybe{T}"/> ha no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Maybe{T}"/> has no value.</param>
		/// <returns>The corresponding <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public CustomResult<TError> ToCustomResult<TError>([NotNull, InstantHandle] Func<TError> errorFactory, [CanBeNull] string failureMessage = null)
        {
            if (HasValue)
                return Result.CustomOk<TError>();
            return Result.CustomFail(failureMessage ?? string.Format(Maybe.FailedToResultMessage, typeof(T)), errorFactory());
        }

        /// <summary>
		/// Convert this <see cref="Maybe{T}"/> to a <see cref="CustomResult{TError}.IsSuccess"/> if it has a value, or a <see cref="CustomResult{TError}.IsFailure"/> if not.
		/// </summary>
        /// <param name="errorObject">Error object to create a custom error object in case this <see cref="Maybe{T}"/> ha no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Maybe{T}"/> has no value.</param>
		/// <returns>The corresponding <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public CustomResult<TError> ToCustomResult<TError>([NotNull] TError errorObject, [CanBeNull] string failureMessage = null)
        {
            if (HasValue)
                return Result.CustomOk<TError>();
            return Result.CustomFail(failureMessage ?? string.Format(Maybe.FailedToResultMessage, typeof(T)), errorObject);
        }

        /// <summary>
		/// Convert this <see cref="Maybe{T}"/> to a <see cref="Result{T, TError}.IsSuccess"/> if it has a value, or a <see cref="Result{T, TError}.IsFailure"/> if not.
		/// </summary>
        /// <param name="errorFactory">Function to create a custom error object in case this <see cref="Maybe{T}"/> ha no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Maybe{T}"/> has no value.</param>
		/// <returns>The corresponding <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T, TError> ToCustomValueResult<TError>([NotNull, InstantHandle] Func<TError> errorFactory, [CanBeNull] string failureMessage = null)
        {
            if (HasValue)
                return Result.Ok<T, TError>(Value);
            return Result.Fail<T, TError>(failureMessage ?? string.Format(Maybe.FailedToResultMessage, typeof(T)), errorFactory());
        }

        /// <summary>
		/// Convert this <see cref="Maybe{T}"/> to a <see cref="Result{T, TError}.IsSuccess"/> if it has a value, or a <see cref="Result{T, TError}.IsFailure"/> if not.
		/// </summary>
        /// <param name="errorObject">Error object to create a custom error object in case this <see cref="Maybe{T}"/> ha no value.</param>
        /// <param name="failureMessage">Failure message in case the <see cref="Maybe{T}"/> has no value.</param>
		/// <returns>The corresponding <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T, TError> ToCustomValueResult<TError>([NotNull] TError errorObject, [CanBeNull] string failureMessage = null)
        {
            if (HasValue)
                return Result.Ok<T, TError>(Value);
            return Result.Fail<T, TError>(failureMessage ?? string.Format(Maybe.FailedToResultMessage, typeof(T)), errorObject);
        }

        #endregion
    }
}

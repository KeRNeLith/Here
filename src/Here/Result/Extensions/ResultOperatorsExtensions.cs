using System;
using static Here.ResultConstants;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> for conversions.
    /// </summary>
    public static class ResultOperatorsExtensions
    {
        #region Gateway to Option

        /// <summary>
        /// Converts this <see cref="Result"/> to an <see cref="Option{Boolean}"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to convert.</param>
        /// <returns>An <see cref="Option{Boolean}"/>.</returns>
        public static Option<bool> ToOption(in this Result result)
        {
            return Option<bool>.Some(result.Logic.IsSuccess);
        }

        /// <summary>
        /// Converts this <see cref="Result{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to convert.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        public static Option<T> ToOption<T>(in this Result<T> result)
        {
            if (result.Logic.IsSuccess)
                return result._value;
            return Option<T>.None;
        }

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to an <see cref="Option{Boolean}"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to convert.</param>
        /// <returns>An <see cref="Option{Boolean}"/>.</returns>
        public static Option<bool> ToOption<TError>(in this CustomResult<TError> result)
        {
            return Option<bool>.Some(result.Logic.IsSuccess);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to convert.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        public static Option<T> ToOption<T, TError>(in this Result<T, TError> result)
        {
            if (result.Logic.IsSuccess)
                return result._value;
            return Option<T>.None;
        }

        #endregion

        #region Gateway to Either

        /// <summary>
        /// Converts this <see cref="Result{T}"/> to an <see cref="Either{String,T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to convert.</param>
        /// <returns>An <see cref="Either{String, T}"/>.</returns>
        public static Either<string, T> ToEither<T>(in this Result<T> result)
        {
            if (result.IsSuccess)
            {
                if (result._value == null)
                    return Either<string, T>.Left(ValueResultToEitherNullValue);
                return Either<string, T>.Right(result._value);
            }

            return Either<string, T>.Left(result.Message);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to an <see cref="Either{TError,T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to convert.</param>
        /// <returns>An <see cref="Either{TError,T}"/>.</returns>
        public static Either<TError, T> ToEither<T, TError>(in this Result<T, TError> result)
        {
            if (result.IsSuccess)
            {
                if (result._value == null)
                {
                    throw new InvalidOperationException(
                        "Cannot convert a Result<T, TError> to an Either<TError, T> while the result has a null value.");
                }

                return Either<TError, T>.Right(result._value);
            }

            return Either<TError, T>.Left(result.Error);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to an <see cref="Either{String,T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to convert.</param>
        /// <returns>An <see cref="Either{String,T}"/>.</returns>
        public static Either<string, T> ToMessageEither<T, TError>(in this Result<T, TError> result)
        {
            if (result.IsSuccess)
            {
                if (result._value == null)
                    return Either<string, T>.Left(ValueCustomResultToEitherNullValue);
                return Either<string, T>.Right(result._value);
            }

            return Either<string, T>.Left(result.Message);
        }

        #endregion
    }
}

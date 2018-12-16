using System;
using static Here.ResultConstants;

namespace Here
{
    // Operators
    public partial struct Result
    {
        #region Gateway to Option

        /// <summary>
        /// Converts this <see cref="Result"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        public Option<bool> ToOption()
        {
            return Option<bool>.Some(Logic.IsSuccess);
        }

        #endregion
    }

    public partial struct Result<T>
    {
        #region Gateway to Option

        /// <summary>
        /// Converts this <see cref="Result{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        public Option<T> ToOption()
        {
            if (Logic.IsSuccess)
                return _value;
            return Option<T>.None;
        }

        #endregion

        #region Gateway to Either

        /// <summary>
        /// Converts this <see cref="Result{T}"/> to an <see cref="Either{String,T}"/>.
        /// </summary>
        /// <returns>An <see cref="Either{String,T}"/>.</returns>
        public Either<string, T> ToEither()
        {
            if (IsSuccess)
            {
                if (_value == null)
                    return Either<string, T>.Left(ValueResultToEitherNullValue);
                return Either<string, T>.Right(_value);
            }

            return Either<string, T>.Left(Message);
        }

        #endregion
    }

    // ReSharper disable once UnusedTypeParameter
    public partial struct CustomResult<TError>
    {
        #region Gateway to Option

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        public Option<bool> ToOption()
        {
            return Option<bool>.Some(Logic.IsSuccess);
        }

        #endregion
    }

    // ReSharper disable once UnusedTypeParameter
    public partial struct Result<T, TError>
    {
        #region Gateway to Option

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        public Option<T> ToOption()
        {
            if (Logic.IsSuccess)
                return _value;
            return Option<T>.None;
        }

        #endregion

        #region Gateway to Either

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to an <see cref="Either{String,T}"/>.
        /// </summary>
        /// <returns>An <see cref="Either{String,T}"/>.</returns>
        public Either<string, T> ToMessageEither()
        {
            if (IsSuccess)
            {
                if (_value == null)
                    return Either<string, T>.Left(ValueCustomResultToEitherNullValue);
                return Either<string, T>.Right(_value);
            }

            return Either<string, T>.Left(Message);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to an <see cref="Either{TError,T}"/>.
        /// </summary>
        /// <returns>An <see cref="Either{TError,T}"/>.</returns>
        public Either<TError, T> ToEither()
        {
            if (IsSuccess)
            {
                if (_value == null)
                {
                    throw new InvalidOperationException(
                        "Cannot convert a Result<T, TError> to an Either<TError, T> while the result has a null value.");
                }

                return Either<TError, T>.Right(_value);
            }

            return Either<TError, T>.Left(Error);
        }

        #endregion
    }
}

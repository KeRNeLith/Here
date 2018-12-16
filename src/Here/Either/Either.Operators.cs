using System;

namespace Here
{
    // Operators
    public partial struct Either<TLeft, TRight>
    {
        #region Gateway to Option

        /// <summary>
        /// Converts this <see cref="Either{TLeft,TRight}"/> to an <see cref="Option{TRight}"/>.
        /// </summary>
        /// <returns>An <see cref="Option{TRight}"/>.</returns>
        public Option<TRight> ToOption()
        {
            if (IsRight)
                return Option<TRight>.Some(_right);
            return Option<TRight>.None;
        }

        #endregion

        #region Gateway to Result

        /// <summary>
        /// Converts this <see cref="Either{TLeft,TRight}"/> to a <see cref="Result"/>.
        /// </summary>
        /// <returns>A <see cref="Result"/>.</returns>
        public Result ToResult()
        {
            if (IsRight)
                return Result.Ok();
            return Result.Fail(ResultConstants.EitherToFailedResult);
        }

        /// <summary>
        /// Converts this <see cref="Either{TLeft,TRight}"/> to a <see cref="Result{TRight}"/>.
        /// </summary>
        /// <returns>A <see cref="Result{TRight}"/>.</returns>
        public Result<TRight> ToValueResult()
        {
            if (IsRight)
                return Result.Ok(_right);
            return Result.Fail<TRight>(ResultConstants.EitherToFailedResult);
        }

        /// <summary>
        /// Converts this <see cref="Either{TLeft,TRight}"/> to a <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        public CustomResult<TLeft> ToCustomResult()
        {
            if (IsRight)
                return Result.CustomOk<TLeft>();
            if (IsLeft)
                return Result.CustomFail(ResultConstants.EitherToFailedResult, _left);

            throw new InvalidOperationException("Cannot convert an Either<TLeft, TRight> in none state to a CustomResult<TLeft>.");
        }

        /// <summary>
        /// Converts this <see cref="Either{TLeft,TRight}"/> to a <see cref="Result{TRight, TLeft}"/>.
        /// </summary>
        /// <returns>A <see cref="Result{TRight, TLeft}"/>.</returns>
        public Result<TRight, TLeft> ToValueCustomResult()
        {
            if (IsRight)
                return Result.Ok<TRight, TLeft>(_right);
            if (IsLeft)
                return Result.Fail<TRight, TLeft>(ResultConstants.EitherToFailedResult, _left);

            throw new InvalidOperationException("Cannot convert an Either<TLeft, TRight> in none state to a CustomResult<TLeft>.");
        }

        #endregion
    }
}
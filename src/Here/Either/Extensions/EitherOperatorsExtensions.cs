using System;
using static Here.ResultConstants;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Either{TLeft,TRight}"/> for conversions.
    /// </summary>
    public static class EitherOperatorsExtensions
    {
        #region Gateway to Option

        /// <summary>
        /// Converts this <see cref="Either{TLeft,TRight}"/> to an <see cref="Option{TRight}"/>.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to convert.</param>
        /// <returns>An <see cref="Option{TRight}"/>.</returns>
        public static Option<TRight> ToOption<TLeft, TRight>(in this Either<TLeft, TRight> either)
        {
            if (either.IsRight)
                return Option<TRight>.Some(either._right);
            return Option<TRight>.None;
        }

        #endregion

        #region Gateway to Result

        /// <summary>
        /// Converts this <see cref="Either{TLeft,TRight}"/> to a <see cref="Result"/>.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to convert.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        public static Result ToResult<TLeft, TRight>(in this Either<TLeft, TRight> either)
        {
            if (either.IsRight)
                return Result.Ok();
            return Result.Fail(EitherToFailedResult);
        }

        /// <summary>
        /// Converts this <see cref="Either{TLeft,TRight}"/> to a <see cref="Result"/>.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to convert.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        public static Result ToResult<TRight>(in this Either<string, TRight> either)
        {
            if (either.IsRight)
                return Result.Ok();
            if (either.IsLeft)
                return Result.Fail(either._left);
            return Result.Fail(EitherToFailedResult);
        }

        /// <summary>
        /// Converts this <see cref="Either{TLeft,TRight}"/> to a <see cref="Result{TRight}"/>.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to convert.</param>
        /// <returns>A <see cref="Result{TRight}"/>.</returns>
        public static Result<TRight> ToValueResult<TLeft, TRight>(in this Either<TLeft, TRight> either)
        {
            if (either.IsRight)
                return Result.Ok(either._right);
            return Result.Fail<TRight>(EitherToFailedResult);
        }

        /// <summary>
        /// Converts this <see cref="Either{TLeft,TRight}"/> to a <see cref="Result{TRight}"/>.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to convert.</param>
        /// <returns>A <see cref="Result{TRight}"/>.</returns>
        public static Result<TRight> ToValueResult<TRight>(in this Either<string, TRight> either)
        {
            if (either.IsRight)
                return Result.Ok(either._right);
            if (either.IsLeft)
                return Result.Fail<TRight>(either._left);
            return Result.Fail<TRight>(EitherToFailedResult);
        }

        /// <summary>
        /// Converts this <see cref="Either{TLeft,TRight}"/> to a <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to convert.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="T:System.InvalidOperationException">If the either is in <see cref="EitherStates.None"/> state.</exception>
        public static CustomResult<TLeft> ToCustomResult<TLeft, TRight>(in this Either<TLeft, TRight> either)
        {
            if (either.IsRight)
                return Result.CustomOk<TLeft>();
            if (either.IsLeft)
                return Result.CustomFail(EitherToFailedResult, either._left);

            throw new InvalidOperationException("Cannot convert an Either<TLeft, TRight> in none state to a CustomResult<TLeft>.");
        }

        /// <summary>
        /// Converts this <see cref="Either{TLeft,TRight}"/> to a <see cref="Result{TRight, TLeft}"/>.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to convert.</param>
        /// <returns>A <see cref="Result{TRight, TLeft}"/>.</returns>
        /// <exception cref="T:System.InvalidOperationException">If the either is in <see cref="EitherStates.None"/> state.</exception>
        public static Result<TRight, TLeft> ToValueCustomResult<TLeft, TRight>(in this Either<TLeft, TRight> either)
        {
            if (either.IsRight)
                return Result.Ok<TRight, TLeft>(either._right);
            if (either.IsLeft)
                return Result.Fail<TRight, TLeft>(EitherToFailedResult, either._left);

            throw new InvalidOperationException("Cannot convert an Either<TLeft, TRight> in none state to a CustomResult<TLeft>.");
        }

        #endregion
    }
}
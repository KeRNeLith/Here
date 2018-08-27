using JetBrains.Annotations;
using Here.Maybes;

namespace Here.Results
{
    // Operators
    public partial struct Result
    {
        /// <summary>
        /// Check the <see cref="Result"/> state, it matches true if it <see cref="IsSuccess"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <returns>True if <see cref="IsSuccess"/>.</returns>
        [PublicAPI, Pure]
        public static bool operator true(Result result) => result.IsSuccess;

        /// <summary>
        /// Check the <see cref="Result"/> state, it matches false if it <see cref="IsFailure"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <returns>True if <see cref="IsFailure"/>.</returns>
        [PublicAPI, Pure]
        public static bool operator false(Result result) => result.IsFailure;

        /// <summary>
        /// Check if the <see cref="Result"/> state <see cref="IsFailure"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <returns>True if <see cref="IsFailure"/>.</returns>
        [PublicAPI, Pure]
        public static bool operator !(Result result) => result.IsFailure;

        /// <summary>
        /// Perform the bitwise OR of given operands.
        /// </summary>
        /// <param name="leftOperand">First <see cref="Result"/> operand.</param>
        /// <param name="rightOperand">Second <see cref="Result"/> operand.</param>
        /// <returns>The first operand that <see cref="IsSuccess"/>, otherwise a failure <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public static Result operator |(Result leftOperand, Result rightOperand)
        {
            if (leftOperand.IsSuccess)
                return leftOperand;
            if (rightOperand.IsSuccess)
                return rightOperand;
            return Fail(ResultConstants.ResultBitwiseOrOperatorErrorMessage);
        }

        /// <summary>
        /// Perform the bitwise AND of given operands.
        /// </summary>
        /// <param name="leftOperand">First <see cref="Result"/> operand.</param>
        /// <param name="rightOperand">Second <see cref="Result"/> operand.</param>
        /// <returns>The last operand that <see cref="IsSuccess"/>, otherwise a failure <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public static Result operator &(Result leftOperand, Result rightOperand)
        {
            if (leftOperand.IsSuccess && rightOperand.IsSuccess)
                return rightOperand;
            return Fail(ResultConstants.ResultBitwiseAndOperatorErrorMessage);
        }

        #region Gateway to Maybe

        /// <summary>
        /// Convert this <see cref="Result"/> to a <see cref="Maybe{Boolean}"/>.
        /// </summary>
        /// <returns>The corresponding <see cref="Maybe{Boolean}"/>.</returns>
        public Maybe<bool> ToMaybe()
        {
            return Maybe<bool>.Some(Logic.IsSuccess);
        }

        #endregion
    }

    public partial struct Result<T>
    {
        #region Gateway to Maybe

        /// <summary>
        /// Convert this <see cref="Result{T}"/> to a <see cref="Maybe{T}"/>.
        /// </summary>
        /// <returns>The corresponding <see cref="Maybe{T}"/>.</returns>
        public Maybe<T> ToMaybe()
        {
            if (Logic.IsSuccess)
                return Value;
            return Maybe<T>.None;
        }

        #endregion
    }

    public partial struct CustomResult<TError>
    {
        #region Gateway to Maybe

        /// <summary>
        /// Convert this <see cref="CustomResult{TError}"/> to a <see cref="Maybe{Boolean}"/>.
        /// </summary>
        /// <returns>The corresponding <see cref="Maybe{Boolean}"/>.</returns>
        public Maybe<bool> ToMaybe()
        {
            return Maybe<bool>.Some(Logic.IsSuccess);
        }

        #endregion
    }

    public partial struct Result<T, TError>
    {
        #region Gateway to Maybe

        /// <summary>
        /// Convert this <see cref="Result{T, TError}"/> to a <see cref="Maybe{T}"/>.
        /// </summary>
        /// <returns>The corresponding <see cref="Maybe{T}"/>.</returns>
        public Maybe<T> ToMaybe()
        {
            if (Logic.IsSuccess)
                return Value;
            return Maybe<T>.None;
        }

        #endregion
    }
}

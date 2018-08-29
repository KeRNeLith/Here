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

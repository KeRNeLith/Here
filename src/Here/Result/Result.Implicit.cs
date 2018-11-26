using JetBrains.Annotations;

namespace Here
{
    // Implicit operators
    public partial struct Result
    {
        /// <summary>
        /// Implicit conversion from <see cref="Result"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to convert.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Option<bool>(in Result result)
        {
            return result.ToOption();
        }
    }

    public partial struct Result<T>
    {
        /// <summary>
        /// Implicit conversion from <see cref="Result{T}"/> to a <see cref="Result"/>.
        /// </summary>
        /// <param name="result"><see cref="Result{T}"/> to convert.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Result(in Result<T> result)
        {
            return new Result(result.Logic);
        }

        /// <summary>
        /// Implicit conversion from <see cref="Result{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="result"><see cref="Result{T}"/> to convert.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Option<T>(in Result<T> result)
        {
            return result.ToOption();
        }
    }

    public partial struct CustomResult<TError>
    {
        /// <summary>
        /// Implicit conversion from <see cref="CustomResult{TError}"/> to a <see cref="Result"/>.
        /// </summary>
        /// <param name="result"><see cref="CustomResult{TError}"/> to convert.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Result(in CustomResult<TError> result)
        {
            return new Result(ResultLogic.ToResultLogic(result.Logic));
        }

        /// <summary>
        /// Implicit conversion from <see cref="CustomResult{TError}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="result"><see cref="CustomResult{TError}"/> to convert.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Option<bool>(in CustomResult<TError> result)
        {
            return result.ToOption();
        }
    }

    public partial struct Result<T, TError>
    {
        /// <summary>
        /// Implicit conversion from <see cref="Result{T, TError}"/> to a <see cref="Result"/>.
        /// </summary>
        /// <param name="result"><see cref="Result{T, TError}"/> to convert.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Result(in Result<T, TError> result)
        {
            return new Result(ResultLogic.ToResultLogic(result.Logic));
        }

        /// <summary>
        /// Implicit conversion from <see cref="Result{T, TError}"/> to a <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <param name="result"><see cref="Result{T, TError}"/> to convert.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator CustomResult<TError>(in Result<T, TError> result)
        {
            return new CustomResult<TError>(result.Logic);
        }

        /// <summary>
        /// Implicit conversion from <see cref="Result{T, TError}"/> to a <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="result"><see cref="Result{T, TError}"/> to convert.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Result<T>(in Result<T, TError> result)
        {
            return new Result<T>(
                result.IsSuccess ? result.Value : default,
                ResultLogic.ToResultLogic(result.Logic));
        }

        /// <summary>
        /// Implicit conversion from <see cref="Result{T, TError}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="result"><see cref="Result{T}"/> to convert.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Option<T>(in Result<T, TError> result)
        {
            return result.ToOption();
        }
    }
}

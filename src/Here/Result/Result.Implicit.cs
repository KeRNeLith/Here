namespace Here.Results
{
	// Implicit operators
    public partial struct Result<T>
    {
        /// <summary>
        /// Implicit convertion from <see cref="Result{T}"/> to a <see cref="Result"/>.
        /// </summary>
        /// <param name="result"><see cref="Result{T}"/> to convert.</param>
        /// <returns>A corresponding <see cref="Result"/>.</returns>
        public static implicit operator Result(Result<T> result)
        {
            return new Result(result._logic);
        }
    }

    public partial struct CustomResult<TError>
    {
        /// <summary>
        /// Implicit convertion from <see cref="CustomResult{TError}"/> to a <see cref="Result"/>.
        /// </summary>
        /// <param name="result"><see cref="CustomResult{TError}"/> to convert.</param>
        /// <returns>A corresponding <see cref="Result"/>.</returns>
        public static implicit operator Result(CustomResult<TError> result)
        {
            return new Result(ResultLogic.ToResultLogic(result._logic));
        }
    }

    public partial struct Result<T, TError>
    {
        /// <summary>
        /// Implicit convertion from <see cref="Result{T, TError}"/> to a <see cref="Result"/>.
        /// </summary>
        /// <param name="result"><see cref="Result{T, TError}"/> to convert.</param>
        /// <returns>A corresponding <see cref="Result"/>.</returns>
        public static implicit operator Result(Result<T, TError> result)
        {
            return new Result(ResultLogic.ToResultLogic(result._logic));
        }

        /// <summary>
        /// Implicit convertion from <see cref="Result{T, TError}"/> to a <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <param name="result"><see cref="Result{T, TError}"/> to convert.</param>
        /// <returns>A corresponding <see cref="CustomResult{TError}"/>.</returns>
        public static implicit operator CustomResult<TError>(Result<T, TError> result)
        {
            return new CustomResult<TError>(result._logic);
        }

        /// <summary>
        /// Implicit convertion from <see cref="Result{T, TError}"/> to a <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="result"><see cref="Result{T, TError}"/> to convert.</param>
        /// <returns>A corresponding <see cref="Result{T}"/>.</returns>
        public static implicit operator Result<T>(Result<T, TError> result)
        {
            return new Result<T>(
                result.IsSuccess ? result.Value : default(T),
                ResultLogic.ToResultLogic(result._logic));
        }
    }
}

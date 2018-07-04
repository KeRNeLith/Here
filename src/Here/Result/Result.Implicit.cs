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
}

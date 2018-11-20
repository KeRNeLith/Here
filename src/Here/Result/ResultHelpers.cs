using JetBrains.Annotations;

namespace Here.Results
{
    /// <summary>
    /// Helpers to work with results.
    /// </summary>
    [PublicAPI]
    public static class ResultHelpers
    {
        #region Success Equal

        /// <summary>
        /// Checks that both <see cref="Result"/> are equal and that they are successful.
        /// </summary>
        /// <param name="result1">First <see cref="Result"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result"/> to compare.</param>
        /// <returns>True if both <see cref="Result"/> are equal and successful, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool SuccessEqual(in Result result1, in Result result2)
        {
            return result1.SuccessEquals(result2);
        }

        /// <summary>
        /// Checks that both <see cref="Result{T}"/> are equal and that they are successful.
        /// </summary>
        /// <param name="result1">First <see cref="Result{T}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T}"/> to compare.</param>
        /// <returns>True if both <see cref="Result{T}"/> are equal and successful, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool SuccessEqual<T>(in Result<T> result1, in Result<T> result2)
        {
            return result1.SuccessEquals(result2);
        }

        /// <summary>
        /// Checks that both <see cref="CustomResult{TError}"/> are equal and that they are successful.
        /// </summary>
        /// <param name="result1">First <see cref="CustomResult{TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="CustomResult{TError}"/> to compare.</param>
        /// <returns>True if both <see cref="CustomResult{TError}"/> are equal and successful, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool SuccessEqual<TError>(in CustomResult<TError> result1, in CustomResult<TError> result2)
        {
            return result1.SuccessEquals(result2);
        }

        /// <summary>
        /// Checks that both <see cref="Result{T, TError}"/> are equal and that they are successful.
        /// </summary>
        /// <param name="result1">First <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>True if both <see cref="Result{T, TError}"/> are equal and successful, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool SuccessEqual<T, TError>(in Result<T, TError> result1, in Result<T, TError> result2)
        {
            return result1.SuccessEquals(result2);
        }

        #endregion
    }
}
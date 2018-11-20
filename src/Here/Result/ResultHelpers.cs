using System.Collections.Generic;
using JetBrains.Annotations;

namespace Here.Results
{
    /// <summary>
    /// Helpers to work with results.
    /// </summary>
    [PublicAPI]
    public static class ResultHelpers
    {
        #region AreEqual

        /// <summary>
        /// Checks that both <see cref="Result"/> are equal.
        /// </summary>
        /// <param name="result1">First <see cref="Result"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result"/> to compare.</param>
        /// <returns>True if both <see cref="Result"/> are equal, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AreEqual(in Result result1, in Result result2)
        {
            return result1.Equals(result2);
        }

        /// <summary>
        /// Checks that both <see cref="Result{T}"/> are equal.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result1">First <see cref="Result{T}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T}"/> to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if both <see cref="Result{T}"/> are equal, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AreEqual<T>(in Result<T> result1, in Result<T> result2, [CanBeNull] in IEqualityComparer<T> equalityComparer = null)
        {
            return Result<T>.AreEqual(result1, result2, equalityComparer);
        }

        /// <summary>
        /// Checks that both <see cref="CustomResult{TError}"/> are equal.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result1">First <see cref="CustomResult{TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="CustomResult{TError}"/> to compare.</param>
        /// <returns>True if both <see cref="CustomResult{TError}"/> are equal, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AreEqual<TError>(in CustomResult<TError> result1, in CustomResult<TError> result2)
        {
            return result1.Equals(result2);
        }

        /// <summary>
        /// Checks that both <see cref="Result{T, TError}"/> are equal.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result1">First <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if both <see cref="Result{T, TError}"/> are equal, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AreEqual<T, TError>(in Result<T, TError> result1, in Result<T, TError> result2, [CanBeNull] in IEqualityComparer<T> equalityComparer = null)
        {
            return Result<T, TError>.AreEqual(result1, result2);
        }

        #endregion

        #region SuccessEqual

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
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result1">First <see cref="Result{T}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T}"/> to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if both <see cref="Result{T}"/> are equal and successful, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool SuccessEqual<T>(in Result<T> result1, in Result<T> result2, [CanBeNull] in IEqualityComparer<T> equalityComparer = null)
        {
            return Result<T>.AreSuccessEqual(result1, result2, equalityComparer);
        }

        /// <summary>
        /// Checks that both <see cref="CustomResult{TError}"/> are equal and that they are successful.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
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
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result1">First <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if both <see cref="Result{T, TError}"/> are equal and successful, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool SuccessEqual<T, TError>(in Result<T, TError> result1, in Result<T, TError> result2, [CanBeNull] in IEqualityComparer<T> equalityComparer = null)
        {
            return Result<T, TError>.AreSuccessEqual(result1, result2, equalityComparer);
        }

        #endregion

        #region Compare

        /// <summary>
        /// Compares this <see cref="Result"/> with the given one.
        /// Order keeps failures first, then warnings and finally successes.
        /// </summary>
        /// <param name="result1">First <see cref="Result"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result"/> to compare.</param>
        /// <returns>An integer that indicates the relative order of compared objects.</returns>
        [PublicAPI, Pure]
        public static int Compare(in Result result1, in Result result2)
        {
            return result1.CompareTo(result2);
        }

        /// <summary>
        /// Compares this <see cref="Result"/> with the given one.
        /// Order keeps failures first, then warnings and finally successes.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result1">First <see cref="Result"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result"/> to compare.</param>
        /// <returns>An integer that indicates the relative order of compared objects.</returns>
        [PublicAPI, Pure]
        public static int Compare<T>(in Result<T> result1, in Result<T> result2)
        {
            return result1.CompareTo(result2);
        }

        /// <summary>
        /// Compares this <see cref="CustomResult{TError}"/> with the given one.
        /// Order keeps failures first, then warnings and finally successes.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result1">First <see cref="CustomResult{TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="CustomResult{TError}"/> to compare.</param>
        /// <returns>An integer that indicates the relative order of compared objects.</returns>
        [PublicAPI, Pure]
        public static int Compare<TError>(in CustomResult<TError> result1, in CustomResult<TError> result2)
        {
            return result1.CompareTo(result2);
        }

        /// <summary>
        /// Compares this <see cref="Result{T,TError}"/> with the given one.
        /// Order keeps failures first, then warnings and finally successes.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result1">First <see cref="Result{T,TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T,TError}"/> to compare.</param>
        /// <returns>An integer that indicates the relative order of compared objects.</returns>
        [PublicAPI, Pure]
        public static int Compare<T, TError>(in Result<T, TError> result1, in Result<T, TError> result2)
        {
            return result1.CompareTo(result2);
        }

        #endregion
    }
}
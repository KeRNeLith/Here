using System.Collections.Generic;
using JetBrains.Annotations;

namespace Here
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
        public static bool AreEqual(Result result1, Result result2)
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
        public static bool AreEqual<T>(Result<T> result1, Result<T> result2, [CanBeNull] IEqualityComparer<T> equalityComparer = null)
        {
            return Result<T>.AreEqual(result1, result2, equalityComparer);
        }

        /// <summary>
        /// Indicates whether <see cref="Result{T}"/> value is equals to the given value.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> that may embed a value to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if the <see cref="Result{T}"/> value is equals to the given value, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AreEqual<T>(Result<T> result, [CanBeNull] T value, [CanBeNull] IEqualityComparer<T> equalityComparer = null)
        {
            return Result<T>.AreEqual(result, value, equalityComparer);
        }

        /// <summary>
        /// Checks that both <see cref="CustomResult{TError}"/> are equal.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result1">First <see cref="CustomResult{TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="CustomResult{TError}"/> to compare.</param>
        /// <returns>True if both <see cref="CustomResult{TError}"/> are equal, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AreEqual<TError>(CustomResult<TError> result1, CustomResult<TError> result2)
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
        public static bool AreEqual<T, TError>(Result<T, TError> result1, Result<T, TError> result2, [CanBeNull] IEqualityComparer<T> equalityComparer = null)
        {
            return Result<T, TError>.AreEqual(result1, result2);
        }

        /// <summary>
        /// Indicates whether <see cref="Result{T, TError}"/> value is equals to the given value.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> that may embed a value to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if the <see cref="Result{T, TError}"/> value is equals to the given value, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AreEqual<T, TError>(Result<T, TError> result, [CanBeNull] T value, [CanBeNull] IEqualityComparer<T> equalityComparer = null)
        {
            return Result<T, TError>.AreEqual(result, value, equalityComparer);
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
        public static bool SuccessEqual(Result result1, Result result2)
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
        public static bool SuccessEqual<T>(Result<T> result1, Result<T> result2, [CanBeNull] IEqualityComparer<T> equalityComparer = null)
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
        public static bool SuccessEqual<TError>(CustomResult<TError> result1, CustomResult<TError> result2)
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
        public static bool SuccessEqual<T, TError>(Result<T, TError> result1, Result<T, TError> result2, [CanBeNull] IEqualityComparer<T> equalityComparer = null)
        {
            return Result<T, TError>.AreSuccessEqual(result1, result2, equalityComparer);
        }

        #endregion

        #region Compare

        /// <summary>
        /// Compares both <see cref="Result"/>.
        /// Order keeps failures first, then warnings and finally successes.
        /// </summary>
        /// <param name="result1">First <see cref="Result"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result"/> to compare.</param>
        /// <returns>An integer that indicates the relative order of compared objects.</returns>
        [PublicAPI, Pure]
        public static int Compare(Result result1, Result result2)
        {
            return result1.CompareTo(result2);
        }

        /// <summary>
        /// Compares both <see cref="Result{T}"/>.
        /// Order keeps failures first, then warnings and finally successes.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result1">First <see cref="Result{T}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T}"/> to compare.</param>
        /// <returns>An integer that indicates the relative order of compared objects.</returns>
        [PublicAPI, Pure]
        public static int Compare<T>(Result<T> result1, Result<T> result2)
        {
            return result1.CompareTo(result2);
        }

        /// <summary>
        /// Compares both <see cref="CustomResult{TError}"/>.
        /// Order keeps failures first, then warnings and finally successes.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result1">First <see cref="CustomResult{TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="CustomResult{TError}"/> to compare.</param>
        /// <returns>An integer that indicates the relative order of compared objects.</returns>
        [PublicAPI, Pure]
        public static int Compare<TError>(CustomResult<TError> result1, CustomResult<TError> result2)
        {
            return result1.CompareTo(result2);
        }

        /// <summary>
        /// Compares both <see cref="Result{T,TError}"/>.
        /// Order keeps failures first, then warnings and finally successes.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result1">First <see cref="Result{T,TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T,TError}"/> to compare.</param>
        /// <returns>An integer that indicates the relative order of compared objects.</returns>
        [PublicAPI, Pure]
        public static int Compare<T, TError>(Result<T, TError> result1, Result<T, TError> result2)
        {
            return result1.CompareTo(result2);
        }

        /// <summary>
        /// Compares both results.
        /// Order keeps failures first, then warning and finally successes.
        /// </summary>
        /// <param name="result1">First <see cref="IResult"/> to compare.</param>
        /// <param name="result2">Second <see cref="IResult"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        [Pure]
        internal static int CompareResults([NotNull] IResult result1, [NotNull] IResult result2)
        {
            if (result1.IsSuccess && !result2.IsSuccess)
                return 1;

            if (!result1.IsSuccess && result2.IsSuccess)
                return -1;

            // Both success
            if (result1.IsSuccess && result2.IsSuccess)
            {
                if (result1.IsWarning && !result2.IsWarning)
                    return -1;
                if (!result1.IsWarning && result2.IsWarning)
                    return 1;
            }

            // Both success with or without warning or both failure
            return 0;
        }

        #endregion

        /// <summary>
        /// Converts the given <paramref name="result"/> into a <see cref="string"/>.
        /// </summary>
        /// <param name="result"><see cref="IResult"/> to convert.</param>
        /// <returns>String representation.</returns>
        [Pure]
        [NotNull]
        internal static string ResultToString([NotNull] IResult result)
        {
            if (result.IsFailure)
                return "Failure";
            if (result.IsWarning)
                return "Warning";
            return "Success";
        }
    }
}
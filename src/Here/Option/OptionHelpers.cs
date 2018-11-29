using JetBrains.Annotations;
using System.Collections.Generic;

namespace Here
{
    /// <summary>
    /// Helpers to work with <see cref="Option{T}"/>.
    /// </summary>
    [PublicAPI]
    public static class OptionHelpers
    {
        /// <summary>
        /// Indicates whether both <see cref="Option{T}"/> are equal.
        /// </summary>
        /// <param name="option1">First <see cref="Option{T}"/> to compare.</param>
        /// <param name="option2">Second <see cref="Option{T}"/> to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if both <see cref="Option{T}"/> are equal, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AreEqual<T>(in Option<T> option1, in Option<T> option2, [CanBeNull] in IEqualityComparer<T> equalityComparer = null)
        {
            return Option<T>.AreEqual(option1, option2, equalityComparer);
        }

        /// <summary>
        /// Indicates whether <see cref="Option{T}"/> value is equals to the given value.
        /// </summary>
        /// <param name="option">First <see cref="Option{T}"/> to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if the <see cref="Option{T}"/> value is equals to the given value, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AreEqual<T>(in Option<T> option, [CanBeNull] in T value, [CanBeNull] in IEqualityComparer<T> equalityComparer = null)
        {
            return Option<T>.AreEqual(option, value, equalityComparer);
        }

        /// <summary>
        /// Compares both <see cref="Option{T}"/>.
        /// Order keeps <see cref="Option{T}.None"/> first and <see cref="Option{T}"/> with value after.
        /// Then it uses the <see cref="Option{T}.Value"/> for the comparison.
        /// </summary>
        /// <param name="option1">First <see cref="Option{T}"/> to compare.</param>
        /// <param name="option2">Second <see cref="Option{T}"/> to compare.</param>
        /// <param name="comparer">Equality comparer to use to compare values.</param>
        /// <returns>An integer that indicates the relative order of compared objects.</returns>
        [PublicAPI, Pure]
        public static int Compare<T>(in Option<T> option1, in Option<T> option2, [CanBeNull] in IComparer<T> comparer = null)
        {
            return Option<T>.Compare(option1, option2, comparer);
        }
    }
}

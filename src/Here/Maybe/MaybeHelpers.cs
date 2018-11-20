using JetBrains.Annotations;
using System.Collections.Generic;

namespace Here.Maybes
{
    /// <summary>
    /// Helpers to work with <see cref="Maybe{T}"/>.
    /// </summary>
    [PublicAPI]
    public static class MaybeHelpers
    {
        /// <summary>
        /// Indicates whether both <see cref="Maybe{T}"/> are equal.
        /// </summary>
        /// <param name="maybe1">First <see cref="Maybe{T}"/> to compare.</param>
        /// <param name="maybe2">Second <see cref="Maybe{T}"/> to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if both <see cref="Maybe{T}"/> are equal, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AreEqual<T>(in Maybe<T> maybe1, in Maybe<T> maybe2, [CanBeNull] in IEqualityComparer<T> equalityComparer = null)
        {
            return Maybe<T>.AreEqual(maybe1, maybe2, equalityComparer);
        }

        /// <summary>
        /// Indicates whether <see cref="Maybe{T}"/> value is equals to the given value.
        /// </summary>
        /// <param name="maybe1">First <see cref="Maybe{T}"/> to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if the <see cref="Maybe{T}"/> value is equals to the given value, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AreEqual<T>(in Maybe<T> maybe1, [CanBeNull] in T value, [CanBeNull] in IEqualityComparer<T> equalityComparer = null)
        {
            return Maybe<T>.AreEqual(maybe1, value, equalityComparer);
        }

        /// <summary>
        /// Compares this <see cref="Maybe{T}"/> with the given one.
        /// Order keeps <see cref="Maybe{T}.None"/> first and <see cref="Maybe{T}"/> with value after.
        /// Then it uses the <see cref="Maybe{T}.Value"/> for the comparison.
        /// </summary>
        /// <param name="maybe1">First <see cref="Maybe{T}"/> to compare.</param>
        /// <param name="maybe2">Second <see cref="Maybe{T}"/> to compare.</param>
        /// <param name="comparer">Equality comparer to use to compare values.</param>
        /// <returns>An integer that indicates the relative order of compared objects.</returns>
        [PublicAPI, Pure]
        public static int Compare<T>(in Maybe<T> maybe1, in Maybe<T> maybe2, [CanBeNull] in IComparer<T> comparer = null)
        {
            return Maybe<T>.Compare(maybe1, maybe2, comparer);
        }
    }
}

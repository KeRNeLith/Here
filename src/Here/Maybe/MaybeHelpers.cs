using Here.Results;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Here.Maybes
{
    /// <summary>
    /// Empty Maybe construction helper. Used by implicit construction.
    /// </summary>
    [PublicAPI]
    public static class Maybe
    {
        /// <summary>
        /// Represents empty Maybe (nothing).
        /// </summary>
        public class NoneClass
        {
        }

        /// <summary>
        /// Failed conversion to <see cref="Result"/> message.
        /// </summary>
        internal static readonly string FailedToResultMessage = "Maybe<{0}> has no value";

        /// <summary>
        /// Represents a non typed empty Maybe.
        /// </summary>
        [PublicAPI, NotNull]
        public static NoneClass None { get; } = new NoneClass();

        #region Helpers

        /// <summary>
        /// Indicates whether both <see cref="Maybe{T}"/> are equal.
        /// </summary>
        /// <param name="maybe1">First <see cref="Maybe{T}"/> to compare.</param>
        /// <param name="maybe2">Second <see cref="Maybe{T}"/> to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if both <see cref="Maybe{T}"/> are equal, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AreEqual<T>(Maybe<T> maybe1, Maybe<T> maybe2, [CanBeNull] IEqualityComparer<T> equalityComparer = null)
        {
            return Maybe<T>.AreEqual(maybe1, maybe2, equalityComparer);
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
        public static int Compare<T>(Maybe<T> maybe1, Maybe<T> maybe2, [CanBeNull] IComparer<T> comparer = null)
        {
            return Maybe<T>.Compare(maybe1, maybe2, comparer);
        }

        #endregion
    }
}

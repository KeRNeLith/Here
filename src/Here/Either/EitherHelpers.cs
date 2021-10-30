using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// Helpers to work with <see cref="Either{TLeft,TRight}"/>, <see cref="EitherLeft{TLeft}"/> and <see cref="EitherRight{TRight}"/>.
    /// </summary>
    [PublicAPI]
    public static class EitherHelpers
    {
        /// <summary>
        /// Indicates whether both <see cref="Either{TLeft,TRight}"/> are equal.
        /// </summary>
        /// <param name="either1">First <see cref="Either{TLeft,TRight}"/> to compare.</param>
        /// <param name="either2">Second <see cref="Either{TLeft,TRight}"/> to compare.</param>
        /// <returns>True if both <see cref="Either{TLeft,TRight}"/> are equal, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AreEqual<TLeft, TRight>(in Either<TLeft, TRight> either1, in Either<TLeft, TRight> either2)
        {
            return Either<TLeft, TRight>.AreEqual(either1, either2);
        }

        /// <summary>
        /// Indicates whether both <see cref="EitherLeft{TLeft}"/> are equal.
        /// </summary>
        /// <param name="eitherLeft1">First <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <param name="eitherLeft2">Second <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <returns>True if both <see cref="EitherLeft{TLeft}"/> are equal, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AreEqual<TLeft>(in EitherLeft<TLeft> eitherLeft1, in EitherLeft<TLeft> eitherLeft2)
        {
            return eitherLeft1.Equals(eitherLeft2);
        }

        /// <summary>
        /// Indicates whether both <see cref="EitherRight{TRight}"/> are equal.
        /// </summary>
        /// <param name="eitherRight1">First <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <param name="eitherRight2">Second <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <returns>True if both <see cref="EitherRight{TRight}"/> are equal, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AreEqual<TRight>(in EitherRight<TRight> eitherRight1, in EitherRight<TRight> eitherRight2)
        {
            return eitherRight1.Equals(eitherRight2);
        }

        /// <summary>
        /// Compares both <see cref="Either{TLeft,TRight}"/>.
        /// Order keeps None first, then Left and finally Right either.
        /// </summary>
        /// <param name="either1">First <see cref="Either{TLeft,TRight}"/> to compare.</param>
        /// <param name="either2">Second <see cref="Either{TLeft,TRight}"/> to compare.</param>
        /// <returns>An integer that indicates the relative order of compared objects.</returns>
        [PublicAPI, Pure]
        public static int Compare<TLeft, TRight>(in Either<TLeft, TRight> either1, in Either<TLeft, TRight> either2)
        {
            return Either<TLeft, TRight>.Compare(either1, either2);
        }

        /// <summary>
        /// Compares both <see cref="EitherLeft{TLeft}"/>.
        /// Order is based on wrapped value.
        /// </summary>
        /// <param name="eitherLeft1">First <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <param name="eitherLeft2">Second <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <returns>An integer that indicates the relative order of compared objects.</returns>
        [PublicAPI, Pure]
        public static int Compare<TLeft>(in EitherLeft<TLeft> eitherLeft1, in EitherLeft<TLeft> eitherLeft2)
        {
            return eitherLeft1.CompareTo(eitherLeft2);
        }

        /// <summary>
        /// Compares both <see cref="EitherRight{TRight}"/>.
        /// Order is based on wrapped value.
        /// </summary>
        /// <param name="eitherRight1">First <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <param name="eitherRight2">Second <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <returns>An integer that indicates the relative order of compared objects.</returns>
        [PublicAPI, Pure]
        public static int Compare<TRight>(in EitherRight<TRight> eitherRight1, in EitherRight<TRight> eitherRight2)
        {
            return eitherRight1.CompareTo(eitherRight2);
        }
    }
}
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="IEither"/>.
    /// </summary>
    public static class EitherExtensions
    {
        /// <summary>
        /// Indicates if this <see cref="IEither"/> is a success (right state).
        /// </summary>
        /// <param name="either"><see cref="IEither"/> to check.</param>
        /// <returns>True if the <see cref="IEither"/> is a success, otherwise false.</returns>
        [PublicAPI]
        public static bool IsSuccess(this IEither either) => either.IsRight;

        /// <summary>
        /// Indicates if this <see cref="IEither"/> is a success (left state).
        /// </summary>
        /// <param name="either"><see cref="IEither"/> to check.</param>
        /// <returns>True if the <see cref="IEither"/> is a failure, otherwise false.</returns>
        [PublicAPI]
        public static bool IsFailure(this IEither either) => either.IsLeft;
    }
}
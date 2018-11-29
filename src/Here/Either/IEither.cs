using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// <see cref="IEither"/> is an instance of an object that is "Left" or "Right".
    /// Convention is that Left is used for failure and Right is used for success.
    /// </summary>
    [PublicAPI]
    public interface IEither
    {
        /// <summary>
        /// Indicates if this <see cref="IEither"/> is a left state.
        /// </summary>
        [PublicAPI]
        bool IsLeft { get; }

        /// <summary>
        /// Indicates if this <see cref="IEither"/> is a right state.
        /// </summary>
        [PublicAPI]
        bool IsRight { get; }
    }
}
using JetBrains.Annotations;

namespace Here.Maybes
{
    /// <summary>
    /// Empty maybe implementation.
    /// Used for implicit construction.
    /// </summary>
    [PublicAPI]
    public static class Maybe
    {
        /// <summary>
        /// Represents nothing.
        /// </summary>
        public class NoneClass
        {
        }

        /// <summary>
        /// No value.
        /// </summary>
        [NotNull]
        public static NoneClass None { get; } = new NoneClass();
    }
}

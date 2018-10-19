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
        /// Failed result message.
        /// </summary>
        internal static readonly string FailedToResultMessage = "Maybe<{0}> has no value";

        /// <summary>
        /// No value.
        /// </summary>
        [PublicAPI, NotNull]
        public static NoneClass None { get; } = new NoneClass();
    }
}

using JetBrains.Annotations;

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
        /// Failed conversion to result message.
        /// </summary>
        internal static readonly string FailedToResultMessage = "Maybe<{0}> has no value";

        /// <summary>
        /// Represents a non typed empty Maybe.
        /// </summary>
        [PublicAPI, NotNull]
        public static NoneClass None { get; } = new NoneClass();
    }
}

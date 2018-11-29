using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// Empty Option construction helper. Used by implicit construction.
    /// </summary>
    [PublicAPI]
    public static class Option
    {
        /// <summary>
        /// Represents empty Option (nothing).
        /// </summary>
        public class NoneClass
        {
        }

        /// <summary>
        /// Failed conversion to result message.
        /// </summary>
        internal static readonly string FailedToResultMessage = "Option<{0}> has no value";

        /// <summary>
        /// Represents a non typed empty Option.
        /// </summary>
        [PublicAPI, NotNull]
        public static NoneClass None { get; } = new NoneClass();
    }
}

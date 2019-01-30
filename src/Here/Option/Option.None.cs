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
        /// Represents an empty <see cref="Option{T}"/> (nothing).
        /// </summary>
        public class OptionNone
        {
            internal OptionNone()
            {
            }
        }

        /// <summary>
        /// Failed conversion to result message.
        /// </summary>
        internal const string FailedToResultMessage = "Option<{0}> has no value";

        /// <summary>
        /// Represents a non typed empty Option.
        /// </summary>
        [PublicAPI, NotNull]
        public static OptionNone None { get; } = new OptionNone();
    }
}

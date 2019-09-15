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
        public class NoneOption
        {
            internal NoneOption()
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
        public static NoneOption None { get; } = new NoneOption();
    }
}

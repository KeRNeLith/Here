using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// Construction helpers for <see cref="Option{T}"/>.
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
        public static readonly NoneOption None = new NoneOption();

        /// <summary>
        /// Constructs an <see cref="Option{T}"/> with a value, or in <see cref="None"/> state.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <returns>
        /// An <see cref="Option{T}"/> in the <see cref="None"/> state if <paramref name="value"/>
        /// is null, and in some state with <paramref name="value"/> otherwise.
        /// </returns>
        [PublicAPI, Pure]
        public static Option<T> From<T>([CanBeNull] in T value)
        {
            if (value == null)
                return None;

            return new Option<T>(value);
        }

        /// <summary>
        /// Constructs an <see cref="Option{T}"/> with a value, or in <see cref="None"/> state.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <returns>
        /// An <see cref="Option{T}"/> in the <see cref="None"/> state if <paramref name="value"/>
        /// is null, and in some state with <paramref name="value"/> otherwise.
        /// </returns>
        [PublicAPI, Pure]
        public static Option<T> From<T>([CanBeNull] T? value)
            where T : struct
        {
            if (value is null)
                return None;

            return new Option<T>(value.Value);
        }
    }
}
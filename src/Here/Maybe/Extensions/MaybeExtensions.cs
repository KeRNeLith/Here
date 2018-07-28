using JetBrains.Annotations;

namespace Here.Maybes.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Maybe{T}"/>.
    /// </summary>
    public static class MaybeExtensions
    {
        /// <summary>
		/// Convert the value to the corresponding <see cref="Maybe{T}"/>.
		/// </summary>
        /// <typeparam name="T">Type of the value to wrap.</typeparam>
		/// <param name="value">Value to convert.</param>
		/// <returns>Corresponding <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> ToMaybe<T>([CanBeNull] this T value)
        {
            return value != null
                ? Maybe<T>.Some(value)
                : Maybe<T>.None;
        }

        /// <summary>
		/// Convert <see cref="Nullable{T}"/> to the corresponding <see cref="Maybe{T}"/>.
		/// </summary>
        /// <typeparam name="T">Type of the value to wrap.</typeparam>
		/// <param name="nullable"><see cref="Nullable{T}"/> to convert.</param>
		/// <returns>Corresponding <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> ToMaybe<T>([CanBeNull] this T? nullable)
            where T : struct
        {
            return nullable?.ToMaybe() ?? Maybe<T>.None;
        }

        /// <summary>
		/// Convert <see cref="Maybe{T}"/> to the corresponding <see cref="Nullable{T}"/>.
		/// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
		/// <param name="maybe"><see cref="Maybe{T}"/> to convert.</param>
		/// <returns>Corresponding <see cref="Nullable{T}"/>.</returns>
        [PublicAPI, Pure]
        public static T? ToNullable<T>(this Maybe<T> maybe)
            where T : struct
        {
            return maybe.HasValue
                ? maybe.Value
                : new T?();
        }

        /// <summary>
        /// Convert the string to a <see cref="Maybe{String}"/>.
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <returns>The corresponding <see cref="Maybe{String}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<string> NoneIfEmpty([CanBeNull] this string str)
        {
            return string.IsNullOrEmpty(str)
                ? Maybe<string>.None
                : Maybe<string>.Some(str);
        }
    }
}

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
		/// <param name="value">Value to convert.</param>
		/// <returns>Corresponding <see cref="Maybe{T}"/>.</returns>
        [NotNull]
        public static Maybe<T> ToMaybe<T>([CanBeNull] this T value)
        {
            return value != null
                ? Maybe<T>.Some(value)
                : Maybe.None;
        }

        /// <summary>
		/// Convert <see cref="Nullable{T}"/> to the corresponding <see cref="Maybe{T}"/>.
		/// </summary>
		/// <param name="nullable"><see cref="Nullable{T}"/> to convert.</param>
		/// <returns>Corresponding <see cref="Maybe{T}"/>.</returns>
        [NotNull]
        public static Maybe<T> ToMaybe<T>([NotNull] this T? nullable)
            where T : struct
        {
            return nullable.HasValue
                ? nullable.Value.ToMaybe()
                : Maybe.None;
        }

        /// <summary>
		/// Convert <see cref="Maybe{T}"/> to the corresponding <see cref="Nullable{T}"/>.
		/// </summary>
		/// <param name="maybe"><see cref="Maybe{T}"/> to convert.</param>
		/// <returns>Corresponding <see cref="Nullable{T}"/>.</returns>
        public static T? ToNullable<T>([NotNull] this Maybe<T> maybe)
            where T : struct
        {
            return maybe.HasValue
                ? maybe.Value
                : new T?();
        }

        /// <summary>
        /// Convert the string to a <see cref="Maybe{string}"/>.
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <returns>The corresponding <see cref="Maybe{string}"/>.</returns>
        [NotNull]
        public static Maybe<string> NoneIfEmpty([CanBeNull] this string str)
        {
            return string.IsNullOrEmpty(str)
                ? Maybe.None
                : Maybe<string>.Some(str);
        }
    }
}

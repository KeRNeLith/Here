using System;
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
        /// Convert <see cref="Nullable"/> to the corresponding <see cref="Maybe{T}"/>.
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
        /// Flatten the given <see cref="Maybe{Maybe}"/> to a <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="embeddedMaybe">A <see cref="Maybe{Maybe}"/>.</param>
        /// <returns>A <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> Flatten<T>(this Maybe<Maybe<T>> embeddedMaybe)
        {
            return embeddedMaybe.HasValue ? embeddedMaybe.Value : Maybe<T>.None;
        }

        /// <summary>
        /// Convert the string to a <see cref="Maybe{String}"/> after applying <see cref="string.IsNullOrEmpty(string)"/>.
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

#if (!NET20 && !NET30 && !NET35)
        /// <summary>
        /// Convert the string to a <see cref="Maybe{String}"/> after applying <see cref="string.IsNullOrWhiteSpace(string)"/>.
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <returns>The corresponding <see cref="Maybe{String}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<string> NoneIfEmptyOrSpace([CanBeNull] this string str)
        {
            return string.IsNullOrWhiteSpace(str)
                ? Maybe<string>.None
                : Maybe<string>.Some(str);
        }
#endif

        /// <summary>
        /// Convert the <see cref="Maybe{String}"/> to another <see cref="Maybe{String}"/> after applying <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="maybe"><see cref="Maybe{String}"/> on which applying the treatment.</param>
        /// <returns>The corresponding <see cref="Maybe{String}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<string> NoneIfEmpty(this Maybe<string> maybe)
        {
            return maybe.IfElse(
                str => string.IsNullOrEmpty(str) ? Maybe<string>.None : Maybe<string>.Some(str), 
                () => Maybe<string>.None);
        }

#if (!NET20 && !NET30 && !NET35)
        /// <summary>
        /// Convert the <see cref="Maybe{String}"/> to another <see cref="Maybe{String}"/> after applying <see cref="string.IsNullOrWhiteSpace(string)"/>.
        /// </summary>
        /// <param name="maybe"><see cref="Maybe{String}"/> on which applying the treatment.</param>
        /// <returns>The corresponding <see cref="Maybe{String}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<string> NoneIfEmptyOrSpace(this Maybe<string> maybe)
        {
            return maybe.IfElse(
                str => string.IsNullOrWhiteSpace(str) ? Maybe<string>.None : Maybe<string>.Some(str),
                () => Maybe<string>.None);
        }
#endif
    }
}

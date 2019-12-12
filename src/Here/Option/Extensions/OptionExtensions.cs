using System;
using JetBrains.Annotations;
#if !SUPPORTS_NULL_EMPTY_OR_WHITE_SPACE
using static Here.HereHelpers;
#endif

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Option{T}"/>.
    /// </summary>
    public static class OptionExtensions
    {
        /// <summary>
        /// Converts this value to an <see cref="Option{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to wrap.</typeparam>
        /// <param name="value">Value to convert.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Option<T> ToOption<T>([CanBeNull] this T value)
        {
            return value != null
                ? Option<T>.Some(value)
                : Option<T>.None;
        }

        /// <summary>
        /// Converts this nullable to an <see cref="Option{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to wrap.</typeparam>
        /// <param name="nullable">Nullable to convert.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Option<T> ToOption<T>([CanBeNull] in this T? nullable)
            where T : struct
        {
            return nullable?.ToOption() ?? Option<T>.None;
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to a <see cref="Nullable{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> to convert.</param>
        /// <returns>An <see cref="Nullable{T}"/>.</returns>
        [PublicAPI, Pure]
        public static T? ToNullable<T>(in this Option<T> option)
            where T : struct
        {
            return option.HasValue
                ? option.Value
                : new T?();
        }

        /// <summary>
        /// Flattens this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="embeddedOption">An <see cref="Option{T}"/>.</param>
        /// <returns>Flattened <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Option<T> Flatten<T>(in this Option<Option<T>> embeddedOption)
        {
            return embeddedOption.HasValue ? embeddedOption.Value : Option<T>.None;
        }

        /// <summary>
        /// Converts this string to an <see cref="Option{T}"/> after applying <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Option<string> NoneIfEmpty([CanBeNull] this string str)
        {
            return string.IsNullOrEmpty(str)
                ? Option<string>.None
                : Option<string>.Some(str);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to another <see cref="Option{T}"/> after applying <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="option"><see cref="Option{T}"/> on which applying the treatment.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Option<string> NoneIfEmpty(in this Option<string> option)
        {
            return option.IfElse(
                str => string.IsNullOrEmpty(str) ? Option<string>.None : Option<string>.Some(str),
                () => Option<string>.None);
        }

#if SUPPORTS_NULL_EMPTY_OR_WHITE_SPACE
        /// <summary>
        /// Converts this string to an <see cref="Option{T}"/> after applying <see cref="string.IsNullOrWhiteSpace(string)"/>.
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
#else
        /// <summary>
        /// Converts this string to an <see cref="Option{T}"/> after applying <see cref="IsNullOrWhiteSpace(string)"/>.
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
#endif
        [PublicAPI, Pure]
        public static Option<string> NoneIfEmptyOrSpace([CanBeNull] this string str)
        {
#if SUPPORTS_NULL_EMPTY_OR_WHITE_SPACE
            return string.IsNullOrWhiteSpace(str)
#else
            return IsNullOrWhiteSpace(str)
#endif
                ? Option<string>.None
                : Option<string>.Some(str);
        }

#if SUPPORTS_NULL_EMPTY_OR_WHITE_SPACE
        /// <summary>
        /// Converts this <see cref="Option{T}"/> to another <see cref="Option{T}"/> after applying <see cref="string.IsNullOrWhiteSpace(string)"/>.
        /// </summary>
        /// <param name="option"><see cref="Option{T}"/> on which applying the treatment.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
#else
        /// <summary>
        /// Converts this <see cref="Option{T}"/> to another <see cref="Option{T}"/> after applying <see cref="IsNullOrWhiteSpace(string)"/>.
        /// </summary>
        /// <param name="option"><see cref="Option{T}"/> on which applying the treatment.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
#endif
        [PublicAPI, Pure]
        public static Option<string> NoneIfEmptyOrSpace(in this Option<string> option)
        {
            return option.IfElse(
#if SUPPORTS_NULL_EMPTY_OR_WHITE_SPACE
                str => string.IsNullOrWhiteSpace(str)
#else
                str => IsNullOrWhiteSpace(str)
#endif
                    ? Option<string>.None 
                    : Option<string>.Some(str),
                () => Option<string>.None);
        }
    }
}

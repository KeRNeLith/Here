using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Here.Maybes.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Maybe{T}"/> for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class MaybeEnumerableExtensions
    {
        /// <summary>
        /// Get a <see cref="Maybe{T}"/> of the first element of this <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">Template type of this <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="enumerable">Enumerable collection.</param>
        /// <returns>The corresponding <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> FirstOrNone<T>([NotNull] this IEnumerable<T> enumerable)
        {
            return enumerable.FirstOrNone(item => true);
        }

        /// <summary>
        /// Get a <see cref="Maybe{T}"/> of the first element of this <see cref="IEnumerable{T}"/> that matches <see cref="Predicate{T}"/>.
        /// </summary>
        /// <typeparam name="T">Template type of this <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="enumerable">Enumerable collection.</param>
        /// <param name="predicate"><see cref="Predicate{T}"/> to check on items.</param>
        /// <returns>The corresponding <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> FirstOrNone<T>([NotNull] this IEnumerable<T> enumerable, [NotNull, InstantHandle] Predicate<T> predicate) 
        {
            foreach(var item in enumerable)
            {
                if (predicate(item))
                    return item.ToMaybe();
            }

            return Maybe.None;
        }

        /// <summary>
        /// Get a <see cref="Maybe{T}"/> of the last element of this <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">Template type of this <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="enumerable">Enumerable collection.</param>
        /// <returns>The corresponding <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> LastOrNone<T>([NotNull] this IEnumerable<T> enumerable)
        {
            return enumerable.LastOrNone(item => true);
        }

        /// <summary>
        /// Get a <see cref="Maybe{T}"/> of the last element of this <see cref="IEnumerable{T}"/> that matches <see cref="Predicate{T}"/>.
        /// </summary>
        /// <typeparam name="T">Template type of this <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="enumerable">Enumerable collection.</param>
        /// <param name="predicate"><see cref="Predicate{T}"/> to check on items.</param>
        /// <returns>The corresponding <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> LastOrNone<T>([NotNull] this IEnumerable<T> enumerable, [NotNull, InstantHandle] Predicate<T> predicate)
        {
            T matchedItem = default(T);
            bool match = false;

            foreach (var item in enumerable)
            {
                if (predicate(item))
                {
                    match = true;
                    matchedItem = item;
                }
            }

            return match ? matchedItem.ToMaybe() : Maybe.None;
        }
    }
}

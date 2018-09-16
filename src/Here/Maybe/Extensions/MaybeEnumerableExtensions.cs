using System;
using System.Collections.Generic;
using JetBrains.Annotations;

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
                    return item;
            }

            return Maybe<T>.None;
        }

        /// <summary>
        /// Get a <see cref="Maybe{T}"/> of the single element of this <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">Template type of this <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="enumerable">Enumerable collection.</param>
        /// <param name="throwInvalidException">Indicate if the method should throw an <see cref="InvalidOperationException"/> 
        /// if the enumerable has more than one value, or if it just return a <see cref="Maybe{T}.None"/>.</param>
        /// <returns>The corresponding <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> SingleOrNone<T>([NotNull] this IEnumerable<T> enumerable, bool throwInvalidException = true)
        {
            using (var enumerator = enumerable.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    T result = enumerator.Current;
                    if (!enumerator.MoveNext())
                        return result;
                    else if (throwInvalidException)
                        throw new InvalidOperationException($"Enumerable<{typeof(T).Name}> contains more than one value.");
                }
            }

            return Maybe<T>.None;
        }

        /// <summary>
        /// Get a <see cref="Maybe{T}"/> of the single element of this <see cref="IEnumerable{T}"/> that matches <see cref="Predicate{T}"/>.
        /// </summary>
        /// <typeparam name="T">Template type of this <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="enumerable">Enumerable collection.</param>
        /// <param name="predicate"><see cref="Predicate{T}"/> to check on items.</param>
        /// <param name="throwInvalidException">Indicate if the method should throw an <see cref="InvalidOperationException"/> 
        /// if the enumerable has more than one value that match the <paramref name="predicate"/>, or if it just return a <see cref="Maybe{T}.None"/>.</param>
        /// <returns>The corresponding <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> SingleOrNone<T>([NotNull] this IEnumerable<T> enumerable, [NotNull, InstantHandle] Predicate<T> predicate, bool throwInvalidException = true)
        {
            using (var enumerator = enumerable.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    T result = enumerator.Current;
                    if (predicate(result))
                    {
                        while (enumerator.MoveNext())
                        {
                            if (predicate(enumerator.Current))
                            {
                                if (throwInvalidException)
                                    throw new InvalidOperationException($"Enumerable<{typeof(T).Name}> contains at least twice the requested value.");
                                return Maybe<T>.None;
                            }
                        }

                        return result;
                    }
                }
            }

            return Maybe<T>.None;
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

            return match ? matchedItem : Maybe<T>.None;
        }

        /// <summary>
        /// Get a the element at the given index in the given <see cref="IEnumerable{T}"/> and return a corresponding <see cref="Maybe{T}"/>.
        /// Note that if the index is out of range it will return a <see cref="Maybe{T}.None"/>.
        /// </summary>
        /// <typeparam name="T">Template type of this <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="enumerable">Enumerable collection.</param>
        /// <param name="index">Index in collection where getting the element.</param>
        /// <returns>The corresponding <see cref="Maybe{T}"/>.</returns> 
        [PublicAPI, Pure]
        public static Maybe<T> ElementAtOrNone<T>([NotNull] this IEnumerable<T> enumerable, int index)
        {
            if (index >= 0)
            {
                if (enumerable is IList<T> list)
                {
                    if (index < list.Count)
                        return list[index];
                }
#if (!NET20 && !NET30 && !NET35 && !NET40)
                else if (enumerable is IReadOnlyList<T> readOnlyList)
                {
                    if (index < readOnlyList.Count)
                        return readOnlyList[index];
                }
#endif
                else
                {
                    using (var enumerator = enumerable.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            if (index == 0)
                                return enumerator.Current;

                            index--;
                        }
                    }
                }
            }

            return Maybe<T>.None;
        }

        /// <summary>
        /// Convert this <see cref="Maybe{T}"/> to an <see cref="IEnumerable{T}"/> with zero or one element.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe">The <see cref="Maybe{T}"/> to convert.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> generated from this <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static IEnumerable<T> ToEnumerable<T>(this Maybe<T> maybe)
        {
            if (maybe.HasValue)
                yield return maybe.Value;
        }

        /// <summary>
        /// Convert this <see cref="Maybe{T}"/> to an <see cref="IEnumerator{T}"/> with zero or one element.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe">The <see cref="Maybe{T}"/> to convert.</param>
        /// <returns>An <see cref="IEnumerator{T}"/> generated from this <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static IEnumerator<T> ToEnumerator<T>(this Maybe<T> maybe)
        {
            if (maybe.HasValue)
                yield return maybe.Value;
        }

        /// <summary>
        /// Extract values from this <see cref="IEnumerable{Maybe{T}}"/> to convert it to an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="enumerable">An enumerable of <see cref="Maybe{T}"/>.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> generated from this <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static IEnumerable<T> ExtractValues<T>(this IEnumerable<Maybe<T>> enumerable)
        {
            foreach (var maybe in enumerable)
            {
                if (maybe.HasValue)
                    yield return maybe.Value;
            }
        }
    }
}

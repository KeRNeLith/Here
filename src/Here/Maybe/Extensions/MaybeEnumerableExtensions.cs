using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Here.Maybes.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Maybe{T}"/> for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class MaybeEnumerableExtensions
    {
        /// <summary>
        /// Gets a <see cref="Maybe{T}"/> of the first element of this <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="IEnumerable{T}"/> element type.</typeparam>
        /// <param name="enumerable">Enumerable..</param>
        /// <returns><see cref="Maybe{T}"/> wrapping the first element.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> FirstOrNone<T>([NotNull] this IEnumerable<T> enumerable)
        {
            return enumerable.FirstOrNone(item => true);
        }

        /// <summary>
        /// Gets a <see cref="Maybe{T}"/> of the first element of this <see cref="IEnumerable{T}"/> that matches the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="IEnumerable{T}"/> element type.</typeparam>
        /// <param name="enumerable">Enumerable.</param>
        /// <param name="predicate">Predicate to check on items.</param>
        /// <returns><see cref="Maybe{T}"/> wrapping the first matching element.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> FirstOrNone<T>([NotNull] this IEnumerable<T> enumerable, [NotNull, InstantHandle] in Predicate<T> predicate) 
        {
            foreach(var item in enumerable)
            {
                if (predicate(item))
                    return item;
            }

            return Maybe<T>.None;
        }

        /// <summary>
        /// Gets a <see cref="Maybe{T}"/> of the single element of this <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="IEnumerable{T}"/> element type.</typeparam>
        /// <param name="enumerable">Enumerable.</param>
        /// <param name="throwInvalidException">Indicates if the method should throw an <see cref="InvalidOperationException"/> 
        /// if the enumerable has more than one value. Otherwise it will just return an empty <see cref="Maybe{T}"/>.</param>
        /// <returns><see cref="Maybe{T}"/> wrapping the single element.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> SingleOrNone<T>([NotNull] this IEnumerable<T> enumerable, in bool throwInvalidException = true)
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
        /// Gets a <see cref="Maybe{T}"/> of the single element of this <see cref="IEnumerable{T}"/> that matches the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="IEnumerable{T}"/> element type.</typeparam>
        /// <param name="enumerable">Enumerable.</param>
        /// <param name="predicate">Predicate to check on items.</param>
        /// <param name="throwInvalidException">Indicates if the method should throw an <see cref="InvalidOperationException"/> 
        /// if the enumerable has more than one value that matches the predicate. Otherwise it will just return an empty <see cref="Maybe{T}"/>.</param>
        /// <returns><see cref="Maybe{T}"/> wrapping the matching single element.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> SingleOrNone<T>([NotNull] this IEnumerable<T> enumerable, [NotNull, InstantHandle] in Predicate<T> predicate, in bool throwInvalidException = true)
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
        /// Gets a <see cref="Maybe{T}"/> of the last element of this <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="IEnumerable{T}"/> element type.</typeparam>
        /// <param name="enumerable">Enumerable.</param>
        /// <returns><see cref="Maybe{T}"/> wrapping the last element.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> LastOrNone<T>([NotNull] this IEnumerable<T> enumerable)
        {
            return enumerable.LastOrNone(item => true);
        }

        /// <summary>
        /// Get a <see cref="Maybe{T}"/> of the last element of this <see cref="IEnumerable{T}"/> that matches the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="IEnumerable{T}"/> element type.</typeparam>
        /// <param name="enumerable">Enumerable.</param>
        /// <param name="predicate">Predicate to check on items.</param>
        /// <returns><see cref="Maybe{T}"/> wrapping the matching last element.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> LastOrNone<T>([NotNull] this IEnumerable<T> enumerable, [NotNull, InstantHandle] in Predicate<T> predicate)
        {
            T matchedItem = default;
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
        /// Gets the element at the given index in the given <see cref="IEnumerable{T}"/> and returns <see cref="Maybe{T}"/> that wrap it.
        /// Note that if the index is out of range it will return an empty <see cref="Maybe{T}"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="IEnumerable{T}"/> element type.</typeparam>
        /// <param name="enumerable">Enumerable.</param>
        /// <param name="index">Index of the element to get in the collection.</param>
        /// <returns><see cref="Maybe{T}"/> wrapping the element.</returns> 
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
        /// Converts this <see cref="Maybe{T}"/> to an <see cref="IEnumerable{T}"/> with one or no element.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe">This <see cref="Maybe{T}"/> to convert.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> generated from this <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure, NotNull]
        public static IEnumerable<T> ToEnumerable<T>(this Maybe<T> maybe)
        {
            if (maybe.HasValue)
                yield return maybe.Value;
        }

        /// <summary>
        /// Converts this <see cref="Maybe{T}"/> to an <see cref="IEnumerator{T}"/> with one or no element.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe">This <see cref="Maybe{T}"/> to convert.</param>
        /// <returns>An <see cref="IEnumerator{T}"/> generated from this <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure, NotNull]
        public static IEnumerator<T> ToEnumerator<T>(this Maybe<T> maybe)
        {
            if (maybe.HasValue)
                yield return maybe.Value;
        }

        /// <summary>
        /// Extracts values from this <see cref="IEnumerable{Maybe}"/> to convert it to an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="enumerable">Enumerable of <see cref="Maybe{T}"/>.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> generated from this enumerable of <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure, NotNull]
        public static IEnumerable<T> ExtractValues<T>([NotNull] this IEnumerable<Maybe<T>> enumerable)
        {
            foreach (var maybe in enumerable)
            {
                if (maybe.HasValue)
                    yield return maybe.Value;
            }
        }

        /// <summary>
        /// Extracts values from this <see cref="IEnumerable{Maybe}"/> to convert it to an array.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="enumerable">Enumerable of <see cref="Maybe{T}"/>.</param>
        /// <returns>An array generated from this enumerable of <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure, NotNull]
        public static T[] ToArray<T>([NotNull] this IEnumerable<Maybe<T>> enumerable)
        {
            return enumerable.ExtractValues().ToArray();
        }

        /// <summary>
        /// Extracts values from this <see cref="IEnumerable{Maybe}"/> to convert it to a <see cref="List{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="enumerable">Enumerable of <see cref="Maybe{T}"/>.</param>
        /// <returns>A <see cref="List{T}"/> generated from this enumerable of <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure, NotNull]
        public static List<T> ToList<T>([NotNull] this IEnumerable<Maybe<T>> enumerable)
        {
            return enumerable.ExtractValues().ToList();
        }

        /// <summary>
        /// Extracts values from this <see cref="IEnumerable{Maybe}"/> to convert it to a <see cref="Dictionary{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">Type of the output dictionary key.</typeparam>
        /// <typeparam name="TValue">Type of the value embedded in <see cref="Maybe{TValue}"/>.</typeparam>
        /// <param name="enumerable">Enumerable of <see cref="Maybe{TValue}"/>.</param>
        /// <param name="keySelector">Method called to create dictionary keys.</param>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> generated from this enumerable of <see cref="Maybe{TValue}"/>.</returns>
        [PublicAPI, Pure, NotNull]
        public static Dictionary<TKey, TValue> ToDictionary<TValue, TKey>([NotNull] this IEnumerable<Maybe<TValue>> enumerable, [NotNull, InstantHandle] in Func<TValue, TKey> keySelector)
        {
            return enumerable.ExtractValues().ToDictionary(keySelector);
        }
    }
}

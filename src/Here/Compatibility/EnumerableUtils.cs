#if !SUPPORTS_SYSTEM_CORE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Here.Utils
{
    /// <summary>
    /// Helper to replace Enumerable standard utilities when not available in target version.
    /// </summary>
    internal static class Enumerable
    {
        /// <summary>
        /// Checks if the given <paramref name="source"/> contains the <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TSource">Element type.</typeparam>
        /// <param name="source">Source enumerable.</param>
        /// <param name="value">Value to check.</param>
        /// <param name="comparer">Value comparer.</param>
        /// <returns>True if the <paramref name="source"/> contains the <paramref name="value"/>.</returns>
        public static bool Contains<TSource>(
            [NotNull, ItemCanBeNull] this IEnumerable<TSource> source,
            [CanBeNull] TSource value,
            [CanBeNull] IEqualityComparer<TSource> comparer = null)
        {
            Debug.Assert(source != null);

            if (comparer is null)
            {
                comparer = EqualityComparer<TSource>.Default;
            }

            foreach (TSource element in source)
            {
                if (comparer.Equals(element, value))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks that at least one element in <paramref name="source"/> fits the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="TSource">Element type.</typeparam>
        /// <param name="source">Source enumerable.</param>
        /// <param name="predicate">Predicate to check on each <paramref name="source"/> elements.</param>
        /// <returns>True if at least one element from <paramref name="source"/> fits the <paramref name="predicate"/>, false otherwise.</returns>
        [Pure]
        public static bool Any<TSource>([NotNull, ItemCanBeNull] this IEnumerable<TSource> source, [NotNull, InstantHandle] Func<TSource, bool> predicate)
        {
            Debug.Assert(source != null);
            Debug.Assert(predicate != null);

            foreach (TSource element in source)
            {
                if (predicate(element))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks that all elements in <paramref name="source"/> fits the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="TSource">Element type.</typeparam>
        /// <param name="source">Source enumerable.</param>
        /// <param name="predicate">Predicate to check on each <paramref name="source"/> elements.</param>
        /// <returns>True if all elements from <paramref name="source"/> fits the <paramref name="predicate"/>, false otherwise.</returns>
        [Pure]
        public static bool All<TSource>([NotNull, ItemCanBeNull] this IEnumerable<TSource> source, [NotNull, InstantHandle] Func<TSource, bool> predicate)
        {
            Debug.Assert(source != null);
            Debug.Assert(predicate != null);

            foreach (TSource element in source)
            {
                if (!predicate(element))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Selects a set of values from given <paramref name="source"/> elements using the given <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="TIn">Element type.</typeparam>
        /// <typeparam name="TOut">Output element type.</typeparam>
        /// <param name="source">Source enumerable.</param>
        /// <param name="selector">Selector applied on each <paramref name="source"/> element.</param>
        /// <returns>Enumerable of selected elements.</returns>
        [Pure]
        [NotNull, ItemCanBeNull]
        public static IEnumerable<TOut> Select<TIn, TOut>([NotNull, ItemCanBeNull] this IEnumerable<TIn> source, [NotNull, InstantHandle] Func<TIn, TOut> selector)
        {
            Debug.Assert(source != null);
            Debug.Assert(selector != null);

            foreach (TIn element in source)
            {
                yield return selector(element);
            }
        }

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable"/> and flattens the resulting sequences
        /// into one sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the sequence returned by <paramref name="selector"/>.</typeparam>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>
        /// An <see cref="IEnumerable"/> whose elements are the result of invoking the one-to-many transform
        /// function on each element of the input sequence.
        /// </returns>
        [Pure]
        [NotNull]
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(
            [NotNull, ItemNotNull] this IEnumerable<TSource> source,
            [NotNull, InstantHandle] Func<TSource, IEnumerable<TResult>> selector)
        {
            foreach (TSource item in source)
            {
                foreach (TResult innerItem in selector(item))
                {
                    yield return innerItem;
                }
            }
        }

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable"/> and flattens the resulting sequences
        /// into one sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TCollection">The collection type returned by <paramref name="collectionSelector"/>.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the sequence returned by <paramref name="collectionSelector"/>.</typeparam>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="collectionSelector">A transform function to apply to each element returning a collection.</param>
        /// <param name="resultSelector">A transform function to apply to each element.</param>
        /// <returns>
        /// An <see cref="IEnumerable"/> whose elements are the result of invoking the one-to-many transform
        /// function on each element of the input sequence.
        /// </returns>
        [Pure]
        [NotNull]
        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(
            [NotNull, ItemNotNull] this IEnumerable<TSource> source,
            [NotNull, InstantHandle] Func<TSource, IEnumerable<TCollection>> collectionSelector,
            [NotNull, InstantHandle] Func<TSource, TCollection, TResult> resultSelector)
        {
            foreach (TSource item in source)
            {
                foreach (TCollection subElement in collectionSelector(item))
                {
                    yield return resultSelector(item, subElement);
                }
            }
        }

        /// <summary>
        /// Casts the elements from the given <paramref name="source"/> to <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="TResult">Result element type.</typeparam>
        /// <param name="source">Source enumerable.</param>
        /// <returns>Enumerable of cast elements.</returns>
        [Pure]
        [NotNull, ItemCanBeNull]
        public static IEnumerable<TResult> Cast<TResult>([NotNull, ItemCanBeNull] this IEnumerable source)
        {
            Debug.Assert(source != null);

            if (source is IEnumerable<TResult> typedSource)
            {
                foreach (TResult item in typedSource)
                {
                    yield return item;
                }
            }
            else
            {
                foreach (object obj in source)
                {
                    yield return (TResult)obj;
                }
            }
        }

        /// <summary>
        /// Gets a sub set enumerable of <paramref name="source"/> elements matching the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Source enumerable.</param>
        /// <param name="predicate">Predicate to check on each source element.</param>
        /// <returns>Enumerable of elements matching <paramref name="predicate"/>.</returns>
        [Pure]
        [NotNull, ItemCanBeNull]
        public static IEnumerable<T> Where<T>([NotNull, ItemNotNull] this IEnumerable<T> source, [NotNull, InstantHandle] Predicate<T> predicate)
        {
            Debug.Assert(source != null);
            Debug.Assert(predicate != null);

            foreach (T element in source)
            {
                if (predicate(element))
                    yield return element;
            }
        }

        private static class EmptyEnumerable<TElement>
        {
            [NotNull, ItemNotNull]
            public static readonly TElement[] Instance = new TElement[0];
        }

        /// <summary>
        /// Gets an empty enumerable of <typeparamref name="T"/> elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <returns>Empty enumerable.</returns>
        [Pure]
        [NotNull, ItemNotNull]
        public static IEnumerable<T> Empty<T>()
        {
            return EmptyEnumerable<T>.Instance;
        }

        #region Buffer helper

        /// <summary>
        /// Buffer of elements.
        /// </summary>
        /// <typeparam name="TElement">Element type.</typeparam>
        private struct Buffer<TElement>
        {
            private readonly TElement[] _items;
            private readonly int _count;

            internal Buffer([NotNull, ItemCanBeNull] IEnumerable<TElement> source)
            {
                TElement[] items = null;
                int count = 0;
                if (source is ICollection<TElement> collection)
                {
                    count = collection.Count;
                    if (count > 0)
                    {
                        items = new TElement[count];
                        collection.CopyTo(items, 0);
                    }
                }
                else
                {
                    foreach (TElement item in source)
                    {
                        if (items is null)
                        {
                            items = new TElement[4];
                        }
                        else if (items.Length == count)
                        {
                            var newItems = new TElement[checked(count * 2)];
                            Array.Copy(items, 0, newItems, 0, count);
                            items = newItems;
                        }

                        items[count] = item;
                        ++count;
                    }
                }

                _items = items;
                _count = count;
            }

            internal TElement[] ToArray()
            {
                if (_count == 0)
                    return new TElement[0];

                if (_items.Length == _count)
                    return _items;

                var result = new TElement[_count];
                Array.Copy(_items, 0, result, 0, _count);
                return result;
            }
        }

        #endregion

        /// <summary>
        /// Converts the given enumerable to an array.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Source enumerable.</param>
        /// <returns>Enumerable converted to array.</returns>
        [Pure]
        [NotNull, ItemCanBeNull]
        public static T[] ToArray<T>([NotNull, ItemCanBeNull] this IEnumerable<T> source)
        {
            Debug.Assert(source != null);

            return new Buffer<T>(source).ToArray();
        }

        /// <summary>
        /// Converts the given enumerable to a <see cref="List{T}"/>.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Source enumerable.</param>
        /// <returns>Enumerable converted to <see cref="List{T}"/>.</returns>
        [Pure]
        [NotNull, ItemCanBeNull]
        public static List<T> ToList<T>([NotNull, ItemCanBeNull] this IEnumerable<T> source)
        {
            Debug.Assert(source != null);

            return new List<T>(source);
        }

        /// <summary>
        /// Converts the given enumerable to a <see cref="Dictionary{TKey,TSource}"/>.
        /// </summary>
        /// <typeparam name="TKey">Key type.</typeparam>
        /// <typeparam name="TSource">Element type.</typeparam>
        /// <param name="source">Source enumerable.</param>
        /// <param name="keySelector">Key selector.</param>
        /// <returns>Enumerable converted to <see cref="Dictionary{TKey,TSource}"/>.</returns>
        [Pure]
        [NotNull]
        public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(
            [NotNull, ItemCanBeNull] this IEnumerable<TSource> source,
            [NotNull, InstantHandle] Func<TSource, TKey> keySelector)
        {
            Debug.Assert(source != null);
            Debug.Assert(keySelector != null);
            
            var dictionary = new Dictionary<TKey, TSource>();
            foreach (TSource element in source)
            {
                dictionary.Add(keySelector(element), HereHelpers.Identity(element));
            }

            return dictionary;
        }

        /// <summary>
        /// Aggregates values from <paramref name="source"/> with the <paramref name="aggregator"/> function.
        /// </summary>
        /// <typeparam name="TSource">Element type.</typeparam>
        /// <param name="source">Source enumerable.</param>
        /// <param name="aggregator">Aggregator function.</param>
        /// <returns>Aggregated value.</returns>
        [Pure]
        public static TSource Aggregate<TSource>([NotNull, ItemCanBeNull] this IEnumerable<TSource> source, [NotNull, InstantHandle] Func<TSource, TSource, TSource> aggregator)
        {
            Debug.Assert(source != null);
            Debug.Assert(aggregator != null);

            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    throw new InvalidOperationException("Sequence contains no elements.");

                TSource result = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    result = aggregator(result, enumerator.Current);
                }

                return result;
            }
        }

        /// <summary>
        /// Aggregates values from <paramref name="source"/> with the <paramref name="aggregator"/> function.
        /// </summary>
        /// <typeparam name="TSource">Element type.</typeparam>
        /// <typeparam name="TAccumulate">Accumulation value.</typeparam>
        /// <param name="source">Source enumerable.</param>
        /// <param name="seed">Initial value.</param>
        /// <param name="aggregator">Aggregator function.</param>
        /// <returns>Aggregated value.</returns>
        [Pure]
        public static TAccumulate Aggregate<TSource, TAccumulate>(
            [NotNull, ItemCanBeNull] this IEnumerable<TSource> source,
            [CanBeNull] TAccumulate seed,
            [NotNull, InstantHandle] Func<TAccumulate, TSource, TAccumulate> aggregator)
        {
            Debug.Assert(source != null);
            Debug.Assert(aggregator != null);

            TAccumulate result = seed;
            foreach (TSource element in source)
            {
                result = aggregator(result, element);
            }

            return result;
        }

        /// <summary>
        /// Checks if both sequences are equal.
        /// </summary>
        /// <typeparam name="TSource">Element type.</typeparam>
        /// <param name="first">First sequence.</param>
        /// <param name="second">Second sequence.</param>
        /// <returns>True if both sequences are equal, false otherwise.</returns>
        [Pure]
        public static bool SequenceEqual<TSource>([NotNull, ItemCanBeNull] this IEnumerable<TSource> first, [NotNull, ItemCanBeNull] IEnumerable<TSource> second)
        {
            Debug.Assert(first != null);
            Debug.Assert(second != null);

            using (IEnumerator<TSource> enumerator1 = first.GetEnumerator())
            using (IEnumerator<TSource> enumerator2 = second.GetEnumerator())
            {
                while (enumerator1.MoveNext())
                {
                    if (!(enumerator2.MoveNext() && Equals(enumerator1.Current, enumerator2.Current)))
                        return false;
                }

                if (enumerator2.MoveNext())
                    return false;
            }

            return true;
        }
    }
}
#endif
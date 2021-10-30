using System;
using System.Collections.Generic;
#if SUPPORTS_SYSTEM_CORE
using System.Linq;
#else
using Here.Utils;
#endif
#if SUPPORTS_AGGRESSIVE_INLINING
using System.Runtime.CompilerServices;
#endif
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Option{T}"/> for <see cref="T:System.Collections.Generic.IEnumerable{T}"/>.
    /// </summary>
    public static class OptionEnumerableExtensions
    {
        /// <summary>
        /// Gets an <see cref="Option{T}"/> of the first element of this <see cref="T:System.Collections.Generic.IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="T:System.Collections.Generic.IEnumerable{T}"/> element type.</typeparam>
        /// <param name="enumerable">Enumerable..</param>
        /// <returns><see cref="Option{T}"/> wrapping the first element.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="enumerable"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<T> FirstOrNone<T>([NotNull] this IEnumerable<T> enumerable)
        {
            return enumerable.FirstOrNone(item => true);
        }

        /// <summary>
        /// Gets an <see cref="Option{T}"/> of the first element of this <see cref="T:System.Collections.Generic.IEnumerable{T}"/> that matches the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="T:System.Collections.Generic.IEnumerable{T}"/> element type.</typeparam>
        /// <param name="enumerable">Enumerable.</param>
        /// <param name="predicate">Predicate to check on items.</param>
        /// <returns><see cref="Option{T}"/> wrapping the first matching element.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="enumerable"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<T> FirstOrNone<T>([NotNull] this IEnumerable<T> enumerable, [NotNull, InstantHandle] in Predicate<T> predicate)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            foreach (T item in enumerable)
            {
                if (predicate(item))
                    return item;
            }

            return Option<T>.None;
        }

        /// <summary>
        /// Gets an <see cref="Option{T}"/> of the single element of this <see cref="T:System.Collections.Generic.IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="T:System.Collections.Generic.IEnumerable{T}"/> element type.</typeparam>
        /// <param name="enumerable">Enumerable.</param>
        /// <param name="throwInvalidException">Indicates if the method should throw an <see cref="T:System.InvalidOperationException"/> 
        /// if the enumerable has more than one value. Otherwise it will just return an empty <see cref="Option{T}"/>.</param>
        /// <returns><see cref="Option{T}"/> wrapping the single element.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="enumerable"/> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">If the <paramref name="enumerable"/> contains more than one element.</exception>
        [PublicAPI, Pure]
        public static Option<T> SingleOrNone<T>([NotNull] this IEnumerable<T> enumerable, in bool throwInvalidException = true)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));

            using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
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

            return Option<T>.None;
        }

        /// <summary>
        /// Gets an <see cref="Option{T}"/> of the single element of this <see cref="T:System.Collections.Generic.IEnumerable{T}"/> that matches the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="T:System.Collections.Generic.IEnumerable{T}"/> element type.</typeparam>
        /// <param name="enumerable">Enumerable.</param>
        /// <param name="predicate">Predicate to check on items.</param>
        /// <param name="throwInvalidException">Indicates if the method should throw an <see cref="T:System.InvalidOperationException"/> 
        /// if the enumerable has more than one value that matches the predicate. Otherwise it will just return an empty <see cref="Option{T}"/>.</param>
        /// <returns><see cref="Option{T}"/> wrapping the matching single element.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="enumerable"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">If the <paramref name="enumerable"/> contains more than one element matching the <paramref name="predicate"/>.</exception>
        [PublicAPI, Pure]
        public static Option<T> SingleOrNone<T>([NotNull] this IEnumerable<T> enumerable, [NotNull, InstantHandle] in Predicate<T> predicate, in bool throwInvalidException = true)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return SingleOrNoneIterator(enumerable, predicate, throwInvalidException);
        }

        [Pure]
        private static Option<T> SingleOrNoneIterator<T>([NotNull] IEnumerable<T> enumerable, [NotNull, InstantHandle] in Predicate<T> predicate, in bool throwInvalidException = true)
        {
            using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
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
                                return Option<T>.None;
                            }
                        }

                        return result;
                    }
                }
            }

            return Option<T>.None;
        }

        /// <summary>
        /// Gets an <see cref="Option{T}"/> of the last element of this <see cref="T:System.Collections.Generic.IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="T:System.Collections.Generic.IEnumerable{T}"/> element type.</typeparam>
        /// <param name="enumerable">Enumerable.</param>
        /// <returns><see cref="Option{T}"/> wrapping the last element.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="enumerable"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<T> LastOrNone<T>([NotNull] this IEnumerable<T> enumerable)
        {
            return enumerable.LastOrNone(item => true);
        }

        /// <summary>
        /// Get an <see cref="Option{T}"/> of the last element of this <see cref="T:System.Collections.Generic.IEnumerable{T}"/> that matches the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="T:System.Collections.Generic.IEnumerable{T}"/> element type.</typeparam>
        /// <param name="enumerable">Enumerable.</param>
        /// <param name="predicate">Predicate to check on items.</param>
        /// <returns><see cref="Option{T}"/> wrapping the matching last element.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="enumerable"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<T> LastOrNone<T>([NotNull] this IEnumerable<T> enumerable, [NotNull, InstantHandle] in Predicate<T> predicate)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            T matchedItem = default;
            bool match = false;

            foreach (T item in enumerable)
            {
                if (predicate(item))
                {
                    match = true;
                    matchedItem = item;
                }
            }

            return match ? matchedItem : Option<T>.None;
        }

        /// <summary>
        /// Gets the element at the given index in the given <see cref="T:System.Collections.Generic.IEnumerable{T}"/> and returns <see cref="Option{T}"/> that wrap it.
        /// Note that if the index is out of range it will return an empty <see cref="Option{T}"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="T:System.Collections.Generic.IEnumerable{T}"/> element type.</typeparam>
        /// <param name="enumerable">Enumerable.</param>
        /// <param name="index">Index of the element to get in the collection.</param>
        /// <returns><see cref="Option{T}"/> wrapping the element.</returns> 
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="enumerable"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<T> ElementAtOrNone<T>([NotNull] this IEnumerable<T> enumerable, int index)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));

            if (index >= 0)
            {
                if (enumerable is IList<T> list)
                {
                    if (index < list.Count)
                        return list[index];
                }
                else
                {
                    using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            if (index == 0)
                                return enumerator.Current;

                            --index;
                        }
                    }
                }
            }

            return Option<T>.None;
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="T:System.Collections.Generic.IEnumerable{T}"/> with one or no element.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option">This <see cref="Option{T}"/> to convert.</param>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable{T}"/> generated from this <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure, NotNull]
        public static IEnumerable<T> ToEnumerable<T>(this Option<T> option)
        {
            if (option.HasValue)
                yield return option._value;
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="T:System.Collections.Generic.IEnumerator{T}"/> with one or no element.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option">This <see cref="Option{T}"/> to convert.</param>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerator{T}"/> generated from this <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure, NotNull]
        public static IEnumerator<T> ToEnumerator<T>(this Option<T> option)
        {
            return option.GetEnumerator();
        }

        /// <summary>
        /// Extracts values from this <see cref="T:System.Collections.Generic.IEnumerable{Option}"/> to convert it to an <see cref="T:System.Collections.Generic.IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in <see cref="Option{T}"/>.</typeparam>
        /// <param name="enumerable">Enumerable of <see cref="Option{T}"/>.</param>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable{T}"/> generated from this enumerable of <see cref="Option{T}"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="enumerable"/> is null.</exception>
        [PublicAPI, Pure, NotNull]
        public static IEnumerable<T> ExtractValues<T>([NotNull] this IEnumerable<Option<T>> enumerable)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));

            return ExtractValuesIterator(enumerable);
        }

        [Pure, NotNull]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static IEnumerable<T> ExtractValuesIterator<T>([NotNull] this IEnumerable<Option<T>> enumerable)
        {
            foreach (Option<T> option in enumerable)
            {
                if (option.HasValue)
                    yield return option._value;
            }
        }

        /// <summary>
        /// Extracts values from this <see cref="T:System.Collections.Generic.IEnumerable{Option}"/> to convert it to an array.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in <see cref="Option{T}"/>.</typeparam>
        /// <param name="enumerable">Enumerable of <see cref="Option{T}"/>.</param>
        /// <returns>An array generated from this enumerable of <see cref="Option{T}"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="enumerable"/> is null.</exception>
        [PublicAPI, Pure, NotNull]
        public static T[] ToArray<T>([NotNull] this IEnumerable<Option<T>> enumerable)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));

            return enumerable.ExtractValues().ToArray();
        }

        /// <summary>
        /// Extracts values from this <see cref="T:System.Collections.Generic.IEnumerable{Option}"/> to convert it to a <see cref="T:System.Collections.Generic.List{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in <see cref="Option{T}"/>.</typeparam>
        /// <param name="enumerable">Enumerable of <see cref="Option{T}"/>.</param>
        /// <returns>A <see cref="T:System.Collections.Generic.List{T}"/> generated from this enumerable of <see cref="Option{T}"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="enumerable"/> is null.</exception>
        [PublicAPI, Pure, NotNull]
        public static List<T> ToList<T>([NotNull] this IEnumerable<Option<T>> enumerable)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));

            return enumerable.ExtractValues().ToList();
        }

        /// <summary>
        /// Extracts values from this <see cref="T:System.Collections.Generic.IEnumerable{Option}"/> to convert it to a <see cref="T:System.Collections.Generic.Dictionary`2{TKey,TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">Type of the output dictionary key.</typeparam>
        /// <typeparam name="TValue">Type of the value embedded in <see cref="Option{T}"/>.</typeparam>
        /// <param name="enumerable">Enumerable of <see cref="Option{T}"/>.</param>
        /// <param name="keySelector">Method called to create dictionary keys.</param>
        /// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2{TKey,TValue}"/> generated from this enumerable of <see cref="Option{T}"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="enumerable"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="keySelector"/> is null.</exception>
        [PublicAPI, Pure, NotNull]
        public static Dictionary<TKey, TValue> ToDictionary<TValue, TKey>([NotNull] this IEnumerable<Option<TValue>> enumerable, [NotNull, InstantHandle] in Func<TValue, TKey> keySelector)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));
            if (keySelector is null)
                throw new ArgumentNullException(nameof(keySelector));

            return enumerable.ExtractValues().ToDictionary(keySelector);
        }
    }
}

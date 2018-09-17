using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Here.Maybes.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Maybe{T}"/> for Linq features (enumerable).
    /// </summary>
    public static class MaybeLinqEnumerableExtensions
    {
        /// <summary>
        /// Check if this <see cref="Maybe{T}"/> has value and return if true enumerable has at least one element.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <returns>True if this <see cref="Maybe{T}"/> enumerable has at least one value.</returns>
        [PublicAPI, Pure]
        public static bool AnyItem<T>(this Maybe<T> maybe)
            where T : IEnumerable
        {
            if (maybe.HasValue)
                return maybe.Value.GetEnumerator().MoveNext();
            return false;
        }

        /// <summary>
        /// Check if this <see cref="Maybe{T}"/> has value and return true if enumerable has at least one element that match the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="predicate">Condition to match.</param>
        /// <returns>True if this <see cref="Maybe{T}"/> enumerable has at least one value that match.</returns>
        [PublicAPI, Pure]
        public static bool AnyItem<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Predicate<object> predicate)
            where T : IEnumerable
        {
            if (maybe.HasValue)
            {
                foreach (var item in maybe.Value)
                {
                    if (predicate(item))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if this <see cref="Maybe{T}"/> has value and return true if enumerable has at least one element that match the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="predicate">Condition to match.</param>
        /// <returns>True if this <see cref="Maybe{T}"/> enumerable has at least one value that match.</returns>
        [PublicAPI, Pure]
        public static bool AnyItem<T, TItem>(this Maybe<T> maybe, [NotNull, InstantHandle] Predicate<TItem> predicate)
            where T : IEnumerable<TItem>
        {
            if (maybe.HasValue)
                return maybe.Value.Any(item => predicate(item));
            return false;
        }

        /// <summary>
        /// Check if this <see cref="Maybe{T}"/> has value and return true if all enumerable items match the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="predicate">Predicate to check.</param>
        /// <returns>True if this <see cref="Maybe{T}"/> enumerable items all match the <paramref name="predicate"/>.</returns>
        [PublicAPI, Pure]
        public static bool AllItems<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Predicate<object> predicate)
            where T : IEnumerable
        {
            if (maybe.HasValue)
            {
                foreach (var item in maybe.Value)
                {
                    if (!predicate(item))
                        return false;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if this <see cref="Maybe{T}"/> has value and return true if all enumerable items match the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="predicate">Predicate to check.</param>
        /// <returns>True if this <see cref="Maybe{T}"/> enumerable items all match the <paramref name="predicate"/>.</returns>
        [PublicAPI, Pure]
        public static bool AllItems<T, TItem>(this Maybe<T> maybe, [NotNull, InstantHandle] Predicate<TItem> predicate)
            where T : IEnumerable<TItem>
        {
            if (maybe.HasValue)
                return maybe.Value.All(item => predicate(item));
            return false;
        }

        /// <summary>
        /// Check if this <see cref="Maybe{T}"/> has value and return true if enumerable contains the given <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="value">Value to check equality with <see cref="Maybe{T}"/> value.</param>
        /// <returns>True if this <see cref="Maybe{T}"/> contains <paramref name="value"/>.</returns>
        [PublicAPI, Pure]
        public static bool ContainsItem<T>(this Maybe<T> maybe, [CanBeNull] object value)
            where T : IEnumerable
        {
            if (maybe.HasValue)
            {
                foreach (var item in maybe.Value)
                {
                    if (EqualityComparer<object>.Default.Equals(item, value))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if this <see cref="Maybe{T}"/> has value and return true if enumerable contains the given <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="value">Value to check equality with <see cref="Maybe{T}"/> value.</param>
        /// <param name="comparer">Equality comparer to use.</param>
        /// <returns>True if this <see cref="Maybe{T}"/> contains <paramref name="value"/>.</returns>
        [PublicAPI, Pure]
        public static bool ContainsItem<T>(this Maybe<T> maybe, [CanBeNull] object value, [NotNull] IEqualityComparer<object> comparer)
            where T : IEnumerable
        {
            if (maybe.HasValue)
            {
                foreach (var item in maybe.Value)
                {
                    if (comparer.Equals(item, value))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if this <see cref="Maybe{T}"/> has value and return true if enumerable contains the given <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="value">Value to check equality with <see cref="Maybe{T}"/> value.</param>
        /// <returns>True if this <see cref="Maybe{T}"/> contains <paramref name="value"/>.</returns>
        [PublicAPI, Pure]
        public static bool ContainsItem<T, TItem>(this Maybe<T> maybe, [CanBeNull] TItem value)
            where T : IEnumerable<TItem>
        {
            if (maybe.HasValue)
                return maybe.Value.Contains(value);
            return false;
        }

        /// <summary>
        /// Check if this <see cref="Maybe{T}"/> has value and return true if enumerable contains the given <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="value">Value to check equality with <see cref="Maybe{T}"/> value.</param>
        /// <param name="comparer">Equality comparer to use.</param>
        /// <returns>True if this <see cref="Maybe{T}"/> contains <paramref name="value"/>.</returns>
        [PublicAPI, Pure]
        public static bool ContainsItem<T, TItem>(this Maybe<T> maybe, [CanBeNull] TItem value, [NotNull] IEqualityComparer<TItem> comparer)
            where T : IEnumerable<TItem>
        {
            if (maybe.HasValue)
                return maybe.Value.Contains(value, comparer);
            return false;
        }

        /// <summary>
        /// Select via the <paramref name="selector"/> method something from this <see cref="Maybe{T}"/> wrapped enumerable if it has value and return selected items.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TItemOut">Enumerable item output type.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="selector">Method called to select the value from this <see cref="Maybe{T}"/> enumerable items.</param>
        /// <returns>A <see cref="Maybe{T}"/> with selected items.</returns>
        [PublicAPI, Pure]
        public static Maybe<IEnumerable<TItemOut>> SelectItems<T, TItemOut>(this Maybe<T> maybe, [NotNull, InstantHandle] Func<object, TItemOut> selector)
            where T : IEnumerable
        {
            if (maybe.HasValue)
            {
                var result = new List<TItemOut>();
                foreach (var item in maybe.Value)
                    result.Add(selector(item));

                // At least one match
                if (result.Count > 0)
                    return result;
            }

            return Maybe<IEnumerable<TItemOut>>.None;
        }

        /// <summary>
        /// Select via the <paramref name="selector"/> method something from this <see cref="Maybe{T}"/> wrapped enumerable if it has value and return selected items.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TItemIn">Enumerable item input type.</typeparam>
        /// <typeparam name="TItemOut">Enumerable item output type.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="selector">Method called to select the value from this <see cref="Maybe{T}"/> enumerable items.</param>
        /// <returns>A <see cref="Maybe{T}"/> with selected items.</returns>
        [PublicAPI, Pure]
        public static Maybe<IEnumerable<TItemOut>> SelectItems<T, TItemIn, TItemOut>(this Maybe<T> maybe, [NotNull, InstantHandle] Func<TItemIn, TItemOut> selector)
            where T : IEnumerable<TItemIn>
        {
            if (maybe.HasValue)
            {
                var selectedItems = maybe.Value.Select(selector).ToArray();
                if (selectedItems.Length > 0)
                    return selectedItems;
            }

            return Maybe<IEnumerable<TItemOut>>.None;
        }

        /// <summary>
        /// Call the <paramref name="predicate"/> function on each item if this <see cref="Maybe{T}"/> has a value and return matched items.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="predicate">Condition to match.</param>
        /// <returns>A <see cref="Maybe{T}"/> with matched items.</returns>
        [PublicAPI, Pure]
        public static Maybe<IEnumerable> WhereItems<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Predicate<object> predicate)
            where T : IEnumerable
        {
            if (maybe.HasValue)
            {
                var result = new List<object>();
                foreach (var item in maybe.Value)
                {
                    if (predicate(item))
                        result.Add(item);
                }

                // At least one match
                if (result.Count > 0)
                    return result;
            }

            return Maybe<IEnumerable>.None;
        }

        /// <summary>
        /// Call the <paramref name="predicate"/> function on each item if this <see cref="Maybe{T}"/> has a value and return matched items.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="predicate">Condition to match.</param>
        /// <returns>A <see cref="Maybe{T}"/> with matched items.</returns>
        [PublicAPI, Pure]
        public static Maybe<IEnumerable<TItem>> WhereItems<T, TItem>(this Maybe<T> maybe, [NotNull, InstantHandle] Predicate<TItem> predicate)
            where T : IEnumerable<TItem>
        {
            if (maybe.HasValue)
            {
                var matchingItems = maybe.Value.Where(item => predicate(item)).ToArray();
                // At least one match
                if (matchingItems.Length > 0)
                    return matchingItems;
            }

            return Maybe<IEnumerable<TItem>>.None;
        }

        /// <summary>
        /// Call the <paramref name="onItem"/> function on each item if this <see cref="Maybe{T}"/> has a value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="onItem">Treatment to do on each item.</param>
        /// <returns>This <see cref="Maybe{T}"/>.</returns>
        [PublicAPI]
        public static Maybe<T> ForEachItems<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Action<object> onItem)
            where T : IEnumerable
        {
            if (maybe.HasValue)
            {
                foreach (var item in maybe.Value)
                    onItem(item);
            }

            return maybe;
        }

        /// <summary>
        /// Call the <paramref name="onItem"/> function on each item if this <see cref="Maybe{T}"/> has a value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="onItem">Treatment to do on each item.</param>
        /// <returns>This <see cref="Maybe{T}"/>.</returns>
        [PublicAPI]
        public static Maybe<T> ForEachItems<T, TItem>(this Maybe<T> maybe, [NotNull, InstantHandle] Action<TItem> onItem)
            where T : IEnumerable<TItem>
        {
            if (maybe.HasValue)
            {
                foreach (var item in maybe.Value)
                    onItem(item);
            }

            return maybe;
        }

        /// <summary>
        /// Compute the aggregator function on this <see cref="Maybe{T}"/> enumerable value.
        /// Use the <paramref name="initialValue"/> as the initial aggregator value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TAggregate">Type of the value aggregator value.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="initialValue">The initial aggregator value.</param>
        /// <param name="aggregator">THe aggregator function called on this <see cref="Maybe{T}"/> enumerable items.</param>
        /// <returns>This <see cref="Maybe{T}"/> value aggregated with 
        /// <paramref name="initialValue"/>, otherwise <paramref name="initialValue"/>.</returns>
        [PublicAPI, NotNull, Pure]
        public static TAggregate AggregateItems<T, TAggregate>(this Maybe<T> maybe,
            [NotNull] TAggregate initialValue,
            [NotNull, InstantHandle] Func<TAggregate, object, TAggregate> aggregator)
            where T : IEnumerable
        {
            if (maybe.HasValue)
            {
                TAggregate aggregate = initialValue;
                foreach (var item in maybe.Value)
                    aggregate = aggregator(aggregate, item);

                return aggregate;
            }

            return initialValue;
        }

        /// <summary>
        /// Compute the aggregator function on this <see cref="Maybe{T}"/> enumerable value.
        /// Use the <paramref name="initialValue"/> as the initial aggregator value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <typeparam name="TAggregate">Type of the value aggregator value.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="initialValue">The initial aggregator value.</param>
        /// <param name="aggregator">THe aggregator function called on this <see cref="Maybe{T}"/> enumerable items.</param>
        /// <returns>This <see cref="Maybe{T}"/> value aggregated with 
        /// <paramref name="initialValue"/>, otherwise <paramref name="initialValue"/>.</returns>
        [PublicAPI, NotNull, Pure]
        public static TAggregate AggregateItems<T, TItem, TAggregate>(this Maybe<T> maybe,
            [NotNull] TAggregate initialValue,
            [NotNull, InstantHandle] Func<TAggregate, TItem, TAggregate> aggregator)
            where T : IEnumerable<TItem>
        {
            if (maybe.HasValue)
                return maybe.Value.Aggregate(initialValue, aggregator);
            return initialValue;
        }
    }
}

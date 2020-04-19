using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Option{T}"/> for Linq features (enumerable).
    /// </summary>
    public static class OptionLinqEnumerableExtensions
    {
        /// <summary>
        /// Checks if this <see cref="Option{T}"/> has a value and returns true if the enumerable has at least one element.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <returns>True if this <see cref="Option{T}"/> enumerable value has at least one value, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool AnyItem<T>(in this Option<T> option)
            where T : IEnumerable
        {
            if (option.HasValue)
                return option.Value.GetEnumerator().MoveNext();
            return false;
        }

        /// <summary>
        /// Checks if this <see cref="Option{T}"/> has a value and returns true if the enumerable has at least one element that matches the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <param name="predicate">Condition to match.</param>
        /// <returns>True if this <see cref="Option{T}"/> enumerable has at least one value that matches, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
        public static bool AnyItem<T>(in this Option<T> option, [NotNull, InstantHandle] in Predicate<object> predicate)
            where T : IEnumerable
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));

            if (option.HasValue)
            {
                foreach (object item in option.Value)
                {
                    if (predicate(item))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if this <see cref="Option{T}"/> has a value and returns true if the enumerable has at least one element that matches the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <param name="predicate">Condition to match.</param>
        /// <returns>True if this <see cref="Option{T}"/> enumerable has at least one value that match, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
        public static bool AnyItem<T, TItem>(in this Option<T> option, [NotNull, InstantHandle] Predicate<TItem> predicate)
            where T : IEnumerable<TItem>
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));

            if (option.HasValue)
                return option.Value.Any(item => predicate(item));
            return false;
        }

        /// <summary>
        /// Checks if this <see cref="Option{T}"/> has a value and returns true if all enumerable items match the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <param name="predicate">Predicate to check.</param>
        /// <returns>True if this <see cref="Option{T}"/> enumerable items all match the <paramref name="predicate"/>, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
        public static bool AllItems<T>(in this Option<T> option, [NotNull, InstantHandle] in Predicate<object> predicate)
            where T : IEnumerable
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));

            if (option.HasValue)
            {
                foreach (object item in option.Value)
                {
                    if (!predicate(item))
                        return false;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if this <see cref="Option{T}"/> has a value and returns true if all enumerable items match the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <param name="predicate">Predicate to check.</param>
        /// <returns>True if this <see cref="Option{T}"/> enumerable items all match the <paramref name="predicate"/>, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
        public static bool AllItems<T, TItem>(in this Option<T> option, [NotNull, InstantHandle] Predicate<TItem> predicate)
            where T : IEnumerable<TItem>
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));

            if (option.HasValue)
                return option.Value.All(item => predicate(item));
            return false;
        }

        /// <summary>
        /// Checks if this <see cref="Option{T}"/> has a value and return true if enumerable contains the given <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the search.</param>
        /// <param name="value">Value to check equality with <see cref="Option{T}"/> value.</param>
        /// <returns>True if this <see cref="Option{T}"/> contains the <paramref name="value"/>, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool ContainsItem<T>(in this Option<T> option, [CanBeNull] in object value)
            where T : IEnumerable
        {
            if (option.HasValue)
            {
                foreach (object item in option.Value)
                {
                    if (EqualityComparer<object>.Default.Equals(item, value))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if this <see cref="Option{T}"/> has a value and returns true if enumerable contains the given <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the search.</param>
        /// <param name="value">Value to check equality with <see cref="Option{T}"/> value.</param>
        /// <param name="comparer">Equality comparer to use.</param>
        /// <returns>True if this <see cref="Option{T}"/> contains the <paramref name="value"/>, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="comparer"/> is null.</exception>
        [PublicAPI, Pure]
        public static bool ContainsItem<T>(in this Option<T> option, [CanBeNull] in object value, [NotNull] in IEqualityComparer<object> comparer)
            where T : IEnumerable
        {
            Throw.IfArgumentNull(comparer, nameof(comparer));

            if (option.HasValue)
            {
                foreach (object item in option.Value)
                {
                    if (comparer.Equals(item, value))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if this <see cref="Option{T}"/> has a value and returns true if enumerable contains the given <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the search.</param>
        /// <param name="value">Value to check equality with <see cref="Option{T}"/> value.</param>
        /// <returns>True if this <see cref="Option{T}"/> contains the <paramref name="value"/>, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool ContainsItem<T, TItem>(in this Option<T> option, [CanBeNull] in TItem value)
            where T : IEnumerable<TItem>
        {
            if (option.HasValue)
                return option.Value.Contains(value);
            return false;
        }

        /// <summary>
        /// Checks if this <see cref="Option{T}"/> has a value and returns true if enumerable contains the given <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the search.</param>
        /// <param name="value">Value to check equality with <see cref="Option{T}"/> value.</param>
        /// <param name="comparer">Equality comparer to use.</param>
        /// <returns>True if this <see cref="Option{T}"/> contains the <paramref name="value"/>, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="comparer"/> is null.</exception>
        [PublicAPI, Pure]
        public static bool ContainsItem<T, TItem>(in this Option<T> option, [CanBeNull] in TItem value, [NotNull] in IEqualityComparer<TItem> comparer)
            where T : IEnumerable<TItem>
        {
            Throw.IfArgumentNull(comparer, nameof(comparer));

            if (option.HasValue)
                return option.Value.Contains(value, comparer);
            return false;
        }

        /// <summary>
        /// Selects via the <paramref name="selector"/> something from this <see cref="Option{T}"/> wrapped enumerable if it has value and returns selected items.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TItemOut">Enumerable item output type.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the selection.</param>
        /// <param name="selector">Method called to select the value from this <see cref="Option{T}"/> enumerable items.</param>
        /// <returns>A <see cref="Option{T}"/> with selected items.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="selector"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<IEnumerable<TItemOut>> SelectItems<T, TItemOut>(in this Option<T> option, [NotNull, InstantHandle] in Func<object, TItemOut> selector)
            where T : IEnumerable
        {
            Throw.IfArgumentNull(selector, nameof(selector));

            if (option.HasValue)
            {
                return option.Value
                    .Cast<object>()
                    .Select(selector)
                    .ToOption();
            }

            return Option<IEnumerable<TItemOut>>.None;
        }

        /// <summary>
        /// Selects via the <paramref name="selector"/> something from this <see cref="Option{T}"/> wrapped enumerable if it has value and returns selected items.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TItemIn">Enumerable item input type.</typeparam>
        /// <typeparam name="TItemOut">Enumerable item output type.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the selection.</param>
        /// <param name="selector">Method called to select the value from this <see cref="Option{T}"/> enumerable items.</param>
        /// <returns>A <see cref="Option{T}"/> with selected items.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="selector"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<IEnumerable<TItemOut>> SelectItems<T, TItemIn, TItemOut>(in this Option<T> option, [NotNull, InstantHandle] in Func<TItemIn, TItemOut> selector)
            where T : IEnumerable<TItemIn>
        {
            Throw.IfArgumentNull(selector, nameof(selector));

            if (option.HasValue)
                return option.Value.Select(selector).ToOption();
            return Option<IEnumerable<TItemOut>>.None;
        }

        /// <summary>
        /// Selects via the <paramref name="selector"/> something from each element of this <see cref="Option{T}"/> wrapped enumerable if it has value and returns selected items.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TItemOut">Enumerable item output type.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the selection.</param>
        /// <param name="selector">Method called to select the value from this <see cref="Option{T}"/> enumerable items.</param>
        /// <returns>A <see cref="Option{T}"/> with selected items.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="selector"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<IEnumerable<TItemOut>> SelectManyItems<T, TItemOut>(in this Option<T> option, [NotNull, InstantHandle] in Func<object, IEnumerable<TItemOut>> selector)
            where T : IEnumerable
        {
            Throw.IfArgumentNull(selector, nameof(selector));

            if (option.HasValue)
            {
                return option.Value
                    .Cast<object>()
                    .SelectMany(selector)
                    .ToOption();
            }

            return Option<IEnumerable<TItemOut>>.None;
        }

        /// <summary>
        /// Selects via the <paramref name="selector"/> something from each element of this <see cref="Option{T}"/> wrapped enumerable if it has value and returns selected items.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TItemIn">Enumerable item input type.</typeparam>
        /// <typeparam name="TItemOut">Enumerable item output type.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the selection.</param>
        /// <param name="selector">Method called to select the value from this <see cref="Option{T}"/> enumerable items.</param>
        /// <returns>A <see cref="Option{T}"/> with selected items.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="selector"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<IEnumerable<TItemOut>> SelectManyItems<T, TItemIn, TItemOut>(in this Option<T> option, [NotNull, InstantHandle] in Func<TItemIn, IEnumerable<TItemOut>> selector)
            where T : IEnumerable<TItemIn>
        {
            Throw.IfArgumentNull(selector, nameof(selector));

            if (option.HasValue)
                return option.Value.SelectMany(selector).ToOption();
            return Option<IEnumerable<TItemOut>>.None;
        }

        /// <summary>
        /// Projects each element of this <see cref="Option{T}"/> wrapped enumerable to an <see cref="IEnumerable{TItemCollection}"/>
        /// via <paramref name="collectionSelector"/>, and flattens the resulting sequences into one sequence using <paramref name="resultSelector"/>.
        /// Does this only if the <paramref name="option"/> has value, otherwise returns an empty <see cref="Option{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TItemCollection">Enumerable intermediate item type.</typeparam>
        /// <typeparam name="TItemOut">Enumerable item output type.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the selection.</param>
        /// <param name="collectionSelector">Method called to select the intermediate value from this <see cref="Option{T}"/> enumerable items.</param>
        /// <param name="resultSelector">Method called to select the result value from intermediate items.</param>
        /// <returns>A <see cref="Option{T}"/> with selected items.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="collectionSelector"/> or <paramref name="resultSelector"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<IEnumerable<TItemOut>> SelectManyItems<T, TItemCollection, TItemOut>(
            in this Option<T> option,
            [NotNull, InstantHandle] in Func<object, IEnumerable<TItemCollection>> collectionSelector,
            [NotNull, InstantHandle] in Func<object, TItemCollection, TItemOut> resultSelector)
            where T : IEnumerable
        {
            Throw.IfArgumentNull(collectionSelector, nameof(collectionSelector));
            Throw.IfArgumentNull(resultSelector, nameof(resultSelector));

            if (option.HasValue)
                return option.Value.Cast<object>().SelectMany(collectionSelector, resultSelector).ToOption();
            return Option<IEnumerable<TItemOut>>.None;
        }

        /// <summary>
        /// Projects each element of this <see cref="Option{T}"/> wrapped enumerable to an <see cref="IEnumerable{TItemCollection}"/>
        /// via <paramref name="collectionSelector"/>, and flattens the resulting sequences into one sequence using <paramref name="resultSelector"/>.
        /// Does this only if the <paramref name="option"/> has value, otherwise returns an empty <see cref="Option{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TItemIn">Enumerable item input type.</typeparam>
        /// <typeparam name="TItemCollection">Enumerable intermediate item type.</typeparam>
        /// <typeparam name="TItemOut">Enumerable item output type.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the selection.</param>
        /// <param name="collectionSelector">Method called to select the intermediate value from this <see cref="Option{T}"/> enumerable items.</param>
        /// <param name="resultSelector">Method called to select the result value from intermediate items.</param>
        /// <returns>A <see cref="Option{T}"/> with selected items.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="collectionSelector"/> or <paramref name="resultSelector"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<IEnumerable<TItemOut>> SelectManyItems<T, TItemIn, TItemCollection, TItemOut>(
            in this Option<T> option,
            [NotNull, InstantHandle] in Func<TItemIn, IEnumerable<TItemCollection>> collectionSelector,
            [NotNull, InstantHandle] in Func<TItemIn, TItemCollection, TItemOut> resultSelector)
            where T : IEnumerable<TItemIn>
        {
            Throw.IfArgumentNull(collectionSelector, nameof(collectionSelector));
            Throw.IfArgumentNull(resultSelector, nameof(resultSelector));

            if (option.HasValue)
                return option.Value.SelectMany(collectionSelector, resultSelector).ToOption();
            return Option<IEnumerable<TItemOut>>.None;
        }

        /// <summary>
        /// Calls the <paramref name="predicate"/> on each item if this <see cref="Option{T}"/> has a value and returns items that match it.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing treatment.</param>
        /// <param name="predicate">Condition to match.</param>
        /// <returns>A <see cref="Option{T}"/> with matched items.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<IEnumerable> WhereItems<T>(in this Option<T> option, [NotNull, InstantHandle] in Predicate<object> predicate)
            where T : IEnumerable
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));

            if (option.HasValue)
                return GetMatchingItems(option.Value, predicate).ToOption();
            return Option<IEnumerable>.None;

            #region Local function

            IEnumerable GetMatchingItems(IEnumerable enumerable, Predicate<object> condition)
            {
                foreach (object item in enumerable)
                {
                    if (condition(item))
                        yield return item;
                }
            }

            #endregion
        }

        /// <summary>
        /// Calls the <paramref name="predicate"/> on each item if this <see cref="Option{T}"/> has a value and returns items that match it.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing treatment.</param>
        /// <param name="predicate">Condition to match.</param>
        /// <returns>A <see cref="Option{T}"/> with matched items.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<IEnumerable<TItem>> WhereItems<T, TItem>(in this Option<T> option, [NotNull, InstantHandle] Predicate<TItem> predicate)
            where T : IEnumerable<TItem>
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));

            if (option.HasValue)
            {
                return option.Value
                    .Where(item => predicate(item))
                    .ToOption();
            }

            return Option<IEnumerable<TItem>>.None;
        }

        /// <summary>
        /// Calls the <paramref name="onItem"/> function on each item if this <see cref="Option{T}"/> has a value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing treatment.</param>
        /// <param name="onItem">Treatment to do on each item.</param>
        /// <returns>This <see cref="Option{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onItem"/> is null.</exception>
        [PublicAPI]
        public static Option<T> ForEachItems<T>(in this Option<T> option, [NotNull, InstantHandle] in Action<object> onItem)
            where T : IEnumerable
        {
            Throw.IfArgumentNull(onItem, nameof(onItem));

            if (option.HasValue)
            {
                foreach (object item in option.Value)
                    onItem(item);
            }

            return option;
        }

        /// <summary>
        /// Calls the <paramref name="onItem"/> function on each item if this <see cref="Option{T}"/> has a value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing treatment.</param>
        /// <param name="onItem">Treatment to do on each item.</param>
        /// <returns>This <see cref="Option{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onItem"/> is null.</exception>
        [PublicAPI]
        public static Option<T> ForEachItems<T, TItem>(in this Option<T> option, [NotNull, InstantHandle] in Action<TItem> onItem)
            where T : IEnumerable<TItem>
        {
            Throw.IfArgumentNull(onItem, nameof(onItem));

            if (option.HasValue)
            {
                foreach (TItem item in option.Value)
                    onItem(item);
            }

            return option;
        }

        /// <summary>
        /// Computes the aggregation of this <see cref="Option{T}"/> enumerable value.
        /// Use the <paramref name="initialValue"/> as the initial aggregation value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TAggregate">Type of the value aggregation value.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the aggregation.</param>
        /// <param name="initialValue">The initial aggregation value.</param>
        /// <param name="aggregator">The aggregator function called on this <see cref="Option{T}"/> enumerable items.</param>
        /// <returns>This <see cref="Option{T}"/> value aggregated with 
        /// <paramref name="initialValue"/>, otherwise <paramref name="initialValue"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="initialValue"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="aggregator"/> is null.</exception>
        [PublicAPI, NotNull, Pure]
        public static TAggregate AggregateItems<T, TAggregate>(in this Option<T> option,
            [NotNull] in TAggregate initialValue,
            [NotNull, InstantHandle] in Func<TAggregate, object, TAggregate> aggregator)
            where T : IEnumerable
        {
            Throw.IfArgumentNull(initialValue, nameof(initialValue));
            Throw.IfArgumentNull(aggregator, nameof(aggregator));

            if (option.HasValue)
            {
                TAggregate aggregate = initialValue;
                foreach (object item in option.Value)
                    aggregate = aggregator(aggregate, item);

                return aggregate;
            }

            return initialValue;
        }

        /// <summary>
        /// Computes the aggregation of this <see cref="Option{T}"/> enumerable values.
        /// Use the <paramref name="initialValue"/> as the initial aggregation value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <typeparam name="TAggregate">Type of the value aggregated value.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the aggregation.</param>
        /// <param name="initialValue">The initial aggregation value.</param>
        /// <param name="aggregator">The aggregator function called on this <see cref="Option{T}"/> enumerable items.</param>
        /// <returns>This <see cref="Option{T}"/> value aggregated with 
        /// <paramref name="initialValue"/>, otherwise <paramref name="initialValue"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="initialValue"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="aggregator"/> is null.</exception>
        [PublicAPI, NotNull, Pure]
        public static TAggregate AggregateItems<T, TItem, TAggregate>(in this Option<T> option,
            [NotNull] in TAggregate initialValue,
            [NotNull, InstantHandle] in Func<TAggregate, TItem, TAggregate> aggregator)
            where T : IEnumerable<TItem>
        {
            Throw.IfArgumentNull(initialValue, nameof(initialValue));
            Throw.IfArgumentNull(aggregator, nameof(aggregator));

            if (option.HasValue)
                return option.Value.Aggregate(initialValue, aggregator);
            return initialValue;
        }
    }
}

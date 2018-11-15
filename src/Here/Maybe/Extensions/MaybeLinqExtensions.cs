using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Here.Maybes.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Maybe{T}"/> for Linq features.
    /// </summary>
    public static class MaybeLinqExtensions
    {
        /// <summary>
        /// Checks if this <see cref="Maybe{T}"/> has value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <returns>True if this <see cref="Maybe{T}"/> has a value, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool Any<T>(this Maybe<T> maybe)
        {
            return maybe.HasValue;
        }

        /// <summary>
        /// Checks if this <see cref="Maybe{T}"/> has value and matches the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="predicate">Predicate to use.</param>
        /// <returns>True if this <see cref="Maybe{T}"/> matches the predicate, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool Any<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Predicate<T> predicate)
        {
            return maybe.Exists(predicate);
        }

        /// <summary>
        /// Checks if this <see cref="Maybe{T}"/> has a value and if it matches the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="predicate">Predicate to check.</param>
        /// <returns>True if this <see cref="Maybe{T}"/> value matches the predicate, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool All<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Predicate<T> predicate)
        {
            return maybe.Exists(predicate);
        }

        /// <summary>
        /// Checks if this <see cref="Maybe{T}"/> contains the given <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="value">Value to check equality with <see cref="Maybe{T}"/> value.</param>
        /// <returns>True if this <see cref="Maybe{T}"/> contains the given <paramref name="value"/>, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool Contains<T>(this Maybe<T> maybe, [CanBeNull] T value)
        {
            if (maybe.HasValue)
                return EqualityComparer<T>.Default.Equals(maybe.Value, value);
            return false;
        }

        /// <summary>
        /// Selects via the <paramref name="selector"/> method something from this <see cref="Maybe{TIn}"/> if it has a value.
        /// </summary>
        /// <typeparam name="TIn">Type of the value embedded in this <see cref="Maybe{TIn}"/>.</typeparam>
        /// <typeparam name="TOut">Type of the value embedded in the selected value.</typeparam>
        /// <param name="maybe"><see cref="Maybe{TIn}"/> on which performing the selection.</param>
        /// <param name="selector">Method called to select the value from this <see cref="Maybe{TIn}"/> value.</param>
        /// <returns>A <see cref="Maybe{TOut}"/> wrapping selected value.</returns>
        [PublicAPI, Pure]
        public static Maybe<TOut> Select<TIn, TOut>(this Maybe<TIn> maybe, [NotNull, InstantHandle] Func<TIn, TOut> selector)
        {
            if (maybe.HasValue)
                return selector(maybe.Value);
            return Maybe<TOut>.None;
        }

        /// <summary>
        /// Returns this <see cref="Maybe{T}"/> if it has a value and matches the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the check.</param>
        /// <param name="predicate">Condition to match.</param>
        /// <returns>This <see cref="Maybe{T}"/> if it matches the <paramref name="predicate"/>, otherwise an empty <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> Where<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Predicate<T> predicate)
        {
            if (maybe.Exists(predicate))
                return maybe;
            return Maybe<T>.None;
        }

        /// <summary>
        /// Applies the given <paramref name="doAction"/> on this <see cref="Maybe{T}"/> value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the treatment.</param>
        /// <param name="doAction">Action to perform on this <see cref="Maybe{T}"/> value.</param>
        [PublicAPI]
        public static void ForEach<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Action<T> doAction)
        {
            if (maybe.HasValue)
                doAction(maybe.Value);
        }

        /// <summary>
        /// Computes the aggregation on this <see cref="Maybe{T}"/> value.
        /// Use the <paramref name="initialValue"/> as the initial aggregation value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TAggregate">Type of the aggregated value.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing the aggregation.</param>
        /// <param name="initialValue">The initial aggregation value.</param>
        /// <param name="aggregator">The aggregator function called on this <see cref="Maybe{T}"/> value.</param>
        /// <returns>This <see cref="Maybe{T}"/> value aggregated with <paramref name="initialValue"/>, 
        /// otherwise <paramref name="initialValue"/>.</returns>
        [PublicAPI, NotNull, Pure]
        public static TAggregate Aggregate<T, TAggregate>(this Maybe<T> maybe, 
            [NotNull] TAggregate initialValue, 
            [NotNull, InstantHandle] Func<TAggregate, T, TAggregate> aggregator)
        {
            if (maybe.HasValue)
                return aggregator(initialValue, maybe.Value);
            return initialValue;
        }
    }
}

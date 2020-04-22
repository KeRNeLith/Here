using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Option{T}"/> for Linq features.
    /// </summary>
    public static class OptionLinqExtensions
    {
        /// <summary>
        /// Checks if this <see cref="Option{T}"/> has value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <returns>True if this <see cref="Option{T}"/> has a value, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool Any<T>(in this Option<T> option)
        {
            return option.HasValue;
        }

        /// <summary>
        /// Checks if this <see cref="Option{T}"/> has value and matches the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <param name="predicate">Predicate to use.</param>
        /// <returns>True if this <see cref="Option{T}"/> matches the predicate, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
        public static bool Any<T>(in this Option<T> option, [NotNull, InstantHandle] in Predicate<T> predicate)
        {
            return option.Exists(predicate);
        }

        /// <summary>
        /// Checks if this <see cref="Option{T}"/> has a value and if it matches the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <param name="predicate">Predicate to check.</param>
        /// <returns>True if this <see cref="Option{T}"/> value matches the predicate, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
        public static bool All<T>(in this Option<T> option, [NotNull, InstantHandle] in Predicate<T> predicate)
        {
            return option.Exists(predicate);
        }

        /// <summary>
        /// Checks if this <see cref="Option{T}"/> contains the given <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <param name="value">Value to check equality with <see cref="Option{T}"/> value.</param>
        /// <returns>True if this <see cref="Option{T}"/> contains the given <paramref name="value"/>, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool Contains<T>(in this Option<T> option, [CanBeNull] in T value)
        {
            if (option.HasValue)
                return EqualityComparer<T>.Default.Equals(option._value, value);
            return false;
        }

        /// <summary>
        /// Selects via the <paramref name="selector"/> method something from this <see cref="Option{T}"/> if it has a value.
        /// </summary>
        /// <typeparam name="TIn">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TOut">Type of the value embedded in the selected value.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the selection.</param>
        /// <param name="selector">Method called to select the value from this <see cref="Option{T}"/> value.</param>
        /// <returns>An <see cref="Option{T}"/> wrapping selected value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="selector"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<TOut> Select<TIn, TOut>(in this Option<TIn> option, [NotNull, InstantHandle] in Func<TIn, TOut> selector)
        {            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            if (option.HasValue)
                return selector(option._value);
            return Option<TOut>.None;
        }

        /// <summary>
        /// Returns this <see cref="Option{T}"/> if it has a value and matches the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the check.</param>
        /// <param name="predicate">Condition to match.</param>
        /// <returns>This <see cref="Option{T}"/> if it matches the <paramref name="predicate"/>, otherwise an empty <see cref="Option{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<T> Where<T>(in this Option<T> option, [NotNull, InstantHandle] in Predicate<T> predicate)
        {
            if (option.Exists(predicate))
                return option;
            return Option<T>.None;
        }

        /// <summary>
        /// Applies the given <paramref name="doAction"/> on this <see cref="Option{T}"/> value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the treatment.</param>
        /// <param name="doAction">Action to perform on this <see cref="Option{T}"/> value.</param>
        /// <returns>A <see cref="Unit"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="doAction"/> is null.</exception>
        [PublicAPI]
        public static Unit ForEach<T>(in this Option<T> option, [NotNull, InstantHandle] in Action<T> doAction)
        {            if (doAction is null)
                throw new ArgumentNullException(nameof(doAction));

            if (option.HasValue)
                doAction(option._value);
            return Unit.Default;
        }

        /// <summary>
        /// Computes the aggregation on this <see cref="Option{T}"/> value.
        /// Use the <paramref name="initialValue"/> as the initial aggregation value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Option{T}"/>.</typeparam>
        /// <typeparam name="TAggregate">Type of the aggregated value.</typeparam>
        /// <param name="option"><see cref="Option{T}"/> on which performing the aggregation.</param>
        /// <param name="initialValue">The initial aggregation value.</param>
        /// <param name="aggregator">The aggregator function called on this <see cref="Option{T}"/> value.</param>
        /// <returns>This <see cref="Option{T}"/> value aggregated with <paramref name="initialValue"/>, 
        /// otherwise <paramref name="initialValue"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="initialValue"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="aggregator"/> is null.</exception>
        [PublicAPI, NotNull, Pure]
        public static TAggregate Aggregate<T, TAggregate>(in this Option<T> option, 
            [NotNull] in TAggregate initialValue, 
            [NotNull, InstantHandle] in Func<TAggregate, T, TAggregate> aggregator)
        {            if (initialValue == null)
                throw new ArgumentNullException(nameof(initialValue));            if (aggregator is null)
                throw new ArgumentNullException(nameof(aggregator));

            if (option.HasValue)
                return aggregator(initialValue, option._value);
            return initialValue;
        }
    }
}

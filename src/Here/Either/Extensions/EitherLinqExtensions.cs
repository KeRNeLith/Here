using System;
using System.Collections.Generic;
#if SUPPORTS_AGGRESSIVE_INLINING
using System.Runtime.CompilerServices;
#endif
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Either{TLeft,TRight}"/> for Linq features.
    /// </summary>
    public static class EitherLinqExtensions
    {
        /// <summary>
        /// Checks if this <see cref="Either{TLeft,TRight}"/> has a right value.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> on which performing the check.</param>
        /// <returns>True if this <see cref="Either{TLeft,TRight}"/> has a right value, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool Any<TLeft, TRight>(this Either<TLeft, TRight> either)
        {
            return either.IsRight;
        }

        /// <summary>
        /// Checks if this <see cref="Either{TLeft,TRight}"/> has a right value and matches the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> on which performing the check.</param>
        /// <param name="predicate">Predicate to use.</param>
        /// <returns>True if this <see cref="Either{TLeft,TRight}"/> has a right value and matches the predicate, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
        public static bool Any<TLeft, TRight>(this Either<TLeft, TRight> either, [NotNull, InstantHandle] Predicate<TRight> predicate)
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));

            return either.IsRight && predicate(either._right);
        }

        /// <summary>
        /// Checks if this <see cref="Either{TLeft,TRight}"/> has a right value and matches the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> on which performing the check.</param>
        /// <param name="predicate">Predicate to check.</param>
        /// <returns>True if this <see cref="Either{TLeft,TRight}"/> has a right value and matches the predicate, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool All<TLeft, TRight>(this Either<TLeft, TRight> either, [NotNull, InstantHandle] Predicate<TRight> predicate)
        {
            return either.Any(predicate);
        }

        /// <summary>
        /// Checks if this <see cref="Either{TLeft,TRight}"/> contains the given <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> on which performing the check.</param>
        /// <param name="value">Value to check equality with <see cref="Either{TLeft,TRight}"/> right value.</param>
        /// <returns>True if this <see cref="Either{TLeft,TRight}"/> contains the given <paramref name="value"/> as right value, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool Contains<TLeft, TRight>(this Either<TLeft, TRight> either, [CanBeNull] TRight value)
        {
            return either.IsRight && EqualityComparer<TRight>.Default.Equals(either._right, value);
        }

        /// <summary>
        /// Selects via the <paramref name="selector"/> method something from this <see cref="Either{TLeft,TRight}"/> if it has a right value.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRightOut">Type of the value embedded as right value in the output <see cref="Either{TLeft, TRightOut}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> on which performing the treatment.</param>
        /// <param name="selector">Method called to select the value from this <see cref="Either{TLeft,TRight}"/> value.</param>
        /// <returns>An <see cref="Either{TLeft,TRight}"/> wrapping selected value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="selector"/> is null.</exception>
        [PublicAPI, Pure]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Either<TLeft, TRightOut> Select<TLeft, TRight, TRightOut>(this Either<TLeft, TRight> either, [NotNull, InstantHandle] Func<TRight, TRightOut> selector)
        {
            return either.Map(selector);
        }

        /// <summary>
        /// Returns this <see cref="Either{TLeft,TRight}"/> if it has a right value and matches the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> on which performing the check.</param>
        /// <param name="predicate">Condition to match.</param>
        /// <returns>This <see cref="Option{T}"/> if it matches the <paramref name="predicate"/>, otherwise an empty <see cref="Option{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        [PublicAPI, Pure]
        public static Either<TLeft, TRight> Where<TLeft, TRight>(this Either<TLeft, TRight> either, [NotNull, InstantHandle] Predicate<TRight> predicate)
        {
            if (either.All(predicate))
                return either;
            return Either<TLeft, TRight>.None;
        }

        /// <summary>
        /// Applies the given <paramref name="doAction"/> on this <see cref="Either{TLeft,TRight}"/> right value.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> on which performing the treatment.</param>
        /// <param name="doAction">Action to perform on this <see cref="Either{TLeft,TRight}"/> right value.</param>
        /// <returns>A <see cref="Unit"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="doAction"/> is null.</exception>
        [PublicAPI]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Unit ForEach<TLeft, TRight>(this Either<TLeft, TRight> either, [NotNull, InstantHandle] Action<TRight> doAction)
        {
            return either.IfRight(doAction);
        }

        /// <summary>
        /// Computes the aggregation on this <see cref="Either{TLeft,TRight}"/> right value.
        /// Use the <paramref name="initialValue"/> as the initial aggregation value.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TAggregate">Type of the aggregated value.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> on which performing the aggregation.</param>
        /// <param name="initialValue">The initial aggregation value.</param>
        /// <param name="aggregator">The aggregator function called on this <see cref="Either{TLeft,TRight}"/> value.</param>
        /// <returns>This <see cref="Either{TLeft,TRight}"/> right value aggregated with <paramref name="initialValue"/>, 
        /// otherwise <paramref name="initialValue"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="initialValue"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="aggregator"/> is null.</exception>
        [PublicAPI, NotNull, Pure]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static TAggregate Aggregate<TLeft, TRight, TAggregate>(this Either<TLeft, TRight> either,
            [NotNull] TAggregate initialValue,
            [NotNull, InstantHandle] Func<TAggregate, TRight, TAggregate> aggregator)
        {
            return either.Fold(initialValue, aggregator);
        }
    }
}

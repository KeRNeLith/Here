using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Here.Maybes.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Maybe{T}"/> for Linq features (enumerable).
    /// </summary>
    public static class MaybeLinqEnumerableExtensions
    {
        /// <summary>
        /// Call the <paramref name="onItem"/> function on each item if this <see cref="Maybe{T}"/> has a value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="onItem">Treatment to do on each item.</param>
        /// <returns>This <see cref="Maybe{T}"/>.</returns>
        [PublicAPI]
        public static Maybe<T> ForEachItem<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Action<object> onItem)
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
        public static Maybe<T> ForEachItem<T, TItem>(this Maybe<T> maybe, [NotNull, InstantHandle] Action<TItem> onItem)
            where T : IEnumerable<TItem>
        {
            if (maybe.HasValue)
            {
                foreach (var item in maybe.Value)
                    onItem(item);
            }

            return maybe;
        }
    }
}

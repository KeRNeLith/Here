using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Here.Maybes.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Maybe{T}"/> for <see cref="IDictionary{TKey, TValue}"/>.
    /// </summary>
    public static class MaybeDictionaryExtensions
    {
        /// <summary>
        /// Try to get the value for the given key in the dictionary.
        /// </summary>
        /// <typeparam name="TKey"><see cref="Type"/> of this <see cref="IDictionary{TKey, TValue}"/> key.</typeparam>
        /// <typeparam name="TValue"><see cref="Type"/> of this <see cref="IDictionary{TKey, TValue}"/> value.</typeparam>
        /// <param name="dictionary"><see cref="IDictionary{TKey, TValue}"/> on which performing treatment.</param>
        /// <param name="key">Searched key.</param>
        /// <returns><see cref="Maybe{TValue}"/> that wrap the result of the get.</returns>
        public static Maybe<TValue> TryGetValue<TKey, TValue>([NotNull] this IDictionary<TKey, TValue> dictionary, [NotNull] TKey key)
        {
            var getter = MaybeTryGetExtensions.CreateGet<TKey, TValue>(dictionary.TryGetValue);
            return getter(key);
        }

        /// <summary>
        /// Try to get the value for the given key in the dictionary.
        /// </summary>
        /// <typeparam name="TKey"><see cref="Type"/> of this <see cref="IReadOnlyDictionary{TKey, TValue}"/> key.</typeparam>
        /// <typeparam name="TValue"><see cref="Type"/> of this <see cref="IReadOnlyDictionary{TKey, TValue}"/> value.</typeparam>
        /// <param name="dictionary"><see cref="IReadOnlyDictionary{TKey, TValue}"/> on which performing treatment.</param>
        /// <param name="key">Searched key.</param>
        /// <returns><see cref="Maybe{TValue}"/> that wrap the result of the get.</returns>
        public static Maybe<TValue> TryGetReadonlyValue<TKey, TValue>([NotNull] this IReadOnlyDictionary<TKey, TValue> dictionary, [NotNull] TKey key)
        {
            var getter = MaybeTryGetExtensions.CreateGet<TKey, TValue>(dictionary.TryGetValue);
            return getter(key);
        }
    }
}

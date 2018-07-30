﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Here.Maybes.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Maybe{T}"/> for <see cref="IDictionary{TKey, TValue}"/>.
    /// </summary>
    public static class MaybeDictionaryExtensions
    {
        // Helper method to try get a value
        [Pure]
        private static Maybe<TValue> TryGetValue<TKey, TValue>(MaybeTryGetExtensions.TryGet<TKey, TValue> tryGetFunc, [CanBeNull] TKey key)
        {
            if (key == null)
                return Maybe<TValue>.None;

            var getter = MaybeTryGetExtensions.CreateGet(tryGetFunc);
            return getter(key);
        }

        /// <summary>
        /// Try to get the value for the given key in the dictionary.
        /// </summary>
        /// <typeparam name="TKey"><see cref="Type"/> of this <see cref="IDictionary{TKey, TValue}"/> key.</typeparam>
        /// <typeparam name="TValue"><see cref="Type"/> of this <see cref="IDictionary{TKey, TValue}"/> value.</typeparam>
        /// <param name="dictionary"><see cref="IDictionary{TKey, TValue}"/> on which performing treatment.</param>
        /// <param name="key">Searched key.</param>
        /// <returns><see cref="Maybe{TValue}"/> that wrap the result of the get.</returns>
        [PublicAPI, Pure]
        public static Maybe<TValue> TryGetValue<TKey, TValue>([NotNull] this IDictionary<TKey, TValue> dictionary, [CanBeNull] TKey key)
        {
            return TryGetValue<TKey, TValue>(dictionary.TryGetValue, key);
        }

        /// <summary>
        /// Try to get the value for the given key in the dictionary.
        /// </summary>
        /// <typeparam name="TKey"><see cref="Type"/> of this <see cref="IDictionary{TKey, Object}"/> key.</typeparam>
        /// <typeparam name="TValue"><see cref="Type"/> of the expected value.</typeparam>
        /// <param name="dictionary"><see cref="IDictionary{TKey, Object}"/> on which performing treatment.</param>
        /// <param name="key">Searched key.</param>
        /// <returns><see cref="Maybe{TValue}"/> that wrap the result of the get.</returns>
        [PublicAPI, Pure]
        public static Maybe<TValue> TryGetValue<TKey, TValue>([NotNull] this IDictionary<TKey, object> dictionary, [CanBeNull] TKey key)
        {
            var objectValue = dictionary.TryGetValue(key);
            if (objectValue.HasValue && objectValue.Value is TValue expectedValue)
                return Maybe<TValue>.Some(expectedValue);
            return Maybe<TValue>.None;
        }

        /// <summary>
        /// Try to get the value for the given key in the dictionary.
        /// </summary>
        /// <typeparam name="TKey"><see cref="Type"/> of this <see cref="IReadOnlyDictionary{TKey, TValue}"/> key.</typeparam>
        /// <typeparam name="TValue"><see cref="Type"/> of this <see cref="IReadOnlyDictionary{TKey, TValue}"/> value.</typeparam>
        /// <param name="dictionary"><see cref="IReadOnlyDictionary{TKey, TValue}"/> on which performing treatment.</param>
        /// <param name="key">Searched key.</param>
        /// <returns><see cref="Maybe{TValue}"/> that wrap the result of the get.</returns>
        [PublicAPI, Pure]
        public static Maybe<TValue> TryGetReadonlyValue<TKey, TValue>([NotNull] this IReadOnlyDictionary<TKey, TValue> dictionary, [CanBeNull] TKey key)
        {
            return TryGetValue<TKey, TValue>(dictionary.TryGetValue, key);
        }

        /// <summary>
        /// Try to get the value for the given key in the dictionary.
        /// </summary>
        /// <typeparam name="TKey"><see cref="Type"/> of this <see cref="IReadOnlyDictionary{TKey, Object}"/> key.</typeparam>
        /// <typeparam name="TValue"><see cref="Type"/> of the expected value.</typeparam>
        /// <param name="dictionary"><see cref="IReadOnlyDictionary{TKey, Object}"/> on which performing treatment.</param>
        /// <param name="key">Searched key.</param>
        /// <returns><see cref="Maybe{TValue}"/> that wrap the result of the get.</returns>
        [PublicAPI, Pure]
        public static Maybe<TValue> TryGetReadonlyValue<TKey, TValue>([NotNull] this IReadOnlyDictionary<TKey, object> dictionary, [CanBeNull] TKey key)
        {
            var objectValue = dictionary.TryGetReadonlyValue(key);
            if (objectValue.HasValue && objectValue.Value is TValue expectedValue)
                return Maybe<TValue>.Some(expectedValue);
            return Maybe<TValue>.None;
        }
    }
}

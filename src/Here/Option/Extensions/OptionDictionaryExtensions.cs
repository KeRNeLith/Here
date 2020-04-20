using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Option{T}"/> for <see cref="IDictionary{TKey, TValue}"/>.
    /// </summary>
    public static class OptionDictionaryExtensions
    {
        // Helper method to try get a value
        [Pure]
        private static Option<TValue> TryGetValue<TKey, TValue>([NotNull] in OptionTryGetExtensions.TryGet<TKey, TValue> tryGetFunc, [CanBeNull] in TKey key)
        {
            if (key == null)
                return Option<TValue>.None;

            return OptionTryGetExtensions.Get(key, tryGetFunc);
        }

        /// <summary>
        /// Try to get the value for the given key in this dictionary.
        /// </summary>
        /// <typeparam name="TKey"><see cref="Type"/> of this <see cref="IDictionary{TKey, TValue}"/> key.</typeparam>
        /// <typeparam name="TValue"><see cref="Type"/> of this <see cref="IDictionary{TKey, TValue}"/> value.</typeparam>
        /// <param name="dictionary"><see cref="IDictionary{TKey, TValue}"/> on which performing the get.</param>
        /// <param name="key">Searched key.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the get.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="dictionary"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<TValue> TryGetValue<TKey, TValue>([NotNull] this IDictionary<TKey, TValue> dictionary, [CanBeNull] in TKey key)
        {
            Throw.IfArgumentNull(dictionary, nameof(dictionary));

            return TryGetValue<TKey, TValue>(dictionary.TryGetValue, key);
        }

        /// <summary>
        /// Try to get the value for the given key in the dictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of this <see cref="IDictionary{TKey, Object}"/> key.</typeparam>
        /// <typeparam name="TValue">Type of the expected value.</typeparam>
        /// <param name="dictionary"><see cref="IDictionary{TKey, Object}"/> on which performing the get.</param>
        /// <param name="key">Searched key.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the get.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="dictionary"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<TValue> TryGetValue<TKey, TValue>([NotNull] this IDictionary<TKey, object> dictionary, [CanBeNull] in TKey key)
        {
            Throw.IfArgumentNull(dictionary, nameof(dictionary));

            Option<object> objectValue = dictionary.TryGetValue(key);
            if (objectValue.HasValue && objectValue._value is TValue expectedValue)
                return Option<TValue>.Some(expectedValue);
            return Option<TValue>.None;
        }

#if SUPPORTS_READONLY_DICTIONARY
        /// <summary>
        /// Try to get the value for the given key in the dictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of this <see cref="IReadOnlyDictionary{TKey, TValue}"/> key.</typeparam>
        /// <typeparam name="TValue">Type of this <see cref="IReadOnlyDictionary{TKey, TValue}"/> value.</typeparam>
        /// <param name="dictionary"><see cref="IReadOnlyDictionary{TKey, TValue}"/> on which performing the get.</param>
        /// <param name="key">Searched key.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the get.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="dictionary"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<TValue> TryGetReadonlyValue<TKey, TValue>([NotNull] this IReadOnlyDictionary<TKey, TValue> dictionary, [CanBeNull] in TKey key)
        {
            Throw.IfArgumentNull(dictionary, nameof(dictionary));

            return TryGetValue<TKey, TValue>(dictionary.TryGetValue, key);
        }

        /// <summary>
        /// Try to get the value for the given key in the dictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of this <see cref="IReadOnlyDictionary{TKey, Object}"/> key.</typeparam>
        /// <typeparam name="TValue">Type of the expected value.</typeparam>
        /// <param name="dictionary"><see cref="IReadOnlyDictionary{TKey, Object}"/> on which performing the get.</param>
        /// <param name="key">Searched key.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the get.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="dictionary"/> is null.</exception>
        [PublicAPI, Pure]
        public static Option<TValue> TryGetReadonlyValue<TKey, TValue>([NotNull] this IReadOnlyDictionary<TKey, object> dictionary, [CanBeNull] in TKey key)
        {
            Throw.IfArgumentNull(dictionary, nameof(dictionary));

            Option<object> objectValue = dictionary.TryGetReadonlyValue(key);
            if (objectValue.HasValue && objectValue._value is TValue expectedValue)
                return Option<TValue>.Some(expectedValue);
            return Option<TValue>.None;
        }
#endif
    }
}

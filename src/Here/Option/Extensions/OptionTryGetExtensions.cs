﻿using System;
using System.Globalization;
#if (!NET20 && !NET30 && !NET35 && !NET40)
using System.Runtime.CompilerServices;
#endif
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Option{T}"/> for trying to get value methods.
    /// </summary>
    public static class OptionTryGetExtensions
    {
        /// <summary>
        /// Try Get delegate that allow to get value from an object.
        /// </summary>
        /// <typeparam name="TInput">Type of the input in which getting the value.</typeparam>
        /// <typeparam name="TValue">Type of the value to try get.</typeparam>
        /// <param name="input">Input object in which getting the value.</param>
        /// <param name="value">Obtained value.</param>
        /// <returns>True if the get succeed, otherwise false.</returns>
        public delegate bool TryGet<in TInput, TValue>([CanBeNull] TInput input, out TValue value);

        /// <summary>
        /// Try parse delegate that allow to parse a string to get a value.
        /// </summary>
        /// <typeparam name="TValue">Type of the value to try parse.</typeparam>
        /// <param name="input">Input string to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <param name="value">Obtained value.</param>
        /// <returns>True if the parse succeed, otherwise false.</returns>
        public delegate bool TryParse<TValue>([CanBeNull] string input, NumberStyles style, IFormatProvider culture, out TValue value);

        /// <summary>
        /// Getter method that try to get a value from input with the given try get function 
        /// and create an <see cref="Option{T}"/> with the result.
        /// </summary>
        /// <typeparam name="TInput">Type of the input in which getting the value.</typeparam>
        /// <typeparam name="TValue">Type of the value to try get.</typeparam>
        /// <param name="input">Input in which getting something.</param>
        /// <param name="tryGetFunc">Try get method.</param>
        /// <returns>The result of the try get wrapped in an <see cref="Option{T}"/>.</returns>
        [PublicAPI, NotNull, Pure]
#if (!NET20 && !NET30 && !NET35 && !NET40)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Option<TValue> Get<TInput, TValue>([CanBeNull] in TInput input, [NotNull, InstantHandle] in TryGet<TInput, TValue> tryGetFunc)
        {
            return tryGetFunc(input, out TValue result)
                ? result
                : Option<TValue>.None;
        }

        [NotNull]
        private static readonly CultureInfo DefaultParseCultureInfo = new CultureInfo("en-US");

        /// <summary>
        /// Parse method that try to parse a value from input string with the given try parse function 
        /// and create an <see cref="Option{T}"/> with the result.
        /// </summary>
        /// <typeparam name="TValue">Type of the value to try parse.</typeparam>
        /// <param name="input">String to parse.</param>
        /// <param name="tryParseFunc">Try parse method.</param>
        /// <returns>The result of the try parse wrapped in an <see cref="Option{T}"/>.</returns>
        [PublicAPI, NotNull, Pure]
#if (!NET20 && !NET30 && !NET35 && !NET40)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Option<TValue> DefaultParse<TValue>([CanBeNull] in string input, [NotNull, InstantHandle] in TryParse<TValue> tryParseFunc)
        {
            return Parse(input, tryParseFunc, NumberStyles.Any, DefaultParseCultureInfo);
        }

        /// <summary>
        /// Parse method that try to parse a value from input string with the given try parse function 
        /// and create an <see cref="Option{T}"/> with the result.
        /// </summary>
        /// <typeparam name="TValue">Type of the value to try parse.</typeparam>
        /// <param name="input">String to parse.</param>
        /// <param name="tryParseFunc">Try parse method.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns>The result of the try parse wrapped in an <see cref="Option{T}"/>.</returns>
        [PublicAPI, NotNull, Pure]
#if (!NET20 && !NET30 && !NET35 && !NET40)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Option<TValue> Parse<TValue>([CanBeNull] in string input, [NotNull, InstantHandle] in TryParse<TValue> tryParseFunc, in NumberStyles style, in IFormatProvider culture)
        {
            return tryParseFunc(input, style, culture, out TValue result)
                ? result
                : Option<TValue>.None;
        }

        #region Common TryParse

        /// <summary>
        /// Try to parse a boolean from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<bool> TryParseBool([CanBeNull] this string str)
        {
            return Get<string, bool>(str, bool.TryParse);
        }

        /// <summary>
        /// Try to parse a char from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<char> TryParseChar([CanBeNull] this string str)
        {
            return Get<string, char>(str, char.TryParse);
        }

        /// <summary>
        /// Try to parse a byte from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<byte> TryParseByte([CanBeNull] this string str)
        {
            return Get<string, byte>(str, byte.TryParse);
        }

        /// <summary>
        /// Try to parse a byte from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<byte> TryParseByte([CanBeNull] this string str, in NumberStyles style, in IFormatProvider culture)
        {
            return Parse<byte>(str, byte.TryParse, style, culture);
        }

        /// <summary>
        /// Try to parse an sbyte from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> TryParseSByte([CanBeNull] this string str)
        {
            return Get<string, sbyte>(str, sbyte.TryParse);
        }

        /// <summary>
        /// Try to parse an sbyte from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> TryParseSByte([CanBeNull] this string str, in NumberStyles style, in IFormatProvider culture)
        {
            return Parse<sbyte>(str, sbyte.TryParse, style, culture);
        }

        /// <summary>
        /// Try to parse a short from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<short> TryParseShort([CanBeNull] this string str)
        {
            return Get<string, short>(str, short.TryParse);
        }

        /// <summary>
        /// Try to parse a short from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<short> TryParseShort([CanBeNull] this string str, in NumberStyles style, in IFormatProvider culture)
        {
            return Parse<short>(str, short.TryParse, style, culture);
        }

        /// <summary>
        /// Try to parse an ushort from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> TryParseUShort([CanBeNull] this string str)
        {       
            return Get<string, ushort>(str, ushort.TryParse);
        }

        /// <summary>
        /// Try to parse an ushort from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> TryParseUShort([CanBeNull] this string str, in NumberStyles style, in IFormatProvider culture)
        {
            return Parse<ushort>(str, ushort.TryParse, style, culture);
        }

        /// <summary>
        /// Try to parse an int from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<int> TryParseInt([CanBeNull] this string str)
        {
            return Get<string, int>(str, int.TryParse);
        }

        /// <summary>
        /// Try to parse an int from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<int> TryParseInt([CanBeNull] this string str, in NumberStyles style, in IFormatProvider culture)
        {
            return Parse<int>(str, int.TryParse, style, culture);
        }

        /// <summary>
        /// Try to parse an uint from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<uint> TryParseUInt([CanBeNull] this string str)
        {
            return Get<string, uint>(str, uint.TryParse);
        }

        /// <summary>
        /// Try to parse an uint from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<uint> TryParseUInt([CanBeNull] this string str, in NumberStyles style, in IFormatProvider culture)
        {
            return Parse<uint>(str, uint.TryParse, style, culture);
        }

        /// <summary>
        /// Try to parse a long from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<long> TryParseLong([CanBeNull] this string str)
        {
            return Get<string, long>(str, long.TryParse);
        }

        /// <summary>
        /// Try to parse a long from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<long> TryParseLong([CanBeNull] this string str, in NumberStyles style, in IFormatProvider culture)
        {
            return Parse<long>(str, long.TryParse, style, culture);
        }

        /// <summary>
        /// Try to parse an ulong from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> TryParseULong([CanBeNull] this string str)
        {
            return Get<string, ulong>(str, ulong.TryParse);
        }

        /// <summary>
        /// Try to parse an ulong from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> TryParseULong([CanBeNull] this string str, in NumberStyles style, in IFormatProvider culture)
        {
            return Parse<ulong>(str, ulong.TryParse, style, culture);
        }

        /// <summary>
        /// Try to parse a decimal from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> TryParseDecimal([CanBeNull] this string str)
        {
            return Get<string, decimal>(str, decimal.TryParse);
        }

        /// <summary>
        /// Try to parse a decimal from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> TryParseDecimal([CanBeNull] this string str, in NumberStyles style, in IFormatProvider culture)
        {
            return Parse<decimal>(str, decimal.TryParse, style, culture);
        }

        /// <summary>
        /// Try to parse a float from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<float> TryParseFloat([CanBeNull] this string str)
        {
            return DefaultParse<float>(str, float.TryParse);
        }

        /// <summary>
        /// Try to parse a float from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<float> TryParseFloat([CanBeNull] this string str, in NumberStyles style, in IFormatProvider culture)
        {
            return Parse<float>(str, float.TryParse, style, culture);
        }

        /// <summary>
        /// Try to parse a double from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<double> TryParseDouble([CanBeNull] this string str)
        {
            return DefaultParse<double>(str, double.TryParse);
        }

        /// <summary>
        /// Try to parse a double from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<double> TryParseDouble([CanBeNull] this string str, in NumberStyles style, in IFormatProvider culture)
        {
            return Parse<double>(str, double.TryParse, style, culture);
        }

        #endregion
    }
}

using System;
using System.Globalization;
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
        /// Create a Getter method that try to get a value from input with the given try get function 
        /// and create an <see cref="Option{T}"/> with the result.
        /// </summary>
        /// <typeparam name="TInput">Type of the input in which getting the value.</typeparam>
        /// <typeparam name="TValue">Type of the value to try get.</typeparam>
        /// <param name="tryGetFunc">Try get method.</param>
        /// <returns>The result of the try get wrapped in an <see cref="Option{T}"/>.</returns>
        [PublicAPI, NotNull, Pure]
        public static Func<TInput, Option<TValue>> CreateGet<TInput, TValue>([NotNull, InstantHandle] TryGet<TInput, TValue> tryGetFunc)
        {
            return input => tryGetFunc(input, out TValue result)
                ? result
                : Option<TValue>.None;
        }

        [NotNull]
        private static readonly CultureInfo DefaultParseCultureInfo = new CultureInfo("en-US");

        /// <summary>
        /// Create a Parse method that try to parse a value from input string with the given try parse function 
        /// and create an <see cref="Option{T}"/> with the result.
        /// </summary>
        /// <typeparam name="TValue">Type of the value to try parse.</typeparam>
        /// <param name="tryParseFunc">Try parse method.</param>
        /// <returns>The result of the try parse wrapped in an <see cref="Option{T}"/>.</returns>
        [PublicAPI, NotNull, Pure]
        public static Func<string, Option<TValue>> CreateDefaultParse<TValue>([NotNull, InstantHandle] in TryParse<TValue> tryParseFunc)
        {
            return CreateParse(tryParseFunc, NumberStyles.Any, DefaultParseCultureInfo);
        }

        /// <summary>
        /// Create a Parse method that try to parse a value from input string with the given try parse function 
        /// and create an <see cref="Option{T}"/> with the result.
        /// </summary>
        /// <typeparam name="TValue">Type of the value to try parse.</typeparam>
        /// <param name="tryParseFunc">Try parse method.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns>The result of the try parse wrapped in an <see cref="Option{T}"/>.</returns>
        [NotNull, Pure]
        private static Func<string, Option<TValue>> CreateParse<TValue>([NotNull, InstantHandle] TryParse<TValue> tryParseFunc, NumberStyles style, IFormatProvider culture)
        {
            return input => tryParseFunc(input, style, culture, out TValue result)
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
            var getter = CreateGet<string, bool>(bool.TryParse);
            return getter(str);
        }

        /// <summary>
        /// Try to parse a char from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<char> TryParseChar([CanBeNull] this string str)
        {
            var getter = CreateGet<string, char>(char.TryParse);
            return getter(str);
        }

        /// <summary>
        /// Try to parse a byte from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<byte> TryParseByte([CanBeNull] this string str)
        {
            var getter = CreateGet<string, byte>(byte.TryParse);
            return getter(str);
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
            var getter = CreateParse<byte>(byte.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try to parse an sbyte from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> TryParseSByte([CanBeNull] this string str)
        {
            var getter = CreateGet<string, sbyte>(sbyte.TryParse);
            return getter(str);
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
            var getter = CreateParse<sbyte>(sbyte.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try to parse a short from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<short> TryParseShort([CanBeNull] this string str)
        {
            var getter = CreateGet<string, short>(short.TryParse);
            return getter(str);
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
            var getter = CreateParse<short>(short.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try to parse an ushort from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> TryParseUShort([CanBeNull] this string str)
        {       
            var getter = CreateGet<string, ushort>(ushort.TryParse);
            return getter(str);
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
            var getter = CreateParse<ushort>(ushort.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try to parse an int from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<int> TryParseInt([CanBeNull] this string str)
        {
            var getter = CreateGet<string, int>(int.TryParse);
            return getter(str);
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
            var getter = CreateParse<int>(int.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try to parse an uint from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<uint> TryParseUInt([CanBeNull] this string str)
        {
            var getter = CreateGet<string, uint>(uint.TryParse);
            return getter(str);
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
            var getter = CreateParse<uint>(uint.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try to parse a long from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<long> TryParseLong([CanBeNull] this string str)
        {
            var getter = CreateGet<string, long>(long.TryParse);
            return getter(str);
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
            var getter = CreateParse<long>(long.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try to parse an ulong from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> TryParseULong([CanBeNull] this string str)
        {
            var getter = CreateGet<string, ulong>(ulong.TryParse);
            return getter(str);
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
            var getter = CreateParse<ulong>(ulong.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try to parse a decimal from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> TryParseDecimal([CanBeNull] this string str)
        {
            var getter = CreateGet<string, decimal>(decimal.TryParse);
            return getter(str);
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
            var getter = CreateParse<decimal>(decimal.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try to parse a float from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<float> TryParseFloat([CanBeNull] this string str)
        {
            var getter = CreateDefaultParse<float>(float.TryParse);
            return getter(str);
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
            var getter = CreateParse<float>(float.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try to parse a double from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<double> TryParseDouble([CanBeNull] this string str)
        {
            var getter = CreateDefaultParse<double>(double.TryParse);
            return getter(str);
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
            var getter = CreateParse<double>(double.TryParse, style, culture);
            return getter(str);
        }

        #endregion
    }
}

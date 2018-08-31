using System;
using System.Globalization;
using JetBrains.Annotations;

namespace Here.Maybes.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Maybe{T}"/> for trying to get value methods.
    /// </summary>
    public static class MaybeTryGetExtensions
    {
        /// <summary>
        /// Try Get delegate that fit common parse or dictionary get methods.
        /// </summary>
        /// <typeparam name="TInput"><see cref="Type"/> of the input/key.</typeparam>
        /// <typeparam name="TValue"><see cref="Type"/> of the value to try get.</typeparam>
        /// <param name="input">Input of the Try get.</param>
        /// <param name="value">Obtained value.</param>
        /// <returns>Indicate if the get succeed.</returns>
        public delegate bool TryGet<in TInput, TValue>([CanBeNull] TInput input, out TValue value);

        /// <summary>
        /// Try parse delegate that fit common parse methods.
        /// </summary>
        /// <typeparam name="TInput"><see cref="Type"/> of the input.</typeparam>
        /// <typeparam name="TValue"><see cref="Type"/> of the value to try get.</typeparam>
        /// <param name="input">Input of the Try parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <param name="value">Obtained value.</param>
        /// <returns>Indicate if the get succeed.</returns>
        public delegate bool TryParse<in TInput, TValue>([CanBeNull] TInput input, NumberStyles style, IFormatProvider culture, out TValue value);

        /// <summary>
        /// Create a Get methods that try to get a value from input with given try get function 
        /// and create a <see cref="Maybe{TValue}"/> with the result. TryGet methods are like int.TryParse, etc.
        /// </summary>
        /// <typeparam name="TInput"><see cref="Type"/> of the input/key.</typeparam>
        /// <typeparam name="TValue"><see cref="Type"/> of the value to try get.</typeparam>
        /// <param name="tryGetFunc">Try get method.</param>
        /// <returns>The result of the try get as <see cref="Maybe{TValue}"/></returns>
        [NotNull, Pure]
        public static Func<TInput, Maybe<TValue>> CreateGet<TInput, TValue>([NotNull, InstantHandle] TryGet<TInput, TValue> tryGetFunc)
        {
            return input => tryGetFunc(input, out TValue result)
                ? result
                : Maybe<TValue>.None;
        }

        private static readonly CultureInfo DefaultParseCultureInfo = new CultureInfo("en-US");

        /// <summary>
        /// Create a Get methods that try to get a value from input with given try get function 
        /// and create a <see cref="Maybe{TValue}"/> with the result. TryGet methods are like int.TryParse, etc.
        /// </summary>
        /// <typeparam name="TInput"><see cref="Type"/> of the input/key.</typeparam>
        /// <typeparam name="TValue"><see cref="Type"/> of the value to try get.</typeparam>
        /// <param name="tryParseFunc">Try get method.</param>
        /// <returns>The result of the try get as <see cref="Maybe{TValue}"/></returns>
        [NotNull, Pure]
        public static Func<TInput, Maybe<TValue>> CreateDefaultParse<TInput, TValue>([NotNull, InstantHandle] TryParse<TInput, TValue> tryParseFunc)
        {
            return CreateParse(tryParseFunc, NumberStyles.Any, DefaultParseCultureInfo);
        }

        /// <summary>
        /// Create a Get methods that try to get a value from input with given try get function 
        /// and create a <see cref="Maybe{TValue}"/> with the result. TryGet methods are like int.TryParse, etc.
        /// </summary>
        /// <typeparam name="TInput"><see cref="Type"/> of the input/key.</typeparam>
        /// <typeparam name="TValue"><see cref="Type"/> of the value to try get.</typeparam>
        /// <param name="tryGetFunc">Try get method.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns>The result of the try get as <see cref="Maybe{TValue}"/></returns>
        [NotNull, Pure]
        public static Func<TInput, Maybe<TValue>> CreateParse<TInput, TValue>([NotNull, InstantHandle] TryParse<TInput, TValue> tryGetFunc, NumberStyles style, IFormatProvider culture)
        {
            return input => tryGetFunc(input, style, culture, out TValue result)
                ? result
                : Maybe<TValue>.None;
        }

        #region Common TryParse

        /// <summary>
        /// Try parse a bool from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Maybe{Boolean}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<bool> TryParseBool([CanBeNull] this string str)
        {
            var getter = CreateGet<string, bool>(bool.TryParse);
            return getter(str);
        }

        /// <summary>
        /// Try parse a char from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Maybe{Char}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<char> TryParseChar([CanBeNull] this string str)
        {
            var getter = CreateGet<string, char>(char.TryParse);
            return getter(str);
        }

        /// <summary>
        /// Try parse a byte from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Maybe{Byte}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> TryParseByte([CanBeNull] this string str)
        {
            var getter = CreateGet<string, byte>(byte.TryParse);
            return getter(str);
        }

        /// <summary>
        /// Try parse a byte from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Maybe{Byte}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> TryParseByte([CanBeNull] this string str, NumberStyles style, IFormatProvider culture)
        {
            var getter = CreateParse<string, byte>(byte.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try parse an sbyte from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Maybe{SByte}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> TryParseSByte([CanBeNull] this string str)
        {
            var getter = CreateGet<string, sbyte>(sbyte.TryParse);
            return getter(str);
        }

        /// <summary>
        /// Try parse an sbyte from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Maybe{SByte}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> TryParseSByte([CanBeNull] this string str, NumberStyles style, IFormatProvider culture)
        {
            var getter = CreateParse<string, sbyte>(sbyte.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try parse a short from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Maybe{Int16}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> TryParseShort([CanBeNull] this string str)
        {
            var getter = CreateGet<string, short>(short.TryParse);
            return getter(str);
        }

        /// <summary>
        /// Try parse a short from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Maybe{Int16}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> TryParseShort([CanBeNull] this string str, NumberStyles style, IFormatProvider culture)
        {
            var getter = CreateParse<string, short>(short.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try parse an ushort from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Maybe{UInt16}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> TryParseUShort([CanBeNull] this string str)
        {       
            var getter = CreateGet<string, ushort>(ushort.TryParse);
            return getter(str);
        }

        /// <summary>
        /// Try parse an ushort from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Maybe{UInt16}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> TryParseUShort([CanBeNull] this string str, NumberStyles style, IFormatProvider culture)
        {
            var getter = CreateParse<string, ushort>(ushort.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try parse an int from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Maybe{Int32}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> TryParseInt([CanBeNull] this string str)
        {
            var getter = CreateGet<string, int>(int.TryParse);
            return getter(str);
        }

        /// <summary>
        /// Try parse an int from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Maybe{Int32}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> TryParseInt([CanBeNull] this string str, NumberStyles style, IFormatProvider culture)
        {
            var getter = CreateParse<string, int>(int.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try parse an uint from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Maybe{UInt32}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> TryParseUInt([CanBeNull] this string str)
        {
            var getter = CreateGet<string, uint>(uint.TryParse);
            return getter(str);
        }

        /// <summary>
        /// Try parse an uint from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Maybe{UInt32}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> TryParseUInt([CanBeNull] this string str, NumberStyles style, IFormatProvider culture)
        {
            var getter = CreateParse<string, uint>(uint.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try parse a long from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Maybe{Int64}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> TryParseLong([CanBeNull] this string str)
        {
            var getter = CreateGet<string, long>(long.TryParse);
            return getter(str);
        }

        /// <summary>
        /// Try parse a long from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Maybe{Int64}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> TryParseLong([CanBeNull] this string str, NumberStyles style, IFormatProvider culture)
        {
            var getter = CreateParse<string, long>(long.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try parse an ulong from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Maybe{UInt64}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> TryParseULong([CanBeNull] this string str)
        {
            var getter = CreateGet<string, ulong>(ulong.TryParse);
            return getter(str);
        }

        /// <summary>
        /// Try parse an ulong from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Maybe{UInt64}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> TryParseULong([CanBeNull] this string str, NumberStyles style, IFormatProvider culture)
        {
            var getter = CreateParse<string, ulong>(ulong.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try parse a decimal from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Maybe{Decimal}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> TryParseDecimal([CanBeNull] this string str)
        {
            var getter = CreateGet<string, decimal>(decimal.TryParse);
            return getter(str);
        }

        /// <summary>
        /// Try parse a decimal from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Maybe{Decimal}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> TryParseDecimal([CanBeNull] this string str, NumberStyles style, IFormatProvider culture)
        {
            var getter = CreateParse<string, decimal>(decimal.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try parse a float from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Maybe{Single}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> TryParseFloat([CanBeNull] this string str)
        {
            var getter = CreateDefaultParse<string, float>(float.TryParse);
            return getter(str);
        }

        /// <summary>
        /// Try parse a float from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Maybe{Single}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> TryParseFloat([CanBeNull] this string str, NumberStyles style, IFormatProvider culture)
        {
            var getter = CreateParse<string, float>(float.TryParse, style, culture);
            return getter(str);
        }

        /// <summary>
        /// Try parse a double from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Maybe{Double}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> TryParseDouble([CanBeNull] this string str)
        {
            var getter = CreateDefaultParse<string, double>(double.TryParse);
            return getter(str);
        }

        /// <summary>
        /// Try parse a double from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="style">Style number to use.</param>
        /// <param name="culture">Format provider (culture) to use.</param>
        /// <returns><see cref="Maybe{Double}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> TryParseDouble([CanBeNull] this string str, NumberStyles style, IFormatProvider culture)
        {
            var getter = CreateParse<string, double>(double.TryParse, style, culture);
            return getter(str);
        }

        #endregion
    }
}

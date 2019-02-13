﻿using System;
using System.Globalization;
#if SUPPORTS_AGGRESSIVE_INLINING
using System.Runtime.CompilerServices;
#endif
using JetBrains.Annotations;
#if !SUPPORTS_NULL_EMPTY_OR_WHITE_SPACE
using static Here.HereHelpers;
#endif

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
        [PublicAPI, Pure]
#if SUPPORTS_AGGRESSIVE_INLINING
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
        [PublicAPI, Pure]
#if SUPPORTS_AGGRESSIVE_INLINING
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
        [PublicAPI, Pure]
#if SUPPORTS_AGGRESSIVE_INLINING
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
        /// Try to parse a <see cref="Boolean"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<bool> TryParseBool([CanBeNull] this string str)
        {
            return Get<string, bool>(str, bool.TryParse);
        }

        /// <summary>
        /// Try to parse a <see cref="Char"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<char> TryParseChar([CanBeNull] this string str)
        {
            return Get<string, char>(str, char.TryParse);
        }

        /// <summary>
        /// Try to parse a <see cref="Byte"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<byte> TryParseByte([CanBeNull] this string str)
        {
            return Get<string, byte>(str, byte.TryParse);
        }

        /// <summary>
        /// Try to parse a <see cref="Byte"/> from the given string.
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
        /// Try to parse an <see cref="SByte"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> TryParseSByte([CanBeNull] this string str)
        {
            return Get<string, sbyte>(str, sbyte.TryParse);
        }

        /// <summary>
        /// Try to parse an <see cref="SByte"/> from the given string.
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
        /// Try to parse a <see cref="Int16"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<short> TryParseShort([CanBeNull] this string str)
        {
            return Get<string, short>(str, short.TryParse);
        }

        /// <summary>
        /// Try to parse a <see cref="Int16"/> from the given string.
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
        /// Try to parse an <see cref="UInt16"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> TryParseUShort([CanBeNull] this string str)
        {       
            return Get<string, ushort>(str, ushort.TryParse);
        }

        /// <summary>
        /// Try to parse an <see cref="UInt16"/> from the given string.
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
        /// Try to parse an <see cref="Int32"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<int> TryParseInt([CanBeNull] this string str)
        {
            return Get<string, int>(str, int.TryParse);
        }

        /// <summary>
        /// Try to parse an <see cref="Int32"/> from the given string.
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
        /// Try to parse an <see cref="UInt32"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<uint> TryParseUInt([CanBeNull] this string str)
        {
            return Get<string, uint>(str, uint.TryParse);
        }

        /// <summary>
        /// Try to parse an <see cref="UInt32"/> from the given string.
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
        /// Try to parse a <see cref="Int64"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<long> TryParseLong([CanBeNull] this string str)
        {
            return Get<string, long>(str, long.TryParse);
        }

        /// <summary>
        /// Try to parse a <see cref="Int64"/> from the given string.
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
        /// Try to parse an <see cref="UInt64"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> TryParseULong([CanBeNull] this string str)
        {
            return Get<string, ulong>(str, ulong.TryParse);
        }

        /// <summary>
        /// Try to parse an <see cref="UInt64"/> from the given string.
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
        /// Try to parse a <see cref="Decimal"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> TryParseDecimal([CanBeNull] this string str)
        {
            return Get<string, decimal>(str, decimal.TryParse);
        }

        /// <summary>
        /// Try to parse a <see cref="Decimal"/> from the given string.
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
        /// Try to parse a <see cref="Single"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<float> TryParseFloat([CanBeNull] this string str)
        {
            return DefaultParse<float>(str, float.TryParse);
        }

        /// <summary>
        /// Try to parse a <see cref="Single"/> from the given string.
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
        /// Try to parse a <see cref="Double"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<double> TryParseDouble([CanBeNull] this string str)
        {
            return DefaultParse<double>(str, double.TryParse);
        }

        /// <summary>
        /// Try to parse a <see cref="Double"/> from the given string.
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

#if SUPPORTS_PARSE_GUID
        /// <summary>
        /// Try to parse a <see cref="Guid"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<Guid> TryParseGuid([CanBeNull] this string str)
        {
            return Get<string, Guid>(str, Guid.TryParse);
        }
#endif

        /// <summary>
        /// Try to parse a <see cref="DateTime"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<DateTime> TryParseDateTime([CanBeNull] this string str)
        {
            return Get<string, DateTime>(str, DateTime.TryParse);
        }

        /// <summary>
        /// Try to parse a <see cref="DateTimeOffset"/> from the given string.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{T}"/> that wrap the result of the parse.</returns>
        [PublicAPI, Pure]
        public static Option<DateTimeOffset> TryParseDateTimeOffset([CanBeNull] this string str)
        {
            return Get<string, DateTimeOffset>(str, DateTimeOffset.TryParse);
        }

        /// <summary>
        /// Try to parse an enumeration value from the given string to its <see cref="Enum"/> equivalent.
        /// </summary>
        /// <typeparam name="TEnum">Enumeration type.</typeparam>
        /// <param name="str">String to parse.</param>
        /// <returns><see cref="Option{TEnum}"/> that wrap the result of the parse.</returns>
        /// <exception cref="ArgumentException">If the <typeparamref name="TEnum"/> is not an enumeration type.</exception>
        [PublicAPI, Pure]
#if SUPPORTS_TRY_PARSE_ENUM && SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Option<TEnum> TryParseEnum<TEnum>([CanBeNull] this string str)
            where TEnum : struct
        {
#if SUPPORTS_TRY_PARSE_ENUM
            return Get<string, TEnum>(str, Enum.TryParse);
#else
            return str.TryParseEnum(typeof(TEnum))
                .IfOr(
                    enumValue => (TEnum)enumValue, 
                    Option<TEnum>.None);
#endif
        }

        /// <summary>
        /// Try to parse an enumeration value from the given string to its <see cref="Enum"/> equivalent.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <param name="enumType">Type of the <see cref="Enum"/> to parse.</param>
        /// <returns><see cref="Option{Object}"/> that wrap the result of the parse.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="enumType"/> is null.</exception>
        /// <exception cref="ArgumentException">If the <paramref name="enumType"/> is not an enumeration type.</exception>
        [PublicAPI, Pure]
        public static Option<object> TryParseEnum([CanBeNull] this string str, [NotNull] Type enumType)
        {
            Throw.IfArgumentNull(enumType, nameof(enumType));
#if SUPPORTS_SYSTEM_TYPE_IS_ENUM
            Throw.IfArgument(!enumType.IsEnum, nameof(enumType));
#endif

#if SUPPORTS_NULL_EMPTY_OR_WHITE_SPACE
            if (string.IsNullOrWhiteSpace(str))
#else
            if (IsNullOrWhiteSpace(str))
#endif
                return Option<object>.None;

            try
            {
                return Enum.Parse(enumType, str);
            }
#if SUPPORTS_SYSTEM_TYPE_IS_ENUM
            catch (Exception)
#else
            catch (Exception ex)
#endif
            {
#if !SUPPORTS_SYSTEM_TYPE_IS_ENUM
                // Because System.Type does not always provide IsEnum property
                // Based on Enum.Parse documentation, to know if we should throw
                // because the given enumType is not an enum, the caught exception
                // must be ArgumentException and concerning the enumType parameter.
                // If it's because the input string is null (or spaces) then it's checked
                // earlier and if it's a value out of the enum then the ParamName is not filled.
                if (ex is ArgumentException argEx 
                    && argEx.ParamName != null 
                    && argEx.ParamName.Equals("enumType", StringComparison.Ordinal))
                    throw;
#endif
                return Option<object>.None;
            }
        }

        #endregion
    }
}

using System;
#if SUPPORTS_AGGRESSIVE_INLINING
using System.Runtime.CompilerServices;
#endif
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Option{T}"/> for numeric types casts.
    /// </summary>
    public static class OptionNumericCastsExtensions
    {
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static Option<TTo> SafeConvert<TFrom, TTo>(this Option<TFrom> option, [NotNull, InstantHandle] Func<TFrom, TTo> converter)
        {
            return option.Cast(
                input =>
                {
                    try
                    {
                        return converter(input);
                    }
                    catch
                    {
                        return Option<TTo>.None;
                    }
                });
        }

        #region Byte to XXX

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<bool> ToBool(this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToDouble);
        }

        #endregion

        #region SByte to XXX

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<bool> ToBool(this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToDouble);
        }

        #endregion

        #region Short to XXX

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<bool> ToBool(this Option<short> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(this Option<short> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(this Option<short> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(this Option<short> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(this Option<short> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(this Option<short> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(this Option<short> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(this Option<short> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(this Option<short> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(this Option<short> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(this Option<short> option)
        {
            return SafeConvert(option, Convert.ToDouble);
        }

        #endregion

        #region UShort to XXX

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<bool> ToBool(this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToDouble);
        }

        #endregion

        #region Int to XXX

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<bool> ToBool(this Option<int> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(this Option<int> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(this Option<int> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(this Option<int> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(this Option<int> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(this Option<int> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(this Option<int> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(this Option<int> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(this Option<int> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(this Option<int> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(this Option<int> option)
        {
            return SafeConvert(option, Convert.ToDouble);
        }

        #endregion

        #region UInt to XXX

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<bool> ToBool(this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToDouble);
        }

        #endregion

        #region Long to XXX

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<bool> ToBool(this Option<long> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(this Option<long> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(this Option<long> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(this Option<long> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(this Option<long> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(this Option<long> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(this Option<long> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(this Option<long> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(this Option<long> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(this Option<long> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(this Option<long> option)
        {
            return SafeConvert(option, Convert.ToDouble);
        }

        #endregion

        #region ULong to XXX

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<bool> ToBool(this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToDouble);
        }

        #endregion

        #region Decimal to XXX

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<bool> ToBool(this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToDouble);
        }

        #endregion

        #region Float to XXX

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<bool> ToBool(this Option<float> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(this Option<float> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(this Option<float> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(this Option<float> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(this Option<float> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(this Option<float> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(this Option<float> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(this Option<float> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(this Option<float> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(this Option<float> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(this Option<float> option)
        {
            return SafeConvert(option, Convert.ToDouble);
        }

        #endregion

        #region Double to XXX

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<bool> ToBool(this Option<double> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(this Option<double> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(this Option<double> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(this Option<double> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(this Option<double> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(this Option<double> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(this Option<double> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(this Option<double> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(this Option<double> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(this Option<double> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(this Option<double> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        #endregion
    }
}

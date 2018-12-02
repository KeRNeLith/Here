using System;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Option{T}"/> for numeric types casts.
    /// </summary>
    public static class OptionNumericCastsExtensions
    {
        private static Option<TTo> SafeConvert<TFrom, TTo>(in this Option<TFrom> option, [NotNull, InstantHandle] Func<TFrom, TTo> converter)
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
        public static Option<bool> ToBool(in this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(in this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(in this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(in this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(in this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(in this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(in this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(in this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(in this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(in this Option<byte> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(in this Option<byte> option)
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
        public static Option<bool> ToBool(in this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(in this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(in this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(in this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(in this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(in this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(in this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(in this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(in this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(in this Option<sbyte> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(in this Option<sbyte> option)
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
        public static Option<bool> ToBool(in this Option<short> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(in this Option<short> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(in this Option<short> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(in this Option<short> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(in this Option<short> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(in this Option<short> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(in this Option<short> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(in this Option<short> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(in this Option<short> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(in this Option<short> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(in this Option<short> option)
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
        public static Option<bool> ToBool(in this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(in this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(in this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(in this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(in this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(in this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(in this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(in this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(in this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(in this Option<ushort> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(in this Option<ushort> option)
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
        public static Option<bool> ToBool(in this Option<int> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(in this Option<int> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(in this Option<int> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(in this Option<int> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(in this Option<int> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(in this Option<int> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(in this Option<int> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(in this Option<int> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(in this Option<int> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(in this Option<int> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(in this Option<int> option)
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
        public static Option<bool> ToBool(in this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(in this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(in this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(in this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(in this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(in this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(in this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(in this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(in this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(in this Option<uint> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(in this Option<uint> option)
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
        public static Option<bool> ToBool(in this Option<long> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(in this Option<long> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(in this Option<long> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(in this Option<long> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(in this Option<long> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(in this Option<long> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(in this Option<long> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(in this Option<long> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(in this Option<long> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(in this Option<long> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(in this Option<long> option)
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
        public static Option<bool> ToBool(in this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(in this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(in this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(in this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(in this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(in this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(in this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(in this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(in this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(in this Option<ulong> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(in this Option<ulong> option)
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
        public static Option<bool> ToBool(in this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(in this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(in this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(in this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(in this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(in this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(in this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(in this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(in this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(in this Option<decimal> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(in this Option<decimal> option)
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
        public static Option<bool> ToBool(in this Option<float> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(in this Option<float> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(in this Option<float> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(in this Option<float> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(in this Option<float> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(in this Option<float> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(in this Option<float> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(in this Option<float> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(in this Option<float> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(in this Option<float> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<double> ToDouble(in this Option<float> option)
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
        public static Option<bool> ToBool(in this Option<double> option)
        {
            return SafeConvert(option, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<byte> ToByte(in this Option<double> option)
        {
            return SafeConvert(option, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<sbyte> ToSByte(in this Option<double> option)
        {
            return SafeConvert(option, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<short> ToShort(in this Option<double> option)
        {
            return SafeConvert(option, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ushort> ToUShort(in this Option<double> option)
        {
            return SafeConvert(option, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<int> ToInt(in this Option<double> option)
        {
            return SafeConvert(option, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<uint> ToUInt(in this Option<double> option)
        {
            return SafeConvert(option, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<long> ToLong(in this Option<double> option)
        {
            return SafeConvert(option, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<ulong> ToULong(in this Option<double> option)
        {
            return SafeConvert(option, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<decimal> ToDecimal(in this Option<double> option)
        {
            return SafeConvert(option, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Option<float> ToFloat(in this Option<double> option)
        {
            return SafeConvert(option, Convert.ToSingle);
        }

        #endregion
    }
}

using System;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Maybe{T}"/> for numeric types casts.
    /// </summary>
    public static class MaybeNumericCastsExtensions
    {
        private static Maybe<TTo> SafeConvert<TFrom, TTo>(in this Maybe<TFrom> maybe, [NotNull, InstantHandle] Func<TFrom, TTo> converter)
        {
            return maybe.Cast(
                input =>
                {
                    try
                    {
                        return converter(input);
                    }
                    catch
                    {
                        return Maybe<TTo>.None;
                    }
                });
        }

        #region Byte to XXX

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{Boolean}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<bool> ToBool(in this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(in this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(in this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(in this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(in this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(in this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(in this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(in this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(in this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(in this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(in this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToDouble);
        }

        #endregion

        #region SByte to XXX

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{Boolean}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<bool> ToBool(in this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(in this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(in this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(in this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(in this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(in this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(in this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(in this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(in this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(in this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(in this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToDouble);
        }

        #endregion

        #region Short to XXX

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{Boolean}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<bool> ToBool(in this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(in this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(in this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(in this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(in this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(in this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(in this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(in this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(in this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(in this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(in this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToDouble);
        }

        #endregion

        #region UShort to XXX

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{Boolean}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<bool> ToBool(in this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(in this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(in this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(in this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(in this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(in this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(in this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(in this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(in this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(in this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(in this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToDouble);
        }

        #endregion

        #region Int to XXX

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{Boolean}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<bool> ToBool(in this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(in this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(in this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(in this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(in this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(in this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(in this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(in this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(in this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(in this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(in this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToDouble);
        }

        #endregion

        #region UInt to XXX

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{Boolean}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<bool> ToBool(in this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(in this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(in this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(in this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(in this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(in this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(in this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(in this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(in this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(in this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(in this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToDouble);
        }

        #endregion

        #region Long to XXX

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{Boolean}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<bool> ToBool(in this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(in this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(in this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(in this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(in this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(in this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(in this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(in this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(in this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(in this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(in this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToDouble);
        }

        #endregion

        #region ULong to XXX

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{Boolean}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<bool> ToBool(in this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(in this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(in this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(in this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(in this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(in this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(in this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(in this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(in this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(in this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(in this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToDouble);
        }

        #endregion

        #region Decimal to XXX

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{Boolean}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<bool> ToBool(in this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(in this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(in this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(in this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(in this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(in this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(in this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(in this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(in this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(in this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(in this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToDouble);
        }

        #endregion

        #region Float to XXX

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{Boolean}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<bool> ToBool(in this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(in this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(in this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(in this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(in this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(in this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(in this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(in this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(in this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(in this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(in this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToDouble);
        }

        #endregion

        #region Double to XXX

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{Boolean}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<bool> ToBool(in this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(in this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(in this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(in this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(in this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(in this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(in this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(in this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(in this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(in this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(in this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        #endregion
    }
}

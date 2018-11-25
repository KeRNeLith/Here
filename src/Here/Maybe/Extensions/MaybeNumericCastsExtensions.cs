using System;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Maybe{T}"/> for numeric types casts.
    /// </summary>
    public static class MaybeNumericCastsExtensions
    {
        private static Maybe<TTo> SafeConvert<TFrom, TTo>(this Maybe<TFrom> maybe, [NotNull, InstantHandle] Func<TFrom, TTo> converter)
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
        public static Maybe<bool> ToBool(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Byte}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<byte> maybe)
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
        public static Maybe<bool> ToBool(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{SByte}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{SByte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<sbyte> maybe)
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
        public static Maybe<bool> ToBool(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int16}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<short> maybe)
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
        public static Maybe<bool> ToBool(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt16}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt16}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<ushort> maybe)
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
        public static Maybe<bool> ToBool(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int32}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<int> maybe)
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
        public static Maybe<bool> ToBool(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt32}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt32}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<uint> maybe)
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
        public static Maybe<bool> ToBool(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Int64}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Int64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<long> maybe)
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
        public static Maybe<bool> ToBool(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{UInt64}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{UInt64}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<ulong> maybe)
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
        public static Maybe<bool> ToBool(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Decimal}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<decimal> maybe)
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
        public static Maybe<bool> ToBool(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Single}"/> to a <see cref="Maybe{Double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Single}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<float> maybe)
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
        public static Maybe<bool> ToBool(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToBoolean);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{Byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{SByte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToSByte);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{Int16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{UInt16}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt16);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{Int32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{UInt32}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt32);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{Int64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{UInt64}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToUInt64);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{Decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToDecimal);
        }

        /// <summary>
        /// Converts this <see cref="Maybe{Double}"/> to a <see cref="Maybe{Single}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{Double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, Convert.ToSingle);
        }

        #endregion
    }
}

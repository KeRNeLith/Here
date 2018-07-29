using System;
using JetBrains.Annotations;

namespace Here.Maybes.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Maybe{T}"/> to cast numeric types.
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
        /// Convert this <see cref="Maybe{byte}"/> to a <see cref="Maybe{sbyte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{byte}"/> to a <see cref="Maybe{short}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{byte}"/> to a <see cref="Maybe{ushort}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{byte}"/> to a <see cref="Maybe{int}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{byte}"/> to a <see cref="Maybe{uint}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{byte}"/> to a <see cref="Maybe{long}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{byte}"/> to a <see cref="Maybe{ulong}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{byte}"/> to a <see cref="Maybe{decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDecimal(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{byte}"/> to a <see cref="Maybe{float}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSingle(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{byte}"/> to a <see cref="Maybe{double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{byte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<byte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDouble(input));
        }

        #endregion

        #region SByte to XXX

        /// <summary>
        /// Convert this <see cref="Maybe{sbyte}"/> to a <see cref="Maybe{byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{sbyte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{sbyte}"/> to a <see cref="Maybe{short}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{sbyte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{sbyte}"/> to a <see cref="Maybe{ushort}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{sbyte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{sbyte}"/> to a <see cref="Maybe{int}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{sbyte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{sbyte}"/> to a <see cref="Maybe{uint}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{sbyte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{sbyte}"/> to a <see cref="Maybe{long}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{sbyte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{sbyte}"/> to a <see cref="Maybe{ulong}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{sbyte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{sbyte}"/> to a <see cref="Maybe{decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{sbyte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDecimal(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{sbyte}"/> to a <see cref="Maybe{float}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{sbyte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSingle(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{sbyte}"/> to a <see cref="Maybe{double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{sbyte}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<sbyte> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDouble(input));
        }

        #endregion

        #region Short to XXX

        /// <summary>
        /// Convert this <see cref="Maybe{short}"/> to a <see cref="Maybe{byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{short}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{short}"/> to a <see cref="Maybe{sbyte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{short}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{short}"/> to a <see cref="Maybe{ushort}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{short}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{short}"/> to a <see cref="Maybe{int}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{short}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{short}"/> to a <see cref="Maybe{uint}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{short}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{short}"/> to a <see cref="Maybe{long}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{short}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{short}"/> to a <see cref="Maybe{ulong}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{short}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{short}"/> to a <see cref="Maybe{decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{short}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDecimal(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{short}"/> to a <see cref="Maybe{float}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{short}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSingle(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{short}"/> to a <see cref="Maybe{double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{short}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<short> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDouble(input));
        }

        #endregion

        #region UShort to XXX

        /// <summary>
        /// Convert this <see cref="Maybe{ushort}"/> to a <see cref="Maybe{byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ushort}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ushort}"/> to a <see cref="Maybe{sbyte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ushort}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ushort}"/> to a <see cref="Maybe{short}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ushort}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ushort}"/> to a <see cref="Maybe{int}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ushort}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ushort}"/> to a <see cref="Maybe{uint}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ushort}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ushort}"/> to a <see cref="Maybe{long}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ushort}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ushort}"/> to a <see cref="Maybe{ulong}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ushort}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ushort}"/> to a <see cref="Maybe{decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ushort}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDecimal(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ushort}"/> to a <see cref="Maybe{float}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ushort}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSingle(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ushort}"/> to a <see cref="Maybe{double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ushort}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<ushort> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDouble(input));
        }

        #endregion

        #region Int to XXX

        /// <summary>
        /// Convert this <see cref="Maybe{int}"/> to a <see cref="Maybe{byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{int}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{int}"/> to a <see cref="Maybe{sbyte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{int}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{int}"/> to a <see cref="Maybe{short}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{int}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{int}"/> to a <see cref="Maybe{ushort}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{int}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{int}"/> to a <see cref="Maybe{uint}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{int}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{int}"/> to a <see cref="Maybe{long}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{int}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{int}"/> to a <see cref="Maybe{ulong}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{int}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{int}"/> to a <see cref="Maybe{decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{int}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDecimal(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{int}"/> to a <see cref="Maybe{float}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{int}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSingle(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{int}"/> to a <see cref="Maybe{double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{int}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<int> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDouble(input));
        }

        #endregion

        #region UInt to XXX

        /// <summary>
        /// Convert this <see cref="Maybe{uint}"/> to a <see cref="Maybe{byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{uint}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{uint}"/> to a <see cref="Maybe{sbyte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{uint}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{uint}"/> to a <see cref="Maybe{short}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{uint}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{uint}"/> to a <see cref="Maybe{ushort}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{uint}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{uint}"/> to a <see cref="Maybe{int}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{uint}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{uint}"/> to a <see cref="Maybe{long}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{uint}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{uint}"/> to a <see cref="Maybe{ulong}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{uint}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{uint}"/> to a <see cref="Maybe{decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{uint}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDecimal(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{uint}"/> to a <see cref="Maybe{float}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{uint}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSingle(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{uint}"/> to a <see cref="Maybe{double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{uint}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<uint> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDouble(input));
        }

        #endregion

        #region Long to XXX

        /// <summary>
        /// Convert this <see cref="Maybe{long}"/> to a <see cref="Maybe{byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{long}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{long}"/> to a <see cref="Maybe{sbyte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{long}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{long}"/> to a <see cref="Maybe{short}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{long}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{long}"/> to a <see cref="Maybe{ushort}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{long}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{long}"/> to a <see cref="Maybe{int}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{long}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{long}"/> to a <see cref="Maybe{uint}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{long}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{long}"/> to a <see cref="Maybe{ulong}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{long}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{long}"/> to a <see cref="Maybe{decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{long}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDecimal(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{long}"/> to a <see cref="Maybe{float}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{long}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSingle(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{long}"/> to a <see cref="Maybe{double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{long}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<long> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDouble(input));
        }

        #endregion

        #region ULong to XXX

        /// <summary>
        /// Convert this <see cref="Maybe{ulong}"/> to a <see cref="Maybe{byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ulong}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ulong}"/> to a <see cref="Maybe{sbyte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ulong}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ulong}"/> to a <see cref="Maybe{short}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{long}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ulong}"/> to a <see cref="Maybe{ushort}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ulong}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ulong}"/> to a <see cref="Maybe{int}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ulong}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ulong}"/> to a <see cref="Maybe{uint}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ulong}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ulong}"/> to a <see cref="Maybe{long}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ulong}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ulong}"/> to a <see cref="Maybe{decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ulong}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDecimal(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ulong}"/> to a <see cref="Maybe{float}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ulong}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSingle(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{ulong}"/> to a <see cref="Maybe{double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{ulong}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<ulong> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDouble(input));
        }

        #endregion

        #region Decimal to XXX

        /// <summary>
        /// Convert this <see cref="Maybe{decimal}"/> to a <see cref="Maybe{byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{decimal}"/> to a <see cref="Maybe{sbyte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{decimal}"/> to a <see cref="Maybe{short}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{decimal}"/> to a <see cref="Maybe{ushort}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{decimal}"/> to a <see cref="Maybe{int}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{decimal}"/> to a <see cref="Maybe{uint}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{decimal}"/> to a <see cref="Maybe{long}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{decimal}"/> to a <see cref="Maybe{ulong}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{decimal}"/> to a <see cref="Maybe{float}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSingle(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{decimal}"/> to a <see cref="Maybe{double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{decimal}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<decimal> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDouble(input));
        }

        #endregion

        #region Float to XXX

        /// <summary>
        /// Convert this <see cref="Maybe{float}"/> to a <see cref="Maybe{byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{float}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{float}"/> to a <see cref="Maybe{sbyte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{float}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{float}"/> to a <see cref="Maybe{short}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{float}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{float}"/> to a <see cref="Maybe{ushort}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{float}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{float}"/> to a <see cref="Maybe{int}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{float}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{float}"/> to a <see cref="Maybe{uint}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{float}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{float}"/> to a <see cref="Maybe{long}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{float}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{float}"/> to a <see cref="Maybe{ulong}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{float}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{float}"/> to a <see cref="Maybe{decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{float}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDecimal(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{float}"/> to a <see cref="Maybe{double}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{float}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<double> ToDouble(this Maybe<float> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDouble(input));
        }

        #endregion

        #region Double to XXX

        /// <summary>
        /// Convert this <see cref="Maybe{double}"/> to a <see cref="Maybe{byte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<byte> ToByte(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{double}"/> to a <see cref="Maybe{sbyte}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<sbyte> ToSByte(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSByte(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{double}"/> to a <see cref="Maybe{short}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<short> ToShort(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{double}"/> to a <see cref="Maybe{ushort}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ushort> ToUShort(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt16(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{double}"/> to a <see cref="Maybe{int}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<int> ToInt(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{double}"/> to a <see cref="Maybe{uint}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<uint> ToUInt(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt32(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{double}"/> to a <see cref="Maybe{long}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<long> ToLong(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{double}"/> to a <see cref="Maybe{ulong}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<ulong> ToULong(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToUInt64(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{double}"/> to a <see cref="Maybe{decimal}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<decimal> ToDecimal(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToDecimal(input));
        }

        /// <summary>
        /// Convert this <see cref="Maybe{double}"/> to a <see cref="Maybe{float}"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{double}"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        [PublicAPI, Pure]
        public static Maybe<float> ToFloat(this Maybe<double> maybe)
        {
            return SafeConvert(maybe, input => Convert.ToSingle(input));
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Maybes
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/> numeric casts extensions.
    /// </summary>
    [TestFixture]
    internal class MaybeNumericCastsExtensionsTests : MaybeTestsBase
    {
        /// <summary>
        /// Call the <paramref name="castFunc"/> and check if the result match expected value.
        /// </summary>
        private static void CastTest<T>([NotNull, InstantHandle] Func<Maybe<T>> castFunc, bool mustHaveValue, T expectedValue)
        {
            var maybe = castFunc();
            if (mustHaveValue)
                CheckMaybeValue(maybe, expectedValue);
            else
                CheckEmptyMaybe(maybe);
        }

        /// <summary>
        /// Call the <paramref name="castFunc"/> on the <paramref name="input"/> and check if the result match expected value.
        /// For cast from float to double.
        /// </summary>
        private static void CastTest(Maybe<float> input, [NotNull, InstantHandle] Func<Maybe<float>, Maybe<double>> castFunc, bool mustHaveValue, double expectedValue)
        {
            var maybe = castFunc(input);
            if (mustHaveValue)
                CheckMaybeNearValue(maybe, expectedValue);
            else
                CheckEmptyMaybe(maybe);
        }

        #region Cast to Bool

        private static IEnumerable<TestCaseData> CreateCastFromByteToBoolTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<byte>.None, false, null);
                yield return new TestCaseData(Maybe<byte>.Some(12), true, true);
                yield return new TestCaseData(Maybe<byte>.Some(0), true, false);
            }
        }

        [TestCaseSource(nameof(CreateCastFromByteToBoolTestCases))]
        public void CastByteToBool(Maybe<byte> maybe, bool mustHaveValue, bool expectedValue)
        {
            CastTest(() => maybe.ToBool(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromSByteToBoolTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<sbyte>.None, false, null);
                yield return new TestCaseData(Maybe<sbyte>.Some(12), true, true);
                yield return new TestCaseData(Maybe<sbyte>.Some(0), true, false);
                yield return new TestCaseData(Maybe<sbyte>.Some(-12), true, true);
            }
        }

        [TestCaseSource(nameof(CreateCastFromSByteToBoolTestCases))]
        public void CastSByteToBool(Maybe<sbyte> maybe, bool mustHaveValue, bool expectedValue)
        {
            CastTest(() => maybe.ToBool(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromShortToBoolTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<short>.None, false, null);
                yield return new TestCaseData(Maybe<short>.Some(12), true, true);
                yield return new TestCaseData(Maybe<short>.Some(0), true, false);
                yield return new TestCaseData(Maybe<short>.Some(-12), true, true);
            }
        }

        [TestCaseSource(nameof(CreateCastFromShortToBoolTestCases))]
        public void CastShortToBool(Maybe<short> maybe, bool mustHaveValue, bool expectedValue)
        {
            CastTest(() => maybe.ToBool(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUShortToBoolTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ushort>.None, false, null);
                yield return new TestCaseData(Maybe<ushort>.Some(12), true, true);
                yield return new TestCaseData(Maybe<ushort>.Some(0), true, false);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUShortToBoolTestCases))]
        public void CastUShortToBool(Maybe<ushort> maybe, bool mustHaveValue, bool expectedValue)
        {
            CastTest(() => maybe.ToBool(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromIntToBoolTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<int>.None, false, null);
                yield return new TestCaseData(Maybe<int>.Some(-12), true, true);
                yield return new TestCaseData(Maybe<int>.Some(0), true, false);
                yield return new TestCaseData(Maybe<int>.Some(12), true, true);
            }
        }

        [TestCaseSource(nameof(CreateCastFromIntToBoolTestCases))]
        public void CastIntToBool(Maybe<int> maybe, bool mustHaveValue, bool expectedValue)
        {
            CastTest(() => maybe.ToBool(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUIntToBoolTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<uint>.None, false, null);
                yield return new TestCaseData(Maybe<uint>.Some(12u), true, true);
                yield return new TestCaseData(Maybe<uint>.Some(0u), true, false);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUIntToBoolTestCases))]
        public void CastUIntToBool(Maybe<uint> maybe, bool mustHaveValue, bool expectedValue)
        {
            CastTest(() => maybe.ToBool(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromLongToBoolTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<long>.None, false, null);
                yield return new TestCaseData(Maybe<long>.Some(-250L), true, true);
                yield return new TestCaseData(Maybe<long>.Some(0L), true, false);
                yield return new TestCaseData(Maybe<long>.Some(300L), true, true);
            }
        }

        [TestCaseSource(nameof(CreateCastFromLongToBoolTestCases))]
        public void CastLongToBool(Maybe<long> maybe, bool mustHaveValue, bool expectedValue)
        {
            CastTest(() => maybe.ToBool(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromULongToBoolTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ulong>.None, false, null);
                yield return new TestCaseData(Maybe<ulong>.Some(0ul), true, false);
                yield return new TestCaseData(Maybe<ulong>.Some(500ul), true, true);
            }
        }

        [TestCaseSource(nameof(CreateCastFromULongToBoolTestCases))]
        public void CastULongToBool(Maybe<ulong> maybe, bool mustHaveValue, bool expectedValue)
        {
            CastTest(() => maybe.ToBool(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDecimalToBoolTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<decimal>.None, false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(-12m), true, true);
                yield return new TestCaseData(Maybe<decimal>.Some(0m), true, false);
                yield return new TestCaseData(Maybe<decimal>.Some(1.2m), true, true);
                yield return new TestCaseData(Maybe<decimal>.Some(500m), true, true);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDecimalToBoolTestCases))]
        public void CastDecimalToBool(Maybe<decimal> maybe, bool mustHaveValue, bool expectedValue)
        {
            CastTest(() => maybe.ToBool(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromFloatToBoolTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<float>.None, false, null);
                yield return new TestCaseData(Maybe<float>.Some(-12f), true, true);
                yield return new TestCaseData(Maybe<float>.Some(0f), true, false);
                yield return new TestCaseData(Maybe<float>.Some(1.6f), true, true);
                yield return new TestCaseData(Maybe<float>.Some(500f), true, true);
            }
        }

        [TestCaseSource(nameof(CreateCastFromFloatToBoolTestCases))]
        public void CastFloatToBool(Maybe<float> maybe, bool mustHaveValue, bool expectedValue)
        {
            CastTest(() => maybe.ToBool(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDoubleToBoolTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<double>.None, false, null);
                yield return new TestCaseData(Maybe<double>.Some(-12d), true, true);
                yield return new TestCaseData(Maybe<double>.Some(0d), true, false);
                yield return new TestCaseData(Maybe<double>.Some(1.6d), true, true);
                yield return new TestCaseData(Maybe<double>.Some(250d), true, true);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDoubleToBoolTestCases))]
        public void CastDoubleToBool(Maybe<double> maybe, bool mustHaveValue, bool expectedValue)
        {
            CastTest(() => maybe.ToBool(), mustHaveValue, expectedValue);
        }

        #endregion

        #region Cast to Byte

        private static IEnumerable<TestCaseData> CreateCastFromSByteToByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<sbyte>.None, false, null);
                yield return new TestCaseData(Maybe<sbyte>.Some(12), true, (byte)12);
                yield return new TestCaseData(Maybe<sbyte>.Some(-12), false, null);
            }
        }
        
        [TestCaseSource(nameof(CreateCastFromSByteToByteTestCases))]
        public void CastSByteToByte(Maybe<sbyte> maybe, bool mustHaveValue, byte expectedValue)
        {
            CastTest(() => maybe.ToByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromShortToByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<short>.None, false, null);
                yield return new TestCaseData(Maybe<short>.Some(12), true, (byte)12);
                yield return new TestCaseData(Maybe<short>.Some(260), false, null);
                yield return new TestCaseData(Maybe<short>.Some(-12), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromShortToByteTestCases))]
        public void CastShortToByte(Maybe<short> maybe, bool mustHaveValue, byte expectedValue)
        {
            CastTest(() => maybe.ToByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUShortToByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ushort>.None, false, null);
                yield return new TestCaseData(Maybe<ushort>.Some(12), true, (byte)12);
                yield return new TestCaseData(Maybe<ushort>.Some(260), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUShortToByteTestCases))]
        public void CastUShortToByte(Maybe<ushort> maybe, bool mustHaveValue, byte expectedValue)
        {
            CastTest(() => maybe.ToByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromIntToByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<int>.None, false, null);
                yield return new TestCaseData(Maybe<int>.Some(-12), false, null);
                yield return new TestCaseData(Maybe<int>.Some(12), true, (byte)12);
                yield return new TestCaseData(Maybe<int>.Some(260), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromIntToByteTestCases))]
        public void CastIntToByte(Maybe<int> maybe, bool mustHaveValue, byte expectedValue)
        {
            CastTest(() => maybe.ToByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUIntToByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<uint>.None, false, null);
                yield return new TestCaseData(Maybe<uint>.Some(12u), true, (byte)12);
                yield return new TestCaseData(Maybe<uint>.Some(260u), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUIntToByteTestCases))]
        public void CastUIntToByte(Maybe<uint> maybe, bool mustHaveValue, byte expectedValue)
        {
            CastTest(() => maybe.ToByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromLongToByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<long>.None, false, null);
                yield return new TestCaseData(Maybe<long>.Some(-250L), false, null);
                yield return new TestCaseData(Maybe<long>.Some(250L), true, (byte)250);
                yield return new TestCaseData(Maybe<long>.Some(300L), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromLongToByteTestCases))]
        public void CastLongToByte(Maybe<long> maybe, bool mustHaveValue, byte expectedValue)
        {
            CastTest(() => maybe.ToByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromULongToByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ulong>.None, false, null);
                yield return new TestCaseData(Maybe<ulong>.Some(250ul), true, (byte)250);
                yield return new TestCaseData(Maybe<ulong>.Some(500ul), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromULongToByteTestCases))]
        public void CastULongToByte(Maybe<ulong> maybe, bool mustHaveValue, byte expectedValue)
        {
            CastTest(() => maybe.ToByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDecimalToByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<decimal>.None, false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(-12m), false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(1.2m), true, (byte)1);
                yield return new TestCaseData(Maybe<decimal>.Some(1.6m), true, (byte)2);
                yield return new TestCaseData(Maybe<decimal>.Some(250m), true, (byte)250);
                yield return new TestCaseData(Maybe<decimal>.Some(500m), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDecimalToByteTestCases))]
        public void CastDecimalToByte(Maybe<decimal> maybe, bool mustHaveValue, byte expectedValue)
        {
            CastTest(() => maybe.ToByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromFloatToByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<float>.None, false, null);
                yield return new TestCaseData(Maybe<float>.Some(-12f), false, null);
                yield return new TestCaseData(Maybe<float>.Some(1.2f), true, (byte)1);
                yield return new TestCaseData(Maybe<float>.Some(1.6f), true, (byte)2);
                yield return new TestCaseData(Maybe<float>.Some(250f), true, (byte)250);
                yield return new TestCaseData(Maybe<float>.Some(500f), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromFloatToByteTestCases))]
        public void CastFloatToByte(Maybe<float> maybe, bool mustHaveValue, byte expectedValue)
        {
            CastTest(() => maybe.ToByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDoubleToByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<double>.None, false, null);
                yield return new TestCaseData(Maybe<double>.Some(-12d), false, null);
                yield return new TestCaseData(Maybe<double>.Some(1.2d), true, (byte)1);
                yield return new TestCaseData(Maybe<double>.Some(1.6d), true, (byte)2);
                yield return new TestCaseData(Maybe<double>.Some(250d), true, (byte)250);
                yield return new TestCaseData(Maybe<double>.Some(500d), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDoubleToByteTestCases))]
        public void CastDoubleToByte(Maybe<double> maybe, bool mustHaveValue, byte expectedValue)
        {
            CastTest(() => maybe.ToByte(), mustHaveValue, expectedValue);
        }

        #endregion

        #region Cast to SByte

        private static IEnumerable<TestCaseData> CreateCastFromByteToSByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<byte>.None, false, null);
                yield return new TestCaseData(Maybe<byte>.Some(12), true, (sbyte)12);
                yield return new TestCaseData(Maybe<byte>.Some(220), false, null);
            }
        }
        
        [TestCaseSource(nameof(CreateCastFromByteToSByteTestCases))]
        public void CastByteToSByte(Maybe<byte> maybe, bool mustHaveValue, sbyte expectedValue)
        {
            CastTest(() => maybe.ToSByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromShortToSByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<short>.None, false, null);
                yield return new TestCaseData(Maybe<short>.Some(12), true, (sbyte)12);
                yield return new TestCaseData(Maybe<short>.Some(220), false, null);
                yield return new TestCaseData(Maybe<short>.Some(-12), true, (sbyte)-12);
                yield return new TestCaseData(Maybe<short>.Some(-220), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromShortToSByteTestCases))]
        public void CastShortToSByte(Maybe<short> maybe, bool mustHaveValue, sbyte expectedValue)
        {
            CastTest(() => maybe.ToSByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUShortToSByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ushort>.None, false, null);
                yield return new TestCaseData(Maybe<ushort>.Some(12), true, (sbyte)12);
                yield return new TestCaseData(Maybe<ushort>.Some(220), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUShortToSByteTestCases))]
        public void CastUShortToSByte(Maybe<ushort> maybe, bool mustHaveValue, sbyte expectedValue)
        {
            CastTest(() => maybe.ToSByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromIntToSByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<int>.None, false, null);
                yield return new TestCaseData(Maybe<int>.Some(-12), true, (sbyte)-12);
                yield return new TestCaseData(Maybe<int>.Some(-260), false, null);
                yield return new TestCaseData(Maybe<int>.Some(12), true, (sbyte)12);
                yield return new TestCaseData(Maybe<int>.Some(260), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromIntToSByteTestCases))]
        public void CastIntToSByte(Maybe<int> maybe, bool mustHaveValue, sbyte expectedValue)
        {
            CastTest(() => maybe.ToSByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUIntToSByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<uint>.None, false, null);
                yield return new TestCaseData(Maybe<uint>.Some(12u), true, (sbyte)12);
                yield return new TestCaseData(Maybe<uint>.Some(260u), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUIntToSByteTestCases))]
        public void CastUIntToSByte(Maybe<uint> maybe, bool mustHaveValue, sbyte expectedValue)
        {
            CastTest(() => maybe.ToSByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromLongToSByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<long>.None, false, null);
                yield return new TestCaseData(Maybe<long>.Some(-12L), true, (sbyte)-12);
                yield return new TestCaseData(Maybe<long>.Some(-250L), false, null);
                yield return new TestCaseData(Maybe<long>.Some(120L), true, (sbyte)120);
                yield return new TestCaseData(Maybe<long>.Some(280L), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromLongToSByteTestCases))]
        public void CastLongToSByte(Maybe<long> maybe, bool mustHaveValue, sbyte expectedValue)
        {
            CastTest(() => maybe.ToSByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromULongToSByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ulong>.None, false, null);
                yield return new TestCaseData(Maybe<ulong>.Some(120ul), true, (sbyte)120);
                yield return new TestCaseData(Maybe<ulong>.Some(280ul), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromULongToSByteTestCases))]
        public void CastULongToSByte(Maybe<ulong> maybe, bool mustHaveValue, sbyte expectedValue)
        {
            CastTest(() => maybe.ToSByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDecimalToSByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<decimal>.None, false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(-120m), true, (sbyte)-120);
                yield return new TestCaseData(Maybe<decimal>.Some(-280m), false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(1.2m), true, (sbyte)1);
                yield return new TestCaseData(Maybe<decimal>.Some(1.6m), true, (sbyte)2);
                yield return new TestCaseData(Maybe<decimal>.Some(120m), true, (sbyte)120);
                yield return new TestCaseData(Maybe<decimal>.Some(280m), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDecimalToSByteTestCases))]
        public void CastDecimalToSByte(Maybe<decimal> maybe, bool mustHaveValue, sbyte expectedValue)
        {
            CastTest(() => maybe.ToSByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromFloatToSByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<float>.None, false, null);
                yield return new TestCaseData(Maybe<float>.Some(-120f), true, (sbyte)-120);
                yield return new TestCaseData(Maybe<float>.Some(-280f), false, null);
                yield return new TestCaseData(Maybe<float>.Some(1.2f), true, (sbyte)1);
                yield return new TestCaseData(Maybe<float>.Some(1.6f), true, (sbyte)2);
                yield return new TestCaseData(Maybe<float>.Some(120f), true, (sbyte)120);
                yield return new TestCaseData(Maybe<float>.Some(280f), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromFloatToSByteTestCases))]
        public void CastFloatToSByte(Maybe<float> maybe, bool mustHaveValue, sbyte expectedValue)
        {
            CastTest(() => maybe.ToSByte(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDoubleToSByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<double>.None, false, null);
                yield return new TestCaseData(Maybe<double>.Some(-120d), true, (sbyte)-120);
                yield return new TestCaseData(Maybe<double>.Some(-280d), false, null);
                yield return new TestCaseData(Maybe<double>.Some(1.2d), true, (sbyte)1);
                yield return new TestCaseData(Maybe<double>.Some(1.6d), true, (sbyte)2);
                yield return new TestCaseData(Maybe<double>.Some(120d), true, (sbyte)120);
                yield return new TestCaseData(Maybe<double>.Some(280d), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDoubleToSByteTestCases))]
        public void CastDoubleToSByte(Maybe<double> maybe, bool mustHaveValue, sbyte expectedValue)
        {
            CastTest(() => maybe.ToSByte(), mustHaveValue, expectedValue);
        }

        #endregion

        #region Cast to Short

        private static IEnumerable<TestCaseData> CreateCastFromByteToShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<byte>.None, false, null);
                yield return new TestCaseData(Maybe<byte>.Some(12), true, (short)12);
                yield return new TestCaseData(Maybe<byte>.Some(220), true, (short)220);
            }
        }

        [TestCaseSource(nameof(CreateCastFromByteToShortTestCases))]
        public void CastByteToShort(Maybe<byte> maybe, bool mustHaveValue, short expectedValue)
        {
            CastTest(() => maybe.ToShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromSByteToShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<sbyte>.None, false, null);
                yield return new TestCaseData(Maybe<sbyte>.Some(12), true, (short)12);
                yield return new TestCaseData(Maybe<sbyte>.Some(-12), true, (short)-12);
            }
        }
        
        [TestCaseSource(nameof(CreateCastFromSByteToShortTestCases))]
        public void CastSByteToShort(Maybe<sbyte> maybe, bool mustHaveValue, short expectedValue)
        {
            CastTest(() => maybe.ToShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUShortToShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ushort>.None, false, null);
                yield return new TestCaseData(Maybe<ushort>.Some(12), true, (short)12);
                yield return new TestCaseData(Maybe<ushort>.Some(47000), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUShortToShortTestCases))]
        public void CastUShortToShort(Maybe<ushort> maybe, bool mustHaveValue, short expectedValue)
        {
            CastTest(() => maybe.ToShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromIntToShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<int>.None, false, null);
                yield return new TestCaseData(Maybe<int>.Some(-12), true, (short)-12);
                yield return new TestCaseData(Maybe<int>.Some(-260), true, (short)-260);
                yield return new TestCaseData(Maybe<int>.Some(-47000), false, null);
                yield return new TestCaseData(Maybe<int>.Some(12), true, (short)12);
                yield return new TestCaseData(Maybe<int>.Some(260), true, (short)260);
                yield return new TestCaseData(Maybe<int>.Some(47000), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromIntToShortTestCases))]
        public void CastIntToShort(Maybe<int> maybe, bool mustHaveValue, short expectedValue)
        {
            CastTest(() => maybe.ToShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUIntToShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<uint>.None, false, null);
                yield return new TestCaseData(Maybe<uint>.Some(28000u), true, (short)28000);
                yield return new TestCaseData(Maybe<uint>.Some(2160000u), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUIntToShortTestCases))]
        public void CastUIntToShort(Maybe<uint> maybe, bool mustHaveValue, short expectedValue)
        {
            CastTest(() => maybe.ToShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromLongToShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<long>.None, false, null);
                yield return new TestCaseData(Maybe<long>.Some(-12L), true, (short)-12);
                yield return new TestCaseData(Maybe<long>.Some(-65000L), false, null);
                yield return new TestCaseData(Maybe<long>.Some(32000L), true, (short)32000);
                yield return new TestCaseData(Maybe<long>.Some(65000L), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromLongToShortTestCases))]
        public void CastLongToShort(Maybe<long> maybe, bool mustHaveValue, short expectedValue)
        {
            CastTest(() => maybe.ToShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromULongToShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ulong>.None, false, null);
                yield return new TestCaseData(Maybe<ulong>.Some(32000ul), true, (short)32000);
                yield return new TestCaseData(Maybe<ulong>.Some(65000ul), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromULongToShortTestCases))]
        public void CastULongToShort(Maybe<ulong> maybe, bool mustHaveValue, short expectedValue)
        {
            CastTest(() => maybe.ToShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDecimalToShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<decimal>.None, false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(-32000m), true, (short)-32000);
                yield return new TestCaseData(Maybe<decimal>.Some(-65000m), false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(32000m), true, (short)32000);
                yield return new TestCaseData(Maybe<decimal>.Some(12.1m), true, (short)12);
                yield return new TestCaseData(Maybe<decimal>.Some(12.6m), true, (short)13);
                yield return new TestCaseData(Maybe<decimal>.Some(65000m), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDecimalToShortTestCases))]
        public void CastDecimalToShort(Maybe<decimal> maybe, bool mustHaveValue, short expectedValue)
        {
            CastTest(() => maybe.ToShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromFloatToShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<float>.None, false, null);
                yield return new TestCaseData(Maybe<float>.Some(-32000f), true, (short)-32000);
                yield return new TestCaseData(Maybe<float>.Some(-65000f), false, null);
                yield return new TestCaseData(Maybe<float>.Some(32000f), true, (short)32000);
                yield return new TestCaseData(Maybe<float>.Some(12.1f), true, (short)12);
                yield return new TestCaseData(Maybe<float>.Some(12.6f), true, (short)13);
                yield return new TestCaseData(Maybe<float>.Some(65000f), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromFloatToShortTestCases))]
        public void CastFloatToShort(Maybe<float> maybe, bool mustHaveValue, short expectedValue)
        {
            CastTest(() => maybe.ToShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDoubleToShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<double>.None, false, null);
                yield return new TestCaseData(Maybe<double>.Some(-32000d), true, (short)-32000);
                yield return new TestCaseData(Maybe<double>.Some(-65000d), false, null);
                yield return new TestCaseData(Maybe<double>.Some(32000d), true, (short)32000);
                yield return new TestCaseData(Maybe<double>.Some(12.1d), true, (short)12);
                yield return new TestCaseData(Maybe<double>.Some(12.6d), true, (short)13);
                yield return new TestCaseData(Maybe<double>.Some(65000d), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDoubleToShortTestCases))]
        public void CastDoubleToShort(Maybe<double> maybe, bool mustHaveValue, short expectedValue)
        {
            CastTest(() => maybe.ToShort(), mustHaveValue, expectedValue);
        }

        #endregion

        #region Cast to UShort

        private static IEnumerable<TestCaseData> CreateCastFromByteToUShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<byte>.None, false, null);
                yield return new TestCaseData(Maybe<byte>.Some(12), true, (ushort)12);
                yield return new TestCaseData(Maybe<byte>.Some(220), true, (ushort)220);
            }
        }

        [TestCaseSource(nameof(CreateCastFromByteToUShortTestCases))]
        public void CastByteToUShort(Maybe<byte> maybe, bool mustHaveValue, ushort expectedValue)
        {
            CastTest(() => maybe.ToUShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromSByteToUShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<sbyte>.None, false, null);
                yield return new TestCaseData(Maybe<sbyte>.Some(12), true, (ushort)12);
                yield return new TestCaseData(Maybe<sbyte>.Some(-12), false, null);
            }
        }
        
        [TestCaseSource(nameof(CreateCastFromSByteToUShortTestCases))]
        public void CastSByteToUShort(Maybe<sbyte> maybe, bool mustHaveValue, ushort expectedValue)
        {
            CastTest(() => maybe.ToUShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromShortToUShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<short>.None, false, null);
                yield return new TestCaseData(Maybe<short>.Some(12), true, (ushort)12);
                yield return new TestCaseData(Maybe<short>.Some(-12), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromShortToUShortTestCases))]
        public void CastShortToUShort(Maybe<short> maybe, bool mustHaveValue, ushort expectedValue)
        {
            CastTest(() => maybe.ToUShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromIntToUShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<int>.None, false, null);
                yield return new TestCaseData(Maybe<int>.Some(-12), false, null);
                yield return new TestCaseData(Maybe<int>.Some(47000), true, (ushort)47000);
                yield return new TestCaseData(Maybe<int>.Some(75000), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromIntToUShortTestCases))]
        public void CastIntToUShort(Maybe<int> maybe, bool mustHaveValue, ushort expectedValue)
        {
            CastTest(() => maybe.ToUShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUIntToUShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<uint>.None, false, null);
                yield return new TestCaseData(Maybe<uint>.Some(47000u), true, (ushort)47000);
                yield return new TestCaseData(Maybe<uint>.Some(2160000u), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUIntToUShortTestCases))]
        public void CastUIntToUShort(Maybe<uint> maybe, bool mustHaveValue, ushort expectedValue)
        {
            CastTest(() => maybe.ToUShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromLongToUShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<long>.None, false, null);
                yield return new TestCaseData(Maybe<long>.Some(-12L), false, null);
                yield return new TestCaseData(Maybe<long>.Some(65000L), true, (ushort)65000);
                yield return new TestCaseData(Maybe<long>.Some(300000L), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromLongToUShortTestCases))]
        public void CastLongToUShort(Maybe<long> maybe, bool mustHaveValue, ushort expectedValue)
        {
            CastTest(() => maybe.ToUShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromULongToUShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ulong>.None, false, null);
                yield return new TestCaseData(Maybe<ulong>.Some(65000ul), true, (ushort)65000);
                yield return new TestCaseData(Maybe<ulong>.Some(300000ul), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromULongToUShortTestCases))]
        public void CastULongToUShort(Maybe<ulong> maybe, bool mustHaveValue, ushort expectedValue)
        {
            CastTest(() => maybe.ToUShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDecimalToUShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<decimal>.None, false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(-20m), false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(1.1m), true, (ushort)1);
                yield return new TestCaseData(Maybe<decimal>.Some(11.6m), true, (ushort)12);
                yield return new TestCaseData(Maybe<decimal>.Some(65000m), true, (ushort)65000);
                yield return new TestCaseData(Maybe<decimal>.Some(300000m), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDecimalToUShortTestCases))]
        public void CastDecimalToUShort(Maybe<decimal> maybe, bool mustHaveValue, ushort expectedValue)
        {
            CastTest(() => maybe.ToUShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromFloatToUShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<float>.None, false, null);
                yield return new TestCaseData(Maybe<float>.Some(-20f), false, null);
                yield return new TestCaseData(Maybe<float>.Some(1.1f), true, (ushort)1);
                yield return new TestCaseData(Maybe<float>.Some(11.6f), true, (ushort)12);
                yield return new TestCaseData(Maybe<float>.Some(65000f), true, (ushort)65000);
                yield return new TestCaseData(Maybe<float>.Some(300000f), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromFloatToUShortTestCases))]
        public void CastFloatToUShort(Maybe<float> maybe, bool mustHaveValue, ushort expectedValue)
        {
            CastTest(() => maybe.ToUShort(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDoubleToUShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<double>.None, false, null);
                yield return new TestCaseData(Maybe<double>.Some(-20d), false, null);
                yield return new TestCaseData(Maybe<double>.Some(1.1d), true, (ushort)1);
                yield return new TestCaseData(Maybe<double>.Some(11.6d), true, (ushort)12);
                yield return new TestCaseData(Maybe<double>.Some(65000d), true, (ushort)65000);
                yield return new TestCaseData(Maybe<double>.Some(300000d), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDoubleToUShortTestCases))]
        public void CastDoubleToUShort(Maybe<double> maybe, bool mustHaveValue, ushort expectedValue)
        {
            CastTest(() => maybe.ToUShort(), mustHaveValue, expectedValue);
        }

        #endregion

        #region Cast to Int

        private static IEnumerable<TestCaseData> CreateCastFromByteToIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<byte>.None, false, null);
                yield return new TestCaseData(Maybe<byte>.Some(12), true, 12);
                yield return new TestCaseData(Maybe<byte>.Some(220), true, 220);
            }
        }

        [TestCaseSource(nameof(CreateCastFromByteToIntTestCases))]
        public void CastByteToInt(Maybe<byte> maybe, bool mustHaveValue, int expectedValue)
        {
            CastTest(() => maybe.ToInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromSByteToIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<sbyte>.None, false, null);
                yield return new TestCaseData(Maybe<sbyte>.Some(12), true, 12);
                yield return new TestCaseData(Maybe<sbyte>.Some(-12), true, -12);
            }
        }

        [TestCaseSource(nameof(CreateCastFromSByteToIntTestCases))]
        public void CastSByteToInt(Maybe<sbyte> maybe, bool mustHaveValue, int expectedValue)
        {
            CastTest(() => maybe.ToInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromShortToIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<short>.None, false, null);
                yield return new TestCaseData(Maybe<short>.Some(12), true, 12);
                yield return new TestCaseData(Maybe<short>.Some(-12), true, -12);
            }
        }

        [TestCaseSource(nameof(CreateCastFromShortToIntTestCases))]
        public void CastShortToInt(Maybe<short> maybe, bool mustHaveValue, int expectedValue)
        {
            CastTest(() => maybe.ToInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUShortToIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ushort>.None, false, null);
                yield return new TestCaseData(Maybe<ushort>.Some(12), true, 12);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUShortToIntTestCases))]
        public void CastUShortToInt(Maybe<ushort> maybe, bool mustHaveValue, int expectedValue)
        {
            CastTest(() => maybe.ToInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUIntToIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<uint>.None, false, null);
                yield return new TestCaseData(Maybe<uint>.Some(2160000u), true, 2160000);
                yield return new TestCaseData(Maybe<uint>.Some(2500000000u), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUIntToIntTestCases))]
        public void CastUIntToInt(Maybe<uint> maybe, bool mustHaveValue, int expectedValue)
        {
            CastTest(() => maybe.ToInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromLongToIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<long>.None, false, null);
                yield return new TestCaseData(Maybe<long>.Some(-12L), true, -12);
                yield return new TestCaseData(Maybe<long>.Some(-3000000000L), false, null);
                yield return new TestCaseData(Maybe<long>.Some(75000L), true, 75000);
                yield return new TestCaseData(Maybe<long>.Some(3000000000L), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromLongToIntTestCases))]
        public void CastLongToInt(Maybe<long> maybe, bool mustHaveValue, int expectedValue)
        {
            CastTest(() => maybe.ToInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromULongToIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ulong>.None, false, null);
                yield return new TestCaseData(Maybe<ulong>.Some(75000ul), true, 75000);
                yield return new TestCaseData(Maybe<ulong>.Some(3000000000ul), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromULongToIntTestCases))]
        public void CastULongToInt(Maybe<ulong> maybe, bool mustHaveValue, int expectedValue)
        {
            CastTest(() => maybe.ToInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDecimalToIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<decimal>.None, false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(-75000m), true, -75000);
                yield return new TestCaseData(Maybe<decimal>.Some(-3000000000m), false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(45.2m), true, 45);
                yield return new TestCaseData(Maybe<decimal>.Some(42.6m), true, 43);
                yield return new TestCaseData(Maybe<decimal>.Some(75000m), true, 75000);
                yield return new TestCaseData(Maybe<decimal>.Some(3000000000m), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDecimalToIntTestCases))]
        public void CastDecimalToInt(Maybe<decimal> maybe, bool mustHaveValue, int expectedValue)
        {
            CastTest(() => maybe.ToInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromFloatToIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<float>.None, false, null);
                yield return new TestCaseData(Maybe<float>.Some(-75000f), true, -75000);
                yield return new TestCaseData(Maybe<float>.Some(-3000000000f), false, null);
                yield return new TestCaseData(Maybe<float>.Some(45.2f), true, 45);
                yield return new TestCaseData(Maybe<float>.Some(42.6f), true, 43);
                yield return new TestCaseData(Maybe<float>.Some(75000f), true, 75000);
                yield return new TestCaseData(Maybe<float>.Some(3000000000f), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromFloatToIntTestCases))]
        public void CastFloatToInt(Maybe<float> maybe, bool mustHaveValue, int expectedValue)
        {
            CastTest(() => maybe.ToInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDoubleToIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<double>.None, false, null);
                yield return new TestCaseData(Maybe<double>.Some(-75000d), true, -75000);
                yield return new TestCaseData(Maybe<double>.Some(-3000000000d), false, null);
                yield return new TestCaseData(Maybe<double>.Some(45.2d), true, 45);
                yield return new TestCaseData(Maybe<double>.Some(42.6d), true, 43);
                yield return new TestCaseData(Maybe<double>.Some(75000d), true, 75000);
                yield return new TestCaseData(Maybe<double>.Some(3000000000d), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDoubleToIntTestCases))]
        public void CastDoubleToInt(Maybe<double> maybe, bool mustHaveValue, int expectedValue)
        {
            CastTest(() => maybe.ToInt(), mustHaveValue, expectedValue);
        }

        #endregion

        #region Cast to UInt

        private static IEnumerable<TestCaseData> CreateCastFromByteToUIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<byte>.None, false, null);
                yield return new TestCaseData(Maybe<byte>.Some(12), true, 12u);
                yield return new TestCaseData(Maybe<byte>.Some(220), true, 220u);
            }
        }

        [TestCaseSource(nameof(CreateCastFromByteToUIntTestCases))]
        public void CastByteToUInt(Maybe<byte> maybe, bool mustHaveValue, uint expectedValue)
        {
            CastTest(() => maybe.ToUInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromSByteToUIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<sbyte>.None, false, null);
                yield return new TestCaseData(Maybe<sbyte>.Some(12), true, 12u);
                yield return new TestCaseData(Maybe<sbyte>.Some(-12), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromSByteToUIntTestCases))]
        public void CastSByteToUInt(Maybe<sbyte> maybe, bool mustHaveValue, uint expectedValue)
        {
            CastTest(() => maybe.ToUInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromShortToUIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<short>.None, false, null);
                yield return new TestCaseData(Maybe<short>.Some(12), true, 12u);
                yield return new TestCaseData(Maybe<short>.Some(-12), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromShortToUIntTestCases))]
        public void CastShortToUInt(Maybe<short> maybe, bool mustHaveValue, uint expectedValue)
        {
            CastTest(() => maybe.ToUInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUShortToUIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ushort>.None, false, null);
                yield return new TestCaseData(Maybe<ushort>.Some(12), true, 12u);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUShortToUIntTestCases))]
        public void CastUShortToUInt(Maybe<ushort> maybe, bool mustHaveValue, uint expectedValue)
        {
            CastTest(() => maybe.ToUInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromIntToUIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<int>.None, false, null);
                yield return new TestCaseData(Maybe<int>.Some(-12), false, null);
                yield return new TestCaseData(Maybe<int>.Some(75000), true, 75000u);
            }
        }

        [TestCaseSource(nameof(CreateCastFromIntToUIntTestCases))]
        public void CastIntToUInt(Maybe<int> maybe, bool mustHaveValue, uint expectedValue)
        {
            CastTest(() => maybe.ToUInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromLongToUIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<long>.None, false, null);
                yield return new TestCaseData(Maybe<long>.Some(-12L), false, null);
                yield return new TestCaseData(Maybe<long>.Some(75000L), true, 75000u);
                yield return new TestCaseData(Maybe<long>.Some(7000000000L), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromLongToUIntTestCases))]
        public void CastLongToUInt(Maybe<long> maybe, bool mustHaveValue, uint expectedValue)
        {
            CastTest(() => maybe.ToUInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromULongToUIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ulong>.None, false, null);
                yield return new TestCaseData(Maybe<ulong>.Some(75000ul), true, 75000u);
                yield return new TestCaseData(Maybe<ulong>.Some(7000000000ul), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromULongToUIntTestCases))]
        public void CastULongToUInt(Maybe<ulong> maybe, bool mustHaveValue, uint expectedValue)
        {
            CastTest(() => maybe.ToUInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDecimalToUIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<decimal>.None, false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(-75000m), false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(23.4m), true, 23u);
                yield return new TestCaseData(Maybe<decimal>.Some(23.7m), true, 24u);
                yield return new TestCaseData(Maybe<decimal>.Some(75000m), true, 75000u);
                yield return new TestCaseData(Maybe<decimal>.Some(7000000000m), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDecimalToUIntTestCases))]
        public void CastDecimalToUInt(Maybe<decimal> maybe, bool mustHaveValue, uint expectedValue)
        {
            CastTest(() => maybe.ToUInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromFloatToUIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<float>.None, false, null);
                yield return new TestCaseData(Maybe<float>.Some(-75000f), false, null);
                yield return new TestCaseData(Maybe<float>.Some(23.4f), true, 23u);
                yield return new TestCaseData(Maybe<float>.Some(23.7f), true, 24u);
                yield return new TestCaseData(Maybe<float>.Some(75000f), true, 75000u);
                yield return new TestCaseData(Maybe<float>.Some(7000000000f), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromFloatToUIntTestCases))]
        public void CastFloatToUInt(Maybe<float> maybe, bool mustHaveValue, uint expectedValue)
        {
            CastTest(() => maybe.ToUInt(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDoubleToUIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<double>.None, false, null);
                yield return new TestCaseData(Maybe<double>.Some(-75000d), false, null);
                yield return new TestCaseData(Maybe<double>.Some(23.4d), true, 23u);
                yield return new TestCaseData(Maybe<double>.Some(23.7d), true, 24u);
                yield return new TestCaseData(Maybe<double>.Some(75000d), true, 75000u);
                yield return new TestCaseData(Maybe<double>.Some(7000000000d), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDoubleToUIntTestCases))]
        public void CastDoubleToUInt(Maybe<double> maybe, bool mustHaveValue, uint expectedValue)
        {
            CastTest(() => maybe.ToUInt(), mustHaveValue, expectedValue);
        }

        #endregion

        #region Cast to Long

        private static IEnumerable<TestCaseData> CreateCastFromByteToLongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<byte>.None, false, null);
                yield return new TestCaseData(Maybe<byte>.Some(12), true, 12L);
                yield return new TestCaseData(Maybe<byte>.Some(220), true, 220L);
            }
        }

        [TestCaseSource(nameof(CreateCastFromByteToLongTestCases))]
        public void CastByteToLong(Maybe<byte> maybe, bool mustHaveValue, long expectedValue)
        {
            CastTest(() => maybe.ToLong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromSByteToLongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<sbyte>.None, false, null);
                yield return new TestCaseData(Maybe<sbyte>.Some(12), true, 12L);
                yield return new TestCaseData(Maybe<sbyte>.Some(-12), true, -12L);
            }
        }

        [TestCaseSource(nameof(CreateCastFromSByteToLongTestCases))]
        public void CastSByteToLong(Maybe<sbyte> maybe, bool mustHaveValue, long expectedValue)
        {
            CastTest(() => maybe.ToLong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromShortToLongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<short>.None, false, null);
                yield return new TestCaseData(Maybe<short>.Some(12), true, 12L);
                yield return new TestCaseData(Maybe<short>.Some(-12), true, -12L);
            }
        }

        [TestCaseSource(nameof(CreateCastFromShortToLongTestCases))]
        public void CastShortToLong(Maybe<short> maybe, bool mustHaveValue, long expectedValue)
        {
            CastTest(() => maybe.ToLong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUShortToLongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ushort>.None, false, null);
                yield return new TestCaseData(Maybe<ushort>.Some(12), true, 12L);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUShortToLongTestCases))]
        public void CastUShortToLong(Maybe<ushort> maybe, bool mustHaveValue, long expectedValue)
        {
            CastTest(() => maybe.ToLong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromIntToLongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<int>.None, false, null);
                yield return new TestCaseData(Maybe<int>.Some(-12), true, -12L);
                yield return new TestCaseData(Maybe<int>.Some(75000), true, 75000L);
            }
        }

        [TestCaseSource(nameof(CreateCastFromIntToLongTestCases))]
        public void CastIntToLong(Maybe<int> maybe, bool mustHaveValue, long expectedValue)
        {
            CastTest(() => maybe.ToLong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUIntToLongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<uint>.None, false, null);
                yield return new TestCaseData(Maybe<uint>.Some(2500000000u), true, 2500000000L);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUIntToLongTestCases))]
        public void CastUIntToLong(Maybe<uint> maybe, bool mustHaveValue, long expectedValue)
        {
            CastTest(() => maybe.ToLong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromULongToLongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ulong>.None, false, null);
                yield return new TestCaseData(Maybe<ulong>.Some(2500000000ul), true, 2500000000L);
                yield return new TestCaseData(Maybe<ulong>.Some(10000000000000000000ul), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromULongToLongTestCases))]
        public void CastULongToLong(Maybe<ulong> maybe, bool mustHaveValue, long expectedValue)
        {
            CastTest(() => maybe.ToLong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDecimalToLongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<decimal>.None, false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(-2500000000m), true, -2500000000L);
                yield return new TestCaseData(Maybe<decimal>.Some(-10000000000000000000m), false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(45.2m), true, 45L);
                yield return new TestCaseData(Maybe<decimal>.Some(45.8m), true, 46L);
                yield return new TestCaseData(Maybe<decimal>.Some(2500000000m), true, 2500000000L);
                yield return new TestCaseData(Maybe<decimal>.Some(10000000000000000000m), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDecimalToLongTestCases))]
        public void CastDecimalToLong(Maybe<decimal> maybe, bool mustHaveValue, long expectedValue)
        {
            CastTest(() => maybe.ToLong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromFloatToLongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<float>.None, false, null);
                yield return new TestCaseData(Maybe<float>.Some(-2500000000f), true, -2500000000L);
                yield return new TestCaseData(Maybe<float>.Some(-10000000000000000000f), false, null);
                yield return new TestCaseData(Maybe<float>.Some(45.2f), true, 45L);
                yield return new TestCaseData(Maybe<float>.Some(45.8f), true, 46L);
                yield return new TestCaseData(Maybe<float>.Some(2500000000f), true, 2500000000L);
                yield return new TestCaseData(Maybe<float>.Some(10000000000000000000f), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromFloatToLongTestCases))]
        public void CastFloatToLong(Maybe<float> maybe, bool mustHaveValue, long expectedValue)
        {
            CastTest(() => maybe.ToLong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDoubleToLongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<double>.None, false, null);
                yield return new TestCaseData(Maybe<double>.Some(-2500000000d), true, -2500000000L);
                yield return new TestCaseData(Maybe<double>.Some(-10000000000000000000d), false, null);
                yield return new TestCaseData(Maybe<double>.Some(45.2d), true, 45L);
                yield return new TestCaseData(Maybe<double>.Some(45.8d), true, 46L);
                yield return new TestCaseData(Maybe<double>.Some(2500000000d), true, 2500000000L);
                yield return new TestCaseData(Maybe<double>.Some(10000000000000000000d), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDoubleToLongTestCases))]
        public void CastDoubleToLong(Maybe<double> maybe, bool mustHaveValue, long expectedValue)
        {
            CastTest(() => maybe.ToLong(), mustHaveValue, expectedValue);
        }

        #endregion

        #region Cast to ULong

        private static IEnumerable<TestCaseData> CreateCastFromByteToULongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<byte>.None, false, null);
                yield return new TestCaseData(Maybe<byte>.Some(12), true, 12ul);
                yield return new TestCaseData(Maybe<byte>.Some(220), true, 220ul);
            }
        }

        [TestCaseSource(nameof(CreateCastFromByteToULongTestCases))]
        public void CastByteToULong(Maybe<byte> maybe, bool mustHaveValue, ulong expectedValue)
        {
            CastTest(() => maybe.ToULong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromSByteToULongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<sbyte>.None, false, null);
                yield return new TestCaseData(Maybe<sbyte>.Some(12), true, 12ul);
                yield return new TestCaseData(Maybe<sbyte>.Some(-12), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromSByteToULongTestCases))]
        public void CastSByteToULong(Maybe<sbyte> maybe, bool mustHaveValue, ulong expectedValue)
        {
            CastTest(() => maybe.ToULong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromShortToULongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<short>.None, false, null);
                yield return new TestCaseData(Maybe<short>.Some(12), true, 12ul);
                yield return new TestCaseData(Maybe<short>.Some(-12), false, null);
            }
        }

        [TestCaseSource(nameof(CreateCastFromShortToULongTestCases))]
        public void CastShortToULong(Maybe<short> maybe, bool mustHaveValue, ulong expectedValue)
        {
            CastTest(() => maybe.ToULong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUShortToULongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ushort>.None, false, null);
                yield return new TestCaseData(Maybe<ushort>.Some(12), true, 12ul);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUShortToULongTestCases))]
        public void CastUShortToULong(Maybe<ushort> maybe, bool mustHaveValue, ulong expectedValue)
        {
            CastTest(() => maybe.ToULong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromIntToULongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<int>.None, false, null);
                yield return new TestCaseData(Maybe<int>.Some(-12), false, null);
                yield return new TestCaseData(Maybe<int>.Some(75000), true, 75000ul);
            }
        }

        [TestCaseSource(nameof(CreateCastFromIntToULongTestCases))]
        public void CastIntToULong(Maybe<int> maybe, bool mustHaveValue, ulong expectedValue)
        {
            CastTest(() => maybe.ToULong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUIntToULongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<uint>.None, false, null);
                yield return new TestCaseData(Maybe<uint>.Some(2500000000u), true, 2500000000ul);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUIntToULongTestCases))]
        public void CastUIntToULong(Maybe<uint> maybe, bool mustHaveValue, ulong expectedValue)
        {
            CastTest(() => maybe.ToULong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromLongToULongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<long>.None, false, null);
                yield return new TestCaseData(Maybe<long>.Some(-2160000L), false, null);
                yield return new TestCaseData(Maybe<long>.Some(2160000L), true, 2160000ul);
            }
        }

        [TestCaseSource(nameof(CreateCastFromLongToULongTestCases))]
        public void CastLongToULong(Maybe<long> maybe, bool mustHaveValue, ulong expectedValue)
        {
            CastTest(() => maybe.ToULong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDecimalToULongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<decimal>.None, false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(-2500000000m), false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(45.2m), true, 45ul);
                yield return new TestCaseData(Maybe<decimal>.Some(45.8m), true, 46ul);
                yield return new TestCaseData(Maybe<decimal>.Some(2500000000m), true, 2500000000ul);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDecimalToULongTestCases))]
        public void CastDecimalToULong(Maybe<decimal> maybe, bool mustHaveValue, ulong expectedValue)
        {
            CastTest(() => maybe.ToULong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromFloatToULongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<float>.None, false, null);
                yield return new TestCaseData(Maybe<float>.Some(-2500000000f), false, null);
                yield return new TestCaseData(Maybe<float>.Some(45.2f), true, 45ul);
                yield return new TestCaseData(Maybe<float>.Some(45.8f), true, 46ul);
                yield return new TestCaseData(Maybe<float>.Some(2500000000f), true, 2500000000ul);
            }
        }

        [TestCaseSource(nameof(CreateCastFromFloatToULongTestCases))]
        public void CastFloatToULong(Maybe<float> maybe, bool mustHaveValue, ulong expectedValue)
        {
            CastTest(() => maybe.ToULong(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDoubleToULongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<double>.None, false, null);
                yield return new TestCaseData(Maybe<double>.Some(-2500000000d), false, null);
                yield return new TestCaseData(Maybe<double>.Some(45.2d), true, 45ul);
                yield return new TestCaseData(Maybe<double>.Some(45.8d), true, 46ul);
                yield return new TestCaseData(Maybe<double>.Some(2500000000d), true, 2500000000ul);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDoubleToULongTestCases))]
        public void CastDoubleToULong(Maybe<double> maybe, bool mustHaveValue, ulong expectedValue)
        {
            CastTest(() => maybe.ToULong(), mustHaveValue, expectedValue);
        }

        #endregion

        #region Cast to Decimal

        private static IEnumerable<TestCaseData> CreateCastFromByteToDecimalTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<byte>.None, false, null);
                yield return new TestCaseData(Maybe<byte>.Some(12), true, 12m);
                yield return new TestCaseData(Maybe<byte>.Some(220), true, 220m);
            }
        }

        [TestCaseSource(nameof(CreateCastFromByteToDecimalTestCases))]
        public void CastByteToDecimal(Maybe<byte> maybe, bool mustHaveValue, decimal expectedValue)
        {
            CastTest(() => maybe.ToDecimal(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromSByteToDecimalTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<sbyte>.None, false, null);
                yield return new TestCaseData(Maybe<sbyte>.Some(12), true, 12m);
                yield return new TestCaseData(Maybe<sbyte>.Some(-12), true, -12m);
            }
        }

        [TestCaseSource(nameof(CreateCastFromSByteToDecimalTestCases))]
        public void CastSByteToDecimal(Maybe<sbyte> maybe, bool mustHaveValue, decimal expectedValue)
        {
            CastTest(() => maybe.ToDecimal(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromShortToDecimalTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<short>.None, false, null);
                yield return new TestCaseData(Maybe<short>.Some(12), true, 12m);
                yield return new TestCaseData(Maybe<short>.Some(-12), true, -12m);
            }
        }

        [TestCaseSource(nameof(CreateCastFromShortToDecimalTestCases))]
        public void CastShortToDecimal(Maybe<short> maybe, bool mustHaveValue, decimal expectedValue)
        {
            CastTest(() => maybe.ToDecimal(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUShortToDecimalTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ushort>.None, false, null);
                yield return new TestCaseData(Maybe<ushort>.Some(12), true, 12m);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUShortToDecimalTestCases))]
        public void CastUShortToDecimal(Maybe<ushort> maybe, bool mustHaveValue, decimal expectedValue)
        {
            CastTest(() => maybe.ToDecimal(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromIntToDecimalTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<int>.None, false, null);
                yield return new TestCaseData(Maybe<int>.Some(-2160000), true, -2160000m);
                yield return new TestCaseData(Maybe<int>.Some(2160000), true, 2160000m);
            }
        }

        [TestCaseSource(nameof(CreateCastFromIntToDecimalTestCases))]
        public void CastIntToDecimal(Maybe<int> maybe, bool mustHaveValue, decimal expectedValue)
        {
            CastTest(() => maybe.ToDecimal(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUIntToDecimalTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<uint>.None, false, null);
                yield return new TestCaseData(Maybe<uint>.Some(2500000000u), true, 2500000000m);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUIntToDecimalTestCases))]
        public void CastUIntToDecimal(Maybe<uint> maybe, bool mustHaveValue, decimal expectedValue)
        {
            CastTest(() => maybe.ToDecimal(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromLongToDecimalTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<long>.None, false, null);
                yield return new TestCaseData(Maybe<long>.Some(-2160000L), true, -2160000m);
                yield return new TestCaseData(Maybe<long>.Some(2160000L), true, 2160000m);
            }
        }

        [TestCaseSource(nameof(CreateCastFromLongToDecimalTestCases))]
        public void CastLongToDecimal(Maybe<long> maybe, bool mustHaveValue, decimal expectedValue)
        {
            CastTest(() => maybe.ToDecimal(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromULongToDecimalTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ulong>.None, false, null);
                yield return new TestCaseData(Maybe<ulong>.Some(2160000ul), true, 2160000m);
                yield return new TestCaseData(Maybe<ulong>.Some(18000000000000000000ul), true, 18000000000000000000m);
            }
        }

        [TestCaseSource(nameof(CreateCastFromULongToDecimalTestCases))]
        public void CastULongToDecimal(Maybe<ulong> maybe, bool mustHaveValue, decimal expectedValue)
        {
            CastTest(() => maybe.ToDecimal(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromFloatToDecimalTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<float>.None, false, null);
                yield return new TestCaseData(Maybe<float>.Some(-2500000000f), true, -2500000000m);
                yield return new TestCaseData(Maybe<float>.Some(45.2f), true, 45.2m);
                yield return new TestCaseData(Maybe<float>.Some(45.8f), true, 45.8m);
                yield return new TestCaseData(Maybe<float>.Some(2500000000f), true, 2500000000m);
            }
        }

        [TestCaseSource(nameof(CreateCastFromFloatToDecimalTestCases))]
        public void CastFloatToDecimal(Maybe<float> maybe, bool mustHaveValue, decimal expectedValue)
        {
            CastTest(() => maybe.ToDecimal(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDoubleToDecimalTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<double>.None, false, null);
                yield return new TestCaseData(Maybe<double>.Some(-2500000000d), true, -2500000000m);
                yield return new TestCaseData(Maybe<double>.Some(45.2d), true, 45.2m);
                yield return new TestCaseData(Maybe<double>.Some(45.8d), true, 45.8m);
                yield return new TestCaseData(Maybe<double>.Some(2500000000d), true, 2500000000m);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDoubleToDecimalTestCases))]
        public void CastDoubleToDecimal(Maybe<double> maybe, bool mustHaveValue, decimal expectedValue)
        {
            CastTest(() => maybe.ToDecimal(), mustHaveValue, expectedValue);
        }

        #endregion

        #region Cast to Float

        private static IEnumerable<TestCaseData> CreateCastFromByteToFloatTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<byte>.None, false, null);
                yield return new TestCaseData(Maybe<byte>.Some(12), true, 12f);
                yield return new TestCaseData(Maybe<byte>.Some(220), true, 220f);
            }
        }

        [TestCaseSource(nameof(CreateCastFromByteToFloatTestCases))]
        public void CastByteToFloat(Maybe<byte> maybe, bool mustHaveValue, float expectedValue)
        {
            CastTest(() => maybe.ToFloat(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromSByteToFloatTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<sbyte>.None, false, null);
                yield return new TestCaseData(Maybe<sbyte>.Some(12), true, 12f);
                yield return new TestCaseData(Maybe<sbyte>.Some(-12), true, -12f);
            }
        }

        [TestCaseSource(nameof(CreateCastFromSByteToFloatTestCases))]
        public void CastSByteToFloat(Maybe<sbyte> maybe, bool mustHaveValue, float expectedValue)
        {
            CastTest(() => maybe.ToFloat(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromShortToFloatTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<short>.None, false, null);
                yield return new TestCaseData(Maybe<short>.Some(12), true, 12f);
                yield return new TestCaseData(Maybe<short>.Some(-12), true, -12f);
            }
        }

        [TestCaseSource(nameof(CreateCastFromShortToFloatTestCases))]
        public void CastShortToFloat(Maybe<short> maybe, bool mustHaveValue, float expectedValue)
        {
            CastTest(() => maybe.ToFloat(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUShortToFloatTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ushort>.None, false, null);
                yield return new TestCaseData(Maybe<ushort>.Some(12), true, 12f);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUShortToFloatTestCases))]
        public void CastUShortToFloat(Maybe<ushort> maybe, bool mustHaveValue, float expectedValue)
        {
            CastTest(() => maybe.ToFloat(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromIntToFloatTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<int>.None, false, null);
                yield return new TestCaseData(Maybe<int>.Some(-2160000), true, -2160000f);
                yield return new TestCaseData(Maybe<int>.Some(2160000), true, 2160000f);
            }
        }

        [TestCaseSource(nameof(CreateCastFromIntToFloatTestCases))]
        public void CastIntToFloat(Maybe<int> maybe, bool mustHaveValue, float expectedValue)
        {
            CastTest(() => maybe.ToFloat(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUIntToFloatTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<uint>.None, false, null);
                yield return new TestCaseData(Maybe<uint>.Some(2160000u), true, 2160000f);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUIntToFloatTestCases))]
        public void CastUIntToFloat(Maybe<uint> maybe, bool mustHaveValue, float expectedValue)
        {
            CastTest(() => maybe.ToFloat(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromLongToFloatTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<long>.None, false, null);
                yield return new TestCaseData(Maybe<long>.Some(-2160000L), true, -2160000f);
                yield return new TestCaseData(Maybe<long>.Some(2160000L), true, 2160000f);
            }
        }

        [TestCaseSource(nameof(CreateCastFromLongToFloatTestCases))]
        public void CastLongToFloat(Maybe<long> maybe, bool mustHaveValue, float expectedValue)
        {
            CastTest(() => maybe.ToFloat(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromULongToFloatTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ulong>.None, false, null);
                yield return new TestCaseData(Maybe<ulong>.Some(2160000ul), true, 2160000f);
            }
        }

        [TestCaseSource(nameof(CreateCastFromULongToFloatTestCases))]
        public void CastULongToFloat(Maybe<ulong> maybe, bool mustHaveValue, float expectedValue)
        {
            CastTest(() => maybe.ToFloat(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDecimalToFloatTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<decimal>.None, false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(-2160000m), true, -2160000f);
                yield return new TestCaseData(Maybe<decimal>.Some(12.4m), true, 12.4f);
                yield return new TestCaseData(Maybe<decimal>.Some(-12.4568m), true, -12.4568f);
                yield return new TestCaseData(Maybe<decimal>.Some(2160000m), true, 2160000f);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDecimalToFloatTestCases))]
        public void CastDecimalToFloat(Maybe<decimal> maybe, bool mustHaveValue, float expectedValue)
        {
            CastTest(() => maybe.ToFloat(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDoubleToFloatTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<double>.None, false, null);
                yield return new TestCaseData(Maybe<double>.Some(-2160000d), true, -2160000f);
                yield return new TestCaseData(Maybe<double>.Some(12.4d), true, 12.4f);
                yield return new TestCaseData(Maybe<double>.Some(-12.4568d), true, -12.4568f);
                yield return new TestCaseData(Maybe<double>.Some(2160000d), true, 2160000f);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDoubleToFloatTestCases))]
        public void CastDoubleToFloat(Maybe<double> maybe, bool mustHaveValue, float expectedValue)
        {
            CastTest(() => maybe.ToFloat(), mustHaveValue, expectedValue);
        }

        #endregion

        #region Cast to Double

        private static IEnumerable<TestCaseData> CreateCastFromByteToDoubleTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<byte>.None, false, null);
                yield return new TestCaseData(Maybe<byte>.Some(12), true, 12d);
                yield return new TestCaseData(Maybe<byte>.Some(220), true, 220d);
            }
        }

        [TestCaseSource(nameof(CreateCastFromByteToDoubleTestCases))]
        public void CastByteToDouble(Maybe<byte> maybe, bool mustHaveValue, double expectedValue)
        {
            CastTest(() => maybe.ToDouble(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromSByteToDoubleTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<sbyte>.None, false, null);
                yield return new TestCaseData(Maybe<sbyte>.Some(12), true, 12d);
                yield return new TestCaseData(Maybe<sbyte>.Some(-12), true, -12d);
            }
        }

        [TestCaseSource(nameof(CreateCastFromSByteToDoubleTestCases))]
        public void CastSByteToDouble(Maybe<sbyte> maybe, bool mustHaveValue, double expectedValue)
        {
            CastTest(() => maybe.ToDouble(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromShortToDoubleTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<short>.None, false, null);
                yield return new TestCaseData(Maybe<short>.Some(12), true, 12d);
                yield return new TestCaseData(Maybe<short>.Some(-12), true, -12d);
            }
        }

        [TestCaseSource(nameof(CreateCastFromShortToDoubleTestCases))]
        public void CastShortToDouble(Maybe<short> maybe, bool mustHaveValue, double expectedValue)
        {
            CastTest(() => maybe.ToDouble(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUShortToDoubleTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ushort>.None, false, null);
                yield return new TestCaseData(Maybe<ushort>.Some(12), true, 12d);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUShortToDoubleTestCases))]
        public void CastUShortToDouble(Maybe<ushort> maybe, bool mustHaveValue, double expectedValue)
        {
            CastTest(() => maybe.ToDouble(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromIntToDoubleTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<int>.None, false, null);
                yield return new TestCaseData(Maybe<int>.Some(-2160000), true, -2160000d);
                yield return new TestCaseData(Maybe<int>.Some(2160000), true, 2160000d);
            }
        }

        [TestCaseSource(nameof(CreateCastFromIntToDoubleTestCases))]
        public void CastIntToFloat(Maybe<int> maybe, bool mustHaveValue, double expectedValue)
        {
            CastTest(() => maybe.ToDouble(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromUIntToDoubleTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<uint>.None, false, null);
                yield return new TestCaseData(Maybe<uint>.Some(2160000u), true, 2160000d);
            }
        }

        [TestCaseSource(nameof(CreateCastFromUIntToDoubleTestCases))]
        public void CastUIntToDouble(Maybe<uint> maybe, bool mustHaveValue, double expectedValue)
        {
            CastTest(() => maybe.ToDouble(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromLongToDoubleTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<long>.None, false, null);
                yield return new TestCaseData(Maybe<long>.Some(-2160000L), true, -2160000d);
                yield return new TestCaseData(Maybe<long>.Some(2160000L), true, 2160000d);
            }
        }

        [TestCaseSource(nameof(CreateCastFromLongToDoubleTestCases))]
        public void CastLongToDouble(Maybe<long> maybe, bool mustHaveValue, double expectedValue)
        {
            CastTest(() => maybe.ToDouble(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromULongToDoubleTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<ulong>.None, false, null);
                yield return new TestCaseData(Maybe<ulong>.Some(2160000ul), true, 2160000d);
            }
        }

        [TestCaseSource(nameof(CreateCastFromULongToDoubleTestCases))]
        public void CastULongToDouble(Maybe<ulong> maybe, bool mustHaveValue, double expectedValue)
        {
            CastTest(() => maybe.ToDouble(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromDecimalToDoubleTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<decimal>.None, false, null);
                yield return new TestCaseData(Maybe<decimal>.Some(-2160000m), true, -2160000d);
                yield return new TestCaseData(Maybe<decimal>.Some(12.4m), true, 12.4d);
                yield return new TestCaseData(Maybe<decimal>.Some(-12.4568m), true, -12.4568d);
                yield return new TestCaseData(Maybe<decimal>.Some(2160000m), true, 2160000d);
            }
        }

        [TestCaseSource(nameof(CreateCastFromDecimalToDoubleTestCases))]
        public void CastDecimalToDouble(Maybe<decimal> maybe, bool mustHaveValue, double expectedValue)
        {
            CastTest(() => maybe.ToDouble(), mustHaveValue, expectedValue);
        }

        private static IEnumerable<TestCaseData> CreateCastFromFloatToDoubleTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(Maybe<float>.None, false, null);
                yield return new TestCaseData(Maybe<float>.Some(-2160000f), true, -2160000d);
                yield return new TestCaseData(Maybe<float>.Some(12.4f), true, 12.4d);
                yield return new TestCaseData(Maybe<float>.Some(-12.4568f), true, -12.4568d);
                yield return new TestCaseData(Maybe<float>.Some(2160000f), true, 2160000d);
            }
        }

        [TestCaseSource(nameof(CreateCastFromFloatToDoubleTestCases))]
        public void CastFloatToDouble(Maybe<float> maybe, bool mustHaveValue, double expectedValue)
        {
            CastTest(maybe, maybeFloat => maybeFloat.ToDouble(), mustHaveValue, expectedValue);
        }

        #endregion
    }
}

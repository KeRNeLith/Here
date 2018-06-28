using NUnit.Framework;
using System.Collections.Generic;
using Here.Maybes.Extensions;
using JetBrains.Annotations;
using System;

namespace Here.Maybes.Tests
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/> try get.
    /// </summary>
    [TestFixture]
    internal class MaybeTryGetTests : MaybeTestsBase
    {
        [Test]
        public void TryGetDictionaries()
        {
            // Dictionary
            var dictionaryIntString = new Dictionary<int, string>
            {
                [2] = "string 2",
                [12] = "string 12",
            };

            var maybeString = dictionaryIntString.TryGetValue(2);
            Assert.IsTrue(maybeString.HasValue);
            Assert.AreEqual("string 2", maybeString.Value);

            maybeString = dictionaryIntString.TryGetValue(42);
            Assert.IsFalse(maybeString.HasValue);
            
            // Readonly Dictionary
            IReadOnlyDictionary<int, string> readonlyDictionaryIntString = new Dictionary<int, string>
            {
                [42] = "string 42",
                [88] = "string 88",
            };
            
            maybeString = readonlyDictionaryIntString.TryGetReadonlyValue(42);
            Assert.IsTrue(maybeString.HasValue);
            Assert.AreEqual("string 42", maybeString.Value);

            maybeString = readonlyDictionaryIntString.TryGetReadonlyValue(12);
            Assert.IsFalse(maybeString.HasValue);
        }

        #region Common TryParse

        /// <summary>
        /// Call the <paramref name="tryParseFunc"/> and check if the result match expected value.
        /// </summary>
        private static void TryParseTest<T>(Func<Maybe<T>> tryParseFunc, bool mustHaveValue, T expectedValue)
        {
            var maybe = tryParseFunc();
            if (mustHaveValue)
            {
                Assert.IsTrue(maybe.HasValue);
                Assert.AreEqual(expectedValue, maybe.Value);
            }
            else
            {
                Assert.IsFalse(maybe.HasValue);
            }
        }

        #region TryParse bool

        private static IEnumerable<TestCaseData> CreateTryParseBoolTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(string.Empty, false, null);
                yield return new TestCaseData("True", true, true);
                yield return new TestCaseData("False", true, false);
                yield return new TestCaseData("true", true, true);
                yield return new TestCaseData("false", true, false);
                yield return new TestCaseData("    true    ", true, true);
                yield return new TestCaseData("0", false, null);
                yield return new TestCaseData("1", false, null);
                yield return new TestCaseData("-1", false, null);
                yield return new TestCaseData("str", false, null);
            }
        }


        [TestCaseSource(nameof(CreateTryParseBoolTestCases))]
        public void TryParseBool(string input, bool mustHaveValue, bool expectedValue)
        {
            TryParseTest(input.TryParseBool, mustHaveValue, expectedValue);
        }

        #endregion

        #region TryParse char

        private static IEnumerable<TestCaseData> CreateTryParseCharTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(string.Empty, false, null);
                yield return new TestCaseData("Abcd", false, null);
                yield return new TestCaseData("a", true, 'a');
            }
        }


        [TestCaseSource(nameof(CreateTryParseCharTestCases))]
        public void TryParseChar(string input, bool mustHaveValue, char expectedValue)
        {
            TryParseTest(input.TryParseChar, mustHaveValue, expectedValue);
        }

        #endregion

        #region TryParse byte

        private static IEnumerable<TestCaseData> CreateTryParseByteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(string.Empty, false, null);
                yield return new TestCaseData("string", false, null);
                yield return new TestCaseData("1024", false, null);
                yield return new TestCaseData("100.1", false, null);
                yield return new TestCaseData("-100", false, null);
                yield return new TestCaseData("100", true, (byte)100);
                yield return new TestCaseData("+100", true, (byte)100);
                yield return new TestCaseData("000000100", true, (byte)100);
                yield return new TestCaseData("   20   ", true, (byte)20);
            }
        }


        [TestCaseSource(nameof(CreateTryParseByteTestCases))]
        public void TryParseByte(string input, bool mustHaveValue, byte expectedValue)
        {
            TryParseTest(input.TryParseByte, mustHaveValue, expectedValue);
        }

        #endregion

        #region TryParse sbyte

        private static IEnumerable<TestCaseData> CreateTryParseSbyteTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(string.Empty, false, null);
                yield return new TestCaseData("string", false, null);
                yield return new TestCaseData("-200", false, null);
                yield return new TestCaseData("-3.6", false, null);
                yield return new TestCaseData("10,1", false, null);
                yield return new TestCaseData("985", false, null);
                yield return new TestCaseData("100", true, (sbyte)100);
                yield return new TestCaseData("+100", true, (sbyte)100);
                yield return new TestCaseData("-100", true, (sbyte)-100);
                yield return new TestCaseData("   20   ", true, (sbyte)20);
            }
        }


        [TestCaseSource(nameof(CreateTryParseSbyteTestCases))]
        public void TryParseSbyte(string input, bool mustHaveValue, sbyte expectedValue)
        {
            TryParseTest(input.TryParseSbyte, mustHaveValue, expectedValue);
        }

        #endregion

        #region TryParse short

        private static IEnumerable<TestCaseData> CreateTryParseShortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(string.Empty, false, null);
                yield return new TestCaseData("string", false, null);
                yield return new TestCaseData("-35000", false, null);
                yield return new TestCaseData("-3.6", false, null);
                yield return new TestCaseData("10.1", false, null);
                yield return new TestCaseData("35000", false, null);
                yield return new TestCaseData("100", true, (short)100);
                yield return new TestCaseData("+100", true, (short)100);
                yield return new TestCaseData("-100", true, (short)-100);
                yield return new TestCaseData("   20   ", true, (short)20);
            }
        }


        [TestCaseSource(nameof(CreateTryParseShortTestCases))]
        public void TryParseShort(string input, bool mustHaveValue, short expectedValue)
        {
            TryParseTest(input.TryParseShort, mustHaveValue, expectedValue);
        }

        #endregion

        #region TryParse ushort

        private static IEnumerable<TestCaseData> CreateTryParseUshortTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(string.Empty, false, null);
                yield return new TestCaseData("string", false, null);
                yield return new TestCaseData("-10", false, null);
                yield return new TestCaseData("10.1", false, null);
                yield return new TestCaseData("66000", false, null);
                yield return new TestCaseData("100", true, (ushort)100);
                yield return new TestCaseData("+100", true, (ushort)100);
                yield return new TestCaseData("   20   ", true, (ushort)20);
            }
        }


        [TestCaseSource(nameof(CreateTryParseUshortTestCases))]
        public void TryParseUshort(string input, bool mustHaveValue, ushort expectedValue)
        {
            TryParseTest(input.TryParseUshort, mustHaveValue, expectedValue);
        }

        #endregion

        #region TryParse int

        private static IEnumerable<TestCaseData> CreateTryParseIntTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(string.Empty, false, null);
                yield return new TestCaseData("string", false, null);
                yield return new TestCaseData("-2200000000", false, null);
                yield return new TestCaseData("10.1", false, null);
                yield return new TestCaseData("2200000000", false, null);
                yield return new TestCaseData("100", true, 100);
                yield return new TestCaseData("+100", true, 100);
                yield return new TestCaseData("   -20   ", true, -20);
            }
        }


        [TestCaseSource(nameof(CreateTryParseIntTestCases))]
        public void TryParseInt(string input, bool mustHaveValue, int expectedValue)
        {
            TryParseTest(input.TryParseInt, mustHaveValue, expectedValue);
        }

        #endregion

        #region TryParse uint

        private static IEnumerable<TestCaseData> CreateTryParseUintTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(string.Empty, false, null);
                yield return new TestCaseData("string", false, null);
                yield return new TestCaseData("-2", false, null);
                yield return new TestCaseData("10.1", false, null);
                yield return new TestCaseData("5000000000", false, null);
                yield return new TestCaseData("100", true, 100u);
                yield return new TestCaseData("   20   ", true, 20u);
            }
        }


        [TestCaseSource(nameof(CreateTryParseUintTestCases))]
        public void TryParseUint(string input, bool mustHaveValue, uint expectedValue)
        {
            TryParseTest(input.TryParseUint, mustHaveValue, expectedValue);
        }

        #endregion

        #region TryParse long

        private static IEnumerable<TestCaseData> CreateTryParseLongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(string.Empty, false, null);
                yield return new TestCaseData("string", false, null);
                yield return new TestCaseData("-10000000000000000000", false, null);
                yield return new TestCaseData("10.1", false, null);
                yield return new TestCaseData("10000000000000000000", false, null);
                yield return new TestCaseData("100", true, 100L);
                yield return new TestCaseData("-100", true, -100L);
                yield return new TestCaseData("   20   ", true, 20L);
            }
        }


        [TestCaseSource(nameof(CreateTryParseLongTestCases))]
        public void TryParseLong(string input, bool mustHaveValue, long expectedValue)
        {
            TryParseTest(input.TryParseLong, mustHaveValue, expectedValue);
        }

        #endregion

        #region TryParse ulong

        private static IEnumerable<TestCaseData> CreateTryParseUlongTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(string.Empty, false, null);
                yield return new TestCaseData("string", false, null);
                yield return new TestCaseData("-100", false, null);
                yield return new TestCaseData("10.1", false, null);
                yield return new TestCaseData("19000000000000000000", false, null);
                yield return new TestCaseData("100", true, 100ul);
                yield return new TestCaseData("   20   ", true, 20ul);
            }
        }


        [TestCaseSource(nameof(CreateTryParseUlongTestCases))]
        public void TryParseUlong(string input, bool mustHaveValue, ulong expectedValue)
        {
            TryParseTest(input.TryParseUlong, mustHaveValue, expectedValue);
        }

        #endregion

        #region TryParse decimal

        private static IEnumerable<TestCaseData> CreateTryParseDecimalTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(string.Empty, false, null);
                yield return new TestCaseData("string", false, null);
                yield return new TestCaseData("100", true, 100m);
                yield return new TestCaseData("   -20   ", true, -20m);
            }
        }


        [TestCaseSource(nameof(CreateTryParseDecimalTestCases))]
        public void TryParseDecimal(string input, bool mustHaveValue, decimal expectedValue)
        {
            TryParseTest(input.TryParseDecimal, mustHaveValue, expectedValue);
        }

        #endregion

        #region TryParse float

        private static IEnumerable<TestCaseData> CreateTryParseFloatTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(string.Empty, false, null);
                yield return new TestCaseData("string", false, null);
                yield return new TestCaseData("100.1", true, 100.1f);
                yield return new TestCaseData("   -20.2   ", true, -20.2f);
            }
        }


        [TestCaseSource(nameof(CreateTryParseFloatTestCases))]
        public void TryParseFloat(string input, bool mustHaveValue, float expectedValue)
        {
            TryParseTest(input.TryParseFloat, mustHaveValue, expectedValue);
        }

        #endregion

        #region TryParse double

        private static IEnumerable<TestCaseData> CreateTryParseDoubleTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(null, false, null);
                yield return new TestCaseData(string.Empty, false, null);
                yield return new TestCaseData("string", false, null);
                yield return new TestCaseData("100.1", true, 100.1);
                yield return new TestCaseData("   -20.2   ", true, -20.2);
            }
        }


        [TestCaseSource(nameof(CreateTryParseDoubleTestCases))]
        public void TryParseDouble(string input, bool mustHaveValue, double expectedValue)
        {
            TryParseTest(input.TryParseDouble, mustHaveValue, expectedValue);
        }

        #endregion

        #endregion
    }
}

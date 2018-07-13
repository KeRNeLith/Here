using System.Collections.Generic;
using NUnit.Framework;
using Here.Maybes;
using Here.Maybes.Extensions;

namespace Here.Tests.Maybes
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class MaybeExtensionsTests : HereTestsBase
    {
        #region Test methods

        private int GetInt()
        {
            return 12;
        }

        private int? GetNullableInt()
        {
            return 42;
        }

        private int? GetNullNullableInt()
        {
            return null;
        }

        private TestClass GetTestClass()
        {
            return new TestClass();
        }

        private TestClass GetNullTestClass()
        {
            return null;
        }

        #endregion

        [Test]
        public void ToMaybe()
        {
            // Value type
            var maybeInt = 1.ToMaybe();
            Assert.IsTrue(maybeInt.HasValue);
            Assert.AreEqual(1, maybeInt.Value);

            // Nullable
            var testNullableNull = (int?)null;
            // ReSharper disable once ExpressionIsAlwaysNull
            var maybeNullableNull = testNullableNull.ToMaybe();
            Assert.IsFalse(maybeNullableNull.HasValue);

            TestStruct? testNullableNotNull = new TestStruct();
            var maybeNullableNotNull = testNullableNotNull.ToMaybe();
            Assert.IsTrue(maybeNullableNotNull.HasValue);
            Assert.AreEqual(testNullableNotNull.Value, maybeNullableNotNull.Value);

            // Reference type
            var testObject = new TestClass();
            var maybeClass = testObject.ToMaybe();
            Assert.IsTrue(maybeClass.HasValue);
            Assert.AreSame(testObject, maybeClass.Value);

            TestClass testObjectNull = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            maybeClass = testObjectNull.ToMaybe();
            Assert.IsFalse(maybeClass.HasValue);
        }

        [Test]
        public void ReturnToMaybe()
        {
            // Value type
            Maybe<int> maybeInt = GetInt();
            Assert.IsTrue(maybeInt.HasValue);
            Assert.AreEqual(12, maybeInt.Value);

            // Nullable
            Maybe<int?> maybeNullableInt = GetNullableInt();
            Assert.IsTrue(maybeNullableInt.HasValue);
            Assert.AreEqual(42, maybeNullableInt.Value);

            maybeNullableInt = GetNullNullableInt();
            Assert.IsFalse(maybeNullableInt.HasValue);

            // Reference type
            Maybe<TestClass> maybeClass = GetTestClass();
            Assert.IsTrue(maybeClass.HasValue);
            Assert.AreEqual(new TestClass(), maybeClass.Value);

            maybeClass = GetNullTestClass();
            Assert.IsFalse(maybeClass.HasValue);
        }

        [Test]
        public void MaybeToNullable()
        {
            // Value type
            var maybeInt = Maybe<int>.Some(12);
            var intNullable = maybeInt.ToNullable();
            Assert.IsTrue(intNullable.HasValue);
            Assert.AreEqual(12, intNullable.Value);

            maybeInt = Maybe.None;
            intNullable = maybeInt.ToNullable();
            Assert.IsFalse(intNullable.HasValue);
        }

        [Test]
        public void StringToMaybe()
        {
            Maybe<string> maybeString;
            
            maybeString = string.Empty.NoneIfEmpty();
            Assert.IsFalse(maybeString.HasValue);
            
            maybeString = "".NoneIfEmpty();
            Assert.IsFalse(maybeString.HasValue);

            string testNull = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            maybeString = testNull.NoneIfEmpty();
            Assert.IsFalse(maybeString.HasValue);
            
            maybeString = "test".NoneIfEmpty();
            Assert.IsTrue(maybeString.HasValue);
            Assert.AreEqual("test", maybeString.Value);
        }

        [Test]
        public void EnumerableFirstOrNone()
        {
            // Value type
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 3 };
            var maybeInt = enumerableInts.FirstOrNone();
            Assert.IsTrue(maybeInt.HasValue);
            Assert.AreEqual(1, maybeInt.Value);
            maybeInt = enumerableInts.FirstOrNone(value => value == 2);
            Assert.IsTrue(maybeInt.HasValue);
            Assert.AreEqual(2, maybeInt.Value);

            maybeInt = enumerableInts.FirstOrNone(value => value == 4);
            Assert.IsFalse(maybeInt.HasValue);
            
            IEnumerable<int> enumerableInts2 = new List<int>();
            maybeInt = enumerableInts2.FirstOrNone();
            Assert.IsFalse(maybeInt.HasValue);
            maybeInt = enumerableInts2.FirstOrNone(value => value == 2);
            Assert.IsFalse(maybeInt.HasValue);

            // Nullables
            IEnumerable<int?> enumerableNullableInts = new List<int?> { 1, 2, 3 };
            var maybeNullable = enumerableNullableInts.FirstOrNone();
            Assert.IsTrue(maybeNullable.HasValue);
            Assert.AreEqual(1, maybeNullable.Value);
            maybeNullable = enumerableNullableInts.FirstOrNone(value => value.HasValue && value.Value == 2);
            Assert.IsTrue(maybeNullable.HasValue);
            Assert.AreEqual(2, maybeNullable.Value);

            maybeNullable = enumerableNullableInts.FirstOrNone(value => value == 4);
            Assert.IsFalse(maybeNullable.HasValue);

            IEnumerable<int?> enumerableNullableInts2 = new List<int?> { null, null };
            maybeNullable = enumerableNullableInts2.FirstOrNone();
            Assert.IsFalse(maybeNullable.HasValue);
            maybeNullable = enumerableNullableInts2.FirstOrNone(value => value.HasValue && value.Value == 2);
            Assert.IsFalse(maybeNullable.HasValue);

            IEnumerable<int?> enumerableNullableInts3 = new List<int?>();
            maybeNullable = enumerableNullableInts3.FirstOrNone();
            Assert.IsFalse(maybeNullable.HasValue);
            maybeNullable = enumerableNullableInts3.FirstOrNone(value => value.HasValue && value.Value == 2);
            Assert.IsFalse(maybeNullable.HasValue);

            // Reference type
            var testObj1 = new TestClass();
            var testObj2 = new TestClass();
            IEnumerable<TestClass> enumerableTestClass = new List<TestClass> { testObj1, testObj2 };
            var maybeClass = enumerableTestClass.FirstOrNone();
            Assert.IsTrue(maybeClass.HasValue);
            Assert.AreSame(testObj1, maybeClass.Value);
            maybeClass = enumerableTestClass.FirstOrNone(value => ReferenceEquals(testObj2, value));
            Assert.IsTrue(maybeClass.HasValue);
            Assert.AreSame(testObj2, maybeClass.Value);

            var testObj3 = new TestClass();
            maybeClass = enumerableTestClass.FirstOrNone(value => ReferenceEquals(testObj3, value));
            Assert.IsFalse(maybeClass.HasValue);

            IEnumerable<TestClass> enumerableTestClass2 = new List<TestClass> { null, null };
            maybeClass = enumerableTestClass2.FirstOrNone();
            Assert.IsFalse(maybeClass.HasValue);
            maybeClass = enumerableTestClass2.FirstOrNone(value => ReferenceEquals(testObj1, value));
            Assert.IsFalse(maybeClass.HasValue);

            IEnumerable<TestClass> enumerableTestClass3 = new List<TestClass>();
            maybeClass = enumerableTestClass3.FirstOrNone();
            Assert.IsFalse(maybeClass.HasValue);
            maybeClass = enumerableTestClass3.FirstOrNone(value => ReferenceEquals(testObj1, value));
            Assert.IsFalse(maybeClass.HasValue);
        }

        [Test]
        public void EnumerableLastOrNone()
        {
            // Value type
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 3 };
            var maybeInt = enumerableInts.LastOrNone();
            Assert.IsTrue(maybeInt.HasValue);
            Assert.AreEqual(3, maybeInt.Value);
            maybeInt = enumerableInts.LastOrNone(value => value == 1);
            Assert.IsTrue(maybeInt.HasValue);
            Assert.AreEqual(1, maybeInt.Value);

            maybeInt = enumerableInts.LastOrNone(value => value == 4);
            Assert.IsFalse(maybeInt.HasValue);

            IEnumerable<int> enumerableInts2 = new List<int>();
            maybeInt = enumerableInts2.LastOrNone();
            Assert.IsFalse(maybeInt.HasValue);
            maybeInt = enumerableInts2.LastOrNone(value => value == 2);
            Assert.IsFalse(maybeInt.HasValue);

            // Nullables
            IEnumerable<int?> enumerableNullableInts = new List<int?> { 1, 2, 3 };
            var maybeNullable = enumerableNullableInts.LastOrNone();
            Assert.IsTrue(maybeNullable.HasValue);
            Assert.AreEqual(3, maybeNullable.Value);
            maybeNullable = enumerableNullableInts.LastOrNone(value => value.HasValue && value.Value == 1);
            Assert.IsTrue(maybeNullable.HasValue);
            Assert.AreEqual(1, maybeNullable.Value);

            maybeNullable = enumerableNullableInts.LastOrNone(value => value.HasValue && value.Value == 4);
            Assert.IsFalse(maybeNullable.HasValue);

            IEnumerable<int?> enumerableNullableInts2 = new List<int?> { null, null };
            maybeNullable = enumerableNullableInts2.LastOrNone();
            Assert.IsFalse(maybeNullable.HasValue);
            maybeNullable = enumerableNullableInts2.LastOrNone(value => value.HasValue && value.Value == 2);
            Assert.IsFalse(maybeNullable.HasValue);

            IEnumerable<int?> enumerableNullableInts3 = new List<int?>();
            maybeNullable = enumerableNullableInts3.LastOrNone();
            Assert.IsFalse(maybeNullable.HasValue);
            maybeNullable = enumerableNullableInts3.LastOrNone(value => value.HasValue && value.Value == 2);
            Assert.IsFalse(maybeNullable.HasValue);

            // Reference type
            var testObj1 = new TestClass();
            var testObj2 = new TestClass();
            IEnumerable<TestClass> enumerableTestClass = new List<TestClass> { testObj1, testObj2 };
            var maybeClass = enumerableTestClass.LastOrNone();
            Assert.IsTrue(maybeClass.HasValue);
            Assert.AreSame(testObj2, maybeClass.Value);
            maybeClass = enumerableTestClass.LastOrNone(value => ReferenceEquals(testObj1, value));
            Assert.IsTrue(maybeClass.HasValue);
            Assert.AreSame(testObj1, maybeClass.Value);

            var testObj3 = new TestClass();
            maybeClass = enumerableTestClass.LastOrNone(value => ReferenceEquals(testObj3, value));
            Assert.IsFalse(maybeClass.HasValue);

            IEnumerable<TestClass> enumerableTestClass2 = new List<TestClass> { null, null };
            maybeClass = enumerableTestClass2.LastOrNone();
            Assert.IsFalse(maybeClass.HasValue);
            maybeClass = enumerableTestClass2.LastOrNone(value => ReferenceEquals(testObj1, value));
            Assert.IsFalse(maybeClass.HasValue);

            IEnumerable<TestClass> enumerableTestClass3 = new List<TestClass>();
            maybeClass = enumerableTestClass3.LastOrNone();
            Assert.IsFalse(maybeClass.HasValue);
            maybeClass = enumerableTestClass3.LastOrNone(value => ReferenceEquals(testObj1, value));
            Assert.IsFalse(maybeClass.HasValue);
        }
    }
}

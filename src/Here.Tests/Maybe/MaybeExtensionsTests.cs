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
    internal class MaybeExtensionsTests : MaybeTestsBase
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
            CheckMaybeValue(maybeInt, 1);

            maybeInt = 1;
            CheckMaybeValue(maybeInt, 1);

            // Nullable
            var testNullableNull = (int?)null;
            // ReSharper disable once ExpressionIsAlwaysNull
            var maybeNullableNull = testNullableNull.ToMaybe();
            CheckEmptyMaybe(maybeNullableNull);

            TestStruct? testNullableNotNull = new TestStruct();
            var maybeNullableNotNull = testNullableNotNull.ToMaybe();
            CheckMaybeValue(maybeNullableNotNull, testNullableNotNull.Value);

            // Reference type
            var testObject = new TestClass();
            var maybeClass = testObject.ToMaybe();
            CheckMaybeSameValue(maybeClass, testObject);

            var testObject2 = new TestClass();
            maybeClass = testObject2;
            CheckMaybeSameValue(maybeClass, testObject2);

            TestClass testObjectNull = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            maybeClass = testObjectNull.ToMaybe();
            CheckEmptyMaybe(maybeClass);
        }

        [Test]
        public void ReturnToMaybe()
        {
            // Value type
            Maybe<int> maybeInt = GetInt();
            CheckMaybeValue(maybeInt, 12);

            // Nullable
            Maybe<int?> maybeNullableInt = GetNullableInt();
            CheckMaybeValue(maybeNullableInt, 42);

            maybeNullableInt = GetNullNullableInt();
            CheckEmptyMaybe(maybeNullableInt);

            // Reference type
            Maybe<TestClass> maybeClass = GetTestClass();
            CheckMaybeValue(maybeClass, new TestClass());

            maybeClass = GetNullTestClass();
            CheckEmptyMaybe(maybeClass);
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
            // Null or empty
            Maybe<string> maybeString;
            
            maybeString = string.Empty.NoneIfEmpty();
            CheckEmptyMaybe(maybeString);
            
            maybeString = "".NoneIfEmpty();
            CheckEmptyMaybe(maybeString);

            string testNull = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            maybeString = testNull.NoneIfEmpty();
            CheckEmptyMaybe(maybeString);

            maybeString = "  ".NoneIfEmpty();
            CheckMaybeValue(maybeString, "  ");

            maybeString = "test".NoneIfEmpty();
            CheckMaybeValue(maybeString, "test");

            // Null or white space
            maybeString = string.Empty.NoneIfEmptyOrSpace();
            CheckEmptyMaybe(maybeString);

            maybeString = "".NoneIfEmptyOrSpace();
            CheckEmptyMaybe(maybeString);

            // ReSharper disable once ExpressionIsAlwaysNull
            maybeString = testNull.NoneIfEmptyOrSpace();
            CheckEmptyMaybe(maybeString);

            maybeString = "  ".NoneIfEmptyOrSpace();
            CheckEmptyMaybe(maybeString);

            maybeString = "test".NoneIfEmptyOrSpace();
            CheckMaybeValue(maybeString, "test");
        }

        [Test]
        public void MaybeStringToMaybe()
        {
            // Null or empty
            Maybe<string> inputMaybeString;
            Maybe<string> maybeString;

            inputMaybeString = Maybe<string>.Some(string.Empty);
            maybeString = inputMaybeString.NoneIfEmpty();
            CheckEmptyMaybe(maybeString);

            inputMaybeString = Maybe<string>.Some("");
            maybeString = inputMaybeString.NoneIfEmpty();
            CheckEmptyMaybe(maybeString);

            inputMaybeString = Maybe<string>.Some("  ");
            maybeString = inputMaybeString.NoneIfEmpty();
            CheckMaybeValue(maybeString, "  ");

            inputMaybeString = Maybe<string>.Some("test");
            maybeString = inputMaybeString.NoneIfEmpty();
            CheckMaybeValue(maybeString, "test");

            // Null or white space
            inputMaybeString = Maybe<string>.Some(string.Empty);
            maybeString = inputMaybeString.NoneIfEmptyOrSpace();
            CheckEmptyMaybe(maybeString);

            inputMaybeString = Maybe<string>.Some("");
            maybeString = inputMaybeString.NoneIfEmptyOrSpace();
            CheckEmptyMaybe(maybeString);

            inputMaybeString = Maybe<string>.Some("  ");
            maybeString = inputMaybeString.NoneIfEmptyOrSpace();
            CheckEmptyMaybe(maybeString);

            inputMaybeString = Maybe<string>.Some("test");
            maybeString = inputMaybeString.NoneIfEmptyOrSpace();
            CheckMaybeValue(maybeString, "test");
        }

        [Test]
        public void EnumerableFirstOrNone()
        {
            // Value type
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 3 };
            var maybeInt = enumerableInts.FirstOrNone();
            CheckMaybeValue(maybeInt, 1);
            maybeInt = enumerableInts.FirstOrNone(value => value == 2);
            CheckMaybeValue(maybeInt, 2);

            maybeInt = enumerableInts.FirstOrNone(value => value == 4);
            CheckEmptyMaybe(maybeInt);
            
            IEnumerable<int> enumerableInts2 = new List<int>();
            maybeInt = enumerableInts2.FirstOrNone();
            CheckEmptyMaybe(maybeInt);
            maybeInt = enumerableInts2.FirstOrNone(value => value == 2);
            CheckEmptyMaybe(maybeInt);

            // Nullables
            IEnumerable<int?> enumerableNullableInts = new List<int?> { 1, 2, 3 };
            var maybeNullable = enumerableNullableInts.FirstOrNone();
            CheckMaybeValue(maybeNullable, 1);
            maybeNullable = enumerableNullableInts.FirstOrNone(value => value.HasValue && value.Value == 2);
            CheckMaybeValue(maybeNullable, 2);

            maybeNullable = enumerableNullableInts.FirstOrNone(value => value == 4);
            CheckEmptyMaybe(maybeNullable);

            IEnumerable<int?> enumerableNullableInts2 = new List<int?> { null, null };
            maybeNullable = enumerableNullableInts2.FirstOrNone();
            CheckEmptyMaybe(maybeNullable);
            maybeNullable = enumerableNullableInts2.FirstOrNone(value => value.HasValue && value.Value == 2);
            CheckEmptyMaybe(maybeNullable);

            IEnumerable<int?> enumerableNullableInts3 = new List<int?>();
            maybeNullable = enumerableNullableInts3.FirstOrNone();
            CheckEmptyMaybe(maybeNullable);
            maybeNullable = enumerableNullableInts3.FirstOrNone(value => value.HasValue && value.Value == 2);
            CheckEmptyMaybe(maybeNullable);

            // Reference type
            var testObj1 = new TestClass();
            var testObj2 = new TestClass();
            IEnumerable<TestClass> enumerableTestClass = new List<TestClass> { testObj1, testObj2 };
            var maybeClass = enumerableTestClass.FirstOrNone();
            CheckMaybeSameValue(maybeClass, testObj1);
            maybeClass = enumerableTestClass.FirstOrNone(value => ReferenceEquals(testObj2, value));
            CheckMaybeSameValue(maybeClass, testObj2);

            var testObj3 = new TestClass();
            maybeClass = enumerableTestClass.FirstOrNone(value => ReferenceEquals(testObj3, value));
            CheckEmptyMaybe(maybeClass);

            IEnumerable<TestClass> enumerableTestClass2 = new List<TestClass> { null, null };
            maybeClass = enumerableTestClass2.FirstOrNone();
            CheckEmptyMaybe(maybeClass);
            maybeClass = enumerableTestClass2.FirstOrNone(value => ReferenceEquals(testObj1, value));
            CheckEmptyMaybe(maybeClass);

            IEnumerable<TestClass> enumerableTestClass3 = new List<TestClass>();
            maybeClass = enumerableTestClass3.FirstOrNone();
            CheckEmptyMaybe(maybeClass);
            maybeClass = enumerableTestClass3.FirstOrNone(value => ReferenceEquals(testObj1, value));
            CheckEmptyMaybe(maybeClass);
        }

        [Test]
        public void EnumerableLastOrNone()
        {
            // Value type
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 3 };
            var maybeInt = enumerableInts.LastOrNone();
            CheckMaybeValue(maybeInt, 3);
            maybeInt = enumerableInts.LastOrNone(value => value == 1);
            CheckMaybeValue(maybeInt, 1);

            maybeInt = enumerableInts.LastOrNone(value => value == 4);
            CheckEmptyMaybe(maybeInt);

            IEnumerable<int> enumerableInts2 = new List<int>();
            maybeInt = enumerableInts2.LastOrNone();
            CheckEmptyMaybe(maybeInt);
            maybeInt = enumerableInts2.LastOrNone(value => value == 2);
            CheckEmptyMaybe(maybeInt);

            // Nullables
            IEnumerable<int?> enumerableNullableInts = new List<int?> { 1, 2, 3 };
            var maybeNullable = enumerableNullableInts.LastOrNone();
            CheckMaybeValue(maybeNullable, 3);
            maybeNullable = enumerableNullableInts.LastOrNone(value => value.HasValue && value.Value == 1);
            CheckMaybeValue(maybeNullable, 1);

            maybeNullable = enumerableNullableInts.LastOrNone(value => value.HasValue && value.Value == 4);
            CheckEmptyMaybe(maybeNullable);

            IEnumerable<int?> enumerableNullableInts2 = new List<int?> { null, null };
            maybeNullable = enumerableNullableInts2.LastOrNone();
            CheckEmptyMaybe(maybeNullable);
            maybeNullable = enumerableNullableInts2.LastOrNone(value => value.HasValue && value.Value == 2);
            CheckEmptyMaybe(maybeNullable);

            IEnumerable<int?> enumerableNullableInts3 = new List<int?>();
            maybeNullable = enumerableNullableInts3.LastOrNone();
            CheckEmptyMaybe(maybeNullable);
            maybeNullable = enumerableNullableInts3.LastOrNone(value => value.HasValue && value.Value == 2);
            CheckEmptyMaybe(maybeNullable);

            // Reference type
            var testObj1 = new TestClass();
            var testObj2 = new TestClass();
            IEnumerable<TestClass> enumerableTestClass = new List<TestClass> { testObj1, testObj2 };
            var maybeClass = enumerableTestClass.LastOrNone();
            CheckMaybeSameValue(maybeClass, testObj2);
            maybeClass = enumerableTestClass.LastOrNone(value => ReferenceEquals(testObj1, value));
            CheckMaybeSameValue(maybeClass, testObj1);

            var testObj3 = new TestClass();
            maybeClass = enumerableTestClass.LastOrNone(value => ReferenceEquals(testObj3, value));
            CheckEmptyMaybe(maybeClass);

            IEnumerable<TestClass> enumerableTestClass2 = new List<TestClass> { null, null };
            maybeClass = enumerableTestClass2.LastOrNone();
            CheckEmptyMaybe(maybeClass);
            maybeClass = enumerableTestClass2.LastOrNone(value => ReferenceEquals(testObj1, value));
            CheckEmptyMaybe(maybeClass);

            IEnumerable<TestClass> enumerableTestClass3 = new List<TestClass>();
            maybeClass = enumerableTestClass3.LastOrNone();
            CheckEmptyMaybe(maybeClass);
            maybeClass = enumerableTestClass3.LastOrNone(value => ReferenceEquals(testObj1, value));
            CheckEmptyMaybe(maybeClass);
        }
    }
}

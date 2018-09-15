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

#if (!NET20 && !NET30 && !NET35)
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
#endif
        }

        [Test]
        public void MaybeStringToMaybe()
        {
            // Null or empty
            Maybe<string> inputMaybeString;
            Maybe<string> maybeString;

            inputMaybeString = Maybe.None;
            maybeString = inputMaybeString.NoneIfEmpty();
            CheckEmptyMaybe(maybeString);

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

#if (!NET20 && !NET30 && !NET35)
            // Null or white space
            inputMaybeString = Maybe.None;
            maybeString = inputMaybeString.NoneIfEmptyOrSpace();
            CheckEmptyMaybe(maybeString);

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
#endif
        }
    }
}

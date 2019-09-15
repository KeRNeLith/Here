using NUnit.Framework;
using Here.Extensions;
using static Here.Tests.Options.OptionTestHelpers;

namespace Here.Tests.Options
{
    /// <summary>
    /// Tests for <see cref="Option{T}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class OptionExtensionsTests : HereTestsBase
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
        public void ToOption()
        {
            // Value type
            var optionInt = 1.ToOption();
            CheckOptionValue(optionInt, 1);

            optionInt = 1;
            CheckOptionValue(optionInt, 1);

            // Nullable
            var testNullableNull = (int?)null;
            // ReSharper disable once ExpressionIsAlwaysNull
            var optionNullableNull = testNullableNull.ToOption();
            CheckEmptyOption(optionNullableNull);

            TestStruct? testNullableNotNull = new TestStruct();
            var optionNullableNotNull = testNullableNotNull.ToOption();
            CheckOptionValue(optionNullableNotNull, testNullableNotNull.Value);

            // Reference type
            var testObject = new TestClass();
            var optionClass = testObject.ToOption();
            CheckOptionSameValue(optionClass, testObject);

            var testObject2 = new TestClass();
            optionClass = testObject2;
            CheckOptionSameValue(optionClass, testObject2);

            TestClass testObjectNull = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            optionClass = testObjectNull.ToOption();
            CheckEmptyOption(optionClass);
        }

        [Test]
        public void ReturnToOption()
        {
            // Value type
            Option<int> optionInt = GetInt();
            CheckOptionValue(optionInt, 12);

            // Nullable
            Option<int?> optionNullableInt = GetNullableInt();
            CheckOptionValue(optionNullableInt, 42);

            optionNullableInt = GetNullNullableInt();
            CheckEmptyOption(optionNullableInt);

            // Reference type
            Option<TestClass> optionClass = GetTestClass();
            CheckOptionValue(optionClass, new TestClass());

            optionClass = GetNullTestClass();
            CheckEmptyOption(optionClass);
        }

        [Test]
        public void OptionToNullable()
        {
            // Value type
            var optionInt = Option<int>.Some(12);
            var intNullable = optionInt.ToNullable();
            Assert.IsTrue(intNullable.HasValue);
            Assert.AreEqual(12, intNullable.Value);

            optionInt = Option.None;
            intNullable = optionInt.ToNullable();
            Assert.IsFalse(intNullable.HasValue);
        }

        [Test]
        public void StringToOption()
        {
            // Null or empty
            Option<string> optionString = string.Empty.NoneIfEmpty();
            CheckEmptyOption(optionString);
            
            optionString = "".NoneIfEmpty();
            CheckEmptyOption(optionString);

            string testNull = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            optionString = testNull.NoneIfEmpty();
            CheckEmptyOption(optionString);

            optionString = "  ".NoneIfEmpty();
            CheckOptionValue(optionString, "  ");

            optionString = "test".NoneIfEmpty();
            CheckOptionValue(optionString, "test");

            // Null or white space
            optionString = string.Empty.NoneIfEmptyOrSpace();
            CheckEmptyOption(optionString);

            optionString = "".NoneIfEmptyOrSpace();
            CheckEmptyOption(optionString);

            // ReSharper disable once ExpressionIsAlwaysNull
            optionString = testNull.NoneIfEmptyOrSpace();
            CheckEmptyOption(optionString);

            optionString = "  ".NoneIfEmptyOrSpace();
            CheckEmptyOption(optionString);

            optionString = "test".NoneIfEmptyOrSpace();
            CheckOptionValue(optionString, "test");
        }

        [Test]
        public void OptionStringToOption()
        {
            // Null or empty
            Option<string> inputOptionString = Option.None;
            Option<string> optionString = inputOptionString.NoneIfEmpty();
            CheckEmptyOption(optionString);

            inputOptionString = Option<string>.Some(string.Empty);
            optionString = inputOptionString.NoneIfEmpty();
            CheckEmptyOption(optionString);

            inputOptionString = Option<string>.Some("");
            optionString = inputOptionString.NoneIfEmpty();
            CheckEmptyOption(optionString);

            inputOptionString = Option<string>.Some("  ");
            optionString = inputOptionString.NoneIfEmpty();
            CheckOptionValue(optionString, "  ");

            inputOptionString = Option<string>.Some("test");
            optionString = inputOptionString.NoneIfEmpty();
            CheckOptionValue(optionString, "test");

            // Null or white space
            inputOptionString = Option.None;
            optionString = inputOptionString.NoneIfEmptyOrSpace();
            CheckEmptyOption(optionString);

            inputOptionString = Option<string>.Some(string.Empty);
            optionString = inputOptionString.NoneIfEmptyOrSpace();
            CheckEmptyOption(optionString);

            inputOptionString = Option<string>.Some("");
            optionString = inputOptionString.NoneIfEmptyOrSpace();
            CheckEmptyOption(optionString);

            inputOptionString = Option<string>.Some("  ");
            optionString = inputOptionString.NoneIfEmptyOrSpace();
            CheckEmptyOption(optionString);

            inputOptionString = Option<string>.Some("test");
            optionString = inputOptionString.NoneIfEmptyOrSpace();
            CheckOptionValue(optionString, "test");
        }
    }
}

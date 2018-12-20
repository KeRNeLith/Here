using System;
using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Options
{
    /// <summary>
    /// Tests for <see cref="Option{T}"/> Linq implementation.
    /// </summary>
    [TestFixture]
    internal class OptionLinqExtensionsTests : OptionTestsBase
    {
        [Test]
        public void OptionAny()
        {
            // Option with value
            var optionInt = Option<int>.Some(12);
            Assert.IsTrue(optionInt.Any());
            Assert.IsTrue(optionInt.Any(intValue => intValue == 12));
            Assert.IsFalse(optionInt.Any(intValue => intValue == 13));

            // Empty option
            Option<int> emptyOptionInt = Option.None;
            Assert.IsFalse(emptyOptionInt.Any());
            Assert.IsFalse(emptyOptionInt.Any(intValue => intValue == 12));

            Assert.Throws<ArgumentNullException>(() => optionInt.Any(null));
        }

        [Test]
        public void OptionAll()
        {
            // Option with value
            var optionInt = Option<int>.Some(12);
            Assert.IsTrue(optionInt.All(intValue => intValue == 12));
            Assert.IsFalse(optionInt.All(intValue => intValue == 13));

            // Empty option
            Option<int> emptyOptionInt = Option.None;
            Assert.IsFalse(emptyOptionInt.All(intValue => intValue == 12));

            Assert.Throws<ArgumentNullException>(() => optionInt.All(null));
        }

        [Test]
        public void OptionContains()
        {
            // Value type
            // Option with value
            var optionInt = Option<int>.Some(12);
            Assert.IsTrue(optionInt.Contains(12));

            // Empty option
            Option<int> emptyOptionInt = Option.None;
            Assert.IsFalse(emptyOptionInt.Contains(12));

            // Reference type
            // Option with value
            var testObject = new TestClass { TestInt = 42 };
            var optionClass = Option<TestClass>.Some(testObject);
            Assert.IsTrue(optionClass.Contains(testObject));
            Assert.IsTrue(optionClass.Contains(new TestClass { TestInt = 42 }));
            Assert.IsFalse(optionClass.Contains(null));  // Option cannot contains null value

            // Empty option
            Option<TestClass> emptyOptionClass = Option.None;
            Assert.IsFalse(emptyOptionClass.Contains(testObject));
            Assert.IsFalse(emptyOptionClass.Contains(null));  // Option cannot contains null value
        }

        [Test]
        public void OptionSelect()
        {
            // Value type
            // Option with value
            var optionInt = Option<int>.Some(12);
            var optionIntResult = optionInt.Select(intValue => intValue + 1);
            CheckOptionValue(optionIntResult, 13);

            // Empty option
            Option<int> emptyOptionInt = Option.None;
            optionIntResult = emptyOptionInt.Select(intValue => intValue + 1);
            CheckEmptyOption(optionIntResult);

            // Reference type
            // Option with value
            var testObject = new TestClass { TestInt = 42 };
            var optionClass = Option<TestClass>.Some(testObject);
            optionIntResult = optionClass.Select(obj => obj.TestInt);
            CheckOptionValue(optionIntResult, 42);

            // Empty option
            Option<TestClass> emptyOptionClass = Option.None;
            optionIntResult = emptyOptionClass.Select(obj => obj.TestInt);
            CheckEmptyOption(optionIntResult);

            Assert.Throws<ArgumentNullException>(() => optionInt.Select((Func<int, int>)null));
        }

        [Test]
        public void OptionWhere()
        {
            // Option with value
            var optionInt = Option<int>.Some(12);
            var optionIntResult = optionInt.Where(intValue => intValue == 12);
            CheckOptionValue(optionIntResult, 12);

            optionIntResult = optionInt.Where(intValue => intValue == 13);
            CheckEmptyOption(optionIntResult);

            // Empty option
            Option<int> emptyOptionInt = Option.None;
            optionIntResult = emptyOptionInt.Where(intValue => intValue == 1);
            CheckEmptyOption(optionIntResult);

            Assert.Throws<ArgumentNullException>(() => optionInt.Where(null));
        }

        [Test]
        public void OptionOfType()
        {
            // Cast succeed
            var testObjectLeaf = new TestClassLeaf();
            var optionTestClass = Option<TestClass>.Some(testObjectLeaf);
            var optionTestClassLeaf = optionTestClass.OfType<TestClassLeaf>();
            CheckOptionSameValue(optionTestClassLeaf, testObjectLeaf);

            // Cast failed
            // From Value type
            var optionInt = Option<int>.Some(12);
            optionTestClass = optionInt.OfType<TestClass>();
            CheckEmptyOption(optionTestClass);

            // From Reference type
            var testObject = new TestClass();
            optionTestClass = Option<TestClass>.Some(testObject);
            optionTestClassLeaf = optionTestClass.OfType<TestClassLeaf>();
            CheckEmptyOption(optionTestClassLeaf);
        }

        [Test]
        public void OptionForEach()
        {
            // Option with value
            int optionValue = -1;
            var optionInt = Option<int>.Some(12);
            optionInt.ForEach(intValue => optionValue = intValue);
            Assert.AreEqual(12, optionValue);

            // Empty option
            optionValue = -1;
            Option<int> emptyOptionInt = Option.None;
            emptyOptionInt.ForEach(intValue => optionValue = intValue);
            Assert.AreEqual(-1, optionValue);

            Assert.Throws<ArgumentNullException>(() => optionInt.ForEach(null));
        }

        [Test]
        public void OptionAggregate()
        {
            // Option with value
            var optionInt = Option<int>.Some(12);
            Assert.AreEqual(15, optionInt.Aggregate(3, (intValue, initValue) => intValue + initValue));

            // Empty option
            Option<int> emptyOptionInt = Option.None;
            Assert.AreEqual(3, emptyOptionInt.Aggregate(3, (intValue, initValue) => intValue + initValue));

            Assert.Throws<ArgumentNullException>(() => optionInt.Aggregate(1, null));
            Assert.Throws<ArgumentNullException>(() => optionInt.Aggregate((Person)null, (person, int32) => null));
            Assert.Throws<ArgumentNullException>(() => optionInt.Aggregate((Person)null, null));
        }
    }
}

using NUnit.Framework;

namespace Here.Tests.Options
{
    /// <summary>
    /// Tests for <see cref="Option{T}"/> operators.
    /// </summary>
    [TestFixture]
    internal class OptionOperatorsTests : OptionTestsBase
    {
        [Test]
        public void OptionBoolOpertors()
        {
            // Option value type
            var optionInt = Option<int>.Some(12);
            if (optionInt)
            {
                // Does not work because it expects a bool?
                //Assert.IsTrue(optionInt);
            }
            else
                Assert.Fail("OptionInt should be true.");

            if (!optionInt)
                Assert.Fail("!OptionInt should not be true.");

            var emptyOptionInt = Option<int>.None;
            if (emptyOptionInt)
                Assert.Fail("EmptyOptionInt should not be true.");

            if (!emptyOptionInt)
            {
            }
            else
                Assert.Fail("!EmptyOptionInt should not be false.");

            // Option reference type
            var optionClass = Option<TestClass>.Some(new TestClass { TestInt = 42 });
            if (optionClass)
            {
                // Does not work because it expects a bool?
                //Assert.IsTrue(optionClass);
            }
            else
                Assert.Fail("OptionClass should be true.");

            if (!optionClass)
                Assert.Fail("!OptionClass should not be true.");

            var emptyOptionClass = Option<TestClass>.None;
            if (emptyOptionClass)
                Assert.Fail("EmptyOptionClass should not be true.");

            if (!emptyOptionClass)
            {
            }
            else
                Assert.Fail("!EmptyOptionClass should not be false.");
        }

        [Test]
        public void OptionLogicalOrOperator()
        {
            // Empty options
            var first = Option<int>.None;
            var second = Option<int>.None;

            if (first || second)
                Assert.Fail("|| performed on 2 None options should be false.");

            if (second || first)
                Assert.Fail("|| performed on 2 None options should be false.");

            // One empty option and one with value
            first = Option<int>.None;
            second = Option<int>.Some(42);

            if (first || second)
            {
            }
            else
                Assert.Fail("|| performed on a None and not None options should be true.");

            if (second || first)
            {
            }
            else
                Assert.Fail("|| performed on a None and not None options should be true.");

            // Both options have values
            first = Option<int>.Some(12);
            second = Option<int>.Some(42);

            if (first || second)
            {
            }
            else
                Assert.Fail("|| performed on 2 not None options should be true.");

            if (second || first)
            {
            }
            else
                Assert.Fail("|| performed on 2 not None options should be true.");
        }

        [Test]
        public void OptionLogicalAndOperator()
        {
            // Empty options
            var first = Option<int>.None;
            var second = Option<int>.None;

            if (first && second)
                Assert.Fail("&& performed on 2 None options should be false.");

            if (second && first)
                Assert.Fail("&& performed on 2 None options should be false.");

            // One empty option and one with value
            first = Option<int>.None;
            second = Option<int>.Some(42);

            if (first && second)
                Assert.Fail("&& performed on a None and a not None options should be false.");

            if (second && first)
                Assert.Fail("&& performed on a None and a not None options should be false.");

            // Both options have values
            first = Option<int>.Some(12);
            second = Option<int>.Some(42);

            if (first && second)
            {
            }
            else
                Assert.Fail("&& performed on 2 not None options should be true.");

            if (second && first)
            {
            }
            else
                Assert.Fail("&& performed on 2 not None options should be true.");
        }
    }
}
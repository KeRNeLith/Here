using NUnit.Framework;
using Here.Maybes;

namespace Here.Tests.Maybes
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/> operators.
    /// </summary>
    [TestFixture]
    internal class MaybeOperatorsTests : MaybeTestsBase
    {
        [Test]
        public void MaybeBoolOpertors()
        {
            // Maybe value type
            var maybeInt = Maybe<int>.Some(12);
            if (maybeInt)
            {
                // Does not work because it expects a bool?
                //Assert.IsTrue(maybeInt);
            }
            else
                Assert.Fail("MaybeInt should be true.");

            if (!maybeInt)
                Assert.Fail("!MaybeInt should not be true.");

            var emptyMaybeInt = Maybe<int>.None;
            if (emptyMaybeInt)
                Assert.Fail("EmptyMaybeInt should not be true.");

            if (!emptyMaybeInt)
            {
            }
            else
                Assert.Fail("!EmptyMaybeInt should not be false.");

            // Maybe reference type
            var maybeClass = Maybe<TestClass>.Some(new TestClass { TestInt = 42 });
            if (maybeClass)
            {
                // Does not work because it expects a bool?
                //Assert.IsTrue(maybeClass);
            }
            else
                Assert.Fail("MaybeClass should be true.");

            if (!maybeClass)
                Assert.Fail("!MaybeClass should not be true.");

            var emptyMaybeClass = Maybe<TestClass>.None;
            if (emptyMaybeClass)
                Assert.Fail("EmptyMaybeClass should not be true.");

            if (!emptyMaybeClass)
            {
            }
            else
                Assert.Fail("!EmptyMaybeClass should not be false.");
        }

        [Test]
        public void MaybeLogicalOrOperator()
        {
            // Empty maybes
            var first = Maybe<int>.None;
            var second = Maybe<int>.None;

            if (first || second)
                Assert.Fail("|| performed on 2 None maybes should be false.");

            if (second || first)
                Assert.Fail("|| performed on 2 None maybes should be false.");

            // One empty maybe and one with value
            first = Maybe<int>.None;
            second = Maybe<int>.Some(42);

            if (first || second)
            {
            }
            else
                Assert.Fail("|| performed on a None and not None maybes should be true.");

            if (second || first)
            {
            }
            else
                Assert.Fail("|| performed on a None and not None maybes should be true.");

            // Both maybes have values
            first = Maybe<int>.Some(12);
            second = Maybe<int>.Some(42);

            if (first || second)
            {
            }
            else
                Assert.Fail("|| performed on 2 not None maybes should be true.");

            if (second || first)
            {
            }
            else
                Assert.Fail("|| performed on 2 not None maybes should be true.");
        }

        [Test]
        public void MaybeLogicalAndOperator()
        {
            // Empty maybes
            var first = Maybe<int>.None;
            var second = Maybe<int>.None;

            if (first && second)
                Assert.Fail("&& performed on 2 None maybes should be false.");

            if (second && first)
                Assert.Fail("&& performed on 2 None maybes should be false.");

            // One empty maybe and one with value
            first = Maybe<int>.None;
            second = Maybe<int>.Some(42);

            if (first && second)
                Assert.Fail("&& performed on a None and a not None maybes should be false.");

            if (second && first)
                Assert.Fail("&& performed on a None and a not None maybes should be false.");

            // Both maybes have values
            first = Maybe<int>.Some(12);
            second = Maybe<int>.Some(42);

            if (first && second)
            {
            }
            else
                Assert.Fail("&& performed on 2 not None maybes should be true.");

            if (second && first)
            {
            }
            else
                Assert.Fail("&& performed on 2 not None maybes should be true.");
        }
    }
}
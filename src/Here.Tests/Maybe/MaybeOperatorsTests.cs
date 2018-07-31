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
        public void MaybeBitwiseOrOperator()
        {
            // Empty maybes
            var first = Maybe<int>.None;
            var second = Maybe<int>.None;

            var bitwiseOrResult = first | second;   // Should be None (Both none)
            Assert.AreEqual(Maybe<int>.None, bitwiseOrResult);
            CheckEmptyMaybe(bitwiseOrResult);

            bitwiseOrResult = second | first;       // Should be None (Both none)
            Assert.AreEqual(Maybe<int>.None, bitwiseOrResult);
            CheckEmptyMaybe(bitwiseOrResult);

            // One empty maybe and one with value
            first = Maybe<int>.None;
            second = Maybe<int>.Some(42);

            bitwiseOrResult = first | second;       // Second should always be selected (Has value)
            Assert.AreEqual(second, bitwiseOrResult);
            CheckMaybeValue(bitwiseOrResult, 42);

            bitwiseOrResult = second | first;       // Second should always be selected (Has value)
            Assert.AreEqual(second, bitwiseOrResult);
            CheckMaybeValue(bitwiseOrResult, 42);

            // Both maybes have values
            first = Maybe<int>.Some(12);
            second = Maybe<int>.Some(42);

            bitwiseOrResult = first | second;       // The first operand should always be selected (Both has value)
            Assert.AreEqual(first, bitwiseOrResult);
            CheckMaybeValue(bitwiseOrResult, 12);

            bitwiseOrResult = second | first;       // The first operand should always be selected (Both has value)
            Assert.AreEqual(second, bitwiseOrResult);
            CheckMaybeValue(bitwiseOrResult, 42);
        }

        [Test]
        public void MaybeLogicalOrOperator()
        {
            // Empty maybes
            var first = Maybe<int>.None;
            var second = Maybe<int>.None;

            var logicalOrResult = first || second;  // Should be None (Both none)
            Assert.AreEqual(Maybe<int>.None, logicalOrResult);
            CheckEmptyMaybe(logicalOrResult);

            logicalOrResult = second || first;      // Should be None (Both none)
            Assert.AreEqual(Maybe<int>.None, logicalOrResult);
            CheckEmptyMaybe(logicalOrResult);

            // One empty maybe and one with value
            first = Maybe<int>.None;
            second = Maybe<int>.Some(42);

            logicalOrResult = first || second;       // Second should always be selected (Has value)
            CheckMaybeValue(logicalOrResult, 42);

            logicalOrResult = second || first;       // Second should always be selected (Has value)
            CheckMaybeValue(logicalOrResult, 42);

            // Both maybes have values
            first = Maybe<int>.Some(12);
            second = Maybe<int>.Some(42);

            logicalOrResult = first || second;       // The first operand should always be selected (Both has value)
            Assert.AreEqual(first, logicalOrResult);
            CheckMaybeValue(logicalOrResult, 12);

            logicalOrResult = second || first;       // The first operand should always be selected (Both has value)
            Assert.AreEqual(second, logicalOrResult);
            CheckMaybeValue(logicalOrResult, 42);
        }

        [Test]
        public void MaybeBitwiseAndOperator()
        {
            // Empty maybes
            var first = Maybe<int>.None;
            var second = Maybe<int>.None;

            var bitwiseAndResult = first & second;   // Should be None (Both none)
            Assert.AreEqual(Maybe<int>.None, bitwiseAndResult);
            CheckEmptyMaybe(bitwiseAndResult);

            bitwiseAndResult = second & first;      // Should be None (Both none)
            Assert.AreEqual(Maybe<int>.None, bitwiseAndResult);
            CheckEmptyMaybe(bitwiseAndResult);

            // One empty maybe and one with value
            first = Maybe<int>.None;
            second = Maybe<int>.Some(42);

            bitwiseAndResult = first & second;       // First should always be selected (Second hasn't a value so it's none)
            Assert.AreEqual(first, bitwiseAndResult);
            CheckEmptyMaybe(bitwiseAndResult);

            bitwiseAndResult = second & first;       // First should always be selected (Second hasn't a value so it's none)
            Assert.AreEqual(first, bitwiseAndResult);
            CheckEmptyMaybe(bitwiseAndResult);

            // Both maybes have values
            first = Maybe<int>.Some(12);
            second = Maybe<int>.Some(42);

            bitwiseAndResult = first & second;       // The second operand should always be selected
            Assert.AreEqual(second, bitwiseAndResult);
            CheckMaybeValue(bitwiseAndResult, 42);

            bitwiseAndResult = second & first;       // The second operand should always be selected
            Assert.AreEqual(first, bitwiseAndResult);
            CheckMaybeValue(bitwiseAndResult, 12);
        }

        [Test]
        public void MaybeLogicalAndOperator()
        {
            // Empty maybes
            var first = Maybe<int>.None;
            var second = Maybe<int>.None;

            var logicalAndResult = first && second;   // Should be None (Both none)
            Assert.AreEqual(first, logicalAndResult);
            CheckEmptyMaybe(logicalAndResult);

            logicalAndResult = second && first;      // Should be None (Both none)
            Assert.AreEqual(second, logicalAndResult);
            CheckEmptyMaybe(logicalAndResult);

            // One empty maybe and one with value
            first = Maybe<int>.None;
            second = Maybe<int>.Some(42);

            logicalAndResult = first && second;       // First should always be selected (Second hasn't a value so it's none)
            Assert.AreEqual(first, logicalAndResult);
            CheckEmptyMaybe(logicalAndResult);

            logicalAndResult = second && first;       // First should always be selected (Second hasn't a value so it's none)
            Assert.AreEqual(first, logicalAndResult);
            CheckEmptyMaybe(logicalAndResult);

            // Both maybes have values
            first = Maybe<int>.Some(12);
            second = Maybe<int>.Some(42);

            logicalAndResult = first && second;       // The second operand should always be selected
            Assert.AreEqual(second, logicalAndResult);
            CheckMaybeValue(logicalAndResult, 42);

            logicalAndResult = second && first;       // The second operand should always be selected
            Assert.AreEqual(first, logicalAndResult);
            CheckMaybeValue(logicalAndResult, 12);
        }
    }
}
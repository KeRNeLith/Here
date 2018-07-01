using NUnit.Framework;

namespace Here.Maybes.Tests
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/>.
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
            Assert.IsFalse(bitwiseOrResult.HasValue);

            bitwiseOrResult = second | first;       // Should be None (Both none)
            Assert.AreEqual(Maybe<int>.None, bitwiseOrResult);
            Assert.IsFalse(bitwiseOrResult.HasValue);

            // One empty maybe and one with value
            first = Maybe<int>.None;
            second = Maybe<int>.Some(42);

            bitwiseOrResult = first | second;       // Second should always be selected (Has value)
            Assert.AreEqual(second, bitwiseOrResult);
            Assert.IsTrue(bitwiseOrResult.HasValue);

            bitwiseOrResult = second | first;       // Second should always be selected (Has value)
            Assert.AreEqual(second, bitwiseOrResult);
            Assert.IsTrue(bitwiseOrResult.HasValue);

            // Both maybes have values
            first = Maybe<int>.Some(12);
            second = Maybe<int>.Some(42);

            bitwiseOrResult = first | second;       // The first operand should always be selected (Both has value)
            Assert.AreEqual(first, bitwiseOrResult);
            Assert.IsTrue(bitwiseOrResult.HasValue);
            Assert.AreEqual(12, bitwiseOrResult.Value);

            bitwiseOrResult = second | first;       // The first operand should always be selected (Both has value)
            Assert.AreEqual(second, bitwiseOrResult);
            Assert.IsTrue(bitwiseOrResult.HasValue);
            Assert.AreEqual(42, bitwiseOrResult.Value);
        }

        [Test]
        public void MaybeLogicalOrOperator()
        {
            // Empty maybes
            var first = Maybe<int>.None;
            var second = Maybe<int>.None;

            var logicalOrResult = first || second;  // Should be None (Both none)
            Assert.AreEqual(Maybe<int>.None, logicalOrResult);
            Assert.IsFalse(logicalOrResult.HasValue);

            logicalOrResult = second || first;      // Should be None (Both none)
            Assert.AreEqual(Maybe<int>.None, logicalOrResult);
            Assert.IsFalse(logicalOrResult.HasValue);

            // One empty maybe and one with value
            first = Maybe<int>.None;
            second = Maybe<int>.Some(42);

            logicalOrResult = first || second;       // Second should always be selected (Has value)
            Assert.AreEqual(second, logicalOrResult);
            Assert.IsTrue(logicalOrResult.HasValue);

            logicalOrResult = second || first;       // Second should always be selected (Has value)
            Assert.AreEqual(second, logicalOrResult);
            Assert.IsTrue(logicalOrResult.HasValue);

            // Both maybes have values
            first = Maybe<int>.Some(12);
            second = Maybe<int>.Some(42);

            logicalOrResult = first || second;       // The first operand should always be selected (Both has value)
            Assert.AreEqual(first, logicalOrResult);
            Assert.IsTrue(logicalOrResult.HasValue);
            Assert.AreEqual(12, logicalOrResult.Value);

            logicalOrResult = second || first;       // The first operand should always be selected (Both has value)
            Assert.AreEqual(second, logicalOrResult);
            Assert.IsTrue(logicalOrResult.HasValue);
            Assert.AreEqual(42, logicalOrResult.Value);
        }

        [Test]
        public void MaybeBitwiseAndOperator()
        {
            // Empty maybes
            var first = Maybe<int>.None;
            var second = Maybe<int>.None;

            var bitwiseAndResult = first & second;   // Should be None (Both none)
            Assert.AreEqual(Maybe<int>.None, bitwiseAndResult);
            Assert.IsFalse(bitwiseAndResult.HasValue);

            bitwiseAndResult = second & first;      // Should be None (Both none)
            Assert.AreEqual(Maybe<int>.None, bitwiseAndResult);
            Assert.IsFalse(bitwiseAndResult.HasValue);

            // One empty maybe and one with value
            first = Maybe<int>.None;
            second = Maybe<int>.Some(42);

            bitwiseAndResult = first & second;       // First should always be selected (Second hasn't a value so it's none)
            Assert.AreEqual(first, bitwiseAndResult);
            Assert.IsFalse(bitwiseAndResult.HasValue);

            bitwiseAndResult = second & first;       // First should always be selected (Second hasn't a value so it's none)
            Assert.AreEqual(first, bitwiseAndResult);
            Assert.IsFalse(bitwiseAndResult.HasValue);

            // Both maybes have values
            first = Maybe<int>.Some(12);
            second = Maybe<int>.Some(42);

            bitwiseAndResult = first & second;       // The second operand should always be selected
            Assert.AreEqual(second, bitwiseAndResult);
            Assert.IsTrue(bitwiseAndResult.HasValue);
            Assert.AreEqual(42, bitwiseAndResult.Value);

            bitwiseAndResult = second & first;       // The second operand should always be selected
            Assert.AreEqual(first, bitwiseAndResult);
            Assert.IsTrue(bitwiseAndResult.HasValue);
            Assert.AreEqual(12, bitwiseAndResult.Value);
        }

        [Test]
        public void MaybeLogicalAndOperator()
        {
            // Empty maybes
            var first = Maybe<int>.None;
            var second = Maybe<int>.None;

            var logicalAndResult = first && second;   // Should be None (Both none)
            Assert.AreEqual(first, logicalAndResult);
            Assert.IsFalse(logicalAndResult.HasValue);

            logicalAndResult = second && first;      // Should be None (Both none)
            Assert.AreEqual(second, logicalAndResult);
            Assert.IsFalse(logicalAndResult.HasValue);

            // One empty maybe and one with value
            first = Maybe<int>.None;
            second = Maybe<int>.Some(42);

            logicalAndResult = first && second;       // First should always be selected (Second hasn't a value so it's none)
            Assert.AreEqual(first, logicalAndResult);
            Assert.IsFalse(logicalAndResult.HasValue);

            logicalAndResult = second && first;       // First should always be selected (Second hasn't a value so it's none)
            Assert.AreEqual(first, logicalAndResult);
            Assert.IsFalse(logicalAndResult.HasValue);

            // Both maybes have values
            first = Maybe<int>.Some(12);
            second = Maybe<int>.Some(42);

            logicalAndResult = first && second;       // The second operand should always be selected
            Assert.AreEqual(second, logicalAndResult);
            Assert.IsTrue(logicalAndResult.HasValue);
            Assert.AreEqual(42, logicalAndResult.Value);

            logicalAndResult = second && first;       // The second operand should always be selected
            Assert.AreEqual(first, logicalAndResult);
            Assert.IsTrue(logicalAndResult.HasValue);
            Assert.AreEqual(12, logicalAndResult.Value);
        }
    }
}
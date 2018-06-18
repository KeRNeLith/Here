using NUnit.Framework;
using Here.Maybes.Extensions;

namespace Here.Maybes.Tests
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/>.
    /// </summary>
    [TestFixture]
    internal class MaybeLinqExtensionsTests : MaybeTestsBase
    {
        [Test]
        public void MaybeAny()
        {
            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            Assert.IsTrue(maybeInt.Any());
            Assert.IsTrue(maybeInt.Any(intValue => intValue == 12));
            Assert.IsFalse(maybeInt.Any(intValue => intValue == 13));

            // Empty maybe
            Maybe<int> emptyMaybeInt = Maybe.None;
            Assert.IsFalse(emptyMaybeInt.Any());
            Assert.IsFalse(emptyMaybeInt.Any(intValue => intValue == 12));
        }

        [Test]
        public void MaybeAll()
        {
            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            Assert.IsTrue(maybeInt.All(intValue => intValue == 12));
            Assert.IsFalse(maybeInt.All(intValue => intValue == 13));

            // Empty maybe
            Maybe<int> emptyMaybeInt = Maybe.None;
            Assert.IsFalse(emptyMaybeInt.All(intValue => intValue == 12));
        }

        [Test]
        public void MaybeContains()
        {
            // Value type
            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            Assert.IsTrue(maybeInt.Contains(12));

            // Empty maybe
            Maybe<int> emptyMaybeInt = Maybe.None;
            Assert.IsFalse(emptyMaybeInt.Contains(12));

            // Reference type
            // Maybe with value
            var testObject = new TestClass { TestInt = 42 };
            var maybeClass = Maybe<TestClass>.Some(testObject);
            Assert.IsTrue(maybeClass.Contains(testObject));
            Assert.IsTrue(maybeClass.Contains(new TestClass { TestInt = 42 }));

            // Empty maybe
            Maybe<TestClass> emptyMaybeClass = Maybe.None;
            Assert.IsFalse(emptyMaybeClass.Contains(testObject));
        }

        [Test]
        public void MaybeSelect()
        {
            // Value type
            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            var maybeIntResult = maybeInt.Select(intValue => intValue + 1);
            Assert.IsTrue(maybeIntResult.HasValue);
            Assert.AreEqual(13, maybeIntResult.Value);

            // Empty maybe
            Maybe<int> emptyMaybeInt = Maybe.None;
            maybeIntResult = emptyMaybeInt.Select(intValue => intValue + 1);
            Assert.IsFalse(maybeIntResult.HasValue);

            // Reference type
            // Maybe with value
            var testObject = new TestClass { TestInt = 42 };
            var maybeClass = Maybe<TestClass>.Some(testObject);
            maybeIntResult = maybeClass.Select(obj => obj.TestInt);
            Assert.IsTrue(maybeIntResult.HasValue);
            Assert.AreEqual(42, maybeIntResult.Value);

            // Empty maybe
            Maybe<TestClass> emptyMaybeClass = Maybe.None;
            maybeIntResult = emptyMaybeClass.Select(obj => obj.TestInt);
            Assert.IsFalse(maybeIntResult.HasValue);
        }

        [Test]
        public void MaybeWhere()
        {
            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            var maybeIntResult = maybeInt.Where(intValue => intValue == 12);
            Assert.IsTrue(maybeIntResult.HasValue);
            Assert.AreEqual(12, maybeIntResult.Value);

            maybeIntResult = maybeInt.Where(intValue => intValue == 13);
            Assert.IsFalse(maybeIntResult.HasValue);

            // Empty maybe
            Maybe<int> emptyMaybeInt = Maybe.None;
            maybeIntResult = emptyMaybeInt.Where(intValue => intValue == 1);
            Assert.IsFalse(maybeIntResult.HasValue);
        }

        [Test]
        public void MaybeForEach()
        {
            // Maybe with value
            int maybeValue = -1;
            var maybeInt = Maybe<int>.Some(12);
            maybeInt.ForEach(intValue => maybeValue = intValue);
            Assert.AreEqual(12, maybeValue);

            // Empty maybe
            maybeValue = -1;
            Maybe<int> emptyMaybeInt = Maybe.None;
            emptyMaybeInt.ForEach(intValue => maybeValue = intValue);
            Assert.AreEqual(-1, maybeValue);
        }

        [Test]
        public void MaybeAggregate()
        {
            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            Assert.AreEqual(15, maybeInt.Aggregate(3, (intValue, initValue) => intValue + initValue));

            // Empty maybe
            Maybe<int> emptyMaybeInt = Maybe.None;
            Assert.AreEqual(3, emptyMaybeInt.Aggregate(3, (intValue, initValue) => intValue + initValue));
        }
    }
}

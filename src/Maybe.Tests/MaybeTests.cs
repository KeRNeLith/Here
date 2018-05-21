using NUnit.Framework;
using System;

namespace Here.Maybes.Tests
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/>.
    /// </summary>
    [TestFixture]
    internal class MaybeTests
    {
        #region Test class

        private class TestClass
        {
            public int TestInt { get; set; }

            public override string ToString()
            {
                return $"TestClass: {TestInt}";
            }
        }

        #endregion

        [Test]
        public void MaybeConstruction()
        {
            // Maybe value type
            // With value
            var maybeInt = Maybe<int>.Some(12);
            Assert.IsTrue(maybeInt.HasValue);
            Assert.IsFalse(maybeInt.HasNoValue);
            Assert.AreEqual(12, maybeInt.Value);

            // No value
            var emptyMaybeInt = Maybe<int>.None;
            Assert.IsFalse(emptyMaybeInt.HasValue);
            Assert.IsTrue(emptyMaybeInt.HasNoValue);
            Assert.Throws<InvalidOperationException>(() => { var unused = emptyMaybeInt.Value; });

            // Implicit none
            Maybe<int> emptyMaybeInt2 = Maybe.None;
            Assert.IsFalse(emptyMaybeInt2.HasValue);
            Assert.IsTrue(emptyMaybeInt2.HasNoValue);
            Assert.Throws<InvalidOperationException>(() => { var unused = emptyMaybeInt2.Value; });

            // Maybe reference type
            // With value
            var testValue = new TestClass { TestInt = 12 };
            var maybeClass = Maybe<TestClass>.Some(testValue);
            Assert.IsTrue(maybeClass.HasValue);
            Assert.IsFalse(maybeClass.HasNoValue);
            Assert.AreSame(testValue, maybeClass.Value);

            // No value
            var emptyMaybeClass = Maybe<TestClass>.None;
            Assert.IsFalse(emptyMaybeClass.HasValue);
            Assert.IsTrue(emptyMaybeClass.HasNoValue);
            Assert.Throws<InvalidOperationException>(() => { var unused = emptyMaybeClass.Value; });

            // Implicit none
            Maybe<TestClass> emptyMaybeClass2 = Maybe.None;
            Assert.IsFalse(emptyMaybeClass2.HasValue);
            Assert.IsTrue(emptyMaybeClass2.HasNoValue);
            Assert.Throws<InvalidOperationException>(() => { var unused = emptyMaybeClass2.Value; });
        }

        [Test]
        public void FlattenMaybe()
        {
            // Flatten Maybe value type
            // With value
            var embedMaybeInt = Maybe<Maybe<int>>.Some(Maybe<int>.Some(42));
            Assert.IsTrue(embedMaybeInt.HasValue);
            Assert.IsTrue(embedMaybeInt.Value.HasValue);
            Assert.AreEqual(42, embedMaybeInt.Value.Value);

            Maybe<int> maybeInt = embedMaybeInt;
            Assert.AreEqual(42, maybeInt.Value);

            // No value
            var emptyEmbedMaybeInt = Maybe<Maybe<int>>.Some(Maybe.None);
            Assert.IsTrue(emptyEmbedMaybeInt.HasValue);
            Assert.IsFalse(emptyEmbedMaybeInt.Value.HasValue);

            Maybe<int> emptyMaybeInt = emptyEmbedMaybeInt;
            Assert.IsFalse(emptyMaybeInt.HasValue);

            // Flatten Maybe reference type
            // With value
            var testValue = new TestClass { TestInt = 42 };
            var embedMaybeClass = Maybe<Maybe<TestClass>>.Some(Maybe<TestClass>.Some(testValue));
            Assert.IsTrue(embedMaybeClass.HasValue);
            Assert.IsTrue(embedMaybeClass.Value.HasValue);
            Assert.AreSame(testValue, embedMaybeClass.Value.Value);

            Maybe<TestClass> maybeClass = embedMaybeClass;
            Assert.AreSame(testValue, maybeClass.Value);

            // No value
            var emptyEmbedMaybeClass = Maybe<Maybe<TestClass>>.Some(Maybe.None);
            Assert.IsTrue(emptyEmbedMaybeClass.HasValue);
            Assert.IsFalse(emptyEmbedMaybeClass.Value.HasValue);

            Maybe<TestClass> emptyMaybeClass = emptyEmbedMaybeClass;
            Assert.IsFalse(emptyMaybeClass.HasValue);
        }

        [Test]
        public void MaybeEquality()
        {
            // Maybe value type
            var maybeInt = Maybe<int>.Some(12);
            var maybeInt2 = Maybe<int>.Some(12);
            var maybeInt3 = Maybe<int>.Some(42);
            var emptyMaybeInt = Maybe<int>.None;

            Assert.IsTrue(maybeInt.Equals(maybeInt2));
            Assert.IsTrue(maybeInt2.Equals(maybeInt));
            Assert.IsTrue(maybeInt == maybeInt2);

            Assert.IsFalse(maybeInt.Equals(maybeInt3));
            Assert.IsTrue(maybeInt != maybeInt3);
            Assert.IsFalse(maybeInt.Equals(emptyMaybeInt));
            Assert.IsTrue(maybeInt != emptyMaybeInt);

            // Maybe reference type
            var testValue = new TestClass { TestInt = 42 };
            var maybeClass = Maybe<TestClass>.Some(testValue);
            var maybeClass2 = Maybe<TestClass>.Some(testValue);
            var maybeClass3 = Maybe<TestClass>.Some(new TestClass { TestInt = 88 });
            var emptyMaybeClass = Maybe<TestClass>.None;

            Assert.IsTrue(maybeClass.Equals(maybeClass2));
            Assert.IsTrue(maybeClass2.Equals(maybeClass));
            Assert.IsTrue(maybeClass == maybeClass2);

            Assert.IsFalse(maybeClass.Equals(maybeClass3));
            Assert.IsTrue(maybeClass != maybeClass3);
            Assert.IsFalse(maybeClass2.Equals(emptyMaybeClass));
            Assert.IsTrue(maybeClass != emptyMaybeClass);

            // Mixed
            Assert.IsFalse(maybeInt.Equals(maybeClass));
            Assert.IsFalse(maybeClass.Equals(maybeInt));

            // With flatten
            var embedMaybeInt = Maybe<Maybe<int>>.Some(Maybe<int>.Some(12));
            Assert.IsTrue(maybeInt.Equals(embedMaybeInt));
            var embedMaybeInt2 = Maybe<Maybe<int>>.Some(Maybe<int>.Some(42));
            Assert.IsFalse(maybeInt.Equals(embedMaybeInt2));

            var embedMaybeClass = Maybe<Maybe<TestClass>>.Some(Maybe<TestClass>.Some(testValue));
            Assert.IsTrue(maybeClass.Equals(embedMaybeClass));
            var embedMaybeClass2 = Maybe<Maybe<TestClass>>.Some(Maybe<TestClass>.Some(new TestClass { TestInt = 99 }));
            Assert.IsFalse(maybeClass.Equals(embedMaybeClass2));
        }

        [Test]
        public void MaybeToString()
        {
            // Maybe value type
            var maybeInt = Maybe<int>.Some(12);
            Assert.AreEqual("12", maybeInt.ToString());

            var emptyMaybeInt = Maybe<int>.None;
            Assert.AreEqual("None", emptyMaybeInt.ToString());

            // Maybe reference type
            var maybeClass = Maybe<TestClass>.Some(new TestClass { TestInt = 42 });
            Assert.AreEqual("TestClass: 42", maybeClass.ToString());

            var emptyMaybeClass = Maybe<TestClass>.None;
            Assert.AreEqual("None", emptyMaybeClass.ToString());
        }
    }
}
using System;
using NUnit.Framework;
using Here.Maybes;
using Here.Maybes.Extensions;

namespace Here.Tests.Maybes
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/>.
    /// </summary>
    [TestFixture]
    internal class MaybeOperationsTests : HereTestsBase
    {
        [Test]
        public void MaybeIf()
        {
            int counter = 0;
            var maybeInt = Maybe<int>.Some(12);
            maybeInt.If(value => ++counter);
            Assert.AreEqual(1, counter);

            var emptyMaybeInt = Maybe<int>.None;
            emptyMaybeInt.If(value => ++counter);
            Assert.AreEqual(1, counter);
        }

        [Test]
        public void MaybeElse()
        {
            int counter = 0;
            var maybeInt = Maybe<int>.Some(12);
            maybeInt.Else(() => ++counter);
            Assert.AreEqual(0, counter);

            var emptyMaybeInt = Maybe<int>.None;
            emptyMaybeInt.Else(() => ++counter);
            Assert.AreEqual(1, counter);
        }

        [Test]
        public void MaybeIfElse()
        {
            int counterIf = 0;
            int counterElse = 0;
            var maybeInt = Maybe<int>.Some(12);
            maybeInt.IfElse(value => ++counterIf, () => ++counterElse);
            Assert.AreEqual(1, counterIf);
            Assert.AreEqual(0, counterElse);

            var emptyMaybeInt = Maybe<int>.None;
            emptyMaybeInt.IfElse(value => ++counterIf, () => ++counterElse);
            Assert.AreEqual(1, counterIf);
            Assert.AreEqual(1, counterElse);
        }

        [Test]
        public void MaybeOr()
        {
            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            Assert.AreEqual(12, maybeInt.Or(25));
            Assert.AreEqual(12, maybeInt.Or(() => 25));
            Maybe<int> maybeResult = maybeInt.Or(() => 25);
            Assert.IsTrue(maybeResult.HasValue);
            Assert.AreEqual(12, maybeResult.Value);
            maybeResult = maybeInt.Or(() => Maybe<int>.None);
            Assert.IsTrue(maybeResult.HasValue);
            Assert.AreEqual(12, maybeResult.Value);
            maybeResult = maybeInt.Or(() => null);
            Assert.IsTrue(maybeResult.HasValue);
            Assert.AreEqual(12, maybeResult.Value);

            Assert.AreEqual(12, maybeInt.OrDefault());

            // Empty maybe
            Maybe<int> emptyMaybeInt = Maybe.None;
            Assert.AreEqual(42, emptyMaybeInt.Or(42));
            Assert.AreEqual(42, emptyMaybeInt.Or(() => 42));
            maybeResult = emptyMaybeInt.Or(() => 42);
            Assert.IsTrue(maybeResult.HasValue);
            Assert.AreEqual(42, maybeResult.Value);
            maybeResult = emptyMaybeInt.Or(() => Maybe<int>.None);
            Assert.IsFalse(maybeResult.HasValue);
            maybeResult = emptyMaybeInt.Or(() => null);
            Assert.IsFalse(maybeResult.HasValue);

            Assert.AreEqual(0, emptyMaybeInt.OrDefault());

            Maybe<TestClass> maybeResultClass;

            // Maybe class with value
            var testObject = new TestClass();
            var defaultTestObject = new TestClass { TestInt = 12 };
            var maybeClass = Maybe<TestClass>.Some(testObject);
            Assert.AreSame(testObject, maybeClass.Or(defaultTestObject));
            Assert.AreSame(testObject, maybeClass.Or(() => defaultTestObject));
            maybeResultClass = maybeClass.Or(() => defaultTestObject);
            Assert.IsTrue(maybeResultClass.HasValue);
            Assert.AreSame(testObject, maybeResultClass.Value);
            maybeResultClass = maybeClass.Or(() => Maybe<TestClass>.None);
            Assert.IsTrue(maybeResultClass.HasValue);
            Assert.AreSame(testObject, maybeResultClass.Value);
            maybeResultClass = maybeClass.Or(() => null);
            Assert.IsTrue(maybeResultClass.HasValue);
            Assert.AreSame(testObject, maybeResultClass.Value);

            Assert.AreSame(testObject, maybeClass.OrDefault());

            // Empty maybe class
            Maybe<TestClass> emptyMaybeClass = Maybe.None;
            Assert.AreSame(defaultTestObject, emptyMaybeClass.Or(defaultTestObject));
            Assert.AreSame(defaultTestObject, emptyMaybeClass.Or(() => defaultTestObject));
            maybeResultClass = emptyMaybeClass.Or(() => defaultTestObject);
            Assert.IsTrue(maybeResultClass.HasValue);
            Assert.AreSame(defaultTestObject, maybeResultClass.Value);
            maybeResultClass = emptyMaybeClass.Or(() => Maybe<TestClass>.None);
            Assert.IsFalse(maybeResultClass.HasValue);
            maybeResultClass = emptyMaybeClass.Or(() => null);
            Assert.IsFalse(maybeResultClass.HasValue);

            Assert.AreEqual(null, emptyMaybeClass.OrDefault());
        }

        [Test]
        public void MaybeOrThrows()
        {
            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            Assert.DoesNotThrow(() => maybeInt.OrThrows(new InvalidOperationException()));
            Assert.DoesNotThrow(() => maybeInt.OrThrows(() => new InvalidOperationException()));
            Assert.AreEqual(12, maybeInt.OrThrows(new InvalidOperationException()));

            // Empty maybe
            Maybe<int> emptyMaybeInt = Maybe.None;
            Assert.Throws<InvalidOperationException>(() => emptyMaybeInt.OrThrows(new InvalidOperationException()));
            Assert.Throws<InvalidOperationException>(() => emptyMaybeInt.OrThrows(() => new InvalidOperationException()));
        }

        [Test]
        public void MaybeCast()
        {
            // Value type
            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            var maybeFloat = maybeInt.Cast(intVal => intVal + 0.1f);
            Assert.IsTrue(maybeFloat.HasValue);
            Assert.AreEqual(12.1f, maybeFloat.Value);

            // Empty maybe
            Maybe<int> emptyMaybeInt = Maybe.None;
            maybeFloat = emptyMaybeInt.Cast(intVal => intVal + 0.1f);
            Assert.IsFalse(maybeFloat.HasValue);


            // Reference type
            // Maybe with value
            var testObjectLeaf = new TestClassLeaf { TestInt = 12 };
            var maybeClassLeaf = Maybe<TestClassLeaf>.Some(testObjectLeaf);
            maybeFloat = maybeClassLeaf.Cast(obj => obj.TestInt + 0.2f);
            Assert.IsTrue(maybeFloat.HasValue);
            Assert.AreEqual(12.2f, maybeFloat.Value);

            var maybeClass = maybeClassLeaf.Cast<TestClassLeaf, TestClass>();
            Assert.IsTrue(maybeClass.HasValue);
            Assert.AreSame(testObjectLeaf, maybeClass.Value);

            // Invalid conversion
            var testObject = new TestClass { TestInt = 42 };
            maybeClass = Maybe<TestClass>.Some(testObject);
            maybeClassLeaf = maybeClass.Cast<TestClass, TestClassLeaf>();
            Assert.IsFalse(maybeClassLeaf.HasValue);

            // Empty maybe
            Maybe<TestClassLeaf> emptyMaybeClassLeaf = Maybe.None;
            maybeFloat = emptyMaybeClassLeaf.Cast(obj => obj.TestInt + 0.2f);
            Assert.IsFalse(maybeFloat.HasValue);

            maybeClass = emptyMaybeClassLeaf.Cast<TestClassLeaf, TestClass>();
            Assert.IsFalse(maybeClass.HasValue);
        }
    }
}

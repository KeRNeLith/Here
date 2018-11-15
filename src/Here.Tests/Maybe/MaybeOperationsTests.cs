﻿using System;
using NUnit.Framework;
using Here.Maybes;
using Here.Maybes.Extensions;

namespace Here.Tests.Maybes
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/> operations.
    /// </summary>
    [TestFixture]
    internal class MaybeOperationsTests : MaybeTestsBase
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
            maybeInt.IfElse(
                value => 
                {
                    ++counterIf;
                }, 
                () => ++counterElse);
            Assert.AreEqual(1, counterIf);
            Assert.AreEqual(0, counterElse);

            var emptyMaybeInt = Maybe<int>.None;
            emptyMaybeInt.IfElse(
                value =>
                {
                    ++counterIf;
                }, 
                () => ++counterElse);
            Assert.AreEqual(1, counterIf);
            Assert.AreEqual(1, counterElse);
        }

        [Test]
        public void MaybeIfElseReturnResult()
        {
            int counterIf = 0;
            int counterElse = 0;
            var maybeInt = Maybe<int>.Some(12);
            Assert.AreEqual(42.2f, maybeInt.IfElse(
                value =>
                {
                    ++counterIf;
                    return 42.2f;
                }, 
                () =>
                {
                    ++counterElse;
                    return 12.2f;
                }));
            Assert.AreEqual(1, counterIf);
            Assert.AreEqual(0, counterElse);

            var emptyMaybeInt = Maybe<int>.None;
            Assert.AreEqual(12.2f, emptyMaybeInt.IfElse(
                value =>
                {
                    ++counterIf;
                    return 42.2f;
                },
                () =>
                {
                    ++counterElse;
                    return 12.2f;
                }));
            Assert.AreEqual(1, counterIf);
            Assert.AreEqual(1, counterElse);
        }

        [Test]
        public void MaybeIfOrReturnResult()
        {
            int counterIf = 0;
            var maybeInt = Maybe<int>.Some(12);
            Assert.AreEqual(42.2f, maybeInt.IfOr(
                value =>
                {
                    ++counterIf;
                    return 42.2f;
                },
                12.2f));
            Assert.AreEqual(1, counterIf);

            var emptyMaybeInt = Maybe<int>.None;
            Assert.AreEqual(12.2f, emptyMaybeInt.IfOr(
                value =>
                {
                    ++counterIf;
                    return 42.2f;
                },
                12.2f));
            Assert.AreEqual(1, counterIf);
        }

        [Test]
        public void MaybeElseOrReturnResult()
        {
            int counterElse = 0;
            var maybeInt = Maybe<int>.Some(12);
            Assert.AreEqual(42.2f, maybeInt.ElseOr(
                () =>
                {
                    ++counterElse;
                    return 12.2f;
                },
                42.2f));
            Assert.AreEqual(0, counterElse);

            var emptyMaybeInt = Maybe<int>.None;
            Assert.AreEqual(12.2f, emptyMaybeInt.ElseOr(
                () =>
                {
                    ++counterElse;
                    return 12.2f;
                },
                42.2f));
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
            CheckMaybeValue(maybeResult, 12);

            maybeResult = maybeInt.Or(() => Maybe<int>.None);
            CheckMaybeValue(maybeResult, 12);

            maybeResult = maybeInt.Or(() => null);
            CheckMaybeValue(maybeResult, 12);

            Assert.AreEqual(12, maybeInt.OrDefault());

            // Empty maybe
            Maybe<int> emptyMaybeInt = Maybe.None;
            Assert.AreEqual(42, emptyMaybeInt.Or(42));
            Assert.AreEqual(42, emptyMaybeInt.Or(() => 42));
            maybeResult = emptyMaybeInt.Or(() => 42);
            CheckMaybeValue(maybeResult, 42);

            maybeResult = emptyMaybeInt.Or(() => Maybe<int>.None);
            CheckEmptyMaybe(maybeResult);

            maybeResult = emptyMaybeInt.Or(() => null);
            CheckEmptyMaybe(maybeResult);

            Assert.AreEqual(0, emptyMaybeInt.OrDefault());

            Maybe<TestClass> maybeResultClass;

            // Maybe class with value
            var testObject = new TestClass();
            var defaultTestObject = new TestClass { TestInt = 12 };
            var maybeClass = Maybe<TestClass>.Some(testObject);
            Assert.AreSame(testObject, maybeClass.Or(defaultTestObject));
            Assert.AreSame(testObject, maybeClass.Or(() => defaultTestObject));

            maybeResultClass = maybeClass.Or(() => defaultTestObject);
            CheckMaybeSameValue(maybeResultClass, testObject);

            maybeResultClass = maybeClass.Or(() => Maybe<TestClass>.None);
            CheckMaybeSameValue(maybeResultClass, testObject);

            maybeResultClass = maybeClass.Or(() => null);
            CheckMaybeSameValue(maybeResultClass, testObject);

            Assert.AreSame(testObject, maybeClass.OrDefault());

            // Empty maybe class
            Maybe<TestClass> emptyMaybeClass = Maybe.None;
            Assert.AreSame(defaultTestObject, emptyMaybeClass.Or(defaultTestObject));
            Assert.AreSame(defaultTestObject, emptyMaybeClass.Or(() => defaultTestObject));

            maybeResultClass = emptyMaybeClass.Or(() => defaultTestObject);
            CheckMaybeSameValue(maybeResultClass, defaultTestObject);

            maybeResultClass = emptyMaybeClass.Or(() => Maybe<TestClass>.None);
            CheckEmptyMaybe(maybeResultClass);

            maybeResultClass = emptyMaybeClass.Or(() => null);
            CheckEmptyMaybe(maybeResultClass);

            Assert.AreEqual(null, emptyMaybeClass.OrDefault());
        }

        [Test]
        public void MaybeIfOrThrows()
        {
            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            Assert.DoesNotThrow(() =>
            {
                float result = maybeInt.IfOrThrows(value => 12.5f, new InvalidOperationException());
                Assert.AreEqual(12.5f, result);
            });
            Assert.DoesNotThrow(() =>
            {
                float result = maybeInt.IfOrThrows(value => 12.6f, () => new InvalidOperationException());
                Assert.AreEqual(12.6f, result);
            });
            Assert.DoesNotThrow(() => maybeInt.IfOrThrows(value => { }, new InvalidOperationException()));
            Assert.DoesNotThrow(() => maybeInt.IfOrThrows(value => { }, () => new InvalidOperationException()));

            // Empty maybe
            Maybe<int> emptyMaybeInt = Maybe.None;
            Assert.Throws<InvalidOperationException>(() =>
            {
                float result = emptyMaybeInt.IfOrThrows(value => 12.5f, new InvalidOperationException());
                Assert.AreEqual(12.5f, result);
            });
            Assert.Throws<InvalidOperationException>(() =>
            {
                float result = emptyMaybeInt.IfOrThrows(value => 12.6f, () => new InvalidOperationException());
                Assert.AreEqual(12.6f, result);
            });
            Assert.Throws<InvalidOperationException>(() => emptyMaybeInt.IfOrThrows(value => { }, new InvalidOperationException()));
            Assert.Throws<InvalidOperationException>(() => emptyMaybeInt.IfOrThrows(value => { }, () => new InvalidOperationException()));
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
        public void MaybeUnwrap()
        {
            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            Assert.AreEqual(12, maybeInt.Unwrap());
            Assert.AreEqual(12, maybeInt.Unwrap(25));
            Assert.AreEqual(12, maybeInt.Unwrap(() => 26));
            Assert.AreEqual(25.5f, maybeInt.Unwrap(value => 25.5f));
            Assert.AreEqual(25.5f, maybeInt.Unwrap(value => 25.5f, 15.1f));
            Assert.AreEqual(25.5f, maybeInt.Unwrap(value => 25.5f, () => 15.2f));

            // Empty maybe
            Maybe<int> emptyMaybeInt = Maybe.None;
            Assert.AreEqual(0, emptyMaybeInt.Unwrap());
            Assert.AreEqual(25, emptyMaybeInt.Unwrap(25));
            Assert.AreEqual(26, emptyMaybeInt.Unwrap(() => 26));
            Assert.AreEqual(0, emptyMaybeInt.Unwrap(value => 25.5f));
            Assert.AreEqual(15.1f, emptyMaybeInt.Unwrap(value => 25.5f, 15.1f));
            Assert.AreEqual(15.2f, emptyMaybeInt.Unwrap(value => 25.5f, () => 15.2f));

            // Maybe class with value
            var testObject = new TestClass();
            var defaultTestObject = new TestClass { TestInt = 12 };
            var maybeClass = Maybe<TestClass>.Some(testObject);
            Assert.AreSame(testObject, maybeClass.Unwrap());
            Assert.AreSame(testObject, maybeClass.Unwrap(defaultTestObject));
            Assert.AreSame(testObject, maybeClass.Unwrap(() => defaultTestObject));
            Assert.AreEqual(25.5f, maybeClass.Unwrap(value => 25.5f));
            Assert.AreEqual(25.5f, maybeClass.Unwrap(value => 25.5f, 15.1f));
            Assert.AreEqual(25.5f, maybeClass.Unwrap(value => 25.5f, () => 15.2f));

            // Empty maybe class
            Maybe<TestClass> emptyMaybeClass = Maybe.None;
            Assert.AreSame(null, emptyMaybeClass.Unwrap());
            Assert.AreSame(defaultTestObject, emptyMaybeClass.Unwrap(defaultTestObject));
            Assert.AreSame(defaultTestObject, emptyMaybeClass.Unwrap(() => defaultTestObject));
            Assert.AreEqual(0.0f, emptyMaybeClass.Unwrap(value => 25.5f));
            Assert.AreEqual(15.1f, emptyMaybeClass.Unwrap(value => 25.5f, 15.1f));
            Assert.AreEqual(15.2f, emptyMaybeClass.Unwrap(value => 25.5f, () => 15.2f));
        }

        [Test]
        public void MaybeCast()
        {
            // Value type
            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            var maybeFloat = maybeInt.Cast(intVal => intVal + 0.1f);
            CheckMaybeValue(maybeFloat, 12.1f);

            // Empty maybe
            Maybe<int> emptyMaybeInt = Maybe.None;
            maybeFloat = emptyMaybeInt.Cast(intVal => intVal + 0.1f);
            CheckEmptyMaybe(maybeFloat);


            // Reference type
            // Maybe with value
            var testObjectLeaf = new TestClassLeaf { TestInt = 12 };
            var maybeClassLeaf = Maybe<TestClassLeaf>.Some(testObjectLeaf);
            maybeFloat = maybeClassLeaf.Cast(obj => obj.TestInt + 0.2f);
            CheckMaybeValue(maybeFloat, 12.2f);

            var maybeClass = maybeClassLeaf.Cast<TestClass>();
            CheckMaybeSameValue(maybeClass, testObjectLeaf);

            // Invalid conversion
            var testObject = new TestClass { TestInt = 42 };
            maybeClass = Maybe<TestClass>.Some(testObject);
            maybeClassLeaf = maybeClass.Cast<TestClassLeaf>();
            CheckEmptyMaybe(maybeClassLeaf);

            // Empty maybe
            Maybe<TestClassLeaf> emptyMaybeClassLeaf = Maybe.None;
            maybeFloat = emptyMaybeClassLeaf.Cast(obj => obj.TestInt + 0.2f);
            CheckEmptyMaybe(maybeFloat);

            maybeClass = emptyMaybeClassLeaf.Cast<TestClass>();
            CheckEmptyMaybe(maybeClass);
        }

        [Test]
        public void MaybeExists()
        {
            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            Assert.IsTrue(maybeInt.Exists(intValue => intValue == 12));
            Assert.IsFalse(maybeInt.Exists(intValue => intValue == 13));

            // Empty maybe
            Maybe<int> emptyMaybeInt = Maybe.None;
            Assert.IsFalse(emptyMaybeInt.Exists(intValue => intValue == 12));
        }
    }
}

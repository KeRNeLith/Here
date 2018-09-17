using System;
using NUnit.Framework;
using Here.Maybes;
using Here.Maybes.Extensions;

namespace Here.Tests.Maybes
{
    /// <summary>
    /// Basic tests for <see cref="Maybe{T}"/>.
    /// </summary>
    [TestFixture]
    internal class MaybeTests : MaybeTestsBase
    {
        #region Test classes

        private class PersonNotEquatable
        {
            private readonly string _name;

            public PersonNotEquatable(string name)
            {
                _name = name;
            }
        }

        private class Person
        {
            private readonly string _name;

            public Person(string name)
            {
                _name = name;
            }

            public override bool Equals(object obj)
            {
                return obj is Person otherPerson
                    && _name.Equals(otherPerson._name, StringComparison.Ordinal);
            }

            public override int GetHashCode()
            {
                return _name.GetHashCode();
            }
        }

        #endregion

        [Test]
        public void MaybeConstruction()
        {
            // Maybe value type
            // With value
            var maybeInt = Maybe<int>.Some(12);
            CheckMaybeValue(maybeInt, 12);

            // No value
            var emptyMaybeInt = Maybe<int>.None;
            CheckEmptyMaybe(emptyMaybeInt);

            // Implicit none
            Maybe<int> emptyMaybeInt2 = Maybe.None;
            CheckEmptyMaybe(emptyMaybeInt2);

            // Maybe reference type
            // With value
            var testValue = new TestClass { TestInt = 12 };
            var maybeClass = Maybe<TestClass>.Some(testValue);
            CheckMaybeSameValue(maybeClass, testValue);

            // No value
            var emptyMaybeClass = Maybe<TestClass>.None;
            CheckEmptyMaybe(emptyMaybeClass);

            // Implicit none
            Maybe<TestClass> emptyMaybeClass2 = Maybe.None;
            CheckEmptyMaybe(emptyMaybeClass2);

            // Null value
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => Maybe<TestClass>.Some(null));
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
            CheckMaybeValue(maybeInt, 42);
            maybeInt = embedMaybeInt.Flatten();
            CheckMaybeValue(maybeInt, 42);

            // No value
            var emptyEmbedMaybeInt = Maybe<Maybe<int>>.Some(Maybe.None);
            Assert.IsTrue(emptyEmbedMaybeInt.HasValue);
            Assert.IsFalse(emptyEmbedMaybeInt.Value.HasValue);

            Maybe<int> emptyMaybeInt = emptyEmbedMaybeInt;
            CheckEmptyMaybe(emptyMaybeInt);
            emptyMaybeInt = emptyEmbedMaybeInt.Flatten();
            CheckEmptyMaybe(emptyMaybeInt);

            // Flatten Maybe reference type
            // With value
            var testValue = new TestClass { TestInt = 42 };
            var embedMaybeClass = Maybe<Maybe<TestClass>>.Some(Maybe<TestClass>.Some(testValue));
            Assert.IsTrue(embedMaybeClass.HasValue);
            Assert.IsTrue(embedMaybeClass.Value.HasValue);
            Assert.AreSame(testValue, embedMaybeClass.Value.Value);

            Maybe<TestClass> maybeClass = embedMaybeClass;
            CheckMaybeSameValue(maybeClass, testValue);
            maybeClass = embedMaybeClass.Flatten();
            CheckMaybeSameValue(maybeClass, testValue);

            // No value
            var emptyEmbedMaybeClass = Maybe<Maybe<TestClass>>.Some(Maybe.None);
            Assert.IsTrue(emptyEmbedMaybeClass.HasValue);
            Assert.IsFalse(emptyEmbedMaybeClass.Value.HasValue);

            Maybe<TestClass> emptyMaybeClass = emptyEmbedMaybeClass;
            CheckEmptyMaybe(emptyMaybeClass);
            emptyMaybeClass = emptyEmbedMaybeClass.Flatten();
            CheckEmptyMaybe(emptyMaybeClass);
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
            var person1 = new Person("Test");
            var maybePerson1 = Maybe<Person>.Some(person1);
            var maybePerson2 = Maybe<Person>.Some(person1);
            var maybePerson3 = Maybe<Person>.Some(new Person("Test"));
            var maybePerson4 = Maybe<Person>.Some(new Person("Test2"));
            var emptyMaybePerson = Maybe<Person>.None;

            Assert.IsTrue(maybePerson1.Equals(maybePerson2));
            Assert.IsTrue(maybePerson2.Equals(maybePerson1));
            Assert.IsTrue(maybePerson1 == maybePerson2);

            Assert.IsTrue(maybePerson1.Equals(maybePerson3));
            Assert.IsTrue(maybePerson3.Equals(maybePerson1));
            Assert.IsTrue(maybePerson1 == maybePerson3);

            Assert.IsFalse(maybePerson1.Equals(maybePerson4));
            Assert.IsFalse(maybePerson4.Equals(maybePerson1));
            Assert.IsTrue(maybePerson1 != maybePerson4);

            Assert.IsFalse(maybePerson2.Equals(emptyMaybePerson));
            Assert.IsFalse(emptyMaybePerson.Equals(maybePerson2));
            Assert.IsFalse(emptyMaybePerson == maybePerson2);
            Assert.IsTrue(maybePerson1 != emptyMaybePerson);

            var person2 = new PersonNotEquatable("Test");
            var maybePerson5 = Maybe<PersonNotEquatable>.Some(person2);
            var maybePerson6 = Maybe<PersonNotEquatable>.Some(person2);
            var maybePerson7 = Maybe<PersonNotEquatable>.Some(new PersonNotEquatable("Test"));
            var emptyMaybePerson2 = Maybe<PersonNotEquatable>.None;

            Assert.IsTrue(maybePerson5.Equals(maybePerson6));
            Assert.IsTrue(maybePerson6.Equals(maybePerson5));
            Assert.IsTrue(maybePerson5 == maybePerson6);

            Assert.IsFalse(maybePerson5.Equals(maybePerson7));
            Assert.IsFalse(maybePerson7.Equals(maybePerson5));
            Assert.IsTrue(maybePerson5 != maybePerson7);

            Assert.IsFalse(maybePerson5.Equals(emptyMaybePerson2));
            Assert.IsFalse(emptyMaybePerson2.Equals(maybePerson5));
            Assert.IsTrue(maybePerson5 != emptyMaybePerson2);

            // Mixed
            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(maybeInt.Equals(maybePerson1));
            Assert.IsFalse(maybePerson1.Equals(maybeInt));
            // ReSharper restore SuspiciousTypeConversion.Global
            Assert.IsFalse(maybePerson1.Equals(12));

            // With flatten
            var embedMaybeInt = Maybe<Maybe<int>>.Some(Maybe<int>.Some(12));
            Assert.IsTrue(maybeInt.Equals(embedMaybeInt));
            var embedMaybeInt2 = Maybe<Maybe<int>>.Some(Maybe<int>.Some(42));
            Assert.IsFalse(maybeInt.Equals(embedMaybeInt2));

            var embedMaybeClass = Maybe<Maybe<Person>>.Some(Maybe<Person>.Some(person1));
            Assert.IsTrue(maybePerson1.Equals(embedMaybeClass));
            var embedMaybeClass2 = Maybe<Maybe<Person>>.Some(Maybe<Person>.Some(new Person("Test3")));
            Assert.IsFalse(maybePerson1.Equals(embedMaybeClass2));

            // Equals with a null value
            Assert.IsFalse(maybeInt.Equals((object)null));
        }

        [Test]
        public void MaybeEqualityValue()
        {
            // Maybe value type
            var maybeInt = Maybe<int>.Some(12);
            var emptyMaybeInt = Maybe<int>.None;

            Assert.IsTrue(maybeInt == 12);
            Assert.IsTrue(12 == maybeInt);
            Assert.IsFalse(maybeInt == 42);
            Assert.IsTrue(maybeInt != 42);
            Assert.IsFalse(12 != maybeInt);

            Assert.IsFalse(emptyMaybeInt == 12);
            Assert.IsFalse(12 == emptyMaybeInt);
            Assert.IsTrue(emptyMaybeInt != 12);
            Assert.IsTrue(15 != emptyMaybeInt);

            // Maybe reference type
            var person1 = new Person("Test");
            var maybePerson1 = Maybe<Person>.Some(person1);
            var maybePerson2 = Maybe<Person>.Some(new Person("Test"));
            var maybePerson3 = Maybe<Person>.Some(new Person("Test2"));
            var emptyMaybePerson = Maybe<Person>.None;

            Assert.IsTrue(maybePerson1 == person1);
            Assert.IsTrue(person1 == maybePerson1);
            Assert.IsFalse(maybePerson1 != person1);
            Assert.IsFalse(person1 != maybePerson1);

            Assert.IsTrue(maybePerson2 == person1);
            Assert.IsTrue(person1 == maybePerson2);
            Assert.IsFalse(maybePerson2 != person1);
            Assert.IsFalse(new Person("Test") != maybePerson2);

            Assert.IsFalse(maybePerson3 == person1);
            Assert.IsFalse(person1 == maybePerson3);
            Assert.IsTrue(maybePerson3 != person1);
            Assert.IsTrue(person1 != maybePerson3);

            Assert.IsFalse(emptyMaybePerson == person1);
            Assert.IsFalse(person1 == emptyMaybePerson);
            Assert.IsTrue(emptyMaybePerson != person1);
            Assert.IsTrue(person1 != emptyMaybePerson);

            var person2 = new PersonNotEquatable("Test");
            var maybePerson4 = Maybe<PersonNotEquatable>.Some(person2);
            var maybePerson5 = Maybe<PersonNotEquatable>.Some(new PersonNotEquatable("Test"));
            var emptyMaybePerson2 = Maybe<PersonNotEquatable>.None;

            Assert.IsTrue(maybePerson4 == person2);
            Assert.IsTrue(person2 == maybePerson4);
            Assert.IsFalse(maybePerson4 != person2);
            Assert.IsFalse(person2 != maybePerson4);

            Assert.IsFalse(maybePerson5 == person2);
            Assert.IsFalse(person2 == maybePerson5);
            Assert.IsTrue(maybePerson5 != person2);
            Assert.IsTrue(person2 != maybePerson5);

            Assert.IsFalse(emptyMaybePerson2 == person2);
            Assert.IsFalse(person2 == emptyMaybePerson2);
            Assert.IsTrue(emptyMaybePerson2 != person2);
            Assert.IsTrue(person2 != emptyMaybePerson2);

            // With flatten
            var embedMaybeInt = Maybe<Maybe<int>>.Some(Maybe<int>.Some(12));
            Assert.IsTrue(embedMaybeInt == 12);
            Assert.IsFalse(embedMaybeInt == 42);

            // On value type null is not a valid value consider implicitly casted to None
            Assert.IsFalse(maybeInt == null);
            Assert.IsTrue(Maybe<int>.None == null);

            // Equals with a null value => These cases are not possible as they 
            // suppose the maybe value is null which should never be the case!
            Assert.IsFalse(maybePerson1 == null);
        }

        [Test]
        public void MaybeCompare()
        {
            // Maybe value type
            var maybeInt1 = Maybe<int>.Some(12);
            var maybeInt2 = Maybe<int>.Some(12);
            var maybeInt3 = Maybe<int>.Some(42);
            var emptyMaybeInt1 = Maybe<int>.None;
            var emptyMaybeInt2 = Maybe<int>.None;

            Assert.AreEqual(0, maybeInt1.CompareTo(maybeInt1));
            Assert.AreEqual(0, maybeInt1.CompareTo((object)maybeInt1));

            Assert.AreEqual(0, maybeInt1.CompareTo(maybeInt2));
            Assert.AreEqual(0, maybeInt1.CompareTo((object)maybeInt2));
            Assert.AreEqual(0, maybeInt2.CompareTo(maybeInt1));
            Assert.AreEqual(0, maybeInt2.CompareTo((object)maybeInt1));

            Assert.AreEqual(-1, maybeInt1.CompareTo(maybeInt3));
            Assert.AreEqual(-1, maybeInt1.CompareTo((object)maybeInt3));
            Assert.AreEqual(1, maybeInt3.CompareTo(maybeInt1));
            Assert.AreEqual(1, maybeInt3.CompareTo((object)maybeInt1));

            Assert.AreEqual(1, maybeInt1.CompareTo(emptyMaybeInt1));
            Assert.AreEqual(1, maybeInt1.CompareTo((object)emptyMaybeInt1));
            Assert.AreEqual(-1, emptyMaybeInt1.CompareTo(maybeInt1));
            Assert.AreEqual(-1, emptyMaybeInt1.CompareTo((object)maybeInt1));

            Assert.AreEqual(0, emptyMaybeInt1.CompareTo(emptyMaybeInt2));
            Assert.AreEqual(0, emptyMaybeInt1.CompareTo((object)emptyMaybeInt2));
            Assert.AreEqual(0, emptyMaybeInt2.CompareTo(emptyMaybeInt1));
            Assert.AreEqual(0, emptyMaybeInt2.CompareTo((object)emptyMaybeInt1));

            // Mixed
            Assert.AreEqual(1, maybeInt1.CompareTo(null));      // Null initialize a Maybe.None
            Assert.AreEqual(0, emptyMaybeInt1.CompareTo(null)); // Null initialize a Maybe.None
            Assert.AreEqual(1, maybeInt1.CompareTo((object)null));
            Assert.AreEqual(0, emptyMaybeInt1.CompareTo((object)null));

            var maybeFloat = Maybe<float>.Some(12.1f);
            var emptyMaybeFloat = Maybe<float>.None;
            Assert.Throws<ArgumentException>(() => maybeInt1.CompareTo(maybeFloat));
            Assert.Throws<ArgumentException>(() => maybeFloat.CompareTo(maybeInt1));

            Assert.Throws<ArgumentException>(() => maybeInt1.CompareTo(emptyMaybeFloat));
            Assert.Throws<ArgumentException>(() => emptyMaybeFloat.CompareTo(maybeInt1));
        }

        [Test]
        public void MaybeHashCode()
        {
            // Equals values
            Maybe<Person> p1 = new Person("Foo Bar");
            Maybe<Person> p2 = new Person("Foo Bar");
            Assert.AreNotSame(p1, p2);
            Assert.IsTrue(p1.Equals(p2));
            Assert.IsTrue(p2.Equals(p1));
            Assert.IsTrue(p1.GetHashCode() == p2.GetHashCode());

            // Different values
            Maybe<Person> p3 = new Person("Bar Foo");
            Assert.AreNotSame(p1, p3);
            Assert.IsFalse(p1.Equals(p3));
            Assert.IsFalse(p3.Equals(p1));
            Assert.IsFalse(p1.GetHashCode() == p3.GetHashCode());

            // Empty maybe
            Maybe<Person> empty = Maybe.None;
            Assert.AreNotSame(p1, empty);
            Assert.IsFalse(p1.Equals(empty));
            Assert.IsFalse(empty.Equals(p1));
            Assert.IsFalse(p1.GetHashCode() == empty.GetHashCode());
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
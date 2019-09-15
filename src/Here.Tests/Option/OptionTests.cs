using System;
using NUnit.Framework;
using Here.Extensions;
using static Here.Tests.Options.OptionTestHelpers;

namespace Here.Tests.Options
{
    /// <summary>
    /// Basic tests for <see cref="Option{T}"/>.
    /// </summary>
    [TestFixture]
    internal class OptionTests : HereTestsBase
    {
        [Test]
        public void OptionConstruction()
        {
            // Option value type
            // With value
            var optionInt = Option<int>.Some(12);
            CheckOptionValue(optionInt, 12);

            // No value
            var emptyOptionInt = Option<int>.None;
            CheckEmptyOption(emptyOptionInt);

            // Implicit none
            Option<int> emptyOptionInt2 = Option.None;
            CheckEmptyOption(emptyOptionInt2);

            // Option reference type
            // With value
            var testValue = new TestClass { TestInt = 12 };
            var optionClass = Option<TestClass>.Some(testValue);
            CheckOptionSameValue(optionClass, testValue);

            // No value
            var emptyOptionClass = Option<TestClass>.None;
            CheckEmptyOption(emptyOptionClass);

            // Implicit none
            Option<TestClass> emptyOptionClass2 = Option.None;
            CheckEmptyOption(emptyOptionClass2);

            // Null value
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => Option<TestClass>.Some(null));
        }

        [Test]
        public void FlattenOption()
        {
            // Flatten Option value type
            // With value
            var embedOptionInt = Option<Option<int>>.Some(Option<int>.Some(42));
            Assert.IsTrue(embedOptionInt.HasValue);
            Assert.IsTrue(embedOptionInt.Value.HasValue);
            Assert.AreEqual(42, embedOptionInt.Value.Value);

            Option<int> optionInt = embedOptionInt;
            CheckOptionValue(optionInt, 42);
            optionInt = embedOptionInt.Flatten();
            CheckOptionValue(optionInt, 42);

            // No value
            var emptyEmbedOptionInt = Option<Option<int>>.Some(Option.None);
            Assert.IsTrue(emptyEmbedOptionInt.HasValue);
            Assert.IsFalse(emptyEmbedOptionInt.Value.HasValue);

            Option<int> emptyOptionInt = emptyEmbedOptionInt;
            CheckEmptyOption(emptyOptionInt);
            emptyOptionInt = emptyEmbedOptionInt.Flatten();
            CheckEmptyOption(emptyOptionInt);

            // Flatten Option reference type
            // With value
            var testValue = new TestClass { TestInt = 42 };
            var embedOptionClass = Option<Option<TestClass>>.Some(Option<TestClass>.Some(testValue));
            Assert.IsTrue(embedOptionClass.HasValue);
            Assert.IsTrue(embedOptionClass.Value.HasValue);
            Assert.AreSame(testValue, embedOptionClass.Value.Value);

            Option<TestClass> optionClass = embedOptionClass;
            CheckOptionSameValue(optionClass, testValue);
            optionClass = embedOptionClass.Flatten();
            CheckOptionSameValue(optionClass, testValue);

            // No value
            var emptyEmbedOptionClass = Option<Option<TestClass>>.Some(Option.None);
            Assert.IsTrue(emptyEmbedOptionClass.HasValue);
            Assert.IsFalse(emptyEmbedOptionClass.Value.HasValue);

            Option<TestClass> emptyOptionClass = emptyEmbedOptionClass;
            CheckEmptyOption(emptyOptionClass);
            emptyOptionClass = emptyEmbedOptionClass.Flatten();
            CheckEmptyOption(emptyOptionClass);
        }

        [Test]
        public void OptionEquality()
        {
            // Option value type
            var optionInt1 = Option<int>.Some(12);
            var optionInt2 = Option<int>.Some(12);
            var optionInt3 = Option<int>.Some(42);
            var emptyOptionInt1 = Option<int>.None;
            var emptyOptionInt2 = Option<int>.None;

            Assert.IsTrue(optionInt1.Equals(optionInt2));
            Assert.IsTrue(optionInt2.Equals(optionInt1));
            Assert.IsTrue(optionInt1 == optionInt2);
            Assert.IsTrue(optionInt2 == optionInt1);
            Assert.IsFalse(optionInt1 != optionInt2);
            Assert.IsFalse(optionInt2 != optionInt1);

            Assert.IsFalse(optionInt1.Equals(optionInt3));
            Assert.IsFalse(optionInt3.Equals(optionInt1));
            Assert.IsFalse(optionInt1 == optionInt3);
            Assert.IsFalse(optionInt3 == optionInt1);
            Assert.IsTrue(optionInt1 != optionInt3);
            Assert.IsTrue(optionInt3 != optionInt1);

            Assert.IsFalse(optionInt1.Equals(emptyOptionInt1));
            Assert.IsFalse(emptyOptionInt1.Equals(optionInt1));
            Assert.IsFalse(optionInt1 == emptyOptionInt1);
            Assert.IsFalse(emptyOptionInt1 == optionInt1);
            Assert.IsTrue(optionInt1 != emptyOptionInt1);
            Assert.IsTrue(emptyOptionInt1 != optionInt1);

            Assert.IsTrue(emptyOptionInt1.Equals(emptyOptionInt2));
            Assert.IsTrue(emptyOptionInt2.Equals(emptyOptionInt1));
            Assert.IsTrue(emptyOptionInt1 == emptyOptionInt2);
            Assert.IsTrue(emptyOptionInt2 == emptyOptionInt1);
            Assert.IsFalse(emptyOptionInt1 != emptyOptionInt2);
            Assert.IsFalse(emptyOptionInt2 != emptyOptionInt1);

            // Option reference type
            var person1 = new Person("Test");
            var optionPerson1 = Option<Person>.Some(person1);
            var optionPerson2 = Option<Person>.Some(person1);
            var optionPerson3 = Option<Person>.Some(new Person("Test"));
            var optionPerson4 = Option<Person>.Some(new Person("Test2"));
            var emptyOptionPerson1 = Option<Person>.None;
            var emptyOptionPerson2 = Option<Person>.None;

            Assert.IsTrue(optionPerson1.Equals(optionPerson2));
            Assert.IsTrue(optionPerson2.Equals(optionPerson1));
            Assert.IsTrue(optionPerson1 == optionPerson2);
            Assert.IsTrue(optionPerson2 == optionPerson1);
            Assert.IsFalse(optionPerson1 != optionPerson2);
            Assert.IsFalse(optionPerson2 != optionPerson1);

            Assert.IsTrue(optionPerson1.Equals(optionPerson3));
            Assert.IsTrue(optionPerson3.Equals(optionPerson1));
            Assert.IsTrue(optionPerson1 == optionPerson3);
            Assert.IsTrue(optionPerson3 == optionPerson1);
            Assert.IsFalse(optionPerson1 != optionPerson3);
            Assert.IsFalse(optionPerson3 != optionPerson1);

            Assert.IsFalse(optionPerson1.Equals(optionPerson4));
            Assert.IsFalse(optionPerson4.Equals(optionPerson1));
            Assert.IsFalse(optionPerson1 == optionPerson4);
            Assert.IsFalse(optionPerson4 == optionPerson1);
            Assert.IsTrue(optionPerson1 != optionPerson4);
            Assert.IsTrue(optionPerson4 != optionPerson1);

            Assert.IsFalse(optionPerson2.Equals(emptyOptionPerson1));
            Assert.IsFalse(emptyOptionPerson1.Equals(optionPerson2));
            Assert.IsFalse(emptyOptionPerson1 == optionPerson2);
            Assert.IsFalse(optionPerson2 == emptyOptionPerson1);
            Assert.IsTrue(optionPerson1 != emptyOptionPerson1);
            Assert.IsTrue(emptyOptionPerson1 != optionPerson1);

            Assert.IsTrue(emptyOptionPerson1.Equals(emptyOptionPerson2));
            Assert.IsTrue(emptyOptionPerson2.Equals(emptyOptionPerson1));
            Assert.IsTrue(emptyOptionPerson1 == emptyOptionPerson2);
            Assert.IsTrue(emptyOptionPerson2 == emptyOptionPerson1);
            Assert.IsFalse(emptyOptionPerson1 != emptyOptionPerson2);
            Assert.IsFalse(emptyOptionPerson2 != emptyOptionPerson1);

            var person2 = new PersonNotEquatable("Test");
            var optionPerson5 = Option<PersonNotEquatable>.Some(person2);
            var optionPerson6 = Option<PersonNotEquatable>.Some(person2);
            var optionPerson7 = Option<PersonNotEquatable>.Some(new PersonNotEquatable("Test"));
            var emptyOptionPerson3 = Option<PersonNotEquatable>.None;
            var emptyOptionPerson4 = Option<PersonNotEquatable>.None;

            Assert.IsTrue(optionPerson5.Equals(optionPerson6));
            Assert.IsTrue(optionPerson6.Equals(optionPerson5));
            Assert.IsTrue(optionPerson5 == optionPerson6);
            Assert.IsTrue(optionPerson6 == optionPerson5);
            Assert.IsFalse(optionPerson5 != optionPerson6);
            Assert.IsFalse(optionPerson6 != optionPerson5);

            Assert.IsFalse(optionPerson5.Equals(optionPerson7));
            Assert.IsFalse(optionPerson7.Equals(optionPerson5));
            Assert.IsFalse(optionPerson5 == optionPerson7);
            Assert.IsFalse(optionPerson7 == optionPerson5);
            Assert.IsTrue(optionPerson5 != optionPerson7);
            Assert.IsTrue(optionPerson7 != optionPerson5);

            Assert.IsFalse(optionPerson5.Equals(emptyOptionPerson3));
            Assert.IsFalse(emptyOptionPerson3.Equals(optionPerson5));
            Assert.IsFalse(optionPerson5 == emptyOptionPerson3);
            Assert.IsFalse(emptyOptionPerson3 == optionPerson5);
            Assert.IsTrue(optionPerson5 != emptyOptionPerson3);
            Assert.IsTrue(emptyOptionPerson3 != optionPerson5);

            Assert.IsTrue(emptyOptionPerson3.Equals(emptyOptionPerson4));
            Assert.IsTrue(emptyOptionPerson4.Equals(emptyOptionPerson3));
            Assert.IsTrue(emptyOptionPerson3 == emptyOptionPerson4);
            Assert.IsTrue(emptyOptionPerson4 == emptyOptionPerson3);
            Assert.IsFalse(emptyOptionPerson3 != emptyOptionPerson4);
            Assert.IsFalse(emptyOptionPerson4 != emptyOptionPerson3);

            // Mixed
            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(optionInt1.Equals(optionPerson1));
            Assert.IsFalse(optionPerson1.Equals(optionInt1));
            Assert.IsFalse(optionPerson1.Equals(12));
            // ReSharper restore SuspiciousTypeConversion.Global

            // With flatten
            var embedOptionInt = Option<Option<int>>.Some(Option<int>.Some(12));
            Assert.IsTrue(optionInt1.Equals(embedOptionInt));
            var embedOptionInt2 = Option<Option<int>>.Some(Option<int>.Some(42));
            Assert.IsFalse(optionInt1.Equals(embedOptionInt2));

            var embedOptionClass = Option<Option<Person>>.Some(Option<Person>.Some(person1));
            Assert.IsTrue(optionPerson1.Equals(embedOptionClass));
            var embedOptionClass2 = Option<Option<Person>>.Some(Option<Person>.Some(new Person("Test3")));
            Assert.IsFalse(optionPerson1.Equals(embedOptionClass2));

            // Equals with an object value
            Assert.IsFalse(optionInt1.Equals((object)null));
            Assert.IsTrue(optionInt1.Equals((object)optionInt1));
            Assert.IsFalse(optionInt1.Equals((object)emptyOptionInt1));
        }

        [Test]
        public void OptionEqualityHelpers()
        {
            var person1 = new Person("Test");
            var optionPerson1 = Option<Person>.Some(person1);
            var optionPerson2 = Option<Person>.Some(person1);
            var optionPerson3 = Option<Person>.Some(new Person("Test"));
            var optionPerson4 = Option<Person>.Some(new Person("Test2"));
            var emptyOptionPerson1 = Option<Person>.None;
            var emptyOptionPerson2 = Option<Person>.None;

            Assert.IsTrue(OptionHelpers.AreEqual(optionPerson1, optionPerson1));
            Assert.IsTrue(OptionHelpers.AreEqual(optionPerson1, person1));
            Assert.IsFalse(OptionHelpers.AreEqual(optionPerson1, new Person("Test2")));
            Assert.IsFalse(OptionHelpers.AreEqual(optionPerson1, null));

            Assert.IsTrue(OptionHelpers.AreEqual(optionPerson1, optionPerson2));
            Assert.IsTrue(OptionHelpers.AreEqual(optionPerson2, optionPerson1));

            Assert.IsTrue(OptionHelpers.AreEqual(optionPerson1, optionPerson3));
            Assert.IsTrue(OptionHelpers.AreEqual(optionPerson3, optionPerson1));
            Assert.IsTrue(OptionHelpers.AreEqual(optionPerson1, new Person("Test")));

            Assert.IsFalse(OptionHelpers.AreEqual(optionPerson1, optionPerson4));
            Assert.IsFalse(OptionHelpers.AreEqual(optionPerson4, optionPerson1));
            Assert.IsTrue(OptionHelpers.AreEqual(optionPerson4, new Person("Test2")));

            Assert.IsFalse(OptionHelpers.AreEqual(optionPerson1, emptyOptionPerson1));
            Assert.IsFalse(OptionHelpers.AreEqual(emptyOptionPerson1, optionPerson1));
            Assert.IsFalse(OptionHelpers.AreEqual(emptyOptionPerson1, person1));
            Assert.IsFalse(OptionHelpers.AreEqual(emptyOptionPerson1, null));

            Assert.IsTrue(OptionHelpers.AreEqual(emptyOptionPerson1, emptyOptionPerson2));
            Assert.IsTrue(OptionHelpers.AreEqual(emptyOptionPerson2, emptyOptionPerson1));
        }

        [Test]
        public void OptionEqualityValue()
        {
            // Option value type
            var optionInt = Option<int>.Some(12);
            var emptyOptionInt = Option<int>.None;

            Assert.IsTrue(optionInt.Equals(12));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsTrue(optionInt.Equals((object)12));
            Assert.IsFalse(optionInt.Equals(42));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsFalse(optionInt.Equals((object)42));
            Assert.IsTrue(optionInt == 12);
            Assert.IsTrue(12 == optionInt);
            Assert.IsFalse(optionInt == 42);
            Assert.IsTrue(optionInt != 42);
            Assert.IsFalse(12 != optionInt);

            Assert.IsFalse(emptyOptionInt.Equals(12));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsFalse(emptyOptionInt.Equals((object)12));
            Assert.IsFalse(emptyOptionInt == 12);
            Assert.IsFalse(12 == emptyOptionInt);
            Assert.IsTrue(emptyOptionInt != 12);
            Assert.IsTrue(15 != emptyOptionInt);

            // Option reference type
            var person1 = new Person("Test");
            var optionPerson1 = Option<Person>.Some(person1);
            var optionPerson2 = Option<Person>.Some(new Person("Test"));
            var optionPerson3 = Option<Person>.Some(new Person("Test2"));
            var emptyOptionPerson1 = Option<Person>.None;

            Assert.IsTrue(optionPerson1.Equals(person1));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsTrue(optionPerson1.Equals((object)person1));
            Assert.IsFalse(optionPerson1.Equals(new Person("Test2")));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsFalse(optionPerson1.Equals((object)new Person("Test2")));
            Assert.IsTrue(optionPerson1 == person1);
            Assert.IsTrue(person1 == optionPerson1);
            Assert.IsFalse(optionPerson1 != person1);
            Assert.IsFalse(person1 != optionPerson1);

            Assert.IsTrue(optionPerson2.Equals(person1));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsTrue(optionPerson2.Equals((object)person1));
            Assert.IsTrue(optionPerson2 == person1);
            Assert.IsTrue(person1 == optionPerson2);
            Assert.IsFalse(optionPerson2 != person1);
            Assert.IsFalse(new Person("Test") != optionPerson2);

            Assert.IsFalse(optionPerson3.Equals(person1));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsFalse(optionPerson3.Equals((object)person1));
            Assert.IsFalse(optionPerson3 == person1);
            Assert.IsFalse(person1 == optionPerson3);
            Assert.IsTrue(optionPerson3 != person1);
            Assert.IsTrue(person1 != optionPerson3);

            Assert.IsFalse(emptyOptionPerson1.Equals(person1));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsFalse(emptyOptionPerson1.Equals((object)person1));
            Assert.IsFalse(emptyOptionPerson1 == person1);
            Assert.IsFalse(person1 == emptyOptionPerson1);
            Assert.IsTrue(emptyOptionPerson1 != person1);
            Assert.IsTrue(person1 != emptyOptionPerson1);

            var person2 = new PersonNotEquatable("Test");
            var person3 = new PersonNotEquatable("Test");
            var optionPerson4 = Option<PersonNotEquatable>.Some(person2);
            var optionPerson5 = Option<PersonNotEquatable>.Some(new PersonNotEquatable("Test"));
            var emptyOptionPerson2 = Option<PersonNotEquatable>.None;

            Assert.IsTrue(optionPerson4.Equals(person2));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsTrue(optionPerson4.Equals((object)person2));
            Assert.IsFalse(optionPerson4.Equals(person3));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsFalse(optionPerson4.Equals((object)person3));
            Assert.IsTrue(optionPerson4 == person2);
            Assert.IsTrue(person2 == optionPerson4);
            Assert.IsFalse(optionPerson4 != person2);
            Assert.IsFalse(person2 != optionPerson4);

            Assert.IsFalse(optionPerson5.Equals(person2));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsFalse(optionPerson5.Equals((object)person2));
            Assert.IsFalse(optionPerson5 == person2);
            Assert.IsFalse(person2 == optionPerson5);
            Assert.IsTrue(optionPerson5 != person2);
            Assert.IsTrue(person2 != optionPerson5);

            Assert.IsFalse(emptyOptionPerson2.Equals(person2));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsFalse(emptyOptionPerson2.Equals((object)person2));
            Assert.IsFalse(emptyOptionPerson2 == person2);
            Assert.IsFalse(person2 == emptyOptionPerson2);
            Assert.IsTrue(emptyOptionPerson2 != person2);
            Assert.IsTrue(person2 != emptyOptionPerson2);

            // With flatten
            var embedOptionInt = Option<Option<int>>.Some(Option<int>.Some(12));
            Assert.IsTrue(embedOptionInt == 12);
            Assert.IsFalse(embedOptionInt == 42);

            // On value type null is not a valid value consider implicitly casted to None
            Assert.IsFalse(optionInt == null);
            Assert.IsTrue(Option<int>.None == null);

            // Equals with a null value => These cases are not possible as they 
            // suppose the option value is null which should never be the case!
            Assert.IsFalse(optionPerson1 == null);
        }

        [Test]
        public void OptionCompare()
        {
            // Option value type
            var optionInt1 = Option<int>.Some(12);
            var optionInt2 = Option<int>.Some(12);
            var optionInt3 = Option<int>.Some(42);
            var emptyOptionInt1 = Option<int>.None;
            var emptyOptionInt2 = Option<int>.None;

            Assert.AreEqual(0, optionInt1.CompareTo(optionInt1));
            Assert.AreEqual(0, optionInt1.CompareTo((object)optionInt1));

            Assert.AreEqual(0, optionInt1.CompareTo(optionInt2));
            Assert.AreEqual(0, optionInt1.CompareTo((object)optionInt2));
            Assert.AreEqual(0, optionInt2.CompareTo(optionInt1));
            Assert.AreEqual(0, optionInt2.CompareTo((object)optionInt1));
            Assert.IsFalse(optionInt1 < optionInt2);
            Assert.IsTrue(optionInt1 <= optionInt2);
            Assert.IsFalse(optionInt1 > optionInt2);
            Assert.IsTrue(optionInt1 >= optionInt2);

            Assert.AreEqual(-1, optionInt1.CompareTo(optionInt3));
            Assert.AreEqual(-1, optionInt1.CompareTo((object)optionInt3));
            Assert.AreEqual(1, optionInt3.CompareTo(optionInt1));
            Assert.AreEqual(1, optionInt3.CompareTo((object)optionInt1));
            Assert.IsTrue(optionInt1 < optionInt3);
            Assert.IsTrue(optionInt1 <= optionInt3);
            Assert.IsFalse(optionInt1 > optionInt3);
            Assert.IsFalse(optionInt1 >= optionInt3);

            Assert.AreEqual(1, optionInt1.CompareTo(emptyOptionInt1));
            Assert.AreEqual(1, optionInt1.CompareTo((object)emptyOptionInt1));
            Assert.AreEqual(-1, emptyOptionInt1.CompareTo(optionInt1));
            Assert.AreEqual(-1, emptyOptionInt1.CompareTo((object)optionInt1));
            Assert.IsFalse(optionInt1 < emptyOptionInt1);
            Assert.IsFalse(optionInt1 <= emptyOptionInt1);
            Assert.IsTrue(optionInt1 > emptyOptionInt1);
            Assert.IsTrue(optionInt1 >= emptyOptionInt1);

            Assert.AreEqual(0, emptyOptionInt1.CompareTo(emptyOptionInt2));
            Assert.AreEqual(0, emptyOptionInt1.CompareTo((object)emptyOptionInt2));
            Assert.AreEqual(0, emptyOptionInt2.CompareTo(emptyOptionInt1));
            Assert.AreEqual(0, emptyOptionInt2.CompareTo((object)emptyOptionInt1));
            Assert.IsFalse(emptyOptionInt1 < emptyOptionInt2);
            Assert.IsTrue(emptyOptionInt1 <= emptyOptionInt2);
            Assert.IsFalse(emptyOptionInt1 > emptyOptionInt2);
            Assert.IsTrue(emptyOptionInt1 >= emptyOptionInt2);

            // Mixed
            Assert.AreEqual(1, optionInt1.CompareTo(null));      // Null initialize a Option.None
            Assert.AreEqual(0, emptyOptionInt1.CompareTo(null)); // Null initialize a Option.None
            Assert.AreEqual(1, optionInt1.CompareTo((object)null));
            Assert.AreEqual(0, emptyOptionInt1.CompareTo((object)null));

            var optionFloat = Option<float>.Some(12.1f);
            var emptyOptionFloat = Option<float>.None;
            Assert.Throws<ArgumentException>(() => { var _ = optionInt1.CompareTo(optionFloat); });
            Assert.Throws<ArgumentException>(() => { var _ = optionFloat.CompareTo(optionInt1); });

            Assert.Throws<ArgumentException>(() => { var _ = optionInt1.CompareTo(emptyOptionFloat); });
            Assert.Throws<ArgumentException>(() => { var _ = emptyOptionFloat.CompareTo(optionInt1); });
        }

        [Test]
        public void OptionCompareHelper()
        {
            // Option value type
            var optionInt1 = Option<int>.Some(12);
            var optionInt2 = Option<int>.Some(12);
            var optionInt3 = Option<int>.Some(42);
            var emptyOptionInt1 = Option<int>.None;
            var emptyOptionInt2 = Option<int>.None;

            Assert.AreEqual(0, OptionHelpers.Compare(optionInt1, optionInt1));

            Assert.AreEqual(0, OptionHelpers.Compare(optionInt1, optionInt2));
            Assert.AreEqual(0, OptionHelpers.Compare(optionInt2, optionInt1));

            Assert.AreEqual(-1, OptionHelpers.Compare(optionInt1, optionInt3));
            Assert.AreEqual(1, OptionHelpers.Compare(optionInt3, optionInt1));

            Assert.AreEqual(1, OptionHelpers.Compare(optionInt1, emptyOptionInt1));
            Assert.AreEqual(-1, OptionHelpers.Compare(emptyOptionInt1, optionInt1));

            Assert.AreEqual(0, OptionHelpers.Compare(emptyOptionInt1, emptyOptionInt2));
            Assert.AreEqual(0, OptionHelpers.Compare(emptyOptionInt2, emptyOptionInt1));
        }

        [Test]
        public void OptionHashCode()
        {
            // Equals values
            Option<Person> p1 = new Person("Foo Bar");
            Option<Person> p2 = new Person("Foo Bar");
            Assert.AreNotSame(p1, p2);
            Assert.IsTrue(p1.Equals(p2));
            Assert.IsTrue(p2.Equals(p1));
            Assert.IsTrue(p1.GetHashCode() == p2.GetHashCode());

            // Different values
            Option<Person> p3 = new Person("Bar Foo");
            Assert.AreNotSame(p1, p3);
            Assert.IsFalse(p1.Equals(p3));
            Assert.IsFalse(p3.Equals(p1));
            Assert.IsFalse(p1.GetHashCode() == p3.GetHashCode());

            // Empty option
            Option<Person> empty = Option.None;
            Assert.AreNotSame(p1, empty);
            Assert.IsFalse(p1.Equals(empty));
            Assert.IsFalse(empty.Equals(p1));
            Assert.IsFalse(p1.GetHashCode() == empty.GetHashCode());
        }

        [Test]
        public void OptionToString()
        {
            // Option value type
            var optionInt = Option<int>.Some(12);
            Assert.AreEqual("12", optionInt.ToString());

            var emptyOptionInt = Option<int>.None;
            Assert.AreEqual("None", emptyOptionInt.ToString());

            // Option reference type
            var optionClass = Option<TestClass>.Some(new TestClass { TestInt = 42 });
            Assert.AreEqual("TestClass: 42", optionClass.ToString());

            var emptyOptionClass = Option<TestClass>.None;
            Assert.AreEqual("None", emptyOptionClass.ToString());
        }
    }
}
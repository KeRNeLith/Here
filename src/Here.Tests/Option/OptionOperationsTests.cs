using System;
using NUnit.Framework;

namespace Here.Tests.Options
{
    /// <summary>
    /// Tests for <see cref="Option{T}"/> operations.
    /// </summary>
    [TestFixture]
    internal class OptionOperationsTests : OptionTestsBase
    {
        [Test]
        public void OptionIf()
        {
            int counter = 0;
            var optionInt = Option<int>.Some(12);
            optionInt.If(value => ++counter);
            Assert.AreEqual(1, counter);

            var emptyOptionInt = Option<int>.None;
            emptyOptionInt.If(value => ++counter);
            Assert.AreEqual(1, counter);

            Assert.Throws<ArgumentNullException>(() => optionInt.If(null));
        }

        [Test]
        public void OptionElse()
        {
            int counter = 0;
            var optionInt = Option<int>.Some(12);
            optionInt.Else(() => ++counter);
            Assert.AreEqual(0, counter);

            var emptyOptionInt = Option<int>.None;
            emptyOptionInt.Else(() => ++counter);
            Assert.AreEqual(1, counter);

            Assert.Throws<ArgumentNullException>(() => optionInt.Else(null));
        }

        [Test]
        public void OptionIfElse()
        {
            int counterIf = 0;
            int counterElse = 0;
            var optionInt = Option<int>.Some(12);
            optionInt.IfElse(
                value => 
                {
                    ++counterIf;
                }, 
                () => ++counterElse);
            Assert.AreEqual(1, counterIf);
            Assert.AreEqual(0, counterElse);

            var emptyOptionInt = Option<int>.None;
            emptyOptionInt.IfElse(
                value =>
                {
                    ++counterIf;
                }, 
                () => ++counterElse);
            Assert.AreEqual(1, counterIf);
            Assert.AreEqual(1, counterElse);

            Assert.Throws<ArgumentNullException>(() => optionInt.IfElse(null, null));
        }

        [Test]
        public void OptionIfElseReturnResult()
        {
            int counterIf = 0;
            int counterElse = 0;
            var optionInt = Option<int>.Some(12);
            Assert.AreEqual(42.2f, optionInt.IfElse(
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

            var emptyOptionInt = Option<int>.None;
            Assert.AreEqual(12.2f, emptyOptionInt.IfElse(
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

            Assert.Throws<ArgumentNullException>(() => optionInt.IfElse(val => 12.2f, null));
            Assert.Throws<ArgumentNullException>(() => optionInt.IfElse(null, () => 12.2f));
            Assert.Throws<ArgumentNullException>(() => optionInt.IfElse(null, null));
        }

        [Test]
        public void OptionIfOrReturnResult()
        {
            int counterIf = 0;
            var optionInt = Option<int>.Some(12);
            Assert.AreEqual(42.2f, optionInt.IfOr(
                value =>
                {
                    ++counterIf;
                    return 42.2f;
                },
                12.2f));
            Assert.AreEqual(1, counterIf);

            var emptyOptionInt = Option<int>.None;
            Assert.AreEqual(12.2f, emptyOptionInt.IfOr(
                value =>
                {
                    ++counterIf;
                    return 42.2f;
                },
                12.2f));
            Assert.AreEqual(1, counterIf);

            Assert.Throws<ArgumentNullException>(() => optionInt.IfOr(null, 1));
            var optionPerson = Option<Person>.Some(new Person("Test"));
            Assert.Throws<ArgumentNullException>(() => optionPerson.IfOr(person => person, null));
            Assert.Throws<ArgumentNullException>(() => optionPerson.IfOr(null, (Person)null));
        }

        [Test]
        public void OptionElseOrReturnResult()
        {
            int counterElse = 0;
            var optionInt = Option<int>.Some(12);
            Assert.AreEqual(42.2f, optionInt.ElseOr(
                () =>
                {
                    ++counterElse;
                    return 12.2f;
                },
                42.2f));
            Assert.AreEqual(0, counterElse);

            var emptyOptionInt = Option<int>.None;
            Assert.AreEqual(12.2f, emptyOptionInt.ElseOr(
                () =>
                {
                    ++counterElse;
                    return 12.2f;
                },
                42.2f));
            Assert.AreEqual(1, counterElse);

            Assert.Throws<ArgumentNullException>(() => optionInt.ElseOr(null, 1));
            var optionPerson = Option<Person>.Some(new Person("Test"));
            Assert.Throws<ArgumentNullException>(() => optionPerson.ElseOr(() => (Person)null, null));
            Assert.Throws<ArgumentNullException>(() => optionPerson.ElseOr(null, (Person)null));
        }

        [Test]
        public void OptionOr()
        {
            // Option with value
            var optionInt = Option<int>.Some(12);
            Assert.AreEqual(12, optionInt.Or(25));
            Assert.AreEqual(12, optionInt.Or(() => 25));

            Option<int> optionResult = optionInt.Or(() => 25);
            CheckOptionValue(optionResult, 12);

            optionResult = optionInt.Or(() => Option<int>.None);
            CheckOptionValue(optionResult, 12);

            optionResult = optionInt.Or(() => null);
            CheckOptionValue(optionResult, 12);

            Assert.AreEqual(12, optionInt.OrDefault());

            // Empty option
            Option<int> emptyOptionInt = Option.None;
            Assert.AreEqual(42, emptyOptionInt.Or(42));
            Assert.AreEqual(42, emptyOptionInt.Or(() => 42));
            optionResult = emptyOptionInt.Or(() => 42);
            CheckOptionValue(optionResult, 42);

            optionResult = emptyOptionInt.Or(() => Option<int>.None);
            CheckEmptyOption(optionResult);

            optionResult = emptyOptionInt.Or(() => null);
            CheckEmptyOption(optionResult);

            Assert.AreEqual(0, emptyOptionInt.OrDefault());

            // Option class with value
            var testObject = new TestClass();
            var defaultTestObject = new TestClass { TestInt = 12 };
            var optionClass = Option<TestClass>.Some(testObject);
            Assert.AreSame(testObject, optionClass.Or(defaultTestObject));
            Assert.AreSame(testObject, optionClass.Or(() => defaultTestObject));

            Option<TestClass> optionResultClass = optionClass.Or(() => defaultTestObject);
            CheckOptionSameValue(optionResultClass, testObject);

            optionResultClass = optionClass.Or(() => Option<TestClass>.None);
            CheckOptionSameValue(optionResultClass, testObject);

            optionResultClass = optionClass.Or(() => null);
            CheckOptionSameValue(optionResultClass, testObject);

            Assert.AreSame(testObject, optionClass.OrDefault());

            // Empty option class
            Option<TestClass> emptyOptionClass = Option.None;
            Assert.AreSame(defaultTestObject, emptyOptionClass.Or(defaultTestObject));
            Assert.AreSame(defaultTestObject, emptyOptionClass.Or(() => defaultTestObject));

            optionResultClass = emptyOptionClass.Or(() => defaultTestObject);
            CheckOptionSameValue(optionResultClass, defaultTestObject);

            optionResultClass = emptyOptionClass.Or(() => Option<TestClass>.None);
            CheckEmptyOption(optionResultClass);

            Assert.Throws<ArgumentNullException>(() => optionClass.Or((TestClass)null));
            Assert.Throws<ArgumentNullException>(() => optionClass.Or((Func<TestClass>)null));
            optionResultClass = optionClass.Or(() => null);     // We don't detect the null return of the factory,
                                                                // and in this case we will not run the function if it is not needed
            CheckOptionSameValue(optionResultClass, testObject);
            Assert.Throws<NullResultException>(() => emptyOptionClass.Or(() => null));
            Assert.Throws<ArgumentNullException>(() => optionInt.Or((Func<Option<int>>)null));
            Assert.Throws<ArgumentNullException>(() => optionInt.Or((Func<Option<int>>)null));

            Assert.AreEqual(null, emptyOptionClass.OrDefault());
        }

        [Test]
        public void OptionIfOrThrows()
        {
            // Option with value
            var optionInt = Option<int>.Some(12);
            Assert.DoesNotThrow(() =>
            {
                float result = optionInt.IfOrThrows(value => 12.5f, new InvalidOperationException());
                Assert.AreEqual(12.5f, result);
            });
            Assert.DoesNotThrow(() =>
            {
                float result = optionInt.IfOrThrows(value => 12.6f, () => new InvalidOperationException());
                Assert.AreEqual(12.6f, result);
            });
            Assert.DoesNotThrow(() => optionInt.IfOrThrows(value => { }, new InvalidOperationException()));
            Assert.DoesNotThrow(() => optionInt.IfOrThrows(value => { }, () => new InvalidOperationException()));

            // Empty option
            Option<int> emptyOptionInt = Option.None;
            Assert.Throws<InvalidOperationException>(() =>
            {
                float result = emptyOptionInt.IfOrThrows(value => 12.5f, new InvalidOperationException());
                Assert.AreEqual(12.5f, result);
            });
            Assert.Throws<InvalidOperationException>(() =>
            {
                float result = emptyOptionInt.IfOrThrows(value => 12.6f, () => new InvalidOperationException());
                Assert.AreEqual(12.6f, result);
            });
            Assert.Throws<InvalidOperationException>(() => emptyOptionInt.IfOrThrows(value => { }, new InvalidOperationException()));
            Assert.Throws<InvalidOperationException>(() => emptyOptionInt.IfOrThrows(value => { }, () => new InvalidOperationException()));

            Assert.Throws<ArgumentNullException>(() => optionInt.IfOrThrows(value => 12, (Exception)null));
            Assert.Throws<ArgumentNullException>(() => optionInt.IfOrThrows(null, new InvalidOperationException()));
            Assert.Throws<ArgumentNullException>(() => optionInt.IfOrThrows(null, (Exception)null));
            Assert.Throws<ArgumentNullException>(() => optionInt.IfOrThrows(value => 12, (Func<Exception>)null));
            Assert.Throws<ArgumentNullException>(() => optionInt.IfOrThrows(null, () => new InvalidOperationException()));
            Assert.Throws<ArgumentNullException>(() => optionInt.IfOrThrows(null, (Func<Exception>)null));

            Assert.Throws<ArgumentNullException>(() => optionInt.IfOrThrows(value => { }, (Exception)null));
            Assert.Throws<ArgumentNullException>(() => optionInt.IfOrThrows(null, new InvalidOperationException()));
            Assert.Throws<ArgumentNullException>(() => optionInt.IfOrThrows(null, (Exception)null));
            Assert.Throws<ArgumentNullException>(() => optionInt.IfOrThrows(value => { }, (Func<Exception>)null));
            Assert.Throws<ArgumentNullException>(() => optionInt.IfOrThrows(null, () => new InvalidOperationException()));
            Assert.Throws<ArgumentNullException>(() => optionInt.IfOrThrows(null, (Func<Exception>)null));
        }

        [Test]
        public void OptionOrThrows()
        {
            // Option with value
            var optionInt = Option<int>.Some(12);
            Assert.DoesNotThrow(() => optionInt.OrThrows(new InvalidOperationException()));
            Assert.DoesNotThrow(() => optionInt.OrThrows(() => new InvalidOperationException()));
            Assert.AreEqual(12, optionInt.OrThrows(new InvalidOperationException()));

            // Empty option
            Option<int> emptyOptionInt = Option.None;
            Assert.Throws<InvalidOperationException>(() => emptyOptionInt.OrThrows(new InvalidOperationException()));
            Assert.Throws<InvalidOperationException>(() => emptyOptionInt.OrThrows(() => new InvalidOperationException()));

            Assert.Throws<ArgumentNullException>(() => emptyOptionInt.OrThrows((Exception)null));
            Assert.Throws<ArgumentNullException>(() => emptyOptionInt.OrThrows((Func<Exception>)null));
        }

        [Test]
        public void OptionUnwrap()
        {
            // Option with value
            var optionInt = Option<int>.Some(12);
            Assert.AreEqual(12, optionInt.Unwrap());
            Assert.AreEqual(12, optionInt.Unwrap(25));
            Assert.AreEqual(12, optionInt.Unwrap(() => 26));
            Assert.AreEqual(25.5f, optionInt.Unwrap(value => 25.5f));
            Assert.AreEqual(25.5f, optionInt.Unwrap(value => 25.5f, 15.1f));
            Assert.AreEqual(25.5f, optionInt.Unwrap(value => 25.5f, () => 15.2f));

            // Empty option
            Option<int> emptyOptionInt = Option.None;
            Assert.AreEqual(0, emptyOptionInt.Unwrap());
            Assert.AreEqual(25, emptyOptionInt.Unwrap(25));
            Assert.AreEqual(26, emptyOptionInt.Unwrap(() => 26));
            Assert.AreEqual(0, emptyOptionInt.Unwrap(value => 25.5f));
            Assert.AreEqual(15.1f, emptyOptionInt.Unwrap(value => 25.5f, 15.1f));
            Assert.AreEqual(15.2f, emptyOptionInt.Unwrap(value => 25.5f, () => 15.2f));

            // Option class with value
            var testObject = new TestClass();
            var defaultTestObject = new TestClass { TestInt = 12 };
            var optionClass = Option<TestClass>.Some(testObject);
            Assert.AreSame(testObject, optionClass.Unwrap());
            Assert.AreSame(testObject, optionClass.Unwrap(defaultTestObject));
            Assert.AreSame(testObject, optionClass.Unwrap(() => defaultTestObject));
            Assert.AreEqual(25.5f, optionClass.Unwrap(value => 25.5f));
            Assert.AreEqual(25.5f, optionClass.Unwrap(value => 25.5f, 15.1f));
            Assert.AreEqual(25.5f, optionClass.Unwrap(value => 25.5f, () => 15.2f));

            // Empty option class
            Option<TestClass> emptyOptionClass = Option.None;
            Assert.AreSame(null, emptyOptionClass.Unwrap());
            Assert.AreSame(defaultTestObject, emptyOptionClass.Unwrap(defaultTestObject));
            Assert.AreSame(null, emptyOptionClass.Unwrap(() => null));
            Assert.AreSame(defaultTestObject, emptyOptionClass.Unwrap(() => defaultTestObject));
            Assert.AreEqual(null, emptyOptionClass.Unwrap(value => (TestClass)null));
            Assert.AreEqual(0.0f, emptyOptionClass.Unwrap(value => 25.5f));
            Assert.AreEqual(15.1f, emptyOptionClass.Unwrap(value => 25.5f, 15.1f));
            Assert.AreEqual(null, emptyOptionClass.Unwrap(value => null, (TestClass)null));
            Assert.AreEqual(15.2f, emptyOptionClass.Unwrap(value => 25.5f, () => 15.2f));
            Assert.AreEqual(null, emptyOptionClass.Unwrap(value => (TestClass)null, () => null));

            Assert.Throws<ArgumentNullException>(() => optionInt.Unwrap(null));
            Assert.Throws<ArgumentNullException>(() => optionInt.Unwrap(null, 12.2f));
            Assert.Throws<ArgumentNullException>(() => optionInt.Unwrap(null, () => 12.2f));
            Assert.Throws<ArgumentNullException>(() => optionInt.Unwrap(val => 12.2f, null));
            Assert.Throws<ArgumentNullException>(() => optionInt.Unwrap((Func<int, float>)null, null));
        }

        [Test]
        public void OptionCast()
        {
            // Value type
            // Option with value
            var optionInt = Option<int>.Some(12);
            var optionFloat = optionInt.Cast(intVal => intVal + 0.1f);
            CheckOptionValue(optionFloat, 12.1f);

            // Empty option
            Option<int> emptyOptionInt = Option.None;
            optionFloat = emptyOptionInt.Cast(intVal => intVal + 0.1f);
            CheckEmptyOption(optionFloat);


            // Reference type
            // Option with value
            var testObjectLeaf = new TestClassLeaf { TestInt = 12 };
            var optionClassLeaf = Option<TestClassLeaf>.Some(testObjectLeaf);
            optionFloat = optionClassLeaf.Cast(obj => obj.TestInt + 0.2f);
            CheckOptionValue(optionFloat, 12.2f);

            var optionClass = optionClassLeaf.Cast<TestClass>();
            CheckOptionSameValue(optionClass, testObjectLeaf);

            // Invalid conversion
            var testObject = new TestClass { TestInt = 42 };
            optionClass = Option<TestClass>.Some(testObject);
            optionClassLeaf = optionClass.Cast<TestClassLeaf>();
            CheckEmptyOption(optionClassLeaf);

            // Empty option
            Option<TestClassLeaf> emptyOptionClassLeaf = Option.None;
            optionFloat = emptyOptionClassLeaf.Cast(obj => obj.TestInt + 0.2f);
            CheckEmptyOption(optionFloat);

            optionClass = emptyOptionClassLeaf.Cast<TestClass>();
            CheckEmptyOption(optionClass);

            Assert.Throws<ArgumentNullException>(() => optionInt.Cast<float>(null));
        }

        [Test]
        public void OptionExists()
        {
            // Option with value
            var optionInt = Option<int>.Some(12);
            Assert.IsTrue(optionInt.Exists(intValue => intValue == 12));
            Assert.IsFalse(optionInt.Exists(intValue => intValue == 13));

            // Empty option
            Option<int> emptyOptionInt = Option.None;
            Assert.IsFalse(emptyOptionInt.Exists(intValue => intValue == 12));

            Assert.Throws<ArgumentNullException>(() => optionInt.Exists(null));
        }

        [Test]
        public void OptionNoneIf()
        {
            // Option with value
            var optionInt = Option<int>.Some(12);
            CheckOptionValue(optionInt.NoneIf(intValue => intValue > 12), 12);
            CheckEmptyOption(optionInt.NoneIf(intValue => intValue >= 12));

            CheckOptionValue(optionInt.NoneIf(intValue => intValue > 12, intValue => intValue + 0.5f), 12.5f);
            CheckEmptyOption(optionInt.NoneIf(intValue => intValue >= 12, intValue => intValue + 0.5f));

            // Empty option
            Option<int> emptyOptionInt = Option.None;
            CheckEmptyOption(emptyOptionInt.NoneIf(intValue => intValue == 42));
            CheckEmptyOption(emptyOptionInt.NoneIf(intValue => intValue > 12, intValue => intValue + 0.5f));

            Assert.Throws<ArgumentNullException>(() => optionInt.NoneIf(null));
            Assert.Throws<ArgumentNullException>(() => optionInt.NoneIf(intValue => intValue > 12, (Func<int, float>)null));
            Assert.Throws<ArgumentNullException>(() => optionInt.NoneIf(null, intValue => intValue + 0.5f));
            Assert.Throws<ArgumentNullException>(() => optionInt.NoneIf(null, (Func<int, float>)null));
        }
    }
}

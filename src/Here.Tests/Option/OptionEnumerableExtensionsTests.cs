using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Options
{
    /// <summary>
    /// Tests for <see cref="Option{T}"/> extensions related to <see cref="IEnumerable{T}"/>.
    /// </summary>
    [TestFixture]
    internal class OptionEnumerableExtensionsTests : OptionTestsBase
    {
        #region Test property

        private IEnumerable<int> Items
        {
            get
            {
                yield return 1;
                yield return 12;
                yield return 13;
                yield return 8;
            }
        }

        #endregion

        [Test]
        public void EnumerableFirstOrNone()
        {
            // Value type
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 3 };
            var optionInt = enumerableInts.FirstOrNone();
            CheckOptionValue(optionInt, 1);
            optionInt = enumerableInts.FirstOrNone(value => value == 2);
            CheckOptionValue(optionInt, 2);

            optionInt = enumerableInts.FirstOrNone(value => value == 4);
            CheckEmptyOption(optionInt);
            
            IEnumerable<int> enumerableInts2 = new List<int>();
            optionInt = enumerableInts2.FirstOrNone();
            CheckEmptyOption(optionInt);
            optionInt = enumerableInts2.FirstOrNone(value => value == 2);
            CheckEmptyOption(optionInt);

            // Nullables
            IEnumerable<int?> enumerableNullableInts = new List<int?> { 1, 2, 3 };
            var optionNullable = enumerableNullableInts.FirstOrNone();
            CheckOptionValue(optionNullable, 1);
            optionNullable = enumerableNullableInts.FirstOrNone(value => value.HasValue && value.Value == 2);
            CheckOptionValue(optionNullable, 2);

            optionNullable = enumerableNullableInts.FirstOrNone(value => value == 4);
            CheckEmptyOption(optionNullable);

            IEnumerable<int?> enumerableNullableInts2 = new List<int?> { null, null };
            optionNullable = enumerableNullableInts2.FirstOrNone();
            CheckEmptyOption(optionNullable);
            optionNullable = enumerableNullableInts2.FirstOrNone(value => value.HasValue && value.Value == 2);
            CheckEmptyOption(optionNullable);

            IEnumerable<int?> enumerableNullableInts3 = new List<int?>();
            optionNullable = enumerableNullableInts3.FirstOrNone();
            CheckEmptyOption(optionNullable);
            optionNullable = enumerableNullableInts3.FirstOrNone(value => value.HasValue && value.Value == 2);
            CheckEmptyOption(optionNullable);

            // Reference type
            var testObj1 = new TestClass();
            var testObj2 = new TestClass();
            IEnumerable<TestClass> enumerableTestClass = new List<TestClass> { testObj1, testObj2 };
            var optionClass = enumerableTestClass.FirstOrNone();
            CheckOptionSameValue(optionClass, testObj1);
            optionClass = enumerableTestClass.FirstOrNone(value => ReferenceEquals(testObj2, value));
            CheckOptionSameValue(optionClass, testObj2);

            var testObj3 = new TestClass();
            optionClass = enumerableTestClass.FirstOrNone(value => ReferenceEquals(testObj3, value));
            CheckEmptyOption(optionClass);

            IEnumerable<TestClass> enumerableTestClass2 = new List<TestClass> { null, null };
            optionClass = enumerableTestClass2.FirstOrNone();
            CheckEmptyOption(optionClass);
            optionClass = enumerableTestClass2.FirstOrNone(value => ReferenceEquals(testObj1, value));
            CheckEmptyOption(optionClass);

            IEnumerable<TestClass> enumerableTestClass3 = new List<TestClass>();
            optionClass = enumerableTestClass3.FirstOrNone();
            CheckEmptyOption(optionClass);
            optionClass = enumerableTestClass3.FirstOrNone(value => ReferenceEquals(testObj1, value));
            CheckEmptyOption(optionClass);
        }

        [Test]
        public void EnumerableSingleOrNone()
        {
            // Test for value type
            // Several values
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 2, 3 };
            Assert.Throws<InvalidOperationException>(() => { var _ = enumerableInts.SingleOrNone(); });
            CheckEmptyOption(enumerableInts.SingleOrNone(false));

            Assert.Throws<InvalidOperationException>(() => { var _ = enumerableInts.SingleOrNone(value => value == 2); });
            CheckEmptyOption(enumerableInts.SingleOrNone(value => value == 2, false));

            CheckOptionValue(enumerableInts.SingleOrNone(value => value == 3), 3);
            CheckOptionValue(enumerableInts.SingleOrNone(value => value == 3, false), 3);

            CheckEmptyOption(enumerableInts.SingleOrNone(value => value == 4));
            CheckEmptyOption(enumerableInts.SingleOrNone(value => value == 4, false));

            // One value
            IEnumerable<int> enumerableInts2 = new List<int> { 12 };
            CheckOptionValue(enumerableInts2.SingleOrNone(), 12);
            CheckOptionValue(enumerableInts2.SingleOrNone(false), 12);

            CheckOptionValue(enumerableInts2.SingleOrNone(value => value == 12), 12);
            CheckOptionValue(enumerableInts2.SingleOrNone(value => value == 12, false), 12);

            CheckEmptyOption(enumerableInts2.SingleOrNone(value => value == 4));
            CheckEmptyOption(enumerableInts2.SingleOrNone(value => value == 4, false));

            // No value
            IEnumerable<int> enumerableInts3 = new List<int>();
            CheckEmptyOption(enumerableInts3.SingleOrNone());
            CheckEmptyOption(enumerableInts3.SingleOrNone(false));

            CheckEmptyOption(enumerableInts3.SingleOrNone(value => value == 4));
            CheckEmptyOption(enumerableInts3.SingleOrNone(value => value == 4, false));

            // Test for reference type
            // Several values
            var testObj1 = new TestClass();
            var testObj2 = new TestClass();
            IEnumerable<TestClass> enumerableTestClass = new List<TestClass> { testObj1, testObj1, testObj2 };

            Assert.Throws<InvalidOperationException>(() => { var _ = enumerableTestClass.SingleOrNone(); });
            CheckEmptyOption(enumerableTestClass.SingleOrNone(false));

            Assert.Throws<InvalidOperationException>(() => { var _ = enumerableTestClass.SingleOrNone(value => ReferenceEquals(testObj1, value)); });
            CheckEmptyOption(enumerableTestClass.SingleOrNone(value => ReferenceEquals(testObj1, value), false));

            CheckOptionSameValue(enumerableTestClass.SingleOrNone(value => ReferenceEquals(testObj2, value)), testObj2);
            CheckOptionSameValue(enumerableTestClass.SingleOrNone(value => ReferenceEquals(testObj2, value), false), testObj2);

            var testObj3 = new TestClass();
            CheckEmptyOption(enumerableTestClass.SingleOrNone(value => ReferenceEquals(testObj3, value)));
            CheckEmptyOption(enumerableTestClass.SingleOrNone(value => ReferenceEquals(testObj3, value), false));

            // One value
            IEnumerable<TestClass> enumerableTestClass2 = new List<TestClass> { testObj1 };

            CheckOptionSameValue(enumerableTestClass2.SingleOrNone(), testObj1);
            CheckOptionSameValue(enumerableTestClass2.SingleOrNone(false), testObj1);

            CheckOptionSameValue(enumerableTestClass2.SingleOrNone(value => ReferenceEquals(testObj1, value)), testObj1);
            CheckOptionSameValue(enumerableTestClass2.SingleOrNone(value => ReferenceEquals(testObj1, value), false), testObj1);

            CheckEmptyOption(enumerableTestClass2.SingleOrNone(value => ReferenceEquals(testObj2, value)));
            CheckEmptyOption(enumerableTestClass2.SingleOrNone(value => ReferenceEquals(testObj2, value), false));

            // Null values
            IEnumerable<TestClass> enumerableTestClass3 = new List<TestClass> { null, null };

            Assert.Throws<InvalidOperationException>(() => { var _ = enumerableTestClass3.SingleOrNone(); });
            CheckEmptyOption(enumerableTestClass3.SingleOrNone(false));

            CheckEmptyOption(enumerableTestClass3.SingleOrNone(value => ReferenceEquals(testObj1, value)));
            CheckEmptyOption(enumerableTestClass3.SingleOrNone(value => ReferenceEquals(testObj1, value), false));

            Assert.Throws<InvalidOperationException>(() => { var _ = enumerableTestClass3.SingleOrNone(value => value is null); });
            CheckEmptyOption(enumerableTestClass3.SingleOrNone(value => value is null, false));

            // No value
            IEnumerable<TestClass> enumerableTestClass4 = new List<TestClass>();

            CheckEmptyOption(enumerableTestClass4.SingleOrNone());
            CheckEmptyOption(enumerableTestClass4.SingleOrNone(false));

            CheckEmptyOption(enumerableTestClass4.SingleOrNone(value => ReferenceEquals(testObj1, value)));
            CheckEmptyOption(enumerableTestClass4.SingleOrNone(value => ReferenceEquals(testObj1, value), false));
        }

        [Test]
        public void EnumerableLastOrNone()
        {
            // Value type
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 3 };
            var optionInt = enumerableInts.LastOrNone();
            CheckOptionValue(optionInt, 3);
            optionInt = enumerableInts.LastOrNone(value => value == 1);
            CheckOptionValue(optionInt, 1);

            optionInt = enumerableInts.LastOrNone(value => value == 4);
            CheckEmptyOption(optionInt);

            IEnumerable<int> enumerableInts2 = new List<int>();
            optionInt = enumerableInts2.LastOrNone();
            CheckEmptyOption(optionInt);
            optionInt = enumerableInts2.LastOrNone(value => value == 2);
            CheckEmptyOption(optionInt);

            // Nullables
            IEnumerable<int?> enumerableNullableInts = new List<int?> { 1, 2, 3 };
            var optionNullable = enumerableNullableInts.LastOrNone();
            CheckOptionValue(optionNullable, 3);
            optionNullable = enumerableNullableInts.LastOrNone(value => value.HasValue && value.Value == 1);
            CheckOptionValue(optionNullable, 1);

            optionNullable = enumerableNullableInts.LastOrNone(value => value.HasValue && value.Value == 4);
            CheckEmptyOption(optionNullable);

            IEnumerable<int?> enumerableNullableInts2 = new List<int?> { null, null };
            optionNullable = enumerableNullableInts2.LastOrNone();
            CheckEmptyOption(optionNullable);
            optionNullable = enumerableNullableInts2.LastOrNone(value => value.HasValue && value.Value == 2);
            CheckEmptyOption(optionNullable);

            IEnumerable<int?> enumerableNullableInts3 = new List<int?>();
            optionNullable = enumerableNullableInts3.LastOrNone();
            CheckEmptyOption(optionNullable);
            optionNullable = enumerableNullableInts3.LastOrNone(value => value.HasValue && value.Value == 2);
            CheckEmptyOption(optionNullable);

            // Reference type
            var testObj1 = new TestClass();
            var testObj2 = new TestClass();
            IEnumerable<TestClass> enumerableTestClass = new List<TestClass> { testObj1, testObj2 };
            var optionClass = enumerableTestClass.LastOrNone();
            CheckOptionSameValue(optionClass, testObj2);
            optionClass = enumerableTestClass.LastOrNone(value => ReferenceEquals(testObj1, value));
            CheckOptionSameValue(optionClass, testObj1);

            var testObj3 = new TestClass();
            optionClass = enumerableTestClass.LastOrNone(value => ReferenceEquals(testObj3, value));
            CheckEmptyOption(optionClass);

            IEnumerable<TestClass> enumerableTestClass2 = new List<TestClass> { null, null };
            optionClass = enumerableTestClass2.LastOrNone();
            CheckEmptyOption(optionClass);
            optionClass = enumerableTestClass2.LastOrNone(value => ReferenceEquals(testObj1, value));
            CheckEmptyOption(optionClass);

            IEnumerable<TestClass> enumerableTestClass3 = new List<TestClass>();
            optionClass = enumerableTestClass3.LastOrNone();
            CheckEmptyOption(optionClass);
            optionClass = enumerableTestClass3.LastOrNone(value => ReferenceEquals(testObj1, value));
            CheckEmptyOption(optionClass);
        }

        [Test]
        public void EnumerableElementAtOrNone()
        {
            // Value type
            IList<int> listInts = new List<int> { 1, 2, 3 };
            CheckEmptyOption(listInts.ElementAtOrNone(-1));
            CheckOptionValue(listInts.ElementAtOrNone(0), 1);
            CheckOptionValue(listInts.ElementAtOrNone(2), 3);
            CheckEmptyOption(listInts.ElementAtOrNone(3));

            IEnumerable<int> enumerableInts = new ReadOnlyCollection<int>(listInts);
            CheckEmptyOption(enumerableInts.ElementAtOrNone(-1));
            CheckOptionValue(enumerableInts.ElementAtOrNone(0), 1);
            CheckOptionValue(enumerableInts.ElementAtOrNone(2), 3);
            CheckEmptyOption(enumerableInts.ElementAtOrNone(3));

            IEnumerable<int> enumerableInts2 = new List<int>();
            CheckEmptyOption(enumerableInts2.ElementAtOrNone(-1));
            CheckEmptyOption(enumerableInts2.ElementAtOrNone(0));

            CheckEmptyOption(Items.ElementAtOrNone(-1));
            CheckOptionValue(Items.ElementAtOrNone(0), 1);
            CheckOptionValue(Items.ElementAtOrNone(1), 12);
            CheckOptionValue(Items.ElementAtOrNone(3), 8);
            CheckEmptyOption(Items.ElementAtOrNone(5));

            // Reference type
            var testObject1 = new TestClass();
            var testObject2 = new TestClass { TestInt = 12 };
            IList<TestClass> listTestClasses = new List<TestClass> { testObject1, testObject2 };
            CheckEmptyOption(listTestClasses.ElementAtOrNone(-1));
            CheckOptionSameValue(listTestClasses.ElementAtOrNone(0), testObject1);
            CheckOptionSameValue(listTestClasses.ElementAtOrNone(1), testObject2);
            CheckEmptyOption(listTestClasses.ElementAtOrNone(2));

            IEnumerable<TestClass> enumerableTestClasses = new ReadOnlyCollection<TestClass>(listTestClasses);
            CheckEmptyOption(enumerableTestClasses.ElementAtOrNone(-1));
            CheckOptionSameValue(enumerableTestClasses.ElementAtOrNone(0), testObject1);
            CheckOptionSameValue(enumerableTestClasses.ElementAtOrNone(1), testObject2);
            CheckEmptyOption(enumerableTestClasses.ElementAtOrNone(2));

            IEnumerable<TestClass> enumerableTestClasses2 = new List<TestClass>();
            CheckEmptyOption(enumerableTestClasses2.ElementAtOrNone(-1));
            CheckEmptyOption(enumerableTestClasses2.ElementAtOrNone(0));

            IEnumerable<TestClass> enumerableTestClasses3 = new List<TestClass> { testObject1, null, testObject2 };
            CheckEmptyOption(enumerableTestClasses3.ElementAtOrNone(-1));
            CheckOptionSameValue(enumerableTestClasses3.ElementAtOrNone(0), testObject1);
            // Even if element [1] exists, it's value is null so it will result in an empty option
            CheckEmptyOption(enumerableTestClasses3.ElementAtOrNone(1));
        }

        [Test]
        public void OptionToEnumerable()
        {
            // Option with value
            var optionInt = Option<int>.Some(12);
            CollectionAssert.AreEqual(new[] { 12 }, optionInt.ToEnumerable());

            // Option no value
            var emptyOptionInt = Option<int>.None;
            CollectionAssert.AreEqual(Enumerable.Empty<int>(), emptyOptionInt.ToEnumerable());
        }

        [Test]
        public void OptionToEnumerator()
        {
            var expected = new List<int> { 12 };

            int index = 0;

            // Option with value
            var optionInt = Option<int>.Some(12);
            var enumerator = optionInt.ToEnumerator();
            while (enumerator.MoveNext())
            {
                Assert.AreEqual(expected[index], enumerator.Current);
                ++index;
            }

            // Option no value
            var emptyOptionInt = Option<int>.None;
            enumerator = emptyOptionInt.ToEnumerator();
            while (enumerator.MoveNext())
            {
                Assert.Fail("Enumerator generated from Option.None shoulb be empty.");
            }
        }

        [Test]
        public void EnumerableOfOptionToEnumerable()
        {
            var list = new List<Option<int>>
            {
                12,
                Option<int>.None,
                0,
                1,
                Option<int>.None,
                10
            };

            CollectionAssert.AreEqual(new[] { 12, 0, 1, 10 }, list.ExtractValues());
        }

        [Test]
        public void EnumerableOptionToArray()
        {
            IEnumerable<Option<int>> enumerableOptionInts = new List<Option<int>> { 1, 5, Option.None, 15 };
            List<Option<int>> listOptionInts = new List<Option<int>> { Option.None, 7, 4, Option.None, 1 };
            Option<int>[] arrayOptionInts = new[] { Option<int>.Some(12), 4, Option.None, 7 };

            CollectionAssert.AreEqual(new[] { 1, 5, 15 }, enumerableOptionInts.ToArray());
            CollectionAssert.AreEqual(new[] { 7, 4, 1 }, listOptionInts.ToArray<int>());     // Template parameter force the use of the extension
            CollectionAssert.AreEqual(new[] { 12, 4, 7 }, arrayOptionInts.ToArray());

#if (!NET20) && (!NET30) && (!NET35) && (!NET40)
            IReadOnlyCollection<Option<int>> readonlyCollectionOptionInts = new ReadOnlyCollection<Option<int>>(listOptionInts);
            CollectionAssert.AreEqual(new[] { 7, 4, 1 }, readonlyCollectionOptionInts.ToArray());
#endif
        }

        [Test]
        public void EnumerableOptionToList()
        {
            IEnumerable<Option<int>> enumerableOptionInts = new List<Option<int>> { 1, 5, Option.None, 15 };
            List<Option<int>> listOptionInts = new List<Option<int>> { Option.None, 7, 4, Option.None, 1 };
            Option<int>[] arrayOptionInts = new[] { Option<int>.Some(12), 4, Option.None, 7 };

            CollectionAssert.AreEqual(new[] { 1, 5, 15 }, enumerableOptionInts.ToList());
            CollectionAssert.AreEqual(new[] { 7, 4, 1 }, listOptionInts.ToList());
            CollectionAssert.AreEqual(new[] { 12, 4, 7 }, arrayOptionInts.ToList());

#if (!NET20) && (!NET30) && (!NET35) && (!NET40)
            IReadOnlyCollection<Option<int>> readonlyCollectionOptionInts = new ReadOnlyCollection<Option<int>>(listOptionInts);
            CollectionAssert.AreEqual(new[] { 7, 4, 1 }, readonlyCollectionOptionInts.ToList());
#endif
        }

        [Test]
        public void EnumerableOptionToDictionary()
        {
            IEnumerable<Option<int>> enumerableOptionInts = new List<Option<int>> { 1, 5, Option.None, 15 };
            List<Option<int>> listOptionInts = new List<Option<int>> { Option.None, 7, 4, Option.None, 1 };
            Option<int>[] arrayOptionInts = new[] { Option<int>.Some(12), 4, Option.None, 7 };

            CollectionAssert.AreEquivalent(
                new Dictionary<string, int> { ["1"] = 1, ["5"] = 5, ["15"] = 15 }, 
                enumerableOptionInts.ToDictionary<int, string>(value => value.ToString()));  // Need explicit arguments
            CollectionAssert.AreEquivalent(
                new Dictionary<string, int> { ["7"] = 7, ["4"] = 4, ["1"] = 1 }, 
                listOptionInts.ToDictionary<int, string>(value => value.ToString()));        // Need explicit arguments
            CollectionAssert.AreEquivalent(
                new Dictionary<string, int> { ["12"] = 12, ["4"] = 4, ["7"] = 7 }, 
                arrayOptionInts.ToDictionary<int, string>(value => value.ToString()));       // Need explicit arguments

#if (!NET20) && (!NET30) && (!NET35) && (!NET40)
            IReadOnlyCollection<Option<int>> readonlyCollectionOptionInts = new ReadOnlyCollection<Option<int>>(listOptionInts);
            CollectionAssert.AreEquivalent(
                new Dictionary<string, int> { ["7"] = 7, ["4"] = 4, ["1"] = 1 }, 
                readonlyCollectionOptionInts.ToDictionary<int, string>(value => value.ToString()));  // Need explicit arguments
#endif
        }
    }
}

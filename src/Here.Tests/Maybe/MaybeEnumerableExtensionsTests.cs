using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Maybes
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/> extensions related to <see cref="IEnumerable{T}"/>.
    /// </summary>
    [TestFixture]
    internal class MaybeEnumerableExtensionsTests : MaybeTestsBase
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
            var maybeInt = enumerableInts.FirstOrNone();
            CheckMaybeValue(maybeInt, 1);
            maybeInt = enumerableInts.FirstOrNone(value => value == 2);
            CheckMaybeValue(maybeInt, 2);

            maybeInt = enumerableInts.FirstOrNone(value => value == 4);
            CheckEmptyMaybe(maybeInt);
            
            IEnumerable<int> enumerableInts2 = new List<int>();
            maybeInt = enumerableInts2.FirstOrNone();
            CheckEmptyMaybe(maybeInt);
            maybeInt = enumerableInts2.FirstOrNone(value => value == 2);
            CheckEmptyMaybe(maybeInt);

            // Nullables
            IEnumerable<int?> enumerableNullableInts = new List<int?> { 1, 2, 3 };
            var maybeNullable = enumerableNullableInts.FirstOrNone();
            CheckMaybeValue(maybeNullable, 1);
            maybeNullable = enumerableNullableInts.FirstOrNone(value => value.HasValue && value.Value == 2);
            CheckMaybeValue(maybeNullable, 2);

            maybeNullable = enumerableNullableInts.FirstOrNone(value => value == 4);
            CheckEmptyMaybe(maybeNullable);

            IEnumerable<int?> enumerableNullableInts2 = new List<int?> { null, null };
            maybeNullable = enumerableNullableInts2.FirstOrNone();
            CheckEmptyMaybe(maybeNullable);
            maybeNullable = enumerableNullableInts2.FirstOrNone(value => value.HasValue && value.Value == 2);
            CheckEmptyMaybe(maybeNullable);

            IEnumerable<int?> enumerableNullableInts3 = new List<int?>();
            maybeNullable = enumerableNullableInts3.FirstOrNone();
            CheckEmptyMaybe(maybeNullable);
            maybeNullable = enumerableNullableInts3.FirstOrNone(value => value.HasValue && value.Value == 2);
            CheckEmptyMaybe(maybeNullable);

            // Reference type
            var testObj1 = new TestClass();
            var testObj2 = new TestClass();
            IEnumerable<TestClass> enumerableTestClass = new List<TestClass> { testObj1, testObj2 };
            var maybeClass = enumerableTestClass.FirstOrNone();
            CheckMaybeSameValue(maybeClass, testObj1);
            maybeClass = enumerableTestClass.FirstOrNone(value => ReferenceEquals(testObj2, value));
            CheckMaybeSameValue(maybeClass, testObj2);

            var testObj3 = new TestClass();
            maybeClass = enumerableTestClass.FirstOrNone(value => ReferenceEquals(testObj3, value));
            CheckEmptyMaybe(maybeClass);

            IEnumerable<TestClass> enumerableTestClass2 = new List<TestClass> { null, null };
            maybeClass = enumerableTestClass2.FirstOrNone();
            CheckEmptyMaybe(maybeClass);
            maybeClass = enumerableTestClass2.FirstOrNone(value => ReferenceEquals(testObj1, value));
            CheckEmptyMaybe(maybeClass);

            IEnumerable<TestClass> enumerableTestClass3 = new List<TestClass>();
            maybeClass = enumerableTestClass3.FirstOrNone();
            CheckEmptyMaybe(maybeClass);
            maybeClass = enumerableTestClass3.FirstOrNone(value => ReferenceEquals(testObj1, value));
            CheckEmptyMaybe(maybeClass);
        }

        [Test]
        public void EnumerableSingleOrNone()
        {
            // Test for value type
            // Several values
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 2, 3 };
            Assert.Throws<InvalidOperationException>(() => { var _ = enumerableInts.SingleOrNone(); });
            CheckEmptyMaybe(enumerableInts.SingleOrNone(false));

            Assert.Throws<InvalidOperationException>(() => { var _ = enumerableInts.SingleOrNone(value => value == 2); });
            CheckEmptyMaybe(enumerableInts.SingleOrNone(value => value == 2, false));

            CheckMaybeValue(enumerableInts.SingleOrNone(value => value == 3), 3);
            CheckMaybeValue(enumerableInts.SingleOrNone(value => value == 3, false), 3);

            CheckEmptyMaybe(enumerableInts.SingleOrNone(value => value == 4));
            CheckEmptyMaybe(enumerableInts.SingleOrNone(value => value == 4, false));

            // One value
            IEnumerable<int> enumerableInts2 = new List<int> { 12 };
            CheckMaybeValue(enumerableInts2.SingleOrNone(), 12);
            CheckMaybeValue(enumerableInts2.SingleOrNone(false), 12);

            CheckMaybeValue(enumerableInts2.SingleOrNone(value => value == 12), 12);
            CheckMaybeValue(enumerableInts2.SingleOrNone(value => value == 12, false), 12);

            CheckEmptyMaybe(enumerableInts2.SingleOrNone(value => value == 4));
            CheckEmptyMaybe(enumerableInts2.SingleOrNone(value => value == 4, false));

            // No value
            IEnumerable<int> enumerableInts3 = new List<int>();
            CheckEmptyMaybe(enumerableInts3.SingleOrNone());
            CheckEmptyMaybe(enumerableInts3.SingleOrNone(false));

            CheckEmptyMaybe(enumerableInts3.SingleOrNone(value => value == 4));
            CheckEmptyMaybe(enumerableInts3.SingleOrNone(value => value == 4, false));

            // Test for reference type
            // Several values
            var testObj1 = new TestClass();
            var testObj2 = new TestClass();
            IEnumerable<TestClass> enumerableTestClass = new List<TestClass> { testObj1, testObj1, testObj2 };

            Assert.Throws<InvalidOperationException>(() => { var _ = enumerableTestClass.SingleOrNone(); });
            CheckEmptyMaybe(enumerableTestClass.SingleOrNone(false));

            Assert.Throws<InvalidOperationException>(() => { var _ = enumerableTestClass.SingleOrNone(value => ReferenceEquals(testObj1, value)); });
            CheckEmptyMaybe(enumerableTestClass.SingleOrNone(value => ReferenceEquals(testObj1, value), false));

            CheckMaybeSameValue(enumerableTestClass.SingleOrNone(value => ReferenceEquals(testObj2, value)), testObj2);
            CheckMaybeSameValue(enumerableTestClass.SingleOrNone(value => ReferenceEquals(testObj2, value), false), testObj2);

            var testObj3 = new TestClass();
            CheckEmptyMaybe(enumerableTestClass.SingleOrNone(value => ReferenceEquals(testObj3, value)));
            CheckEmptyMaybe(enumerableTestClass.SingleOrNone(value => ReferenceEquals(testObj3, value), false));

            // One value
            IEnumerable<TestClass> enumerableTestClass2 = new List<TestClass> { testObj1 };

            CheckMaybeSameValue(enumerableTestClass2.SingleOrNone(), testObj1);
            CheckMaybeSameValue(enumerableTestClass2.SingleOrNone(false), testObj1);

            CheckMaybeSameValue(enumerableTestClass2.SingleOrNone(value => ReferenceEquals(testObj1, value)), testObj1);
            CheckMaybeSameValue(enumerableTestClass2.SingleOrNone(value => ReferenceEquals(testObj1, value), false), testObj1);

            CheckEmptyMaybe(enumerableTestClass2.SingleOrNone(value => ReferenceEquals(testObj2, value)));
            CheckEmptyMaybe(enumerableTestClass2.SingleOrNone(value => ReferenceEquals(testObj2, value), false));

            // Null values
            IEnumerable<TestClass> enumerableTestClass3 = new List<TestClass> { null, null };

            Assert.Throws<InvalidOperationException>(() => { var _ = enumerableTestClass3.SingleOrNone(); });
            CheckEmptyMaybe(enumerableTestClass3.SingleOrNone(false));

            CheckEmptyMaybe(enumerableTestClass3.SingleOrNone(value => ReferenceEquals(testObj1, value)));
            CheckEmptyMaybe(enumerableTestClass3.SingleOrNone(value => ReferenceEquals(testObj1, value), false));

            Assert.Throws<InvalidOperationException>(() => { var _ = enumerableTestClass3.SingleOrNone(value => value is null); });
            CheckEmptyMaybe(enumerableTestClass3.SingleOrNone(value => value is null, false));

            // No value
            IEnumerable<TestClass> enumerableTestClass4 = new List<TestClass>();

            CheckEmptyMaybe(enumerableTestClass4.SingleOrNone());
            CheckEmptyMaybe(enumerableTestClass4.SingleOrNone(false));

            CheckEmptyMaybe(enumerableTestClass4.SingleOrNone(value => ReferenceEquals(testObj1, value)));
            CheckEmptyMaybe(enumerableTestClass4.SingleOrNone(value => ReferenceEquals(testObj1, value), false));
        }

        [Test]
        public void EnumerableLastOrNone()
        {
            // Value type
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 3 };
            var maybeInt = enumerableInts.LastOrNone();
            CheckMaybeValue(maybeInt, 3);
            maybeInt = enumerableInts.LastOrNone(value => value == 1);
            CheckMaybeValue(maybeInt, 1);

            maybeInt = enumerableInts.LastOrNone(value => value == 4);
            CheckEmptyMaybe(maybeInt);

            IEnumerable<int> enumerableInts2 = new List<int>();
            maybeInt = enumerableInts2.LastOrNone();
            CheckEmptyMaybe(maybeInt);
            maybeInt = enumerableInts2.LastOrNone(value => value == 2);
            CheckEmptyMaybe(maybeInt);

            // Nullables
            IEnumerable<int?> enumerableNullableInts = new List<int?> { 1, 2, 3 };
            var maybeNullable = enumerableNullableInts.LastOrNone();
            CheckMaybeValue(maybeNullable, 3);
            maybeNullable = enumerableNullableInts.LastOrNone(value => value.HasValue && value.Value == 1);
            CheckMaybeValue(maybeNullable, 1);

            maybeNullable = enumerableNullableInts.LastOrNone(value => value.HasValue && value.Value == 4);
            CheckEmptyMaybe(maybeNullable);

            IEnumerable<int?> enumerableNullableInts2 = new List<int?> { null, null };
            maybeNullable = enumerableNullableInts2.LastOrNone();
            CheckEmptyMaybe(maybeNullable);
            maybeNullable = enumerableNullableInts2.LastOrNone(value => value.HasValue && value.Value == 2);
            CheckEmptyMaybe(maybeNullable);

            IEnumerable<int?> enumerableNullableInts3 = new List<int?>();
            maybeNullable = enumerableNullableInts3.LastOrNone();
            CheckEmptyMaybe(maybeNullable);
            maybeNullable = enumerableNullableInts3.LastOrNone(value => value.HasValue && value.Value == 2);
            CheckEmptyMaybe(maybeNullable);

            // Reference type
            var testObj1 = new TestClass();
            var testObj2 = new TestClass();
            IEnumerable<TestClass> enumerableTestClass = new List<TestClass> { testObj1, testObj2 };
            var maybeClass = enumerableTestClass.LastOrNone();
            CheckMaybeSameValue(maybeClass, testObj2);
            maybeClass = enumerableTestClass.LastOrNone(value => ReferenceEquals(testObj1, value));
            CheckMaybeSameValue(maybeClass, testObj1);

            var testObj3 = new TestClass();
            maybeClass = enumerableTestClass.LastOrNone(value => ReferenceEquals(testObj3, value));
            CheckEmptyMaybe(maybeClass);

            IEnumerable<TestClass> enumerableTestClass2 = new List<TestClass> { null, null };
            maybeClass = enumerableTestClass2.LastOrNone();
            CheckEmptyMaybe(maybeClass);
            maybeClass = enumerableTestClass2.LastOrNone(value => ReferenceEquals(testObj1, value));
            CheckEmptyMaybe(maybeClass);

            IEnumerable<TestClass> enumerableTestClass3 = new List<TestClass>();
            maybeClass = enumerableTestClass3.LastOrNone();
            CheckEmptyMaybe(maybeClass);
            maybeClass = enumerableTestClass3.LastOrNone(value => ReferenceEquals(testObj1, value));
            CheckEmptyMaybe(maybeClass);
        }

        [Test]
        public void EnumerableElementAtOrNone()
        {
            // Value type
            IList<int> listInts = new List<int> { 1, 2, 3 };
            CheckEmptyMaybe(listInts.ElementAtOrNone(-1));
            CheckMaybeValue(listInts.ElementAtOrNone(0), 1);
            CheckMaybeValue(listInts.ElementAtOrNone(2), 3);
            CheckEmptyMaybe(listInts.ElementAtOrNone(3));

            IEnumerable<int> enumerableInts = new ReadOnlyCollection<int>(listInts);
            CheckEmptyMaybe(enumerableInts.ElementAtOrNone(-1));
            CheckMaybeValue(enumerableInts.ElementAtOrNone(0), 1);
            CheckMaybeValue(enumerableInts.ElementAtOrNone(2), 3);
            CheckEmptyMaybe(enumerableInts.ElementAtOrNone(3));

            IEnumerable<int> enumerableInts2 = new List<int>();
            CheckEmptyMaybe(enumerableInts2.ElementAtOrNone(-1));
            CheckEmptyMaybe(enumerableInts2.ElementAtOrNone(0));

            CheckEmptyMaybe(Items.ElementAtOrNone(-1));
            CheckMaybeValue(Items.ElementAtOrNone(0), 1);
            CheckMaybeValue(Items.ElementAtOrNone(1), 12);
            CheckMaybeValue(Items.ElementAtOrNone(3), 8);
            CheckEmptyMaybe(Items.ElementAtOrNone(5));

            // Reference type
            var testObject1 = new TestClass();
            var testObject2 = new TestClass { TestInt = 12 };
            IList<TestClass> listTestClasses = new List<TestClass> { testObject1, testObject2 };
            CheckEmptyMaybe(listTestClasses.ElementAtOrNone(-1));
            CheckMaybeSameValue(listTestClasses.ElementAtOrNone(0), testObject1);
            CheckMaybeSameValue(listTestClasses.ElementAtOrNone(1), testObject2);
            CheckEmptyMaybe(listTestClasses.ElementAtOrNone(2));

            IEnumerable<TestClass> enumerableTestClasses = new ReadOnlyCollection<TestClass>(listTestClasses);
            CheckEmptyMaybe(enumerableTestClasses.ElementAtOrNone(-1));
            CheckMaybeSameValue(enumerableTestClasses.ElementAtOrNone(0), testObject1);
            CheckMaybeSameValue(enumerableTestClasses.ElementAtOrNone(1), testObject2);
            CheckEmptyMaybe(enumerableTestClasses.ElementAtOrNone(2));

            IEnumerable<TestClass> enumerableTestClasses2 = new List<TestClass>();
            CheckEmptyMaybe(enumerableTestClasses2.ElementAtOrNone(-1));
            CheckEmptyMaybe(enumerableTestClasses2.ElementAtOrNone(0));

            IEnumerable<TestClass> enumerableTestClasses3 = new List<TestClass> { testObject1, null, testObject2 };
            CheckEmptyMaybe(enumerableTestClasses3.ElementAtOrNone(-1));
            CheckMaybeSameValue(enumerableTestClasses3.ElementAtOrNone(0), testObject1);
            // Even if element [1] exists, it's value is null so it will result in an empty maybe
            CheckEmptyMaybe(enumerableTestClasses3.ElementAtOrNone(1));
        }

        [Test]
        public void MaybeToEnumerable()
        {
            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            CollectionAssert.AreEqual(new[] { 12 }, maybeInt.ToEnumerable());

            // Maybe no value
            var emptyMaybeInt = Maybe<int>.None;
            CollectionAssert.AreEqual(Enumerable.Empty<int>(), emptyMaybeInt.ToEnumerable());
        }

        [Test]
        public void MaybeToEnumerator()
        {
            var expected = new List<int> { 12 };

            int index = 0;

            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            var enumerator = maybeInt.ToEnumerator();
            while (enumerator.MoveNext())
            {
                Assert.AreEqual(expected[index], enumerator.Current);
                ++index;
            }

            // Maybe no value
            var emptyMaybeInt = Maybe<int>.None;
            enumerator = emptyMaybeInt.ToEnumerator();
            while (enumerator.MoveNext())
            {
                Assert.Fail("Enumerator generated from Maybe.None shoulb be empty.");
            }
        }

        [Test]
        public void EnumerableOfMaybeToEnumerable()
        {
            var list = new List<Maybe<int>>
            {
                12,
                Maybe<int>.None,
                0,
                1,
                Maybe<int>.None,
                10
            };

            CollectionAssert.AreEqual(new[] { 12, 0, 1, 10 }, list.ExtractValues());
        }

        [Test]
        public void EnumerableMaybeToArray()
        {
            IEnumerable<Maybe<int>> enumerableMaybeInts = new List<Maybe<int>> { 1, 5, Maybe.None, 15 };
            List<Maybe<int>> listMaybeInts = new List<Maybe<int>> { Maybe.None, 7, 4, Maybe.None, 1 };
            Maybe<int>[] arrayMaybeInts = new[] { Maybe<int>.Some(12), 4, Maybe.None, 7 };

            CollectionAssert.AreEqual(new[] { 1, 5, 15 }, enumerableMaybeInts.ToArray());
            CollectionAssert.AreEqual(new[] { 7, 4, 1 }, listMaybeInts.ToArray<int>());     // Template parameter force the use of the extension
            CollectionAssert.AreEqual(new[] { 12, 4, 7 }, arrayMaybeInts.ToArray());

#if (!NET20) && (!NET30) && (!NET35) && (!NET40)
            IReadOnlyCollection<Maybe<int>> readonlyCollectionMaybeInts = new ReadOnlyCollection<Maybe<int>>(listMaybeInts);
            CollectionAssert.AreEqual(new[] { 7, 4, 1 }, readonlyCollectionMaybeInts.ToArray());
#endif
        }

        [Test]
        public void EnumerableMaybeToList()
        {
            IEnumerable<Maybe<int>> enumerableMaybeInts = new List<Maybe<int>> { 1, 5, Maybe.None, 15 };
            List<Maybe<int>> listMaybeInts = new List<Maybe<int>> { Maybe.None, 7, 4, Maybe.None, 1 };
            Maybe<int>[] arrayMaybeInts = new[] { Maybe<int>.Some(12), 4, Maybe.None, 7 };

            CollectionAssert.AreEqual(new[] { 1, 5, 15 }, enumerableMaybeInts.ToList());
            CollectionAssert.AreEqual(new[] { 7, 4, 1 }, listMaybeInts.ToList());
            CollectionAssert.AreEqual(new[] { 12, 4, 7 }, arrayMaybeInts.ToList());

#if (!NET20) && (!NET30) && (!NET35) && (!NET40)
            IReadOnlyCollection<Maybe<int>> readonlyCollectionMaybeInts = new ReadOnlyCollection<Maybe<int>>(listMaybeInts);
            CollectionAssert.AreEqual(new[] { 7, 4, 1 }, readonlyCollectionMaybeInts.ToList());
#endif
        }

        [Test]
        public void EnumerableMaybeToDictionary()
        {
            IEnumerable<Maybe<int>> enumerableMaybeInts = new List<Maybe<int>> { 1, 5, Maybe.None, 15 };
            List<Maybe<int>> listMaybeInts = new List<Maybe<int>> { Maybe.None, 7, 4, Maybe.None, 1 };
            Maybe<int>[] arrayMaybeInts = new[] { Maybe<int>.Some(12), 4, Maybe.None, 7 };

            CollectionAssert.AreEquivalent(
                new Dictionary<string, int> { ["1"] = 1, ["5"] = 5, ["15"] = 15 }, 
                enumerableMaybeInts.ToDictionary<int, string>(value => value.ToString()));  // Need explicit arguments
            CollectionAssert.AreEquivalent(
                new Dictionary<string, int> { ["7"] = 7, ["4"] = 4, ["1"] = 1 }, 
                listMaybeInts.ToDictionary<int, string>(value => value.ToString()));        // Need explicit arguments
            CollectionAssert.AreEquivalent(
                new Dictionary<string, int> { ["12"] = 12, ["4"] = 4, ["7"] = 7 }, 
                arrayMaybeInts.ToDictionary<int, string>(value => value.ToString()));       // Need explicit arguments

#if (!NET20) && (!NET30) && (!NET35) && (!NET40)
            IReadOnlyCollection<Maybe<int>> readonlyCollectionMaybeInts = new ReadOnlyCollection<Maybe<int>>(listMaybeInts);
            CollectionAssert.AreEquivalent(
                new Dictionary<string, int> { ["7"] = 7, ["4"] = 4, ["1"] = 1 }, 
                readonlyCollectionMaybeInts.ToDictionary<int, string>(value => value.ToString()));  // Need explicit arguments
#endif
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Here.Maybes;
using Here.Maybes.Extensions;

namespace Here.Tests.Maybes
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/> extensions related to <see cref="IEnumerable{T}"/>.
    /// </summary>
    [TestFixture]
    internal class MaybeEnumerableExtensionsTests : MaybeTestsBase
    {
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
    }
}

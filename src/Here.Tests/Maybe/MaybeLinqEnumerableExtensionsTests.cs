using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Here.Maybes;
using Here.Maybes.Extensions;

namespace Here.Tests.Maybes
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/> Linq implementation (enumerable).
    /// </summary>
    [TestFixture]
    internal class MaybeLinqEnumerableExtensionsTests : MaybeTestsBase
    {
        #region Test property

        private IEnumerable Items
        {
            get
            {
                yield return 1;
                yield return new TestClass();
                yield return "Test";
            }
        }

        #endregion

        [Test]
        public void MaybeAnyItem()
        {
            IEnumerable<int> emptyEnumerable = new List<int>();
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 3, 4, 5 };
            IList<int> listInts = new List<int> { 2, 3, 4, 5, 6 };
            IDictionary<string, int> dictionaryStringsInts = new Dictionary<string, int>
            {
                ["3"] = 3,
                ["4"] = 4,
                ["5"] = 5
            };

            // Not empty maybe
            // Enumerable<int>
            var maybeEnumerableInts = Maybe<IEnumerable<int>>.Some(emptyEnumerable);
            Assert.IsFalse(maybeEnumerableInts.AnyItem());
            Assert.IsFalse(maybeEnumerableInts.AnyItem(value => value is int i && i == 2));
            Assert.IsFalse(maybeEnumerableInts.AnyItem((int value) => value == 2));

            maybeEnumerableInts = Maybe<IEnumerable<int>>.Some(enumerableInts);
            Assert.IsTrue(maybeEnumerableInts.AnyItem());
            Assert.IsTrue(maybeEnumerableInts.AnyItem(value => value is int i && i == 2));
            Assert.IsFalse(maybeEnumerableInts.AnyItem(value => value is int i && i == 6));
            Assert.IsTrue(maybeEnumerableInts.AnyItem((int value) => value == 2));
            Assert.IsFalse(maybeEnumerableInts.AnyItem((int value) => value == 6));

            // List<int>
            var maybeListInts = Maybe<IList<int>>.Some(listInts);
            Assert.IsTrue(maybeListInts.AnyItem());
            Assert.IsTrue(maybeListInts.AnyItem(value => value is int i && i == 2));
            Assert.IsFalse(maybeListInts.AnyItem(value => value is int i && i == 7));
            Assert.IsTrue(maybeListInts.AnyItem((int value) => value == 2));
            Assert.IsFalse(maybeListInts.AnyItem((int value) => value == 7));

            // Dictionary<string, int>
            var maybeStringsInts = Maybe<IDictionary<string, int>>.Some(dictionaryStringsInts);
            Assert.IsTrue(maybeStringsInts.AnyItem());
            Assert.IsTrue(maybeStringsInts.AnyItem(value => value is KeyValuePair<string, int> pair && pair.Value == 3));
            Assert.IsFalse(maybeStringsInts.AnyItem(value => value is KeyValuePair<string, int> pair && pair.Value == 7));
            Assert.IsTrue(maybeStringsInts.AnyItem((KeyValuePair<string, int> pair) => pair.Value == 3));
            Assert.IsFalse(maybeStringsInts.AnyItem((KeyValuePair<string, int> pair) => pair.Value == 7));

            // Enumerable
            var maybeEnumerable = Maybe<IEnumerable>.Some(Items);
            Assert.IsTrue(maybeEnumerable.AnyItem());
            Assert.IsTrue(maybeEnumerable.AnyItem(value => value is string));
            Assert.IsFalse(maybeEnumerable.AnyItem(value => value is float));

            maybeEnumerable = Maybe<IEnumerable>.Some(listInts);
            Assert.IsTrue(maybeEnumerable.AnyItem());
            Assert.IsTrue(maybeEnumerable.AnyItem(value => value is int));
            Assert.IsFalse(maybeEnumerable.AnyItem(value => value is float));

            // Empty maybe
            var emptyMaybe = Maybe<IList<int>>.None;
            Assert.IsFalse(emptyMaybe.AnyItem());
            Assert.IsFalse(emptyMaybe.AnyItem(value => true));
            Assert.IsFalse(emptyMaybe.AnyItem((int value) => true));
        }

        [Test]
        public void MaybeAllItems()
        {
            IEnumerable<int> emptyEnumerable = new List<int>();
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 3, 4, 5 };
            IList<int> listInts = new List<int> { 2, 3, 4, 5, 6 };
            IDictionary<string, int> dictionaryStringsInts = new Dictionary<string, int>
            {
                ["3"] = 3,
                ["4"] = 4,
                ["5"] = 5
            };

            // Not empty maybe
            // Enumerable<int>
            var maybeEnumerableInts = Maybe<IEnumerable<int>>.Some(emptyEnumerable);
            Assert.IsTrue(maybeEnumerableInts.AllItems(value => true));
            Assert.IsTrue(maybeEnumerableInts.AllItems(value => value is int));
            Assert.IsTrue(maybeEnumerableInts.AllItems((int value) => true));
            Assert.IsTrue(maybeEnumerableInts.AllItems((int value) => value > 0));

            maybeEnumerableInts = Maybe<IEnumerable<int>>.Some(enumerableInts);
            Assert.IsTrue(maybeEnumerableInts.AllItems(value => value is int i && i > 0));
            Assert.IsFalse(maybeEnumerableInts.AllItems(value => value is int i && i == 6));
            Assert.IsTrue(maybeEnumerableInts.AllItems((int value) => value > 0));
            Assert.IsFalse(maybeEnumerableInts.AllItems((int value) => value == 6));

            // List<int>
            var maybeListInts = Maybe<IList<int>>.Some(listInts);
            Assert.IsTrue(maybeListInts.AllItems(value => value is int i && i > 1));
            Assert.IsFalse(maybeListInts.AllItems(value => value is int i && i == 7));
            Assert.IsTrue(maybeListInts.AllItems((int value) => value > 1));
            Assert.IsFalse(maybeListInts.AllItems((int value) => value == 7));

            // Dictionary<string, int>
            var maybeStringsInts = Maybe<IDictionary<string, int>>.Some(dictionaryStringsInts);
            Assert.IsTrue(maybeStringsInts.AllItems(value => value is KeyValuePair<string, int>));
            Assert.IsFalse(maybeStringsInts.AllItems(value => value is KeyValuePair<string, int> pair && pair.Value == 7));
            Assert.IsTrue(maybeStringsInts.AllItems((KeyValuePair<string, int> pair) => pair.Value > 2));
            Assert.IsFalse(maybeStringsInts.AllItems((KeyValuePair<string, int> pair) => pair.Value == 7));

            // Enumerable
            var maybeEnumerable = Maybe<IEnumerable>.Some(Items);
            Assert.IsTrue(maybeEnumerable.AllItems(value => value != null));
            Assert.IsFalse(maybeEnumerable.AllItems(value => value is float));

            maybeEnumerable = Maybe<IEnumerable>.Some(listInts);
            Assert.IsTrue(maybeEnumerable.AllItems(value => value is int));
            Assert.IsFalse(maybeEnumerable.AllItems(value => value is int i && i > 25));

            // Empty maybe
            var emptyMaybe = Maybe<IList<int>>.None;
            Assert.IsFalse(emptyMaybe.AllItems(value => true));
            Assert.IsFalse(emptyMaybe.AllItems((int value) => true));
        }

        [Test]
        public void MaybeContainsItem()
        {
            var testObject1 = new TestClass { TestInt = 12 };
            var testObject2 = new TestClass { TestInt = 13 };
            var testObject3 = new TestClass { TestInt = 14 };
            IEnumerable<TestClass> emptyEnumerable = new List<TestClass>();
            IEnumerable<TestClass> enumerableTestClasses = new List<TestClass> { testObject1, testObject2 };
            IList<TestClass> listTestClasses = new List<TestClass> { testObject1, testObject3 };
            IDictionary<string, int> dictionaryStringsInts = new Dictionary<string, int>
            {
                ["3"] = 3,
                ["4"] = 4,
                ["5"] = 5
            };

            // Not empty maybe
            // Enumerable<int>
            var maybeEnumerableTestClasses = Maybe<IEnumerable<TestClass>>.Some(emptyEnumerable);
            Assert.IsFalse(maybeEnumerableTestClasses.ContainsItem((TestClass)null));
            Assert.IsFalse(maybeEnumerableTestClasses.ContainsItem(null, EqualityComparer<TestClass>.Default));
            Assert.IsFalse(maybeEnumerableTestClasses.ContainsItem(testObject1));
            Assert.IsFalse(maybeEnumerableTestClasses.ContainsItem(testObject1, EqualityComparer<TestClass>.Default));

            maybeEnumerableTestClasses = Maybe<IEnumerable<TestClass>>.Some(enumerableTestClasses);
            Assert.IsFalse(maybeEnumerableTestClasses.ContainsItem((TestClass)null));
            Assert.IsFalse(maybeEnumerableTestClasses.ContainsItem(null, EqualityComparer<TestClass>.Default));
            Assert.IsTrue(maybeEnumerableTestClasses.ContainsItem(testObject2));
            Assert.IsTrue(maybeEnumerableTestClasses.ContainsItem(testObject2, EqualityComparer<TestClass>.Default));
            Assert.IsFalse(maybeEnumerableTestClasses.ContainsItem(testObject3));
            Assert.IsFalse(maybeEnumerableTestClasses.ContainsItem(testObject3, EqualityComparer<TestClass>.Default));

            // List<int>
            var maybeListInts = Maybe<IList<TestClass>>.Some(listTestClasses);
            Assert.IsFalse(maybeListInts.ContainsItem((TestClass)null));
            Assert.IsFalse(maybeListInts.ContainsItem(null, EqualityComparer<TestClass>.Default));
            Assert.IsTrue(maybeListInts.ContainsItem(testObject1));
            Assert.IsTrue(maybeListInts.ContainsItem(testObject1, EqualityComparer<TestClass>.Default));
            Assert.IsFalse(maybeListInts.ContainsItem(testObject2));
            Assert.IsFalse(maybeListInts.ContainsItem(testObject2, EqualityComparer<TestClass>.Default));

            // Dictionary<string, int>
            var maybeStringsInts = Maybe<IDictionary<string, int>>.Some(dictionaryStringsInts);
            Assert.IsTrue(maybeStringsInts.ContainsItem(new KeyValuePair<string, int>("3", 3)));
            Assert.IsTrue(maybeStringsInts.ContainsItem(new KeyValuePair<string, int>("3", 3), EqualityComparer<KeyValuePair<string, int>>.Default));
            Assert.IsFalse(maybeStringsInts.ContainsItem(new KeyValuePair<string, int>("1", 3)));
            Assert.IsFalse(maybeStringsInts.ContainsItem(new KeyValuePair<string, int>("1", 3), EqualityComparer<KeyValuePair<string, int>>.Default));

            // Enumerable
            var maybeEnumerable = Maybe<IEnumerable>.Some(Items);
            Assert.IsFalse(maybeEnumerable.ContainsItem(null));
            Assert.IsFalse(maybeEnumerable.ContainsItem(null, EqualityComparer<object>.Default));
            Assert.IsTrue(maybeEnumerable.ContainsItem((object)"Test"));
            Assert.IsTrue(maybeEnumerable.ContainsItem(1, EqualityComparer<object>.Default));
            Assert.IsFalse(maybeEnumerable.ContainsItem((object)testObject2));
            Assert.IsFalse(maybeEnumerable.ContainsItem(testObject2, EqualityComparer<object>.Default));

            maybeEnumerable = Maybe<IEnumerable>.Some(listTestClasses);
            Assert.IsFalse(maybeEnumerable.ContainsItem(null));
            Assert.IsFalse(maybeEnumerable.ContainsItem(null, EqualityComparer<object>.Default));
            Assert.IsTrue(maybeEnumerable.ContainsItem((object)testObject1));
            Assert.IsTrue(maybeEnumerable.ContainsItem(testObject1, EqualityComparer<object>.Default));
            Assert.IsFalse(maybeEnumerable.ContainsItem((object)testObject2));
            Assert.IsFalse(maybeEnumerable.ContainsItem(testObject2, EqualityComparer<object>.Default));

            // Empty maybe
            var emptyMaybe = Maybe<IList<TestClass>>.None;
            Assert.IsFalse(emptyMaybe.ContainsItem((TestClass)null));
            Assert.IsFalse(emptyMaybe.ContainsItem(null, EqualityComparer<TestClass>.Default));
            Assert.IsFalse(emptyMaybe.ContainsItem(testObject1));
            Assert.IsFalse(emptyMaybe.ContainsItem(testObject2, EqualityComparer<TestClass>.Default));
        }

        [Test]
        public void MaybeSelectItems()
        {
            Func<object, float> selectFromObject = value =>
            {
                if (value is int i)
                    return i + 0.1f;
                return 0.2f;
            };

            Func<int, float> selectFromInt = value => value + 0.5f;

            IEnumerable<int> emptyEnumerable = new List<int>();
            IEnumerable<int> enumerableInts = new List<int> { 3, 4, 5 };
            IList<int> listInts = new List<int> { 4, 5, 6 };
            IDictionary<string, int> dictionaryStringsInts = new Dictionary<string, int>
            {
                ["3"] = 3,
                ["4"] = 4,
                ["5"] = 5
            };

            // Not empty maybe
            // Enumerable<int>
            var maybeEnumerableInts = Maybe<IEnumerable<int>>.Some(emptyEnumerable);
            CheckEmptyMaybe(maybeEnumerableInts.SelectItems(selectFromObject));
            CheckEmptyMaybe(maybeEnumerableInts.SelectItems(selectFromInt));

            maybeEnumerableInts = Maybe<IEnumerable<int>>.Some(enumerableInts);
            CheckMaybeCollectionValue(maybeEnumerableInts.SelectItems(
                selectFromObject), 
                new[] { 3.1f, 4.1f, 5.1f });
            CheckMaybeCollectionValue(maybeEnumerableInts.SelectItems(
                selectFromInt),
                new[] { 3.5f, 4.5f, 5.5f });

            // List<int>
            var maybeListInts = Maybe<IList<int>>.Some(listInts);
            CheckMaybeCollectionValue(maybeListInts.SelectItems(
                selectFromObject),
                new[] { 4.1f, 5.1f, 6.1f });
            CheckMaybeCollectionValue(maybeListInts.SelectItems(
                selectFromInt),
                new[] { 4.5f, 5.5f, 6.5f });

            // Dictionary<string, int>
            var maybeStringsInts = Maybe<IDictionary<string, int>>.Some(dictionaryStringsInts);
            CheckMaybeCollectionValue(maybeStringsInts.SelectItems(
                 value =>
                 {
                     if (value is KeyValuePair<string, int> pair)
                         return pair.Value + 0.2f;
                     return 0.4f;
                 }),
                 new[] { 3.2f, 4.2f, 5.2f });
            CheckMaybeCollectionValue(maybeStringsInts.SelectItems(
                (KeyValuePair<string, int> pair) =>
                {
                    return pair.Value + 0.3f;
                }),
                new[] { 3.3f, 4.3f, 5.3f });

            // Enumerable
            var maybeEnumerable = Maybe<IEnumerable>.Some(Items);
            CheckMaybeCollectionValue(maybeEnumerable.SelectItems(
                 value =>
                 {
                     if (value is int i)
                         return i +1;
                     return 42;
                 }),
                 new[] { 2, 42, 42 });

            maybeEnumerable = Maybe<IEnumerable>.Some(listInts);
            CheckMaybeCollectionValue(maybeEnumerable.SelectItems(
                  value =>
                  {
                      if (value is int i)
                          return i + 1;
                      return 42;
                  }),
                  new[] { 5, 6, 7 });

            // Empty maybe
            var emptyMaybe = Maybe<IList<int>>.None;
            CheckEmptyMaybe(emptyMaybe.SelectItems(selectFromObject));
            CheckEmptyMaybe(emptyMaybe.SelectItems(selectFromInt));
        }

        [Test]
        public void MaybeWhereItems()
        {
            IEnumerable<int> emptyEnumerable = new List<int>();
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 3, 4, 5 };
            IList<int> listInts = new List<int> { 2, 3, 4, 5, 6 };
            IDictionary<string, int> dictionaryStringsInts = new Dictionary<string, int>
            {
                ["3"] = 3,
                ["4"] = 4,
                ["5"] = 5
            };

            // Not empty maybe
            // Enumerable<int>
            var maybeEnumerableInts = Maybe<IEnumerable<int>>.Some(emptyEnumerable);
            CheckEmptyMaybe(maybeEnumerableInts.WhereItems(value => true));
            CheckEmptyMaybe(maybeEnumerableInts.WhereItems((int value) => true));

            maybeEnumerableInts = Maybe<IEnumerable<int>>.Some(enumerableInts);
            CheckMaybeCollectionValue(maybeEnumerableInts.WhereItems(value => (int)value >= 3), new[] { 3, 4, 5 });
            CheckMaybeCollectionValue(maybeEnumerableInts.WhereItems((int value) => value >= 4), new[] { 4, 5 });
            CheckEmptyMaybe(maybeEnumerableInts.WhereItems(value => (int)value >= 6));
            CheckEmptyMaybe(maybeEnumerableInts.WhereItems((int value) => value >= 7));

            // List<int>
            var maybeListInts = Maybe<IList<int>>.Some(listInts);
            CheckMaybeCollectionValue(maybeListInts.WhereItems(value => (int)value >= 3), new[] { 3, 4, 5, 6 });
            CheckMaybeCollectionValue(maybeListInts.WhereItems((int value) => value >= 4), new[] { 4, 5, 6 });
            CheckEmptyMaybe(maybeListInts.WhereItems(value => (int)value > 6));
            CheckEmptyMaybe(maybeListInts.WhereItems((int value) => value >= 7));

            // Dictionary<string, int>
            var maybeStringsInts = Maybe<IDictionary<string, int>>.Some(dictionaryStringsInts);
            CheckMaybeDictionaryValue(
                maybeStringsInts.WhereItems(pair => ((KeyValuePair<string, int>)pair).Value > 3), 
                new Dictionary<string, int> { ["4"] = 4, ["5"] = 5 });
            CheckMaybeDictionaryValue(
                maybeStringsInts.WhereItems((KeyValuePair<string, int> pair) => pair.Value >= 4),
                new Dictionary<string, int> { ["4"] = 4, ["5"] = 5 });
            CheckEmptyMaybe(maybeStringsInts.WhereItems(pair => false));
            CheckEmptyMaybe(maybeStringsInts.WhereItems((KeyValuePair<string, int> pair) => pair.Value >= 7));

            // Enumerable
            var maybeEnumerable = Maybe<IEnumerable>.Some(Items);
            CheckMaybeCollectionValue(maybeEnumerable.WhereItems(item => item is string), new object[] { "Test" });

            maybeEnumerable = Maybe<IEnumerable>.Some(listInts);
            CheckMaybeCollectionValue(maybeEnumerable.WhereItems(item => item is int i && i > 4), new[] { 5, 6 });

            // Empty maybe
            var emptyMaybe = Maybe<IList<int>>.None;
            CheckEmptyMaybe(emptyMaybe.WhereItems(item => true));
            CheckEmptyMaybe(emptyMaybe.WhereItems((int value) => value > 1));
        }

        [Test]
        public void MaybeForEachItems()
        {
            IEnumerable<int> emptyEnumerable = new List<int>();
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 3, 4, 5 };
            IList<int> listInts = new List<int> { 2, 3, 4, 5, 6 };
            IDictionary<string, int> dictionaryStringsInts = new Dictionary<string, int>
            {
                ["3"] = 3,
                ["4"] = 4,
                ["5"] = 5
            };

            // Not empty maybe
            // Enumerable<int>
            int counter = 0;
            var maybeEnumerableInts = Maybe<IEnumerable<int>>.Some(emptyEnumerable);
            maybeEnumerableInts.ForEachItems(value => ++counter);
            Assert.AreEqual(0, counter);

            counter = 0;
            maybeEnumerableInts = Maybe<IEnumerable<int>>.Some(enumerableInts);
            maybeEnumerableInts.ForEachItems(value => ++counter);
            Assert.AreEqual(5, counter);

            maybeEnumerableInts.ForEachItems((int value) => ++counter);
            Assert.AreEqual(10, counter);

            // List<int>
            counter = 0;
            var maybeListInts = Maybe<IList<int>>.Some(listInts);
            maybeListInts.ForEachItems(value => ++counter);
            Assert.AreEqual(5, counter);

            maybeListInts.ForEachItems((int value) => ++counter);
            Assert.AreEqual(10, counter);

            // Dictionary<string, int>
            counter = 0;
            var maybeStringsInts = Maybe<IDictionary<string, int>>.Some(dictionaryStringsInts);
            maybeStringsInts.ForEachItems(value => ++counter);
            Assert.AreEqual(3, counter);

            maybeStringsInts.ForEachItems((KeyValuePair<string, int> value) => ++counter);
            Assert.AreEqual(6, counter);

            // Enumerable
            counter = 0;
            var maybeEnumerable = Maybe<IEnumerable>.Some(Items);
            maybeEnumerable.ForEachItems(value => ++counter);
            Assert.AreEqual(3, counter);

            counter = 0;
            maybeEnumerable = Maybe<IEnumerable>.Some(listInts);
            maybeEnumerable.ForEachItems(value => ++counter);
            Assert.AreEqual(5, counter);

            // Empty maybe
            counter = 0;
            var emptyMaybe = Maybe<IList<int>>.None;

            emptyMaybe.ForEachItems(item => ++counter);
            Assert.AreEqual(0, counter);

            emptyMaybe.ForEachItems((int item) => ++counter);
            Assert.AreEqual(0, counter);
        }

#if (NET20) || (NET30)
        // System.Core and NUnit both define this delegate that is conflicting
        // Defining it here allows to use it without conflict.
        public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
#endif

        [Test]
        public void MaybeAggregateItems()
        {
            Func<int, int, int> accumulatorFromInt = (accumulator, current) => accumulator + 2 * current;

            Func<int, object, int> accumulatorFromObject = (accumulator, current) =>
            {
                int add = 0;
                if (current is int)
                    add = 2;
                else if (current is string)
                    add = 5;

                return accumulator + add;
            };

            IEnumerable<int> emptyEnumerable = new List<int>();
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 3 };
            IList<int> listInts = new List<int> { 2, 3, 4 };
            IDictionary<string, int> dictionaryStringsInts = new Dictionary<string, int>
            {
                ["3"] = 3,
                ["4"] = 4,
                ["5"] = 5
            };

            // Not empty maybe
            // Enumerable<int>
            var maybeEnumerableInts = Maybe<IEnumerable<int>>.Some(emptyEnumerable);
            Assert.AreEqual(1, maybeEnumerableInts.AggregateItems(1, (acc, cur) => accumulatorFromObject(acc, cur)));
            Assert.AreEqual(1, maybeEnumerableInts.AggregateItems(1, (int acc, int cur) => accumulatorFromInt(acc, cur)));

            maybeEnumerableInts = Maybe<IEnumerable<int>>.Some(enumerableInts);
            Assert.AreEqual(7, maybeEnumerableInts.AggregateItems(1, (acc, cur) => accumulatorFromObject(acc, cur)));
            Assert.AreEqual(13, maybeEnumerableInts.AggregateItems(1, (int acc, int cur) => accumulatorFromInt(acc, cur)));

            // List<int>
            var maybeListInts = Maybe<IList<int>>.Some(listInts);
            Assert.AreEqual(7, maybeListInts.AggregateItems(1, (acc, cur) => accumulatorFromObject(acc, cur)));
            Assert.AreEqual(19, maybeListInts.AggregateItems(1, (int acc, int cur) => accumulatorFromInt(acc, cur)));

            // Dictionary<string, int>
            var maybeStringsInts = Maybe<IDictionary<string, int>>.Some(dictionaryStringsInts);
            Assert.AreEqual(8, maybeStringsInts.AggregateItems(1, (acc, cur) => acc * 2));
            Assert.AreEqual(25, maybeStringsInts.AggregateItems(1, (int acc, KeyValuePair<string, int> cur) => acc + 2 * cur.Value));

            // Enumerable
            var maybeEnumerable = Maybe<IEnumerable>.Some(Items);
            Assert.AreEqual(8, maybeEnumerable.AggregateItems(1, (acc, cur) => accumulatorFromObject(acc, cur)));

            maybeEnumerable = Maybe<IEnumerable>.Some(listInts);
            Assert.AreEqual(7, maybeEnumerable.AggregateItems(1, (acc, cur) => accumulatorFromObject(acc, cur)));

            // Empty maybe
            var emptyMaybe = Maybe<IList<int>>.None;
            Assert.AreEqual(1, emptyMaybe.AggregateItems(1, (acc, cur) => accumulatorFromObject(acc, cur)));
            Assert.AreEqual(1, emptyMaybe.AggregateItems(1, (int acc, int cur) => accumulatorFromInt(acc, cur)));
        }
    }
}

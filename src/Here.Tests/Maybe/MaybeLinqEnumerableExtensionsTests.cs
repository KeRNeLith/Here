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
            Assert.IsTrue(maybeEnumerable.AllItems(value => value is object));
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
    }
}

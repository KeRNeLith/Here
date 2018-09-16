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
        public void MaybeWhereItem()
        {
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
            var maybeEnumerableInts = Maybe<IEnumerable<int>>.Some(enumerableInts);
            CheckMaybeCollectionValue(maybeEnumerableInts.WhereItem(value => (int)value >= 3), new[] { 3, 4, 5 });
            CheckMaybeCollectionValue(maybeEnumerableInts.WhereItem((int value) => value >= 4), new[] { 4, 5 });
            CheckEmptyMaybe(maybeEnumerableInts.WhereItem(value => (int)value >= 6));
            CheckEmptyMaybe(maybeEnumerableInts.WhereItem((int value) => value >= 7));

            // List<int>
            var maybeListInts = Maybe<IList<int>>.Some(listInts);
            CheckMaybeCollectionValue(maybeListInts.WhereItem(value => (int)value >= 3), new[] { 3, 4, 5, 6 });
            CheckMaybeCollectionValue(maybeListInts.WhereItem((int value) => value >= 4), new[] { 4, 5, 6 });
            CheckEmptyMaybe(maybeListInts.WhereItem(value => (int)value > 6));
            CheckEmptyMaybe(maybeListInts.WhereItem((int value) => value >= 7));

            // Dictionary<string, int>
            var maybeStringsInts = Maybe<IDictionary<string, int>>.Some(dictionaryStringsInts);
            CheckMaybeDictionaryValue(
                maybeStringsInts.WhereItem(pair => ((KeyValuePair<string, int>)pair).Value > 3), 
                new Dictionary<string, int> { ["4"] = 4, ["5"] = 5 });
            CheckMaybeDictionaryValue(
                maybeStringsInts.WhereItem((KeyValuePair<string, int> pair) => pair.Value >= 4),
                new Dictionary<string, int> { ["4"] = 4, ["5"] = 5 });
            CheckEmptyMaybe(maybeStringsInts.WhereItem(pair => false));
            CheckEmptyMaybe(maybeStringsInts.WhereItem((KeyValuePair<string, int> pair) => pair.Value >= 7));

            // Enumerable
            var maybeEnumerable = Maybe<IEnumerable>.Some(Items);
            CheckMaybeCollectionValue(maybeEnumerable.WhereItem(item => item is string), new object[] { "Test" });

            maybeEnumerable = Maybe<IEnumerable>.Some(listInts);
            CheckMaybeCollectionValue(maybeEnumerable.WhereItem(item => item is int i && i > 4), new[] { 5, 6 });

            // Empty maybe
            var emptyMaybe = Maybe<IList<int>>.None;
            CheckEmptyMaybe(emptyMaybe.WhereItem(item => true));
            CheckEmptyMaybe(emptyMaybe.WhereItem((int value) => value > 1));
        }

        [Test]
        public void MaybeForEachItem()
        {
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
            var maybeEnumerableInts = Maybe<IEnumerable<int>>.Some(enumerableInts);
            maybeEnumerableInts.ForEachItem(value => ++counter);
            Assert.AreEqual(5, counter);

            maybeEnumerableInts.ForEachItem((int value) => ++counter);
            Assert.AreEqual(10, counter);

            // List<int>
            counter = 0;
            var maybeListInts = Maybe<IList<int>>.Some(listInts);
            maybeListInts.ForEachItem(value => ++counter);
            Assert.AreEqual(5, counter);

            maybeListInts.ForEachItem((int value) => ++counter);
            Assert.AreEqual(10, counter);

            // Dictionary<string, int>
            counter = 0;
            var maybeStringsInts = Maybe<IDictionary<string, int>>.Some(dictionaryStringsInts);
            maybeStringsInts.ForEachItem(value => ++counter);
            Assert.AreEqual(3, counter);

            maybeStringsInts.ForEachItem((KeyValuePair<string, int> value) => ++counter);
            Assert.AreEqual(6, counter);

            // Enumerable
            counter = 0;
            var maybeEnumerable = Maybe<IEnumerable>.Some(Items);
            maybeEnumerable.ForEachItem(value => ++counter);
            Assert.AreEqual(3, counter);

            counter = 0;
            maybeEnumerable = Maybe<IEnumerable>.Some(listInts);
            maybeEnumerable.ForEachItem(value => ++counter);
            Assert.AreEqual(5, counter);

            // Empty maybe
            counter = 0;
            var emptyMaybe = Maybe<IList<int>>.None;

            emptyMaybe.ForEachItem(item => ++counter);
            Assert.AreEqual(0, counter);

            emptyMaybe.ForEachItem((int item) => ++counter);
            Assert.AreEqual(0, counter);
        }
    }
}

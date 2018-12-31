using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Options
{
    /// <summary>
    /// Tests for <see cref="Option{T}"/> Linq implementation (enumerable).
    /// </summary>
    [TestFixture]
    internal class OptionLinqEnumerableExtensionsTests : OptionTestsBase
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
        public void OptionAnyItem()
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

            // Not empty option
            // Enumerable<int>
            var optionEnumerableInts = Option<IEnumerable<int>>.Some(emptyEnumerable);
            Assert.IsFalse(optionEnumerableInts.AnyItem());
            Assert.IsFalse(optionEnumerableInts.AnyItem(value => value is int i && i == 2));
            Assert.IsFalse(optionEnumerableInts.AnyItem((int value) => value == 2));

            optionEnumerableInts = Option<IEnumerable<int>>.Some(enumerableInts);
            Assert.IsTrue(optionEnumerableInts.AnyItem());
            Assert.IsTrue(optionEnumerableInts.AnyItem(value => value is int i && i == 2));
            Assert.IsFalse(optionEnumerableInts.AnyItem(value => value is int i && i == 6));
            Assert.IsTrue(optionEnumerableInts.AnyItem((int value) => value == 2));
            Assert.IsFalse(optionEnumerableInts.AnyItem((int value) => value == 6));

            // List<int>
            var optionListInts = Option<IList<int>>.Some(listInts);
            Assert.IsTrue(optionListInts.AnyItem());
            Assert.IsTrue(optionListInts.AnyItem(value => value is int i && i == 2));
            Assert.IsFalse(optionListInts.AnyItem(value => value is int i && i == 7));
            Assert.IsTrue(optionListInts.AnyItem((int value) => value == 2));
            Assert.IsFalse(optionListInts.AnyItem((int value) => value == 7));

            // Dictionary<string, int>
            var optionStringsInts = Option<IDictionary<string, int>>.Some(dictionaryStringsInts);
            Assert.IsTrue(optionStringsInts.AnyItem());
            Assert.IsTrue(optionStringsInts.AnyItem(value => value is KeyValuePair<string, int> pair && pair.Value == 3));
            Assert.IsFalse(optionStringsInts.AnyItem(value => value is KeyValuePair<string, int> pair && pair.Value == 7));
            Assert.IsTrue(optionStringsInts.AnyItem((KeyValuePair<string, int> pair) => pair.Value == 3));
            Assert.IsFalse(optionStringsInts.AnyItem((KeyValuePair<string, int> pair) => pair.Value == 7));

            // Enumerable
            var optionEnumerable = Option<IEnumerable>.Some(Items);
            Assert.IsTrue(optionEnumerable.AnyItem());
            Assert.IsTrue(optionEnumerable.AnyItem(value => value is string));
            Assert.IsFalse(optionEnumerable.AnyItem(value => value is float));

            optionEnumerable = Option<IEnumerable>.Some(listInts);
            Assert.IsTrue(optionEnumerable.AnyItem());
            Assert.IsTrue(optionEnumerable.AnyItem(value => value is int));
            Assert.IsFalse(optionEnumerable.AnyItem(value => value is float));

            // Empty option
            var emptyOption = Option<IList<int>>.None;
            Assert.IsFalse(emptyOption.AnyItem());
            Assert.IsFalse(emptyOption.AnyItem(value => true));
            Assert.IsFalse(emptyOption.AnyItem((int value) => true));

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.AnyItem(null));
            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.AnyItem((Predicate<int>)null));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public void OptionAllItems()
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

            // Not empty option
            // Enumerable<int>
            var optionEnumerableInts = Option<IEnumerable<int>>.Some(emptyEnumerable);
            Assert.IsTrue(optionEnumerableInts.AllItems(value => true));
            Assert.IsTrue(optionEnumerableInts.AllItems(value => value is int));
            Assert.IsTrue(optionEnumerableInts.AllItems((int value) => true));
            Assert.IsTrue(optionEnumerableInts.AllItems((int value) => value > 0));

            optionEnumerableInts = Option<IEnumerable<int>>.Some(enumerableInts);
            Assert.IsTrue(optionEnumerableInts.AllItems(value => value is int i && i > 0));
            Assert.IsFalse(optionEnumerableInts.AllItems(value => value is int i && i == 6));
            Assert.IsTrue(optionEnumerableInts.AllItems((int value) => value > 0));
            Assert.IsFalse(optionEnumerableInts.AllItems((int value) => value == 6));

            // List<int>
            var optionListInts = Option<IList<int>>.Some(listInts);
            Assert.IsTrue(optionListInts.AllItems(value => value is int i && i > 1));
            Assert.IsFalse(optionListInts.AllItems(value => value is int i && i == 7));
            Assert.IsTrue(optionListInts.AllItems((int value) => value > 1));
            Assert.IsFalse(optionListInts.AllItems((int value) => value == 7));

            // Dictionary<string, int>
            var optionStringsInts = Option<IDictionary<string, int>>.Some(dictionaryStringsInts);
            Assert.IsTrue(optionStringsInts.AllItems(value => value is KeyValuePair<string, int>));
            Assert.IsFalse(optionStringsInts.AllItems(value => value is KeyValuePair<string, int> pair && pair.Value == 7));
            Assert.IsTrue(optionStringsInts.AllItems((KeyValuePair<string, int> pair) => pair.Value > 2));
            Assert.IsFalse(optionStringsInts.AllItems((KeyValuePair<string, int> pair) => pair.Value == 7));

            // Enumerable
            var optionEnumerable = Option<IEnumerable>.Some(Items);
            Assert.IsTrue(optionEnumerable.AllItems(value => value != null));
            Assert.IsFalse(optionEnumerable.AllItems(value => value is float));

            optionEnumerable = Option<IEnumerable>.Some(listInts);
            Assert.IsTrue(optionEnumerable.AllItems(value => value is int));
            Assert.IsFalse(optionEnumerable.AllItems(value => value is int i && i > 25));

            // Empty option
            var emptyOption = Option<IList<int>>.None;
            Assert.IsFalse(emptyOption.AllItems(value => true));
            Assert.IsFalse(emptyOption.AllItems((int value) => true));

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.AllItems(null));
            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.AllItems((Predicate<int>)null));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public void OptionContainsItem()
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

            // Not empty option
            // Enumerable<int>
            var optionEnumerableTestClasses = Option<IEnumerable<TestClass>>.Some(emptyEnumerable);
            Assert.IsFalse(optionEnumerableTestClasses.ContainsItem((TestClass)null));
            Assert.IsFalse(optionEnumerableTestClasses.ContainsItem(null, EqualityComparer<TestClass>.Default));
            Assert.IsFalse(optionEnumerableTestClasses.ContainsItem(testObject1));
            Assert.IsFalse(optionEnumerableTestClasses.ContainsItem(testObject1, EqualityComparer<TestClass>.Default));

            optionEnumerableTestClasses = Option<IEnumerable<TestClass>>.Some(enumerableTestClasses);
            Assert.IsFalse(optionEnumerableTestClasses.ContainsItem((TestClass)null));
            Assert.IsFalse(optionEnumerableTestClasses.ContainsItem(null, EqualityComparer<TestClass>.Default));
            Assert.IsTrue(optionEnumerableTestClasses.ContainsItem(testObject2));
            Assert.IsTrue(optionEnumerableTestClasses.ContainsItem(testObject2, EqualityComparer<TestClass>.Default));
            Assert.IsFalse(optionEnumerableTestClasses.ContainsItem(testObject3));
            Assert.IsFalse(optionEnumerableTestClasses.ContainsItem(testObject3, EqualityComparer<TestClass>.Default));

            // List<int>
            var optionListInts = Option<IList<TestClass>>.Some(listTestClasses);
            Assert.IsFalse(optionListInts.ContainsItem((TestClass)null));
            Assert.IsFalse(optionListInts.ContainsItem(null, EqualityComparer<TestClass>.Default));
            Assert.IsTrue(optionListInts.ContainsItem(testObject1));
            Assert.IsTrue(optionListInts.ContainsItem(testObject1, EqualityComparer<TestClass>.Default));
            Assert.IsFalse(optionListInts.ContainsItem(testObject2));
            Assert.IsFalse(optionListInts.ContainsItem(testObject2, EqualityComparer<TestClass>.Default));

            // Dictionary<string, int>
            var optionStringsInts = Option<IDictionary<string, int>>.Some(dictionaryStringsInts);
            Assert.IsTrue(optionStringsInts.ContainsItem(new KeyValuePair<string, int>("3", 3)));
            Assert.IsTrue(optionStringsInts.ContainsItem(new KeyValuePair<string, int>("3", 3), EqualityComparer<KeyValuePair<string, int>>.Default));
            Assert.IsFalse(optionStringsInts.ContainsItem(new KeyValuePair<string, int>("1", 3)));
            Assert.IsFalse(optionStringsInts.ContainsItem(new KeyValuePair<string, int>("1", 3), EqualityComparer<KeyValuePair<string, int>>.Default));

            // Enumerable
            var optionEnumerable = Option<IEnumerable>.Some(Items);
            Assert.IsFalse(optionEnumerable.ContainsItem(null));
            Assert.IsFalse(optionEnumerable.ContainsItem(null, EqualityComparer<object>.Default));
            Assert.IsTrue(optionEnumerable.ContainsItem("Test"));
            Assert.IsTrue(optionEnumerable.ContainsItem(1, EqualityComparer<object>.Default));
            Assert.IsFalse(optionEnumerable.ContainsItem(testObject2));
            Assert.IsFalse(optionEnumerable.ContainsItem(testObject2, EqualityComparer<object>.Default));

            optionEnumerable = Option<IEnumerable>.Some(listTestClasses);
            Assert.IsFalse(optionEnumerable.ContainsItem(null));
            Assert.IsFalse(optionEnumerable.ContainsItem(null, EqualityComparer<object>.Default));
            Assert.IsTrue(optionEnumerable.ContainsItem(testObject1));
            Assert.IsTrue(optionEnumerable.ContainsItem(testObject1, EqualityComparer<object>.Default));
            Assert.IsFalse(optionEnumerable.ContainsItem(testObject2));
            Assert.IsFalse(optionEnumerable.ContainsItem(testObject2, EqualityComparer<object>.Default));

            // Empty option
            var emptyOption = Option<IList<TestClass>>.None;
            Assert.IsFalse(emptyOption.ContainsItem((TestClass)null));
            Assert.IsFalse(emptyOption.ContainsItem(null, EqualityComparer<TestClass>.Default));
            Assert.IsFalse(emptyOption.ContainsItem(testObject1));
            Assert.IsFalse(emptyOption.ContainsItem(testObject2, EqualityComparer<TestClass>.Default));

            Assert.Throws<ArgumentNullException>(() => optionEnumerableTestClasses.ContainsItem<IEnumerable<TestClass>, TestClass>(null, null));
            Assert.Throws<ArgumentNullException>(() => optionEnumerable.ContainsItem(null, null));
        }

        [Test]
        public void OptionSelectItems()
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

            // Not empty option
            // Enumerable<int>
            var optionEnumerableInts = Option<IEnumerable<int>>.Some(emptyEnumerable);
            CheckEmptyOption(optionEnumerableInts.SelectItems(selectFromObject));
            CheckEmptyOption(optionEnumerableInts.SelectItems(selectFromInt));

            optionEnumerableInts = Option<IEnumerable<int>>.Some(enumerableInts);
            CheckOptionCollectionValue(optionEnumerableInts.SelectItems(
                selectFromObject), 
                new[] { 3.1f, 4.1f, 5.1f });
            CheckOptionCollectionValue(optionEnumerableInts.SelectItems(
                selectFromInt),
                new[] { 3.5f, 4.5f, 5.5f });

            // List<int>
            var optionListInts = Option<IList<int>>.Some(listInts);
            CheckOptionCollectionValue(optionListInts.SelectItems(
                selectFromObject),
                new[] { 4.1f, 5.1f, 6.1f });
            CheckOptionCollectionValue(optionListInts.SelectItems(
                selectFromInt),
                new[] { 4.5f, 5.5f, 6.5f });

            // Dictionary<string, int>
            var optionStringsInts = Option<IDictionary<string, int>>.Some(dictionaryStringsInts);
            CheckOptionCollectionValue(optionStringsInts.SelectItems(
                 value =>
                 {
                     if (value is KeyValuePair<string, int> pair)
                         return pair.Value + 0.2f;
                     return 0.4f;
                 }),
                 new[] { 3.2f, 4.2f, 5.2f });
            CheckOptionCollectionValue(optionStringsInts.SelectItems(
                (KeyValuePair<string, int> pair) =>
                {
                    return pair.Value + 0.3f;
                }),
                new[] { 3.3f, 4.3f, 5.3f });

            // Enumerable
            var optionEnumerable = Option<IEnumerable>.Some(Items);
            CheckOptionCollectionValue(optionEnumerable.SelectItems(
                 value =>
                 {
                     if (value is int i)
                         return i +1;
                     return 42;
                 }),
                 new[] { 2, 42, 42 });

            optionEnumerable = Option<IEnumerable>.Some(listInts);
            CheckOptionCollectionValue(optionEnumerable.SelectItems(
                  value =>
                  {
                      if (value is int i)
                          return i + 1;
                      return 42;
                  }),
                  new[] { 5, 6, 7 });

            // Empty option
            var emptyOption = Option<IList<int>>.None;
            CheckEmptyOption(emptyOption.SelectItems(selectFromObject));
            CheckEmptyOption(emptyOption.SelectItems(selectFromInt));

            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.SelectItems((Func<object, int>)null));
            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.SelectItems((Func<int, int>)null));
        }

        [Test]
        public void OptionWhereItems()
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

            // Not empty option
            // Enumerable<int>
            var optionEnumerableInts = Option<IEnumerable<int>>.Some(emptyEnumerable);
            CheckEmptyOption(optionEnumerableInts.WhereItems(value => true));
            CheckEmptyOption(optionEnumerableInts.WhereItems((int value) => true));

            optionEnumerableInts = Option<IEnumerable<int>>.Some(enumerableInts);
            CheckOptionCollectionValue(optionEnumerableInts.WhereItems(value => (int)value >= 3), new[] { 3, 4, 5 });
            CheckOptionCollectionValue(optionEnumerableInts.WhereItems((int value) => value >= 4), new[] { 4, 5 });
            CheckEmptyOption(optionEnumerableInts.WhereItems(value => (int)value >= 6));
            CheckEmptyOption(optionEnumerableInts.WhereItems((int value) => value >= 7));

            // List<int>
            var optionListInts = Option<IList<int>>.Some(listInts);
            CheckOptionCollectionValue(optionListInts.WhereItems(value => (int)value >= 3), new[] { 3, 4, 5, 6 });
            CheckOptionCollectionValue(optionListInts.WhereItems((int value) => value >= 4), new[] { 4, 5, 6 });
            CheckEmptyOption(optionListInts.WhereItems(value => (int)value > 6));
            CheckEmptyOption(optionListInts.WhereItems((int value) => value >= 7));

            // Dictionary<string, int>
            var optionStringsInts = Option<IDictionary<string, int>>.Some(dictionaryStringsInts);
            CheckOptionDictionaryValue(
                optionStringsInts.WhereItems(pair => ((KeyValuePair<string, int>)pair).Value > 3), 
                new Dictionary<string, int> { ["4"] = 4, ["5"] = 5 });
            CheckOptionDictionaryValue(
                optionStringsInts.WhereItems((KeyValuePair<string, int> pair) => pair.Value >= 4),
                new Dictionary<string, int> { ["4"] = 4, ["5"] = 5 });
            CheckEmptyOption(optionStringsInts.WhereItems(pair => false));
            CheckEmptyOption(optionStringsInts.WhereItems((KeyValuePair<string, int> pair) => pair.Value >= 7));

            // Enumerable
            var optionEnumerable = Option<IEnumerable>.Some(Items);
            CheckOptionCollectionValue(optionEnumerable.WhereItems(item => item is string), new object[] { "Test" });

            optionEnumerable = Option<IEnumerable>.Some(listInts);
            CheckOptionCollectionValue(optionEnumerable.WhereItems(item => item is int i && i > 4), new[] { 5, 6 });

            // Empty option
            var emptyOption = Option<IList<int>>.None;
            CheckEmptyOption(emptyOption.WhereItems(item => true));
            CheckEmptyOption(emptyOption.WhereItems((int value) => value > 1));

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.WhereItems(null));
            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.WhereItems((Predicate<int>)null));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public void OptionForEachItems()
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

            // Not empty option
            // Enumerable<int>
            int counter = 0;
            var optionEnumerableInts = Option<IEnumerable<int>>.Some(emptyEnumerable);
            optionEnumerableInts.ForEachItems(value => ++counter);
            Assert.AreEqual(0, counter);

            counter = 0;
            optionEnumerableInts = Option<IEnumerable<int>>.Some(enumerableInts);
            optionEnumerableInts.ForEachItems(value => ++counter);
            Assert.AreEqual(5, counter);

            optionEnumerableInts.ForEachItems((int value) => ++counter);
            Assert.AreEqual(10, counter);

            // List<int>
            counter = 0;
            var optionListInts = Option<IList<int>>.Some(listInts);
            optionListInts.ForEachItems(value => ++counter);
            Assert.AreEqual(5, counter);

            optionListInts.ForEachItems((int value) => ++counter);
            Assert.AreEqual(10, counter);

            // Dictionary<string, int>
            counter = 0;
            var optionStringsInts = Option<IDictionary<string, int>>.Some(dictionaryStringsInts);
            optionStringsInts.ForEachItems(value => ++counter);
            Assert.AreEqual(3, counter);

            optionStringsInts.ForEachItems((KeyValuePair<string, int> value) => ++counter);
            Assert.AreEqual(6, counter);

            // Enumerable
            counter = 0;
            var optionEnumerable = Option<IEnumerable>.Some(Items);
            optionEnumerable.ForEachItems(value => ++counter);
            Assert.AreEqual(3, counter);

            counter = 0;
            optionEnumerable = Option<IEnumerable>.Some(listInts);
            optionEnumerable.ForEachItems(value => ++counter);
            Assert.AreEqual(5, counter);

            // Empty option
            counter = 0;
            var emptyOption = Option<IList<int>>.None;

            emptyOption.ForEachItems(item => ++counter);
            Assert.AreEqual(0, counter);

            emptyOption.ForEachItems((int item) => ++counter);
            Assert.AreEqual(0, counter);

            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.ForEachItems(null));
            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.ForEachItems((Action<int>)null));
        }

#if (NET20) || (NET30)
        // System.Core and NUnit both define this delegate that is conflicting
        // Defining it here allows to use it without conflict.
        public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
#endif

        [Test]
        public void OptionAggregateItems()
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

            // Not empty option
            // Enumerable<int>
            var optionEnumerableInts = Option<IEnumerable<int>>.Some(emptyEnumerable);
            Assert.AreEqual(1, optionEnumerableInts.AggregateItems(1, (acc, cur) => accumulatorFromObject(acc, cur)));
            Assert.AreEqual(1, optionEnumerableInts.AggregateItems(1, (int acc, int cur) => accumulatorFromInt(acc, cur)));

            optionEnumerableInts = Option<IEnumerable<int>>.Some(enumerableInts);
            Assert.AreEqual(7, optionEnumerableInts.AggregateItems(1, (acc, cur) => accumulatorFromObject(acc, cur)));
            Assert.AreEqual(13, optionEnumerableInts.AggregateItems(1, (int acc, int cur) => accumulatorFromInt(acc, cur)));

            // List<int>
            var optionListInts = Option<IList<int>>.Some(listInts);
            Assert.AreEqual(7, optionListInts.AggregateItems(1, (acc, cur) => accumulatorFromObject(acc, cur)));
            Assert.AreEqual(19, optionListInts.AggregateItems(1, (int acc, int cur) => accumulatorFromInt(acc, cur)));

            // Dictionary<string, int>
            var optionStringsInts = Option<IDictionary<string, int>>.Some(dictionaryStringsInts);
            Assert.AreEqual(8, optionStringsInts.AggregateItems(1, (acc, cur) => acc * 2));
            Assert.AreEqual(25, optionStringsInts.AggregateItems(1, (int acc, KeyValuePair<string, int> cur) => acc + 2 * cur.Value));

            // Enumerable
            var optionEnumerable = Option<IEnumerable>.Some(Items);
            Assert.AreEqual(8, optionEnumerable.AggregateItems(1, (acc, cur) => accumulatorFromObject(acc, cur)));

            optionEnumerable = Option<IEnumerable>.Some(listInts);
            Assert.AreEqual(7, optionEnumerable.AggregateItems(1, (acc, cur) => accumulatorFromObject(acc, cur)));

            // Empty option
            var emptyOption = Option<IList<int>>.None;
            Assert.AreEqual(1, emptyOption.AggregateItems(1, (acc, cur) => accumulatorFromObject(acc, cur)));
            Assert.AreEqual(1, emptyOption.AggregateItems(1, (int acc, int cur) => accumulatorFromInt(acc, cur)));

            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.AggregateItems(1, null));
            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.AggregateItems((Person)null, (acc, cur) => null));
            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.AggregateItems((Person)null, null));
            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.AggregateItems<IEnumerable<int>, int, float>(1.0f, null));
            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.AggregateItems(null, (Person acc, int cur) => null));
            Assert.Throws<ArgumentNullException>(() => optionEnumerableInts.AggregateItems<IEnumerable<int>, int, Person>(null, null));
        }
    }
}

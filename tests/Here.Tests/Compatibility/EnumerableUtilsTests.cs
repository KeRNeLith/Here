#if !SUPPORTS_SYSTEM_CORE
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Here.Utils;

namespace Here.Tests
{
    /// <summary>
    /// Tests related to <see cref="Enumerable"/>.
    /// </summary>
    [TestFixture]
    internal class EnumerableUtilsTests
    {
        private static IEnumerable<int> GetInts()
        {
            yield return 4;
            yield return 5;
            yield return 6;
            yield return 7;
            yield return 8;
            yield return 9;
            yield return 10;
        }

        [Test]
        public void ToArray()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var emptyList = new List<int>();
            var list = new List<int> { 1, 2, 3, 4 };

            CollectionAssert.AreEqual(new int[] { }, Enumerable.ToArray(emptyList));
            CollectionAssert.AreEqual(new [] { 1, 2, 3, 4 }, Enumerable.ToArray(list));

            CollectionAssert.AreEqual(new int[] { }, Enumerable.ToArray(Enumerable.Empty<int>()));
            CollectionAssert.AreEqual(new[] { 4, 5, 6, 7, 8, 9, 10 }, Enumerable.ToArray(GetInts()));
        }

        [Test]
        public void Aggregate()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var emptyList = new List<int>();
            var list = new List<int> { 1, 2, 3, 4 };

            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => Enumerable.Aggregate(emptyList, (v1, v2) => v1 + v2));
            Assert.AreEqual(10, Enumerable.Aggregate(list, (v1, v2) => v1 + v2));
        }

        [Test]
        public void SequenceEqual()
        {
            // ReSharper disable CollectionNeverUpdated.Local
            var emptyList1 = new List<int>();
            var emptyList2 = new List<int>();
            // ReSharper restore CollectionNeverUpdated.Local
            var list1 = new List<int> { 1, 2, 3, 4 };
            var list2 = new List<int> { 1, 2, 3, 4 };
            var list3 = new List<int> { 1, 2, 3, 4, 5 };

            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.IsTrue(Enumerable.SequenceEqual(emptyList1, emptyList1));
            Assert.IsTrue(Enumerable.SequenceEqual(emptyList1, emptyList2));
            Assert.IsTrue(Enumerable.SequenceEqual(emptyList2, emptyList1));

            Assert.IsFalse(Enumerable.SequenceEqual(emptyList1, list1));
            Assert.IsFalse(Enumerable.SequenceEqual(list1, emptyList1));

            Assert.IsTrue(Enumerable.SequenceEqual(list1, list1));
            Assert.IsTrue(Enumerable.SequenceEqual(list1, list2));
            Assert.IsTrue(Enumerable.SequenceEqual(list2, list1));

            Assert.IsFalse(Enumerable.SequenceEqual(list1, list3));
            Assert.IsFalse(Enumerable.SequenceEqual(list3, list1));
        }
    }
}
#endif
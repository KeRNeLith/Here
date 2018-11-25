using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Here.Tests.Maybes
{
    /// <summary>
    /// Base class for<see cref="Maybe{T}"/> tests.
    /// </summary>
    internal class MaybeTestsBase : HereTestsBase
    {
        // Methods to check Maybe
        private static double Epslion = 0.00001;

        protected static void CheckMaybeNearValue(Maybe<double> maybe, double value)
        {
            Assert.IsTrue(maybe.HasValue);
            Assert.IsFalse(maybe.HasNoValue);
            Assert.AreEqual(value, maybe.Value, Epslion);
    }

        protected static void CheckMaybeValue<T>(Maybe<T> maybe, [NotNull] T value)
        {
            Assert.IsTrue(maybe.HasValue);
            Assert.IsFalse(maybe.HasNoValue);
            Assert.AreEqual(value, maybe.Value);
        }

        protected static void CheckMaybeSameValue<T>(Maybe<T> maybe, [NotNull] T value)
            where T : class
        {
            Assert.IsTrue(maybe.HasValue);
            Assert.IsFalse(maybe.HasNoValue);
            Assert.AreSame(value, maybe.Value);
        }

        protected static void CheckMaybeCollectionValue<T>(Maybe<T> maybe, [NotNull] IEnumerable value)
            where T : IEnumerable
        {
            Assert.IsTrue(maybe.HasValue);
            Assert.IsFalse(maybe.HasNoValue);
            CollectionAssert.AreEqual(value, maybe.Value);
        }

        protected static void CheckMaybeDictionaryValue<T, TKey, TValue>(Maybe<T> maybe, [NotNull] IDictionary<TKey, TValue> value)
            where T : IEnumerable
        {
            Assert.IsTrue(maybe.HasValue);
            Assert.IsFalse(maybe.HasNoValue);
            CollectionAssert.AreEquivalent(value, maybe.Value);
        }

        protected static void CheckEmptyMaybe<T>(Maybe<T> maybe)
        {
            Assert.IsFalse(maybe.HasValue);
            Assert.IsTrue(maybe.HasNoValue);
            Assert.Throws<InvalidOperationException>(() => { var unused = maybe.Value; });
        }
    }
}
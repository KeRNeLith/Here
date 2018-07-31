using System;
using JetBrains.Annotations;
using NUnit.Framework;
using Here.Maybes;

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

        protected static void CheckEmptyMaybe<T>(Maybe<T> maybe)
        {
            Assert.IsFalse(maybe.HasValue);
            Assert.IsTrue(maybe.HasNoValue);
            Assert.Throws<InvalidOperationException>(() => { var unused = maybe.Value; });
        }
    }
}